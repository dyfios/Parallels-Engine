using System.Collections.Generic;
using UnityEngine;
using FiveSQD.Parallels.Runtime.Engine.Materials;
using FiveSQD.Parallels.Entity.Placement;
using FiveSQD.Parallels.Runtime.Engine.Tags;

namespace FiveSQD.Parallels.Entity
{
    public class MeshEntity : BaseEntity
    {
        private MeshRenderer[] meshRenderers;

        private MeshCollider[] meshColliders;

        private BoxCollider boxCollider;

        private Rigidbody rigidBody;

        private bool gravitational = false;

        private GameObject highlightCube;

        public override bool Delete()
        {
            return base.Delete();
        }

        public override EntityMotion? GetMotion()
        {
            if (rigidBody == null)
            {
                Utilities.LogSystem.LogError("[MeshEntity->GetMotion] No rigidbody for mesh entity.");
                return null;
            }

            return new EntityMotion
            {
                angularVelocity = rigidBody.angularVelocity,
                stationary = rigidBody.isKinematic,
                velocity = rigidBody.velocity
            };
        }

        public override EntityPhysicalProperties? GetPhysicalProperties()
        {
            if (rigidBody == null)
            {
                Utilities.LogSystem.LogError("[MeshEntity->GetPhysicalProperties] No rigidbody for mesh entity.");
                return null;
            }

            return new EntityPhysicalProperties
            {
                angularDrag = rigidBody.angularDrag,
                centerOfMass = rigidBody.centerOfMass,
                drag = rigidBody.drag,
                gravitational = gravitational,
                mass = rigidBody.mass
            };
        }

        public override Vector3 GetSize()
        {
            Bounds bounds = new Bounds(transform.position, Vector3.zero);
            MeshRenderer[] rends = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer rend in rends)
            {
                bounds.Encapsulate(rend.bounds);
            }

            return bounds.size;
        }

        public override bool SetHighlight(bool highlight)
        {
            highlightCube.SetActive(highlight);

            return true;
        }

        public override bool SetInteractionState(InteractionState stateToSet)
        {
            switch (stateToSet)
            {
                case InteractionState.Physical:
                    MakePhysical();
                    return true;

                case InteractionState.Placing:
                    MakePlacing();
                    return true;

                case InteractionState.Static:
                    MakeStatic();
                    return true;
                
                case InteractionState.Hidden:
                    MakeHidden();
                    return true;
                
                default:
                    Utilities.LogSystem.LogWarning("[MeshEntity->SetInteractionState] Interaction state invalid.");
                    return false;
            }
        }

        public override bool SetMotion(EntityMotion? motionToSet)
        {
            if (!motionToSet.HasValue)
            {
                Utilities.LogSystem.LogWarning("[MeshEntity->SetMotion] Invalid motion.");
                return false;
            }

            if (rigidBody == null)
            {
                Utilities.LogSystem.LogError("[MeshEntity->SetMotion] No rigidbody for mesh entity.");
                return false;
            }

            if (motionToSet.Value.stationary.HasValue)
            {
                if (motionToSet.Value.stationary == true)
                {
                    rigidBody.isKinematic = true;
                    rigidBody.useGravity = false;
                    rigidBody.angularVelocity = Vector3.zero;
                    rigidBody.velocity = Vector3.zero;
                    return true;
                }
                else
                {
                    rigidBody.isKinematic = false;
                    rigidBody.useGravity = gravitational;
                }
            }

            if (motionToSet.Value.angularVelocity != null)
            {
                rigidBody.angularVelocity = motionToSet.Value.angularVelocity;
            }

            if (motionToSet.Value.velocity != null)
            {
                rigidBody.velocity = motionToSet.Value.velocity;
            }

            return true;
        }

        public override bool SetPhysicalProperties(EntityPhysicalProperties? epp)
        {
            if (!epp.HasValue)
            {
                Utilities.LogSystem.LogWarning("[MeshEntity->SetPhysicalProperties] Invalid physical properties.");
                return false;
            }

            if (rigidBody == null)
            {
                Utilities.LogSystem.LogError("[MeshEntity->SetPhysicalProperties] No rigidbody for mesh entity.");
                return false;
            }

            if (epp.Value.angularDrag.HasValue)
            {
                rigidBody.angularDrag = epp.Value.angularDrag.Value;
            }

            if (epp.Value.centerOfMass != null)
            {
                rigidBody.centerOfMass = epp.Value.centerOfMass;
            }

            if (epp.Value.drag.HasValue)
            {
                rigidBody.drag = epp.Value.drag.Value;
            }

            if (epp.Value.gravitational.HasValue)
            {
                gravitational = epp.Value.gravitational.Value;
                rigidBody.useGravity = gravitational;
            }

            if (epp.Value.mass.HasValue)
            {
                rigidBody.mass = epp.Value.mass.Value;
            }

            return true;
        }

        public override bool SetSize(Vector3 size)
        {
            if (meshRenderers == null)
            {
                Utilities.LogSystem.LogError("[MeshEntity->SetSize] Unable to find mesh to size.");
                return false;
            }

            Bounds bounds = new Bounds(transform.position, Vector3.zero);
            foreach (MeshRenderer rend in meshRenderers)
            {
                bounds.Encapsulate(rend.bounds);
            }

            transform.localScale = new Vector3(
                size.x * transform.localScale.x / bounds.size.x,
                size.y * transform.localScale.y / bounds.size.y,
                size.z * transform.localScale.z / bounds.size.z);

            return true;
        }

        public override bool SetVisibility(bool visible)
        {
            // Use base functionality.
            return base.SetVisibility(visible);
        }

        public override void Initialize(System.Guid idToSet)
        {
            base.Initialize(idToSet);

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                gameObject.AddComponent<Rigidbody>();
            }
            SetRigidbody(rb);

            SetRenderers(gameObject.GetComponentsInChildren<MeshRenderer>());

            BoxCollider boxCollider = null;
            foreach (BoxCollider bc in gameObject.GetComponentsInChildren<BoxCollider>())
            {
                if (bc.tag == TagManager.physicsColliderTag)
                {
                    boxCollider = bc;
                    break;
                }
            }

            if (boxCollider == null)
            {
                boxCollider = gameObject.AddComponent<BoxCollider>();
            }

            List<MeshCollider> mcs = new List<MeshCollider>();
            foreach (MeshCollider mc in gameObject.GetComponentsInChildren<MeshCollider>())
            {
                if (mc.tag == TagManager.meshColliderTag)
                {
                    mcs.Add(mc);
                    break;
                }
            }

            SetColliders(boxCollider, mcs.ToArray());

            MakeHidden();
            SetUpHighlightVolume();
        }

        public override void TearDown()
        {
            base.TearDown();
        }

        public void SetRenderers(MeshRenderer[] mrs)
        {
            if (mrs == null)
            {
                Utilities.LogSystem.LogWarning("[MeshEntity->SetRenderer] No mesh renderer.");
            }
            meshRenderers = mrs;
        }

        public void SetColliders(BoxCollider bc, MeshCollider[] mc)
        {
            if (bc == null)
            {
                Utilities.LogSystem.LogWarning("[MeshEntity->SetColliders] No box collider.");
            }
            boxCollider = bc;

            if (mc == null)
            {
                Utilities.LogSystem.LogWarning("[MeshEntity->SetColliders] No mesh collider.");
            }
            meshColliders = mc;
        }

        public void SetRigidbody(Rigidbody rb)
        {
            if (rb == null)
            {
                Utilities.LogSystem.LogWarning("[MeshEntity->SetRigidbody] No rigidbody.");
            }
            rigidBody = rb;
        }

        private void MakeHidden()
        {
            switch (interactionState)
            {
                case InteractionState.Physical:
                    break;

                case InteractionState.Placing:
                    break;

                case InteractionState.Static:
                    break;

                case InteractionState.Hidden:
                default:
                    break;
            }

            rigidBody.isKinematic = true;

            foreach (MeshCollider meshCollider in meshColliders)
            {
                meshCollider.enabled = false;
            }
            boxCollider.enabled = false;
            gameObject.SetActive(false);
            interactionState = InteractionState.Hidden;
        }

        private void MakeStatic()
        {
            switch (interactionState)
            {
                case InteractionState.Hidden:
                    // Handled in main sequence.
                    break;

                case InteractionState.Physical:
                    // Handled in main sequence.
                    break;

                case InteractionState.Placing:
                    break;

                case InteractionState.Static:
                default:
                    break;
            }

            gameObject.SetActive(true);
            rigidBody.isKinematic = true;
            foreach (MeshCollider meshCollider in meshColliders)
            {
                meshCollider.enabled = false;
            }
            boxCollider.enabled = false;
            interactionState = InteractionState.Static;
        }

        private void MakePhysical()
        {
            switch (interactionState)
            {
                case InteractionState.Hidden:
                    // Handled in main sequence.
                    break;

                case InteractionState.Placing:
                    break;

                case InteractionState.Static:
                    // Handled in main sequence.
                    break;

                case InteractionState.Physical:
                default:
                    break;
            }

            gameObject.SetActive(true);
            interactionState = InteractionState.Physical;
        }

        private void MakePlacing()
        {
            switch (interactionState)
            {
                case InteractionState.Hidden:
                    break;

                case InteractionState.Physical:
                    break;

                case InteractionState.Static:
                    break;

                case InteractionState.Placing:
                default:
                    break;
            }

            EBSPartCollectionManager.StartPlacing(this);
            
            gameObject.SetActive(true);
            interactionState = InteractionState.Placing;
        }

        private void SetUpHighlightVolume()
        {
            Bounds bounds = new Bounds(transform.position, Vector3.zero);
            MeshRenderer[] rends = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer rend in rends)
            {
                bounds.Encapsulate(rend.bounds);
            }

            bounds.center = bounds.center - transform.position;

            highlightCube = new GameObject("HighlightVolume");

            Vector3[] vertices =
            {
                    new Vector3(bounds.min.x, bounds.min.y, bounds.min.z),
                    new Vector3 (bounds.max.x, bounds.min.y, bounds.min.z),
                    new Vector3 (bounds.max.x, bounds.max.y, bounds.min.z),
                    new Vector3 (bounds.min.x, bounds.min.y, bounds.min.z),
                    new Vector3 (bounds.min.x, bounds.max.y, bounds.max.z),
                    new Vector3 (bounds.max.x, bounds.max.y, bounds.max.z),
                    new Vector3 (bounds.max.x, bounds.min.y, bounds.max.z),
                    new Vector3 (bounds.min.x, bounds.min.y, bounds.max.z),
                };

            int[] triangles =
            {
                    0, 2, 1, //face front
			        0, 3, 2,
                    2, 3, 4, //face top
			        2, 4, 5,
                    1, 2, 5, //face right
			        1, 5, 6,
                    0, 7, 4, //face left
			        0, 4, 3,
                    5, 4, 7, //face back
			        5, 7, 6,
                    0, 6, 7, //face bottom
			        0, 1, 6
                };

            Mesh mesh = highlightCube.AddComponent<MeshFilter>().mesh;
            MeshRenderer hRend = highlightCube.AddComponent<MeshRenderer>();
            hRend.material = MaterialManager.HighlightMaterial;
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.Optimize();
            mesh.RecalculateNormals();

            highlightCube.transform.SetParent(transform);
            highlightCube.transform.localPosition = Vector3.zero;
            highlightCube.transform.localRotation = Quaternion.identity;
            highlightCube.transform.localScale = Vector3.one;
            highlightCube.SetActive(false);
        }
    }
}