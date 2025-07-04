using UnityEngine;

namespace Zoo
{
    public class ZooAnimalEatenEvent : IBusEvent
    {
        public readonly Vector3 PredatorWorldPos;

        public ZooAnimalEatenEvent(Vector3 predatorWorldPos)
        {
            PredatorWorldPos = predatorWorldPos;
        }
    }
}