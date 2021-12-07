using System;
using System.Collections.Generic;
using UnityEngine;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.LoadMeshEntity;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.LoadMeshEntity_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityVisibility;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityVisibility_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.CompareEntities;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.CompareEntities_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.DeleteEntity;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.DeleteEntity_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityHighlightState;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityHighlightState_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityParent;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityParent_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityParent;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityParent_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityChildren;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityChildren_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityPosition;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityPosition_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityPosition;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityPostion_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityRotation;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityRotation_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityRotation;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityRotation_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityScale;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityScale_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityScale;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityScale_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntitySize;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntitySize_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntitySize;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntitySize_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityPhysicalProperties;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityPhysicalProperties_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityPhysicalProperties;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityPhysicalProperties_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityInteractionState;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityInteractionState_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityInteractionState;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityInteractionState_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityMotion;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.SetEntityMotion_Return;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityMotion;
using FiveSQD.Parallels.Entity.Schema.TwentyTwentyOne.One.Zero.GetEntityMotion_Return;
using FiveSQD.Parallels.Infrastructure.Models;
using FiveSQD.Parallels.Runtime.Engine;

namespace FiveSQD.Parallels.Entity
{
    public class EntityManager : BaseManager
    {
        public MeshManager meshManager;

        /// <summary>
        /// Dictionary of loaded entities.
        /// </summary>
        private static Dictionary<Guid, BaseEntity> entities = new Dictionary<Guid, BaseEntity>();
        
        /// <summary>
        /// Get a new entity ID.
        /// </summary>
        private static Guid GetEntityID()
        {
            return Guid.NewGuid();
        }

        // TODO.
        public override void Initialize()
        {
            meshManager.Initialize();
        }

        /*public override void HandleMessage(VOSMessage message)
        {
            base.HandleMessage(message);

            object returnMessage = null;
            string returnTopic = "VOS.RTN.ERROR";
            switch (message.topic)
            {
                case "VOS.API.STD.ENTITY.SETVISIBILITY":
                    returnMessage = SetVisibility((SetEntityVisibility) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.SETVISIBILITY";
                    break;

                case "VOS.API.STD.ENTITY.DELETE":
                    returnMessage = DeleteEntity((DeleteEntity) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.DELETE";
                    break;

                case "VOS.API.STD.ENTITY.SETHIGHLIGHT":
                    returnMessage = SetHighlight((SetEntityHighlightState) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.SETHIGHLIGHT";
                    break;

                case "VOS.API.STD.ENTITY.SETPARENT":
                    returnMessage = SetParent((SetEntityParent) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.SETPARENT";
                    break;

                case "VOS.API.STD.ENTITY.GETPARENT":
                    returnMessage = GetParent((GetEntityParent) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.GETPARENT";
                    break;

                case "VOS.API.STD.ENTITY.GETCHILDREN":
                    returnMessage = GetChildren((GetEntityChildren) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.GETCHILDREN";
                    break;

                case "VOS.API.STD.ENTITY.SETPOSITION":
                    returnMessage = SetPosition((SetEntityPosition) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.SETPOSITION";
                    break;

                case "VOS.API.STD.ENTITY.GETPOSITION":
                    returnMessage = GetPosition((GetEntityPosition) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.GETPOSITION";
                    break;

                case "VOS.API.STD.ENTITY.SETROTATION":
                    returnMessage = SetRotation((SetEntityRotation) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.SETROTATION";
                    break;

                case "VOS.API.STD.ENTITY.GETROTATION":
                    returnMessage = GetRotation((GetEntityRotation) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.GETROTATION";
                    break;

                case "VOS.API.STD.ENTITY.SETSCALE":
                    returnMessage = SetScale((SetEntityScale) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.SETSCALE";
                    break;

                case "VOS.API.STD.ENTITY.GETSCALE":
                    returnMessage = GetScale((GetEntityScale) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.GETSCALE";
                    break;

                case "VOS.API.STD.ENTITY.SETSIZE":
                    returnMessage = SetSize((SetEntitySize) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.SETSIZE";
                    break;

                case "VOS.API.STD.ENTITY.GETSIZE":
                    returnMessage = GetSize((GetEntitySize) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.GETSIZE";
                    break;

                case "VOS.API.STD.ENTITY.COMPARE":
                    returnMessage = Compare((CompareEntities) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.COMPARE";
                    break;

                case "VOS.API.STD.ENTITY.SETPHYSICALPROPERTIES":
                    returnMessage = SetPhysicalProperties((SetEntityPhysicalProperties) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.SETPHYSICALPROPERTIES";
                    break;

                case "VOS.API.STD.ENTITY.GETPHYSICALPROPERTIES":
                    returnMessage = GetPhysicalProperties((GetEntityPhysicalProperties) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.GETPHYSICALPROPERTIES";
                    break;

                case "VOS.API.STD.ENTITY.SETINTERACTIONSTATE":
                    returnMessage = SetInteractionState((SetEntityInteractionState) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.SETINTERACTIONSTATE";
                    break;

                case "VOS.API.STD.ENTITY.GETINTERACTIONSTATE":
                    returnMessage = GetInteractionState((GetEntityInteractionState) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.GETINTERACTIONSTATE";
                    break;

                case "VOS.API.STD.ENTITY.SETMOTION":
                    returnMessage = SetMotion((SetEntityMotion) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.SETMOTION";
                    break;

                case "VOS.API.STD.ENTITY.GETMOTION":
                    returnMessage = GetMotion((GetEntityMotion) message.body, Guid.Parse(message.header.uuid));
                    returnTopic = "VOS.RTN.STD.ENTITY.GETMOTION";
                    break;
            }

            // Create return message.
            VOSMessage reply = new VOSMessage()
            {
                topic = returnTopic,
                header = new MessageHeader
                {
                    uuid = message.header.uuid
                },
                body = returnMessage
            };

            // Send return message.
            Runtime.Parallels.VOSManager.SendMessage(reply);
        }*/

        /// <summary>
        /// Handle a LoadMeshEntity request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="path">UUID of the request message.</param>
        /// <returns>A LoadMeshEntity_Return message body.</returns>
        public LoadMeshEntity_Return LoadMeshEntity(LoadMeshEntity messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->LoadMeshEntity] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new LoadMeshEntity_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }
            
            // Validate entity path.
            if (string.IsNullOrEmpty(messageBody.EntityPath))
            {
                string error = "[EntityManager->LoadMeshEntity] Invalid Parameter: ENTITY-PATH";
                Utilities.LogSystem.LogWarning(error);
                return new LoadMeshEntity_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }
            
            // Validate parent.
            if (messageBody.ParentUuid == null)
            {
                // Root object.
            }
            
            // Validate position.
            if (messageBody.Position == null)
            {
                string error = "[EntityManager->LoadMeshEntity] Invalid Parameter: POSITION";
                Utilities.LogSystem.LogWarning(error);
                return new LoadMeshEntity_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate rotation.
            if (messageBody.Rotation == null)
            {
                string error = "[EntityManager->LoadMeshEntity] Invalid Parameter: ROTATION";
                Utilities.LogSystem.LogWarning(error);
                return new LoadMeshEntity_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate scale.
            if (messageBody.Scale == null)
            {
                string error = "[EntityManager->LoadMeshEntity] Invalid Parameter: SCALE";
                Utilities.LogSystem.LogWarning(error);
                return new LoadMeshEntity_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Load entity.
            Guid id = LoadMeshEntityFromGLTF(messageBody.EntityPath,
                messageBody.ParentUuid == null ? null : FindEntity(messageBody.ParentUuid),
                new Vector3((float) messageBody.Position.X,
                (float) messageBody.Position.Y, (float) messageBody.Position.Z),
                new Quaternion((float) messageBody.Rotation.X,
                (float) messageBody.Rotation.Y, (float) messageBody.Rotation.Z, (float) messageBody.Rotation.W),
                new Vector3((float) messageBody.Scale.X, (float) messageBody.Scale.Y, (float) messageBody.Scale.Z));

            // Send return message.
            return new LoadMeshEntity_Return
            {
                RequestUuid = uuid,
                EntityUuid = id,
                Status = "Success"
            };
        }
        
        /// <summary>
        /// Handle a SetEntityVisibility request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="path">UUID of the request message.</param>
        /// <returns>A SetEntityVisibility_Return message body.</returns>
        public SetEntityVisibility_Return SetVisibility(SetEntityVisibility messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->SetVisibility] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityVisibility_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->SetVisibility] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityVisibility_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Get visibility.
            if (messageBody.Visible == null)
            {
                string error = "[EntityManager->SetVisibility] Invalid Parameter: VISIBLE";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityVisibility_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(messageBody.EntityUuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->SetVisibility] Entity Not Found: "
                    + messageBody.EntityUuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityVisibility_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Set visibility.
            if (entityToSet.SetVisibility(messageBody.Visible) == false)
            {
                string error = "[EntityManager->SetVisibility] Error Setting Visibility: "
                    + messageBody.EntityUuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityVisibility_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new SetEntityVisibility_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Compares the two entity instances.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A CompareEntities_Return message body.</returns>
        public CompareEntities_Return Compare(CompareEntities messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->Compare] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new CompareEntities_Return
                {
                    RequestUuid = uuid,
                    Equal = false,
                    Status = error
                };
            }

            // Validate entity 1 UUID.
            if (messageBody.Entity1Uuid == null)
            {
                string error = "[EntityManager->Compare] Invalid Parameter: ENTITY-1-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new CompareEntities_Return
                {
                    RequestUuid = uuid,
                    Equal = false,
                    Status = error
                };
            }

            // Validate entity 2 UUID.
            if (messageBody.Entity2Uuid == null)
            {
                string error = "[EntityManager->Compare] Invalid Parameter: ENTITY-2-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new CompareEntities_Return
                {
                    RequestUuid = uuid,
                    Equal = false,
                    Status = error
                };
            }

            // Find entity 1.
            BaseEntity entity1 = FindEntity(messageBody.Entity1Uuid);
            if (entity1 == null)
            {
                string error = "[EntityManager->Compare] Entity Not Found: " + messageBody.Entity1Uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new CompareEntities_Return
                {
                    RequestUuid = uuid,
                    Equal = false,
                    Status = error
                };
            }

            // Find entity 2.
            BaseEntity entity2 = FindEntity(messageBody.Entity2Uuid);
            if (entity2 == null)
            {
                string error = "[EntityManager->Compare] Entity Not Found: " + messageBody.Entity2Uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new CompareEntities_Return
                {
                    RequestUuid = uuid,
                    Equal = false,
                    Status = error
                };
            }

            // Get comparison.
            bool equal = entity1.Compare(entity2);

            // Send return message.
            return new CompareEntities_Return
            {
                RequestUuid = uuid,
                Equal = equal,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a DeleteEntity request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A DeleteEntity_Return message body.</returns>
        public DeleteEntity_Return DeleteEntity(DeleteEntity messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->Delete] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new DeleteEntity_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->Delete] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new DeleteEntity_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(uuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->DeleteEntity] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new DeleteEntity_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Delete entity.
            if (entityToSet.Delete() == false)
            {
                string error = "[EntityManager->Delete] Error Deleting Entity: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new DeleteEntity_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new DeleteEntity_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a SetHighlight request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityHighlight_Return message body.</returns>
        public SetEntityHighlightState_Return SetHighlight(SetEntityHighlightState messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->SetHighlight] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityHighlightState_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->SetHighlight] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityHighlightState_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(uuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->SetHighlight] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityHighlightState_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Set highlight.
            if (entityToSet.SetHighlight(messageBody.Highlight) == false)
            {
                string error = "[EntityManager->SetHighlight] Error Setting Highlight: " + uuid;
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityHighlightState_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new SetEntityHighlightState_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a SetParent request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityParent_Return message body.</returns>
        public SetEntityParent_Return SetParent(SetEntityParent messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->SetParent] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityParent_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->SetParent] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityParent_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(uuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->SetParent] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityParent_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find parent entity.
            BaseEntity parentEntityToSet = null;
            if (messageBody.ParentUuid == null)
            {
                // Root Object.
            }
            else
            {
                parentEntityToSet = FindEntity(messageBody.ParentUuid);
                if (parentEntityToSet == null)
                {
                    string error = "[EntityManager->SetParent] Parent Entity Not Found: " + uuid.ToString();
                    Utilities.LogSystem.LogWarning(error);
                    return new SetEntityParent_Return
                    {
                        RequestUuid = uuid,
                        Status = error
                    };
                }
            }

            // Set parent.
            if (entityToSet.SetParent(parentEntityToSet) == false)
            {
                string error = "[EntityPackage->SetParent] Error Setting Parent: "
                    + uuid.ToString() + " : " + messageBody.ParentUuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityParent_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new SetEntityParent_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a GetParent request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityParent_Return message body.</returns>
        public GetEntityParent_Return GetParent(GetEntityParent messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->GetParent] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityParent_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->GetParent] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityParent_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToGet = FindEntity(uuid);
            if (entityToGet == null)
            {
                string error = "[EntityManager->GetParent] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityParent_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Get parent.
            BaseEntity parentEntity = entityToGet.GetParent();

            // Send return message.
            return new GetEntityParent_Return
            {
                RequestUuid = uuid,
                ParentUuid = parentEntity.id,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a GetChildren request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityChildren_Return message body.</returns>
        public GetEntityChildren_Return GetChildren(GetEntityChildren messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->GetChildren] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityChildren_Return
                {
                    RequestUuid = uuid,
                    ChildUuids = null,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->GetChildren] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityChildren_Return
                {
                    RequestUuid = uuid,
                    ChildUuids = null,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToGet = FindEntity(uuid);
            if (entityToGet == null)
            {
                string error = "[EntityManager->GetChildren] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityChildren_Return
                {
                    RequestUuid = uuid,
                    ChildUuids = null,
                    Status = error
                };
            }

            // Get children.
            BaseEntity[] childEntities = entityToGet.GetChildren();

            // Send return message.
            List<Guid> children = new List<Guid>();
            foreach (BaseEntity childEntity in childEntities)
            {
                children.Add(childEntity.id);
            }
            return new GetEntityChildren_Return
            {
                RequestUuid = uuid,
                ChildUuids = children.ToArray(),
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a SetPosition request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityPosition_Return message body.</returns>
        public SetEntityPosition_Return SetPosition(SetEntityPosition messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->SetPosition] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPosition_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->SetPosition] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPosition_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(uuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->GetChildren] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPosition_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate position.
            if (messageBody.Position == null || messageBody.Position.X == null
                || messageBody.Position.Y == null || messageBody.Position.Z == null)
            {
                string error = "[EntityManager->SetPosition] Invalid Parameter: POSITION";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPosition_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate local.
            if (messageBody.Local == null)
            {
                string error = "[EntityManager->SetPosition] Invalid Parameter: LOCAL";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPosition_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Set position.
            if (entityToSet.SetPosition(
                new Vector3((float) messageBody.Position.X,
                (float) messageBody.Position.Y, (float) messageBody.Position.Z),
                messageBody.Local) == false)
            {
                string error = "[EntityManager->SetPosition] Error Setting Position: " + messageBody.EntityUuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPosition_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new SetEntityPosition_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a GetPosition request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityPosition_Return message body.</returns>
        public GetEntityPosition_Return GetPosition(GetEntityPosition messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->GetPosition] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityPosition_Return
                {
                    RequestUuid = uuid,
                    Position = null,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->GetPosition] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityPosition_Return
                {
                    RequestUuid = uuid,
                    Position = null,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToGet = FindEntity(uuid);
            if (entityToGet == null)
            {
                string error = "[EntityManager->GetPosition] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityPosition_Return
                {
                    RequestUuid = uuid,
                    Position = null,
                    Status = error
                };
            }

            // Validate local.
            if (messageBody.Local == null)
            {
                string error = "[EntityManager->GetPosition] Invalid Parameter: LOCAL";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityPosition_Return
                {
                    RequestUuid = uuid,
                    Position = null,
                    Status = error
                };
            }

            // Get position.
            Vector3 position = entityToGet.GetPosition(messageBody.Local);

            // Send return message.
            return new GetEntityPosition_Return
            {
                RequestUuid = uuid,
                Position = new Schema.TwentyTwentyOne.One.Zero.GetEntityPostion_Return.PropertiesPosition
                {
                    X = position.x,
                    Y = position.y,
                    Z = position.z
                },
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a SetRotation request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityRotation_Return message body.</returns>
        public SetEntityRotation_Return SetRotation(SetEntityRotation messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->SetRotation] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityRotation_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->SetRotation] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityRotation_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(uuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->SetRotation] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityRotation_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate rotation.
            if (messageBody.Rotation == null || messageBody.Rotation.X == null
                || messageBody.Rotation.Y == null || messageBody.Rotation.Z == null
                || messageBody.Rotation.W == null)
            {
                string error = "[EntityManager->SetRotation] Invalid Parameter: ROTATION";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityRotation_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate local.
            if (messageBody.Local == null)
            {
                string error = "[EntityManager->SetRotation] Invalid Parameter: LOCAL";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityRotation_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Set rotation.
            if (entityToSet.SetRotation(new Quaternion((float) messageBody.Rotation.X,
                (float) messageBody.Rotation.Y, (float) messageBody.Rotation.Z,
                (float) messageBody.Rotation.W), messageBody.Local) == false)
            {
                string error = "[EntityManager->SetRotation] Error Setting Rotation: " + messageBody.EntityUuid;
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityRotation_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new SetEntityRotation_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a GetRotation request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityRotation_Return message body.</returns>
        public GetEntityRotation_Return GetRotation(GetEntityRotation messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->GetRotation] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityRotation_Return
                {
                    RequestUuid = uuid,
                    Rotation = null,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->GetRotation] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityRotation_Return
                {
                    RequestUuid = uuid,
                    Rotation = null,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToGet = FindEntity(uuid);
            if (entityToGet == null)
            {
                string error = "[EntityManager->GetRotation] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityRotation_Return
                {
                    RequestUuid = uuid,
                    Rotation = null,
                    Status = error
                };
            }

            // Validate local.
            if (messageBody.Local == null)
            {
                string error = "[EntityManager->GetRotation] Invalid Parameter: LOCAL";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityRotation_Return
                {
                    RequestUuid = uuid,
                    Rotation = null,
                    Status = error
                };
            }

            // Get rotation.
            Quaternion rotation = entityToGet.GetRotation(messageBody.Local);

            // Send return message.
            return new GetEntityRotation_Return
            {
                RequestUuid = uuid,
                Rotation = new Schema.TwentyTwentyOne.One.Zero.GetEntityRotation_Return.PropertiesRotation
                {
                    X = rotation.x,
                    Y = rotation.y,
                    Z = rotation.z,
                    W = rotation.w
                },
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a SetScale request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityScale_Return message body.</returns>
        public SetEntityScale_Return SetScale(SetEntityScale messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->SetScale] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityScale_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->SetScale] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityScale_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(uuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->SetScale] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityScale_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate scale.
            if (messageBody.Scale == null || messageBody.Scale.X == null
                || messageBody.Scale.Y == null || messageBody.Scale.Z == null)
            {
                string error = "[EntityManager->SetScale] Invalid Parameter: SCALE";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityScale_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Set scale.
            if (entityToSet.SetScale(
                new Vector3((float) messageBody.Scale.X, (float) messageBody.Scale.Y,
                (float) messageBody.Scale.Z)) == false)
            {
                string error = "[EntityManager->SetScale] Error Setting Scale: " + messageBody.EntityUuid;
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityScale_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new SetEntityScale_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a GetScale request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityScale_Return message body.</returns>
        public GetEntityScale_Return GetScale(GetEntityScale messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->GetScale] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityScale_Return
                {
                    RequestUuid = uuid,
                    Scale = null,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->GetScale] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityScale_Return
                {
                    RequestUuid = uuid,
                    Scale = null,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToGet = FindEntity(uuid);
            if (entityToGet == null)
            {
                string error = "[EntityManager->GetScale] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityScale_Return
                {
                    RequestUuid = uuid,
                    Scale = null,
                    Status = error
                };
            }

            // Get scale.
            Vector3 scale = entityToGet.GetScale();

            // Send return message.
            return new GetEntityScale_Return
            {
                RequestUuid = uuid,
                Scale = new Schema.TwentyTwentyOne.One.Zero.GetEntityScale_Return.PropertiesScale
                {
                    X = scale.x,
                    Y = scale.y,
                    Z = scale.z
                },
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a SetSize request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntitySize_Return message body.</returns>
        public SetEntitySize_Return SetSize(SetEntitySize messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->SetSize] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntitySize_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->SetSze] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntitySize_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(uuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->SetSize] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntitySize_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate size.
            if (messageBody.Size == null || messageBody.Size.X == null
                || messageBody.Size.Y == null || messageBody.Size.Z == null)
            {
                string error = "[EntityManager->SetSize] Invalid Parameter: SIZE";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntitySize_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Set size.
            if (entityToSet.SetSize(
                new Vector3((float) messageBody.Size.X, (float) messageBody.Size.Y,
                (float) messageBody.Size.Z)) == false)
            {
                string error = "[EntityManager->SetSize] Error Setting Size: " + messageBody.EntityUuid;
                Utilities.LogSystem.LogWarning(error);
                return new SetEntitySize_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new SetEntitySize_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a GetSize request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntitySize_Return message body.</returns>
        public GetEntitySize_Return GetSize(GetEntitySize messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->GetSize] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntitySize_Return
                {
                    RequestUuid = uuid,
                    Size = null,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->GetSize] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntitySize_Return
                {
                    RequestUuid = uuid,
                    Size = null,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToGet = FindEntity(uuid);
            if (entityToGet == null)
            {
                string error = "[EntityManager->GetSize] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new GetEntitySize_Return
                {
                    RequestUuid = uuid,
                    Size = null,
                    Status = error
                };
            }

            // Get size.
            Vector3 size = entityToGet.GetSize();

            // Send return message.
            return new GetEntitySize_Return
            {
                RequestUuid = uuid,
                Size = new Schema.TwentyTwentyOne.One.Zero.GetEntitySize_Return.PropertiesSize
                {
                    X = size.x,
                    Y = size.y,
                    Z = size.z
                },
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a SetPhysicalProperties request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityPhysicalProperties_Return message body.</returns>
        public SetEntityPhysicalProperties_Return SetPhysicalProperties(SetEntityPhysicalProperties messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->SetEntityPhysicalProperties] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->SetEntityPhysicalProperties] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(uuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->SetEntityPhysicalProperties] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate angular drag.
            if (messageBody.AngularDrag == null)
            {
                string error = "[EntityManager->SetEntityPhysicalProperties] Invalid Parameter: ANGULAR-DRAG";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate center of mass.
            if (messageBody.CenterOfMass == null || messageBody.CenterOfMass.X == null
                || messageBody.CenterOfMass.Y == null || messageBody.CenterOfMass.Z == null)
            {
                string error = "[EntityManager->SetEntityPhysicalProperties] Invalid Parameter: CENTER-OF-MASS";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate drag.
            if (messageBody.Drag == null)
            {
                string error = "[EntityManager->SetEntityPhysicalProperties] Invalid Parameter: DRAG";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate gravitational.
            if (messageBody.Gravitational == null)
            {
                string error = "[EntityManager->SetEntityPhysicalProperties] Invalid Parameter: GRAVITATIONAL";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate mass.
            if (messageBody.Mass == null)
            {
                string error = "[EntityManager->SetEntityPhysicalProperties] Invalid Parameter: MASS";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            BaseEntity.EntityPhysicalProperties props = new BaseEntity.EntityPhysicalProperties();
            props.angularDrag = (float) messageBody.AngularDrag;
            props.centerOfMass = new Vector3((float) messageBody.CenterOfMass.X,
                (float) messageBody.CenterOfMass.Y, (float) messageBody.CenterOfMass.Z);
            props.drag = (float) messageBody.Drag;
            props.gravitational = messageBody.Gravitational;
            props.mass = (float) messageBody.Mass;

            // Set physical properties.
            if (entityToSet.SetPhysicalProperties(props) == false)
            {
                string error = "[EntityPackage->SetPhysicalProperties] Error Setting Physical Properties: " + messageBody.EntityUuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new SetEntityPhysicalProperties_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a GetPhysicalProperties request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityPhysicalProperties_Return message body.</returns>
        public GetEntityPhysicalProperties_Return GetPhysicalProperties(GetEntityPhysicalProperties messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->GetEntityPhysicalProperties] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->GetEntityPhysicalProperties] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToGet = FindEntity(uuid);
            if (entityToGet == null)
            {
                string error = "[EntityManager->GetEntityPhysicalProperties] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Get properties.
            BaseEntity.EntityPhysicalProperties? props = entityToGet.GetPhysicalProperties();
            if (props.HasValue == false)
            {
                string error = "[EntityManager->GetPhysicalProperties] Entity Physical Properties Not Found: " + messageBody.EntityUuid;
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityPhysicalProperties_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new GetEntityPhysicalProperties_Return
            {
                RequestUuid = uuid,
                AngularDrag = props.Value.angularDrag.HasValue ? props.Value.angularDrag.Value : 0,
                CenterOfMass = new Schema.TwentyTwentyOne.One.Zero.GetEntityPhysicalProperties_Return.PropertiesCenterOfMass
                {
                    X = props.Value.centerOfMass.x,
                    Y = props.Value.centerOfMass.y,
                    Z = props.Value.centerOfMass.z
                },
                Drag = props.Value.drag.HasValue ? props.Value.drag.Value : 0,
                EntityUuid = messageBody.EntityUuid,
                Gravitational = props.Value.gravitational.HasValue ? props.Value.gravitational.Value : false,
                Mass = props.Value.mass.HasValue ? props.Value.mass.Value : 0,
                Status = "success"
            };
        }

        /// <summary>
        /// Handle a SetInteractionState request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityInteractionState_Return message body.</returns>
        public SetEntityInteractionState_Return SetInteractionState(SetEntityInteractionState messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->SetEntityInteractionState] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityInteractionState_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->SetEntityInteractionState] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityInteractionState_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(uuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->SetEntityInteractionState] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityInteractionState_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate interaction state.
            if (messageBody.InteractionState == null)
            {
                string error = "[EntityManager->SetEntityInteractionState] Invalid Parameter: INTERACTION-STATE";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityInteractionState_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            BaseEntity.InteractionState interactionState = BaseEntity.InteractionState.Hidden;
            switch (messageBody.InteractionState.ToUpper())
            {
                case "HIDDEN":
                    interactionState = BaseEntity.InteractionState.Hidden;
                    break;

                case "PHYSICAL":
                    interactionState = BaseEntity.InteractionState.Physical;
                    break;

                case "PLACING":
                    interactionState = BaseEntity.InteractionState.Placing;
                    break;

                case "STATIC":
                    interactionState = BaseEntity.InteractionState.Static;
                    break;

                default:
                    string error = "[EntityManager->SetInteractionState] Invalid Interaction State: "
                        + messageBody.InteractionState;
                    Utilities.LogSystem.LogWarning(error);
                    return new SetEntityInteractionState_Return
                    {
                        RequestUuid = uuid,
                        Status = error
                    };
            }

            // Set interaction state.
            if (entityToSet.SetInteractionState(interactionState) == false)
            {
                string error = "[EntityManager->SetInteractionState] Error Setting Interaction State: " + messageBody.EntityUuid;
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityInteractionState_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new SetEntityInteractionState_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a GetInteractionState request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityInteractionState_Return message body.</returns>
        public GetEntityInteractionState_Return GetInteractionState(GetEntityInteractionState messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->GetEntityInteractionState] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityInteractionState_Return
                {
                    RequestUuid = uuid,
                    InteractionState = null,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->GetEntityInteractionState] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityInteractionState_Return
                {
                    RequestUuid = uuid,
                    InteractionState = null,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToGet = FindEntity(uuid);
            if (entityToGet == null)
            {
                string error = "[EntityManager->GetEntityInteractionState] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityInteractionState_Return
                {
                    RequestUuid = uuid,
                    InteractionState = null,
                    Status = error
                };
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
                    string error = "[EntityManager->GetInteractionState] Unable To Get Interaction State: " + messageBody.EntityUuid.ToString();
                    Utilities.LogSystem.LogWarning(error);
                    return new GetEntityInteractionState_Return
                    {
                        RequestUuid = uuid,
                        InteractionState = null,
                        Status = error
                    };
            }

            // Send return message.
            return new GetEntityInteractionState_Return
            {
                RequestUuid = uuid,
                InteractionState = interactionStateString,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a SetMotion request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A SetEntityMotion_Return message body.</returns>
        public SetEntityMotion_Return SetMotion(SetEntityMotion messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->SetEntityMotion] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->SetEntityMotion] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToSet = FindEntity(uuid);
            if (entityToSet == null)
            {
                string error = "[EntityManager->SetEntityMotion] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate angular velocity.
            if (messageBody.AngularVelocity == null || messageBody.AngularVelocity.X == null
                || messageBody.AngularVelocity.Y == null || messageBody.AngularVelocity.Z == null)
            {
                string error = "[EntityManager->SetEntityMotion] Invalid Parameter: ANGULAR-VELOCITY";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Validate velocity.
            if (messageBody.Velocity == null || messageBody.Velocity.X == null
                || messageBody.Velocity.Y == null || messageBody.Velocity.Z == null)
            {
                string error = "[EntityManager->SetEntityMotion] Invalid Parameter: VELOCITY";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            if (messageBody.Stationary == null)
            {
                string error = "[EntityManager->SetEntityMotion] Invalid Parameter: STATIONARY";
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            BaseEntity.EntityMotion motion = new BaseEntity.EntityMotion
            {
                angularVelocity = new Vector3((float) messageBody.AngularVelocity.X,
                (float) messageBody.AngularVelocity.Y, (float) messageBody.AngularVelocity.Z),
                velocity = new Vector3((float) messageBody.Velocity.X,
                (float) messageBody.Velocity.Y, (float) messageBody.Velocity.Z),
                stationary = messageBody.Stationary
            };

            // Set physical properties.
            if (entityToSet.SetMotion(motion) == false)
            {
                string error = "[EntityManager->SetPhysicalProperties] Error Setting Motion: " + messageBody.EntityUuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new SetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    Status = error
                };
            }

            // Send return message.
            return new SetEntityMotion_Return
            {
                RequestUuid = uuid,
                Status = "Success"
            };
        }

        /// <summary>
        /// Handle a GetMotion request.
        /// </summary>
        /// <param name="messageBody">Body of the request message.</param>
        /// <param name="uuid">UUID of the request message.</param>
        /// <returns>A GetEntityMotion_Return message body.</returns>
        public GetEntityMotion_Return GetMotion(GetEntityMotion messageBody, Guid uuid)
        {
            // Validate UUID.
            if (uuid == null)
            {
                string error = "[EntityManager->GetEntityMotion] No UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    AngularVelocity = null,
                    Velocity = null,
                    Status = error
                };
            }

            // Validate entity UUID.
            if (messageBody.EntityUuid == null)
            {
                string error = "[EntityManager->GetEntityMotion] Invalid Parameter: ENTITY-UUID";
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    AngularVelocity = null,
                    Velocity = null,
                    Status = error
                };
            }

            // Find entity.
            BaseEntity entityToGet = FindEntity(uuid);
            if (entityToGet == null)
            {
                string error = "[EntityManager->GetEntityMotion] Entity Not Found: " + uuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    AngularVelocity = null,
                    Velocity = null,
                    Status = error
                };
            }

            // Get motion.
            BaseEntity.EntityMotion? motion = entityToGet.GetMotion();
            if (motion.HasValue == false)
            {
                string error = "[EntityManager->GetMotion] Entity Motion Not Found: " + messageBody.EntityUuid.ToString();
                Utilities.LogSystem.LogWarning(error);
                return new GetEntityMotion_Return
                {
                    RequestUuid = uuid,
                    AngularVelocity = null,
                    Velocity = null,
                    Status = error
                };
            }

            // Send return message.
            return new GetEntityMotion_Return
            {
                RequestUuid = uuid,
                AngularVelocity = new Schema.TwentyTwentyOne.One.Zero.GetEntityMotion_Return.PropertiesAngularVelocity()
                {
                    X = motion.Value.angularVelocity.x,
                    Y = motion.Value.angularVelocity.y,
                    Z = motion.Value.angularVelocity.z
                },
                Velocity = new Schema.TwentyTwentyOne.One.Zero.GetEntityMotion_Return.PropertiesVelocity()
                {
                    X = motion.Value.velocity.x,
                    Y = motion.Value.velocity.y,
                    Z = motion.Value.velocity.z
                },
                Stationary = motion.Value.stationary.HasValue ? motion.Value.stationary.Value : false,
                Status = "Success"
            };
        }

        /// <summary>
        /// Loads a mesh entity from a GLTF file.
        /// </summary>
        /// <param name="path">Path to the GLTF file.</param>
        /// <param name="parentEntity">Entity to set as parent of loaded entity.</param>
        /// <param name="position">Position to apply to loaded entity.</param>
        /// <param name="rotation">Rotation to apply to loaded entity.</param>
        /// <param name="scale">Scale to apply to loaded entity.</param>
        /// <returns>The UUID (in Guid form) of the entity instance.</returns>
        public Guid LoadMeshEntityFromGLTF(string path, BaseEntity parentEntity,
            Vector3 position, Quaternion rotation, Vector3 scale, bool isSize = false,
            string[] resources = null)
        {
            Guid entityID = GetEntityID();
            Action<GameObject> callback =
                (GameObject go) => { OnGLTFLoaded(go, entityID, parentEntity, position, rotation, scale, isSize); };
            StartCoroutine(LoadGLTF(path, callback, resources));
            return entityID;
        }
        
        private System.Collections.IEnumerator LoadGLTF(string path, Action<GameObject> callback, string[] resources)
        {
            TabController tc = Runtime.Parallels.TabsController.GetActiveTab();
            if (tc == null)
            {
                string error = "[EntityAPI->LoadMeshEntity] No tab controller active.";
                Utilities.LogSystem.LogError(error);
                yield return null;
            }
            string uri = tc.GetURL();
            int index = uri.IndexOf("/");
            if (index > 0)
            {
                uri = uri.Substring(0, index);
            }

            Dictionary<string, bool> entityFiles = new Dictionary<string, bool>();
            if (resources != null)
            {
                foreach (string resource in resources)
                {
                    entityFiles.Add(uri + "/" + resource, false);
                    yield return StartCoroutine(Runtime.Parallels.VEMLManager.StartFileRequest(uri + "/" + resource, entityFiles));
                }
            }

            // Wait for all scripts to download.
            float elapsedTime = 0f;
            bool allLoaded = true;
            do
            {
                allLoaded = true;
                foreach (bool file in entityFiles.Values)
                {
                    if (file == false)
                    {
                        allLoaded = false;
                        yield return new WaitForSeconds(0.25f);
                        elapsedTime += 0.25f;
                        break;
                    }
                }
            } while (allLoaded == false && elapsedTime < Runtime.Parallels.VEMLManager.timeout);

            MeshManager.LoadMeshFromGLTF(System.IO.Path.Combine(Runtime.IO.IOManager.CACHEDIRECTORYPATH, uri.Replace(":", "~") + "/" + path), callback);
        }

        /// <summary>
        /// Callback for post-load operations on GLTF model.
        /// </summary>
        /// <param name="model">Reference to the loaded model.</param>
        /// <param name="id">ID of the loaded model.</param>
        /// <param name="parent">Parent entity to apply.</param>
        /// <param name="position">Position to apply to loaded model.</param>
        /// <param name="rotation">Rotation to apply to loaded model.</param>
        /// <param name="scale">Scale to apply to loaded model.</param>
        public static void OnGLTFLoaded(GameObject model, Guid id, BaseEntity parent,
            Vector3 position, Quaternion rotation, Vector3 scale, bool isSize)
        {
            MeshEntity entity = model.AddComponent<MeshEntity>();
            entities.Add(id, entity);
            entity.Initialize(id);

            entity.SetParent(parent);
            entity.SetPosition(position, true);
            entity.SetRotation(rotation, true);
            if (isSize)
            {
                entity.SetSize(scale);
            }
            else
            {
                entity.SetScale(scale);
            }
        }
        
        /// <summary>
        /// Check if entity with given ID exists.
        /// </summary>
        /// <param name="id">ID of entity to check.</param>
        /// <returns>Whether or not entity exists.</returns>
        public static bool Exists(Guid id)
        {
            return entities.ContainsKey(id);
        }
        
        /// <summary>
        /// Find entity with given ID.
        /// </summary>
        /// <param name="id">ID of entity to find.</param>
        /// <returns>Reference to the found entity.</returns>
        public static BaseEntity FindEntity(Guid id)
        {
            foreach (KeyValuePair<Guid, BaseEntity> entity in entities)
            {
                Debug.Log(entity.Key.ToString());
            }
            return entities[id];
        }
        
        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns>Array of entities.</returns>
        public static BaseEntity[] GetAllEntities()
        {
            BaseEntity[] allEntities = new BaseEntity[entities.Count];
            entities.Values.CopyTo(allEntities, 0);
            return allEntities;
        }
    }
}