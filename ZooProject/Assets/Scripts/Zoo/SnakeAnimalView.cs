using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Zoo
{
    /// <summary>
    /// предположу что никаких сложностей не нужно в конкретных реализациях
    /// </summary>
    public class SnakeAnimalView : ZooAnimalView
    {
        [SerializeField]
        Transform tongue;

        [SerializeField]
        float tongueSpeed = 3;

        [SerializeField]
        float tongueSpeed2 = 30;

        float tongueFlickRandomOffset = 0;

        bool isMovingTongue = true;

        protected override void Awake()
        {
            base.Awake();
            tongueFlickRandomOffset = Random.Range(0, (float)Math.PI * 2.0f);
        }

        void Update()
        {
            TransformTongue();
        }

        private void TransformTongue()
        {
            if (isMovingTongue)
                tongue.localScale = TongueScale();
            else
                tongue.localScale = Vector3.one;
        }

        private Vector3 TongueScale()
        {
            float t = Time.time + tongueFlickRandomOffset;

            float move = (1 + (float)Math.Sin(t * tongueSpeed)) * (1 + (float)Math.Sin(t * tongueSpeed2)) * 0.25f;
            return new Vector3(1, 1, move);
        }

        public override void Die()
        {
            isMovingTongue = false;
            base.Die();
        }
    }
}