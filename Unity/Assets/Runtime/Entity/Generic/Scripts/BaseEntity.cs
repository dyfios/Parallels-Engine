using System;
using System.Collections.Generic;
using UnityEngine;
using FiveSQD.Parallels.Entity.Placement;

namespace FiveSQD.Parallels.Entity
{
    public class BaseEntity : MonoBehaviour
    {
        public struct EntityPhysicalProperties
        {
            public float? angularDrag;
            public Vector3 centerOfMass;
            public float? drag;
            public bool? gravitational;
            public float? mass;
        }

        public struct EntityMotion
        {
            public Vector3 angularVelocity;
            public bool? stationary;
            public Vector3 velocity;
        }

        public enum InteractionState { Hidden, Static, Physical, Placing }

        public List<PlacementSocket> sockets = new List<PlacementSocket>();

        protected InteractionState interactionState;

        private bool idInitialized = false;
        private Guid id_internal;
        public Guid id
        {
            set
            {
                if (idInitialized == true)
                {
                    throw new InvalidOperationException("BaseEntity id is an immutable property.");
                }
                id_internal = value;
            }
            get
            {
                return id_internal;
            }
        }

        public virtual bool SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
            return true;
        }

        public virtual bool Delete()
        {
            Destroy(gameObject);
            return true;
        }

        public virtual bool SetHighlight(bool highlight)
        {
            return false;
        }

        public bool SetParent(BaseEntity parent)
        {
            if (parent == null)
            {
                return true;
            }
            
            transform.SetParent(parent.transform);

            return true;
        }

        public BaseEntity GetParent()
        {
            if (transform.parent == null) // TODO.
            {
                return null;
            }

            BaseEntity parent = transform.parent.GetComponent<BaseEntity>();
            if (parent == null)
            {
                Utilities.LogSystem.LogError("[BaseEntity->GetParent()] Parent not an entity.");
                return null;
            }

            return parent;
        }

        public BaseEntity[] GetChildren()
        {
            List<BaseEntity> children = new List<BaseEntity>();
            foreach (BaseEntity child in GetComponentsInChildren<BaseEntity>())
            {
                if (child != this)
                {
                    children.Add(child);
                }
            }
            return children.ToArray();
        }

        public bool SetPosition(Vector3 position, bool local)
        {
            if (position == null)
            {
                Utilities.LogSystem.LogWarning("[BaseEntity->SetPosition] Position value null.");
                return false;
            }

            if (local)
            {
                transform.localPosition = position;
            }
            else
            {
                transform.position = position;
            }
            return true;
        }

        public Vector3 GetPosition(bool local)
        {
            return local ? transform.localPosition : transform.position;
        }

        public bool SetRotation(Quaternion rotation, bool local)
        {
            if (rotation == null)
            {
                Utilities.LogSystem.LogWarning("[BaseEntity->SetRotation] Rotation value null.");
                return false;
            }

            if (local)
            {
                transform.localRotation = rotation;
            }
            else
            {
                transform.rotation = rotation;
            }
            return true;
        }

        public Quaternion GetRotation(bool local)
        {
            return local ? transform.localRotation : transform.rotation;
        }

        public bool SetScale(Vector3 scale)
        {
            if (scale == null)
            {
                Utilities.LogSystem.LogWarning("[BaseEntity->SetScale] Scale value null.");
                return false;
            }
            transform.localScale = scale;
            return true;
        }

        public Vector3 GetScale()
        {
            return transform.localScale;
        }

        public virtual bool SetSize(Vector3 size)
        {
            throw new System.NotImplementedException("SetSize() not implemented.");
        }

        public virtual Vector3 GetSize()
        {
            throw new System.NotImplementedException("GetSize() not implemented.");
        }

        public bool Compare(BaseEntity otherEntity)
        {
            return otherEntity == this;
        }

        public virtual bool SetPhysicalProperties(EntityPhysicalProperties? propertiesToSet)
        {
            return false;
        }

        public virtual EntityPhysicalProperties? GetPhysicalProperties()
        {
            return null;
        }

        public virtual bool SetInteractionState(InteractionState stateToSet)
        {
            return false;
        }

        public InteractionState GetInteractionState()
        {
            return interactionState;
        }

        public virtual bool SetMotion(EntityMotion? motionToSet)
        {
            return false;
        }

        public virtual EntityMotion? GetMotion()
        {
            return null;
        }

        public virtual void Initialize(Guid idToSet)
        {
            id = idToSet;

            // TODO event.
        }

        public virtual void TearDown()
        {

        }
    }
}