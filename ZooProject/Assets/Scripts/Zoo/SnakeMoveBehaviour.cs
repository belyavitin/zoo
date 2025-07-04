using UnityEngine;

namespace Zoo
{
    public class SnakeMoveBehaviour : AliveMoveBehaviour
    {
        [SerializeField]
        float slitherPower = 10;
        [SerializeField]
        float slitherFrequency = 1;
        [SerializeField]
        float slitherTorque = 1;

        private void FixedUpdate()
        {
            if (!animal.CanMove())
                return;

            if (!IsInBounds())
                TurnBackInstantly();

            // змеи обычно по синусоиде ползают, сильно заморачиваться не буду
            rigidbody.AddTorque(GetSlitherTorque());
            rigidbody.AddForce(GetSlitherForce());
        }


        private Vector3 GetSlitherForce()
        {
            return transform.rotation * Vector3.forward * slitherPower;
        }

        private Vector3 GetSlitherTorque()
        {
            return new Vector3(0, Mathf.Sin(Time.time * slitherFrequency) * slitherTorque, 0);
        }
    }
}