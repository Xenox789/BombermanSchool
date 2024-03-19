using Unity.Entities;
using Unity.Mathematics;

namespace Bomberman
{
    public struct MapProperties : IComponentData
    {
        public int2 Dimensions;
        public Entity GrassPrefab;
    }
}