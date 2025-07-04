using UnityEngine;
using GameLogging;

namespace Zoo
{
    public abstract class AnimalBehaviour : MonoBehaviourLogger
    {
        protected new Rigidbody rigidbody;
        protected ZooAnimal animal;

        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animal = GetComponent<ZooAnimal>();
        }
    }
}