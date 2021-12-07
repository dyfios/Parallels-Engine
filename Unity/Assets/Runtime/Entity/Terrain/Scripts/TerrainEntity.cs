using System;
using UnityEngine;
using FiveSQD.Parallels.Runtime.Engine.Materials;

namespace FiveSQD.Parallels.Entity
{
    public class TerrainEntity : BaseEntity
    {
        public Terrain terrain;

        public TerrainCollider terrainCollider;

        private GameObject highlightCube;

        public static TerrainEntity Create(float[,] heights)
        {
            GameObject terrainGO = new GameObject("TerrainEntity");
            TerrainEntity terrainEntity = terrainGO.AddComponent<TerrainEntity>();
            Terrain terrain = terrainGO.AddComponent<Terrain>();
            TerrainCollider terrainCollider = terrainGO.AddComponent<TerrainCollider>();
            terrainEntity.terrain = terrain;
            terrainEntity.terrainCollider = terrainCollider;
            terrain.terrainData = new TerrainData();
            terrainCollider.terrainData = terrain.terrainData;
            terrain.terrainData.SetHeights(0, 0, heights);
            return terrainEntity;
        }

        public override bool Delete()
        {
            return base.Delete();
        }

        public override EntityMotion? GetMotion()
        {
            return base.GetMotion();
        }

        public override EntityPhysicalProperties? GetPhysicalProperties()
        {
            return new EntityPhysicalProperties()
            {
                angularDrag = float.PositiveInfinity,
                centerOfMass = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity),
                drag = float.PositiveInfinity,
                gravitational = false,
                mass = float.PositiveInfinity
            };
        }

        public override Vector3 GetSize()
        {
            return terrain.terrainData.size;
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
                    Utilities.LogSystem.LogWarning("[TerrainEntity->SetInteractionState] Placing not valid for terrain.");
                    return false;

                case InteractionState.Static:
                    MakeStatic();
                    return true;

                case InteractionState.Hidden:
                    MakeHidden();
                    return true;

                default:
                    Utilities.LogSystem.LogWarning("[TerrainEntity->SetInteractionState] Interaction state invalid.");
                    return false;
            }
        }

        public override bool SetMotion(EntityMotion? motionToSet)
        {
            return base.SetMotion(motionToSet);
        }

        public override bool SetPhysicalProperties(EntityPhysicalProperties? propertiesToSet)
        {
            return base.SetPhysicalProperties(propertiesToSet);
        }

        public override bool SetSize(Vector3 size)
        {
            terrain.terrainData.size = size;

            return true;
        }

        public override bool SetVisibility(bool visible)
        {
            terrain.drawHeightmap = visible;

            return true;
        }

        public override void Initialize(Guid idToSet)
        {
            base.Initialize(idToSet);

            MakeHidden();
        }

        public override void TearDown()
        {
            base.TearDown();
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

            terrainCollider.enabled = false;
            terrain.enabled = false;
            gameObject.SetActive(false);
            interactionState = InteractionState.Hidden;
        }

        private void MakeStatic()
        {
            switch(interactionState)
            {
                case InteractionState.Hidden:
                    break;

                case InteractionState.Physical:
                    break;

                case InteractionState.Placing:
                    break;

                case InteractionState.Static:
                default:
                    break;
            }

            gameObject.SetActive(true);
            terrain.enabled = true;
            terrainCollider.enabled = false;
        }

        private void MakePhysical()
        {
            switch (interactionState)
            {
                case InteractionState.Hidden:
                    break;

                case InteractionState.Placing:
                    break;

                case InteractionState.Static:
                    break;

                case InteractionState.Physical:
                default:
                    break;
            }

            gameObject.SetActive(true);
            terrain.enabled = true;
            terrainCollider.enabled = true;
        }

        private void SetUpHighlightVolume()
        {
            highlightCube = new GameObject("HighlightVolume");

            Vector3[] vertices =
            {
                    new Vector3(terrain.terrainData.bounds.min.x, terrain.terrainData.bounds.min.y, terrain.terrainData.bounds.min.z),
                    new Vector3 (terrain.terrainData.bounds.max.x, terrain.terrainData.bounds.min.y, terrain.terrainData.bounds.min.z),
                    new Vector3 (terrain.terrainData.bounds.max.x, terrain.terrainData.bounds.max.y, terrain.terrainData.bounds.min.z),
                    new Vector3 (terrain.terrainData.bounds.min.x, terrain.terrainData.bounds.min.y, terrain.terrainData.bounds.min.z),
                    new Vector3 (terrain.terrainData.bounds.min.x, terrain.terrainData.bounds.max.y, terrain.terrainData.bounds.max.z),
                    new Vector3 (terrain.terrainData.bounds.max.x, terrain.terrainData.bounds.max.y, terrain.terrainData.bounds.max.z),
                    new Vector3 (terrain.terrainData.bounds.max.x, terrain.terrainData.bounds.min.y, terrain.terrainData.bounds.max.z),
                    new Vector3 (terrain.terrainData.bounds.min.x, terrain.terrainData.bounds.min.y, terrain.terrainData.bounds.max.z),
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