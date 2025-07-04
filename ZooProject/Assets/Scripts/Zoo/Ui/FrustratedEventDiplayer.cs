using UnityEngine;

namespace Zoo
{
    public class FrustratedEventDiplayer : UiEventDisplayer<ZooAnimalFrustratedEvent>
    {
        public override Vector3 Pos(ZooAnimalFrustratedEvent message)
        {
            return message.WorldPos;
        }
    }
}
