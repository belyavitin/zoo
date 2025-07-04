using UnityEngine;

namespace Zoo
{
    public class ZooAnimalEatenEvent : IZooAnimalEvent
    {
        public readonly Vector3 PredatorWorldPos;

        public ZooAnimalEatenEvent(Vector3 predatorWorldPos)
        {
            PredatorWorldPos = predatorWorldPos;
        }
    }
}