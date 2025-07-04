using UnityEngine;

namespace Zoo
{
    public class OutOfBoundsEventDisplayer : UiEventDisplayer<ZooAnimalOutOfBoundsEvent>
    {
        public override Vector3 Pos(ZooAnimalOutOfBoundsEvent message)
        {
            return message.WorldPos;
        }
    }
}
