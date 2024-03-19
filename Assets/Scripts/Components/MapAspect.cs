using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Bomberman
{
    public readonly partial struct MapAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _transform;
        private LocalTransform Transfrom => _transform.ValueRO;

        private readonly RefRO<MapProperties> _mapProperties;

        public int Rows => _mapProperties.ValueRO.Dimensions.x;
        public int Columns => _mapProperties.ValueRO.Dimensions.y;
        public Entity GrassPrefab => _mapProperties.ValueRO.GrassPrefab;

        public LocalTransform GetGrassEntityTransfrom()
        {
            return new LocalTransform
            {
                Position = new float3(0, 0, 0),
                Rotation = quaternion.identity,
                Scale = 1f,
            };
        }
    }
}