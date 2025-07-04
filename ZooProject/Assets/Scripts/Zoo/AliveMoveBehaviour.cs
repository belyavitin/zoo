using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Zoo
{
    // в теории животные могут оказаться на стенке - спавн, запрыгивание или выталкивание,
    // поэтому для того чтобы они не упирались в стенку мы не будем использовать коллизию
    // а будем использовать "фрустрацию" - чем дольше животное стоит на месте
    // тем больше вероятность что оно захочет развернуться в другую сторону
    // да и вообще, фрустрация от затраченных впустую усилий - это свойство большинства животных
    // не говоря уж про людей :)
    public class AliveMoveBehaviour : BoundedMoveBehaviour
    {
        [SerializeField]
        private float frustrationFadePerDistance = 1;
        [SerializeField]
        private float frustrationAddPerSecond = 1;
        [SerializeField]
        private float frustrationThreshold = 5;
        [SerializeField, Range(0, 180)]
        private float frustrationDirectionRandomity = 90;
        [SerializeField]
        private float standFrustration;

        private Vector3? lastPosition;

        protected override void Update()
        {
            base.Update();

            if (animal.CanMove())
                UpdateFrustration();
        }

        private void FrustratedChangeDirection()
        {
            Msg.Dispatch(new ZooAnimalFrustratedEvent(AnimalPosition));
            var e = transform.rotation.eulerAngles;
            rigidbody.MoveRotation(Quaternion.Euler(e.x, 180 - e.y + Random.Range(-frustrationDirectionRandomity, frustrationDirectionRandomity), e.z));
        }

        private void UpdateFrustration()
        {
            if (!lastPosition.HasValue)
                lastPosition = AnimalPosition;

            Vector3 delta = AnimalPosition - lastPosition.Value;
            delta.y = 0;
            standFrustration -= delta.magnitude * frustrationFadePerDistance;
            standFrustration += Time.deltaTime * frustrationAddPerSecond;

            lastPosition = AnimalPosition;

            if (standFrustration > frustrationThreshold)
            {
                standFrustration -= frustrationThreshold;
                FrustratedChangeDirection();
            }
            else if (standFrustration < 0)
            {
                standFrustration = 0;
            }
        }
    }
}