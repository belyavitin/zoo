using UnityEngine;

namespace Zoo
{
    public class TastyEventDisplayer : UiEventDisplayer<ZooAnimalEatenEvent>
    {
        public override Vector3 Pos(ZooAnimalEatenEvent message)
        {
            return message.PredatorWorldPos;
        }
    }
}
