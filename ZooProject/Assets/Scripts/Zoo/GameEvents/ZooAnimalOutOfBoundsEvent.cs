using UnityEngine;

namespace Zoo
{
    public class ZooAnimalOutOfBoundsEvent : IBusEvent
    {
        public readonly Vector3 WorldPos;

        public ZooAnimalOutOfBoundsEvent(Vector3 worldPos)
        {
            WorldPos = worldPos;
        }
    }
}