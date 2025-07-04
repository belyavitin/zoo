using UnityEngine;

namespace Zoo
{
    public class ZooAnimalFrustratedEvent : IZooAnimalEvent
    {
        public readonly Vector3 WorldPos;

        public ZooAnimalFrustratedEvent(Vector3 worldPos)
        {
            WorldPos = worldPos;
        }
    }
}