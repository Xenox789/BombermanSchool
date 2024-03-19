using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Bomberman
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CreateWorldSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MapProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var mapEntity = SystemAPI.GetSingletonEntity<MapProperties>();
            var map = SystemAPI.GetAspect<MapAspect>(mapEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            for (int i = 0; i < map.Rows; i++)
            {
                for (int j = 0; j < map.Columns; j++)
                {
                    var newGrass = ecb.Instantiate(map.GrassPrefab);
                    var newGrassTransform = map.GetGrassEntityTransfrom();
                    ecb.SetComponent(newGrass, new LocalToWorld { Value = newGrassTransform.ToMatrix() });
                }
            }

            ecb.Playback(state.EntityManager);
        }
    }
}