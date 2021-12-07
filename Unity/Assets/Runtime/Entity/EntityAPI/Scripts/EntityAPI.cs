using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiveSQD.Parallels.Entity
{
    public class EntityAPI : MonoBehaviour
    {
        public class Transform
        {
            public Vector3 position;

            public Quaternion rotation;

            public Vector3 scale;

            public Vector3 size;

            public bool local;
        }

        public class PhysicalProperties
        {
            public float angularDrag;

            public Vector3 centerOfMass;

            public float drag;

            public bool gravitational;

            public float mass;
        }

        public class Motion
        {
            public Vector3 angularVelocity;

            public Vector3 velocity;

            public bool stationary;
        }

        /// <summary>
        /// Load a mesh entity.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="path">UUID of the request message.</param>
        /// <returns>A LoadMeshEntity_Return message body.</returns>
        public static string LoadMeshEntity(string entityPath, string parentUUID,
            Vector3 position, Quaternion rotation, Vector3 scale, bool isSize = false,
            string[] resources = null)
        {
            // Validate entity path.
            if (string.IsNullOrEmpty(entityPath))
            {
                string error = "[EntityAPI->LoadMeshEntity] Invalid Parameter: entityPath";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Validate parent.
            if (parentUUID == null)
            {
                // Root object.
            }

            // Validate position.
            if (position == null)
            {
                string error = "[EntityAPI->LoadMeshEntity] Invalid Parameter: position";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Validate rotation.
            if (rotation == null)
            {
                string error = "[EntityAPI->LoadMeshEntity] Invalid Parameter: rotation";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Validate scale.
            if (scale == null)
            {
                string error = "[EntityAPI->LoadMeshEntity] Invalid Parameter: scale";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Load entity.
            Guid id = Runtime.Parallels.EntityManager.LoadMeshEntityFromGLTF(entityPath,
                parentUUID == null ? null : EntityManager.FindEntity(Guid.Parse(parentUUID)),
                position, rotation, scale, isSize, resources);

            // Send return message.
            return id.ToString();
        }

        /// <summary>
        /// Load a mesh entity.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="path">UUID of the request message.</param>
        /// <returns>A LoadMeshEntity_Return message body.</returns>
        public static string LoadContainerEntity(string parentUUID,
            Vector3 position, Quaternion rotation, Vector3 scale, bool isSize = false)
        {
            // Validate parent.
            if (parentUUID == null)
            {
                // Root object.
            }

            // Validate position.
            if (position == null)
            {
                string error = "[EntityAPI->LoadMeshEntity] Invalid Parameter: position";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Validate rotation.
            if (rotation == null)
            {
                string error = "[EntityAPI->LoadMeshEntity] Invalid Parameter: rotation";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Validate scale.
            if (scale == null)
            {
                string error = "[EntityAPI->LoadMeshEntity] Invalid Parameter: scale";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Load entity.
            Guid id;// = EntityManager.LoadMeshEntityFromGLTF(entityPath,
                //parentUUID == null ? null : EntityManager.FindEntity(Guid.Parse(parentUUID)),
                //position, rotation, scale);

            if (isSize)
            {
                EntityAPI.SetSize(id.ToString(), scale);
            }

            // Send return message.
            return id.ToString();
        }

        /// <summary>
        /// Handle a SetEntityVisibility request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="path">UUID of the request message.</param>
        /// <returns>A SetEntityVisibility_Return message body.</returns>
        public static bool SetVisibility(string entityUUID, bool visible)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->SetVisibility] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->SetVisibility] Entity Not Found: "
                    + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Set visibility.
            if (entityToSet.SetVisibility(visible) == false)
            {
                string error = "[EntityAPI->SetVisibility] Error Setting Visibility: "
                    + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return true;
        }

        /// <summary>
        /// Compares the two entity instances.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A CompareEntities_Return message body.</returns>
        public static bool Compare(string entity1UUID, string entity2UUID)
        {
            // Validate entity 1 UUID.
            if (entity1UUID == null)
            {
                string error = "[EntityAPI->Compare] Invalid Parameter: entity1UUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate entity 2 UUID.
            if (entity2UUID == null)
            {
                string error = "[EntityAPI->Compare] Invalid Parameter: entity2UUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity 1.
            BaseEntity entity1 = EntityManager.FindEntity(Guid.Parse(entity1UUID));
            if (entity1 == null)
            {
                string error = "[EntityAPI->Compare] Entity Not Found: " + entity1UUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity 2.
            BaseEntity entity2 = EntityManager.FindEntity(Guid.Parse(entity2UUID));
            if (entity2 == null)
            {
                string error = "[EntityAPI->Compare] Entity Not Found: " + entity2UUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Get comparison.
            bool equal = entity1.Compare(entity2);

            // Send return message.
            return equal;
        }

        /// <summary>
        /// Handle a DeleteEntity request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A DeleteEntity_Return message body.</returns>
        public static bool DeleteEntity(string entityUUID)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->Delete] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->DeleteEntity] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Delete entity.
            if (entityToSet.Delete() == false)
            {
                string error = "[EntityAPI->Delete] Error Deleting Entity: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return true;
        }

        /// <summary>
        /// Handle a SetHighlight request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityHighlight_Return message body.</returns>
        public static bool SetHighlight(string entityUUID, bool highlight)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->SetHighlight] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->SetHighlight] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Set highlight.
            if (entityToSet.SetHighlight(highlight) == false)
            {
                string error = "[EntityAPI->SetHighlight] Error Setting Highlight: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return true;
        }

        /// <summary>
        /// Handle a SetParent request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityParent_Return message body.</returns>
        public static bool SetParent(string entityUUID, string parentUUID)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->SetParent] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->SetParent] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find parent entity.
            BaseEntity parentEntityToSet = null;
            if (parentUUID == null)
            {
                // Root Object.
            }
            else
            {
                parentEntityToSet = EntityManager.FindEntity(Guid.Parse(parentUUID));
                if (parentEntityToSet == null)
                {
                    string error = "[EntityAPI->SetParent] Parent Entity Not Found: " + parentUUID;
                    Utilities.LogSystem.LogWarning(error);
                    return false;
                }
            }

            // Set parent.
            if (entityToSet.SetParent(parentEntityToSet) == false)
            {
                string error = "[EntityPackage->SetParent] Error Setting Parent: "
                    + entityUUID + " : " + parentUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return true;
        }

        /// <summary>
        /// Handle a GetParent request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityParent_Return message body.</returns>
        public static string GetParent(string entityUUID)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->GetParent] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Find entity.
            BaseEntity entityToGet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToGet == null)
            {
                string error = "[EntityAPI->GetParent] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Get parent.
            BaseEntity parentEntity = entityToGet.GetParent();

            // Send return message.
            return parentEntity == null ? null : parentEntity.id.ToString();
        }

        /// <summary>
        /// Handle a GetChildren request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityChildren_Return message body.</returns>
        public static string[] GetChildren(string entityUUID)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->GetChildren] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Find entity.
            BaseEntity entityToGet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToGet == null)
            {
                string error = "[EntityAPI->GetChildren] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Get children.
            BaseEntity[] childEntities = entityToGet.GetChildren();

            // Send return message.
            List<string> children = new List<string>();
            foreach (BaseEntity childEntity in childEntities)
            {
                children.Add(childEntity.id.ToString());
            }
            return children.ToArray();
        }

        /// <summary>
        /// Handle a SetPosition request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityPosition_Return message body.</returns>
        public static bool SetPosition(string entityUUID, Vector3 position, bool local)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->SetPosition] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->GetChildren] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate position.
            if (position == null)
            {
                string error = "[EntityAPI->SetPosition] Invalid Parameter: position";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Set position.
            if (entityToSet.SetPosition(position, local) == false)
            {
                string error = "[EntityAPI->SetPosition] Error Setting Position: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return true;
        }

        /// <summary>
        /// Handle a SetRotation request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityRotation_Return message body.</returns>
        public static bool SetRotation(string entityUUID, Quaternion rotation, bool local)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->SetRotation] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->SetRotation] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate rotation.
            if (rotation == null)
            {
                string error = "[EntityAPI->SetRotation] Invalid Parameter: rotation";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Set rotation.
            if (entityToSet.SetRotation(rotation, local) == false)
            {
                string error = "[EntityAPI->SetRotation] Error Setting Rotation: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return true;
        }

        /// <summary>
        /// Handle a SetScale request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityScale_Return message body.</returns>
        public static bool SetScale(string entityUUID, Vector3 scale)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->SetScale] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->SetScale] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate scale.
            if (scale == null)
            {
                string error = "[EntityAPI->SetScale] Invalid Parameter: scale";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Set scale.
            if (entityToSet.SetScale(scale) == false)
            {
                string error = "[EntityAPI->SetScale] Error Setting Scale: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return true;
        }

        /// <summary>
        /// Handle a SetSize request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntitySize_Return message body.</returns>
        public static bool SetSize(string entityUUID, Vector3 size)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->SetSze] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            Debug.Log(entityUUID);
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->SetSize] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate size.
            if (size == null)
            {
                string error = "[EntityAPI->SetSize] Invalid Parameter: size";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Set size.
            if (entityToSet.SetSize(size) == false)
            {
                string error = "[EntityAPI->SetSize] Error Setting Size: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return true;
        }

        /// <summary>
        /// Handle a GetScale request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityScale_Return message body.</returns>
        public static Transform GetTransform(string entityUUID, bool local)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->GetTransform] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Find entity.
            BaseEntity entityToGet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToGet == null)
            {
                string error = "[EntityAPI->GetScale] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Get position.
            Vector3 position = entityToGet.GetPosition(local);

            // Get rotation.
            Quaternion rotation = entityToGet.GetRotation(local);

            // Get scale.
            Vector3 scale = entityToGet.GetScale();

            // Get size.
            Vector3 size = entityToGet.GetSize();

            // Send return message.
            return new Transform()
            {
                position = position,
                rotation = rotation,
                scale = scale,
                size = size,
                local = local
            };
        }

        /// <summary>
        /// Handle a SetPhysicalProperties request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityPhysicalProperties_Return message body.</returns>
        public static bool SetPhysicalProperties(string entityUUID, PhysicalProperties properties)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->SetEntityPhysicalProperties] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->SetEntityPhysicalProperties] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate properties.
            if (properties == null)
            {
                string error = "[EntityAPI->SetEntityPhysicalProperties] Invalid Parameter: properties";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate center of mass.
            if (properties.centerOfMass == null)
            {
                string error = "[EntityAPI->SetEntityPhysicalProperties] Invalid Parameter: centerOfMass";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            BaseEntity.EntityPhysicalProperties props = new BaseEntity.EntityPhysicalProperties();
            props.angularDrag = properties.angularDrag;
            props.centerOfMass = properties.centerOfMass;
            props.drag = properties.drag;
            props.gravitational = properties.gravitational;
            props.mass = properties.mass;

            // Set physical properties.
            if (entityToSet.SetPhysicalProperties(props) == false)
            {
                string error = "[EntityAPI->SetPhysicalProperties] Error Setting Physical Properties: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return false;
        }

        /// <summary>
        /// Handle a GetPhysicalProperties request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityPhysicalProperties_Return message body.</returns>
        public static PhysicalProperties GetPhysicalProperties(string entityUUID)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->GetEntityPhysicalProperties] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Find entity.
            BaseEntity entityToGet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToGet == null)
            {
                string error = "[EntityAPI->GetEntityPhysicalProperties] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Get properties.
            BaseEntity.EntityPhysicalProperties? props = entityToGet.GetPhysicalProperties();
            if (props.HasValue == false)
            {
                string error = "[EntityAPI->GetPhysicalProperties] Entity Physical Properties Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Send return message.
            return new PhysicalProperties
            {
                angularDrag = props.Value.angularDrag.HasValue ? props.Value.angularDrag.Value : 0,
                centerOfMass = props.Value.centerOfMass,
                drag = props.Value.drag.HasValue ? props.Value.drag.Value : 0,
                gravitational = props.Value.gravitational.HasValue ? props.Value.gravitational.Value : false,
                mass = props.Value.mass.HasValue ? props.Value.mass.Value : 0
            };
        }

        /// <summary>
        /// Handle a SetInteractionState request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityInteractionState_Return message body.</returns>
        public static bool SetInteractionState(string entityUUID, string interactionState)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->SetEntityInteractionState] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->SetEntityInteractionState] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate interaction state.
            if (interactionState == null)
            {
                string error = "[EntityAPI->SetEntityInteractionState] Invalid Parameter: interactionStatw";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            BaseEntity.InteractionState interactionStateEnum = BaseEntity.InteractionState.Hidden;
            switch (interactionState.ToUpper())
            {
                case "HIDDEN":
                    interactionStateEnum = BaseEntity.InteractionState.Hidden;
                    break;

                case "PHYSICAL":
                    interactionStateEnum = BaseEntity.InteractionState.Physical;
                    break;

                case "PLACING":
                    interactionStateEnum = BaseEntity.InteractionState.Placing;
                    break;

                case "STATIC":
                    interactionStateEnum = BaseEntity.InteractionState.Static;
                    break;

                default:
                    string error = "[EntityAPI->SetInteractionState] Invalid Interaction State: "
                        + interactionState;
                    Utilities.LogSystem.LogWarning(error);
                    return false;
            }

            // Set interaction state.
            if (entityToSet.SetInteractionState(interactionStateEnum) == false)
            {
                string error = "[EntityAPI->SetInteractionState] Error Setting Interaction State: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return true;
        }

        /// <summary>
        /// Handle a GetInteractionState request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityInteractionState_Return message body.</returns>
        public static string GetInteractionState(string entityUUID)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->GetEntityInteractionState] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Find entity.
            BaseEntity entityToGet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToGet == null)
            {
                string error = "[EntityAPI->GetEntityInteractionState] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Get interaction state.
            BaseEntity.InteractionState interactionState = entityToGet.GetInteractionState();
            string interactionStateString;
            switch (interactionState)
            {
                case BaseEntity.InteractionState.Hidden:
                    interactionStateString = "HIDDEN";
                    break;

                case BaseEntity.InteractionState.Physical:
                    interactionStateString = "PHYSICAL";
                    break;

                case BaseEntity.InteractionState.Placing:
                    interactionStateString = "PLACING";
                    break;

                case BaseEntity.InteractionState.Static:
                    interactionStateString = "STATIC";
                    break;

                default:
                    string error = "[EntityAPI->GetInteractionState] Unable To Get Interaction State: " + entityUUID;
                    Utilities.LogSystem.LogWarning(error);
                    return null;
            }

            // Send return message.
            return interactionStateString;
        }

        /// <summary>
        /// Handle a SetMotion request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityMotion_Return message body.</returns>
        public static bool SetMotion(string entityUUID, Motion motion)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->SetEntityMotion] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Find entity.
            BaseEntity entityToSet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToSet == null)
            {
                string error = "[EntityAPI->SetEntityMotion] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate motion.
            if (motion == null)
            {
                string error = "[EntityAPI->SetEntityMotion] Invalid Parameter: motion";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate angular velocity.
            if (motion.angularVelocity == null)
            {
                string error = "[EntityAPI->SetEntityMotion] Invalid Parameter: motion.angularVelocity";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Validate velocity.
            if (motion.velocity == null)
            {
                string error = "[EntityAPI->SetEntityMotion] Invalid Parameter: motion.velocity";
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            BaseEntity.EntityMotion motionOutput = new BaseEntity.EntityMotion
            {
                angularVelocity = motion.angularVelocity,
                velocity = motion.velocity,
                stationary = motion.stationary
            };

            // Set physical properties.
            if (entityToSet.SetMotion(motionOutput) == false)
            {
                string error = "[EntityAPI->SetPhysicalProperties] Error Setting Motion: " + entityUUID.ToString();
                Utilities.LogSystem.LogWarning(error);
                return false;
            }

            // Send return message.
            return true;
        }

        /// <summary>
        /// Handle a GetMotion request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityMotion_Return message body.</returns>
        public static Motion GetMotion(string entityUUID)
        {
            // Validate entity UUID.
            if (entityUUID == null)
            {
                string error = "[EntityAPI->GetEntityMotion] Invalid Parameter: entityUUID";
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Find entity.
            BaseEntity entityToGet = EntityManager.FindEntity(Guid.Parse(entityUUID));
            if (entityToGet == null)
            {
                string error = "[EntityAPI->GetEntityMotion] Entity Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Get motion.
            BaseEntity.EntityMotion? motion = entityToGet.GetMotion();
            if (motion.HasValue == false)
            {
                string error = "[EntityAPI->GetMotion] Entity Motion Not Found: " + entityUUID;
                Utilities.LogSystem.LogWarning(error);
                return null;
            }

            // Send return message.
            return new Motion
            {
                angularVelocity = motion.Value.angularVelocity,
                velocity = motion.Value.velocity,
                stationary = motion.Value.stationary.HasValue ? motion.Value.stationary.Value : false
            };
        }
    }
}