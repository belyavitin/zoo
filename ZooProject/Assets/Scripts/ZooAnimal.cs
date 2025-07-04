using GameLogging;
using JGM.MessagingSystem;
using UnityEngine;

namespace Zoo
{
    [RequireComponent(typeof(ZooAnimalView))]
    [DisallowMultipleComponent]
    public class ZooAnimal : MonoBehaviourLogger, IZooAnimal
    {
        // чтобы было интереснее, сделаем чтобы трупы исчезали не сразу
        [SerializeField]
        private float DeathDelaySec = 1;

        private ZooAnimalView view;

        // очевидно что типов будет больше двух
        private EZooAnimalDietaryType DietaryType = EZooAnimalDietaryType.Prey;

        private enum EAnimalState { Dead, Alive };
        private EAnimalState State = EAnimalState.Alive;

        public Vector3 WorldCoords { get { return transform.position; } }

        void Awake()
        {
            view = GetComponent<ZooAnimalView>();
        }

        internal bool TryEat(ZooAnimal touched)
        {
            if (!IsAlive())
                return false;

            // если понадобится разделять трупоедов и вампиров то можно здесь допилить
            if (!touched.IsAlive())
                return false;

            touched.Die();
            MessagingSystem.Instance.Dispatch(new ZooAnimalEatenEvent(WorldCoords));
            return true;
        }

        private void SetState(EAnimalState state)
        {
            this.State = state;
        }

        private bool IsAlive()
        {
            return State != EAnimalState.Dead;
        }

        public bool CanMove()
        {
            return IsAlive();
        }

        private void Die()
        {
            // два раза не умрёшь лол
            if (State == EAnimalState.Dead)
                return;

            view.Die();
            MessagingSystem.Instance.Dispatch(new ZooAnimalDeathEvent(DietaryType));
            SetState(EAnimalState.Dead);
            /// здесь может быть анимация смерти или некромантия какая-нибудь
            GameObject.Destroy(gameObject, DeathDelaySec);
        }

        public void SetDietaryType(EZooAnimalDietaryType updated)
        {
            DietaryType = updated;
        }
    }
}