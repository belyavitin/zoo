using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Zoo
{
    public class ToadJumpBehaviour : AliveMoveBehaviour
    {
        [SerializeField, Range(1, 10)]
        private float timeBetweenJumps;

        [SerializeField, Range(1, 10)]
        private float timeBetweenJumpsRandomAdd = 1;

        [SerializeField, Range(1, 100)]
        private float jumpPower = 5;

        [SerializeField, Range(-45, 45)]
        private float jumpAngle = 5;

        [SerializeField, Range(1, 180)]
        private float jumpDirectionRandom = 90;

        private float timeToJump;
        private float direction;
        private bool isOutOfBounds;

        protected override void Awake()
        {
            base.Awake();
            timeToJump = Time.time;
        }

        protected override void Update()
        {
            base.Update();

            if (!animal.CanMove())
                return;

            isOutOfBounds = !boundsChecker.IsInBounds(AnimalPosition);

            if (timeToJump > Time.time)
                return;

            timeToJump = Time.time + timeBetweenJumps + Random.Range(0, timeBetweenJumpsRandomAdd);

            Jump();
        }

        private void Jump()
        {
            direction = transform.rotation.eulerAngles.y + Random.Range(-this.jumpDirectionRandom, this.jumpDirectionRandom);
            direction = ValidateDirectionWithinBounds(direction);

            Quaternion lookDirection = Quaternion.Euler(0, direction, 0);
            Quaternion jumpDirection = Quaternion.Euler(jumpAngle, direction, 0);

            var jumpForce = jumpDirection * Vector3.forward * jumpPower;

            rigidbody.AddForce(jumpForce, ForceMode.Impulse);

            rigidbody.MoveRotation(lookDirection);
        }
    }
}