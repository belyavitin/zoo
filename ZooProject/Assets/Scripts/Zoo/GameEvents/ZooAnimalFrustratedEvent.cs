using UnityEngine;

namespace Zoo
{
    public class ZooAnimalFrustratedEvent : IBusEvent
    {
        public readonly Vector3 WorldPos;

        public ZooAnimalFrustratedEvent(Vector3 worldPos)
        {
            WorldPos = worldPos;
        }
    }
}