using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;

namespace Bomberman
{
    public class MapMono : MonoBehaviour
    {
        public int2 Dimensions;
        public GameObject GrassPrefab;
    }

    public class MapBaker : Baker<MapMono>
    {
        public override void Bake(MapMono authoring)
        {
            Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            AddComponent(entity, new MapProperties
            {
                Dimensions = authoring.Dimensions,
                GrassPrefab = entity
            });
        }
    }
}
