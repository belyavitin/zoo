using JGM.MessagingSystem;
using UnityEngine;

namespace Zoo
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ZooAnimal))]
    [DisallowMultipleComponent]
    public class PredatorBehavior : AnimalBehaviour, IZooBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            animal.SetDietaryType(EZooAnimalDietaryType.Predator);
        }
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.rigidbody == null)
                return;

            var touched = collision.rigidbody.GetComponent<ZooAnimal>();
            if (touched != null)
                if (animal.TryEat(touched))
                    MessagingSystem.Instance.Dispatch(new ZooAnimalEatenEvent(animal.WorldCoords));
        }
    }
}