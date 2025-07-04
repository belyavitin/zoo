using UnityEngine;

namespace Zoo
{
    public class OutOfBoundsEventDisplayer : UiEventDisplayer<ZooAnimalFrustratedEvent>
    {
        public override Vector3 Pos(ZooAnimalFrustratedEvent message)
        {
            return message.WorldPos;
        }
    }
}
