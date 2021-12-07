using System.Collections.Generic;
using UnityEngine;
using EasyBuildSystem.Features.Scripts.Core.Base.Piece;
using EasyBuildSystem.Features.Scripts.Core.Base.Manager;
using EasyBuildSystem.Features.Scripts.Core.Base.Socket;
using EasyBuildSystem.Features.Scripts.Core.Base.Piece.Enums;
using EasyBuildSystem.Features.Scripts.Core.Base.Builder;
using EasyBuildSystem.Features.Scripts.Extensions;

namespace FiveSQD.Parallels.Entity.Placement
{
    public class EBSPartCollectionManager : MonoBehaviour
    {
        void Start()
        {
            InitializePieceCollection();
        }

        public static void AddToBuildManager(PieceBehaviour pieceBehaviour)
        {
            BuildManager.Instance.AddPiece(pieceBehaviour);
        }

        public static void RemoveFromBuildManager(PieceBehaviour pieceBehaviour)
        {
            BuildManager.Instance.RemovePiece(pieceBehaviour);
        }

        public static PieceBehaviour AddStaticInstance(MeshEntity entity)
        {
            PieceBehaviour pieceBehaviour = entity.gameObject.AddComponent<PieceBehaviour>();

            pieceBehaviour.ChangeState(StateType.Placed);

            AddToBuildManager(pieceBehaviour);
            return pieceBehaviour;
        }

        public static void RemoveStaticInstance(PieceBehaviour pieceBehaviour)
        {
            RemoveFromBuildManager(pieceBehaviour);

            Destroy(pieceBehaviour);
        }

        public static PieceBehaviour StartPlacing(MeshEntity entity)
        {
            PieceBehaviour pieceBehaviour = entity.gameObject.AddComponent<PieceBehaviour>();
            pieceBehaviour.MeshBounds = entity.gameObject.GetChildsBounds();
            pieceBehaviour.Sockets = SetUpSockets(entity.gameObject, entity.sockets, pieceBehaviour);
            pieceBehaviour.PreviewDisableObjects = new GameObject[0];
            pieceBehaviour.PreviewDisableBehaviours = new MonoBehaviour[0];
            pieceBehaviour.PreviewDisableColliders = new Collider[0];

            BuilderBehaviour.Instance.SelectPrefab(pieceBehaviour);
            BuilderBehaviour.Instance.ChangeMode(EasyBuildSystem.Features.Scripts.Core.Base.Builder.Enums.BuildMode.Placement);

            return pieceBehaviour;
        }

        public static void StopPlacing(PieceBehaviour partBehaviour)
        {
            if (partBehaviour.CurrentState == StateType.Placed)
            {
                return;
            }

            if (!BuilderBehaviour.Instance.AllowPlacement)
            {
                return;
            }

            PieceBehaviour previewPart = BuilderBehaviour.Instance.CurrentPreview;
            if (previewPart)
            {
                partBehaviour.transform.position = previewPart.transform.position;
                partBehaviour.transform.rotation = previewPart.transform.rotation;
                Destroy(previewPart.gameObject);
            }
            partBehaviour.ChangeState(StateType.Placed);
            BuilderBehaviour.Instance.CurrentMode = EasyBuildSystem.Features.Scripts.Core.Base.Builder.Enums.BuildMode.None;
            if (BuilderBehaviour.Instance.CurrentEditionPreview) Destroy(BuilderBehaviour.Instance.CurrentEditionPreview.gameObject);
            if (BuilderBehaviour.Instance.CurrentPreview) Destroy(BuilderBehaviour.Instance.CurrentPreview.gameObject);
            RemoveSocketPartOffsets(partBehaviour);
        }

        public static void UpdatePartBehaviourSocketOffsets(PieceBehaviour pb)
        {
            RemovePartBehaviourSocketOffsets(pb);
            AddPartBehaviourSocketOffsets(pb);
        }

        private void InitializePieceCollection()
        {
            BuildManager.Instance.Pieces = new List<PieceBehaviour>();
        }

        private static SocketBehaviour[] SetUpSockets(GameObject prefab, List<PlacementSocket> sockets, PieceBehaviour pb)
        {
            if (sockets == null)
            {
                return new SocketBehaviour[0];
            }

            List<SocketBehaviour> socks = new List<SocketBehaviour>();

            foreach (PlacementSocket socket in sockets)
            {
                socks.Add(SetUpSocket(prefab, socket.position, socket.rotation, pb));
            }

            return socks.ToArray();
        }

        private static SocketBehaviour SetUpSocket(GameObject part, Vector3 pos, Quaternion rot, PieceBehaviour pb)
        {
            GameObject sockObj = new GameObject("Socket");
            sockObj.transform.SetParent(part.transform);
            sockObj.transform.localPosition = pos; // TODO: meters.
            sockObj.transform.localRotation = rot;
            SocketBehaviour sock = sockObj.AddComponent<SocketBehaviour>();

            return sock;
        }

        private static void AddPartBehaviourSocketOffsets(PieceBehaviour pb)
        {
            foreach (Transform t in pb.transform)
            {
                SocketBehaviour sb = t.GetComponent<SocketBehaviour>();
                if (sb)
                {
                    AddSocketPartOffsets(sb, pb);
                }
            }
        }

        private static void RemovePartBehaviourSocketOffsets(PieceBehaviour pb)
        {
            foreach (Transform t in pb.transform)
            {
                SocketBehaviour sb = t.GetComponent<SocketBehaviour>();
                if (sb)
                {
                    RemoveSocketPartOffsets(pb);
                }
            }
        }

        private static void AddSocketPartOffsets(SocketBehaviour sockToAddFor, PieceBehaviour pb)
        {
            foreach (BaseEntity entity in EntityManager.GetAllEntities())
            {
                foreach (SocketBehaviour sb in entity.GetComponentsInChildren<SocketBehaviour>())
                {
                    if (!sb.PartOffsets.Exists(x => x.Piece == pb))
                    {
                        // Socket behaviour doesn't have offset for this part. Create it.
                        AddSocketPartOffsetsToSocket(sb, pb);
                    }
                }
            }
        }

        private static void RemoveSocketPartOffsets(PieceBehaviour pb)
        {
            foreach (BaseEntity entity in EntityManager.GetAllEntities())
            {
                foreach (SocketBehaviour sb in entity.GetComponentsInChildren<SocketBehaviour>())
                {
                    sb.PartOffsets.Clear();
                }
            }
        }

        private static void AddSocketPartOffsetsToSocket(SocketBehaviour sbToAddTo, PieceBehaviour pbToAdd)
        {
            SocketBehaviour sbToAdd = null;
            float minDiff = float.MaxValue;
            Quaternion adjoiningRot = Quaternion.identity;
            foreach (Transform t in pbToAdd.transform)
            {
                SocketBehaviour sb = t.GetComponent<SocketBehaviour>();
                if (sb)
                {
                    foreach (Quaternion rot in GetAdjoiningRotations(sb.transform.rotation))
                    {
                        float diff = Quaternion.Angle(sbToAddTo.transform.rotation, rot);
                        if (diff < minDiff)
                        {
                            minDiff = diff;
                            sbToAdd = sb;
                            adjoiningRot = rot;
                        }
                    }
                }
            }

            EasyBuildSystem.Features.Scripts.Core.Base.Socket.Data.Offset poToAdd
                = new EasyBuildSystem.Features.Scripts.Core.Base.Socket.Data.Offset(pbToAdd);
            if (sbToAdd)
            {
                Vector3 inverseRot = new Vector3(-1 * sbToAddTo.transform.localEulerAngles.x,
                    -1 * sbToAddTo.transform.localEulerAngles.y, -1 * sbToAddTo.transform.localEulerAngles.z);
                poToAdd.Position = Quaternion.Euler(inverseRot) * (-1 * sbToAdd.transform.localPosition);
                poToAdd.Rotation = inverseRot;
            }
            sbToAddTo.PartOffsets.Add(poToAdd);
        }

        private static void RemoveSocketPartOffsetsFromSocket(SocketBehaviour sbToRemoveFrom, PieceBehaviour pbToRemove)
        {
            sbToRemoveFrom.PartOffsets.Remove(new EasyBuildSystem.Features.Scripts.Core.Base.Socket.Data.Offset(pbToRemove));
        }

        // TODO: Create separate socket class.
        private static Quaternion[] GetAdjoiningRotations(Quaternion rotation)
        {
            Quaternion[] returnVal = new Quaternion[2];

            GameObject temp = new GameObject();
            temp.transform.rotation = rotation;
            temp.transform.Rotate(0, 180, 0);
            returnVal[0] = temp.transform.rotation;
            temp.transform.rotation = rotation;
            temp.transform.Rotate(0, 0, 180f);
            returnVal[1] = temp.transform.rotation;
            Destroy(temp);

            return returnVal;
        }
    }
}