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

        private IZooAnimalView view;

        // очевидно что типов будет больше двух
        private EZooAnimalDietaryType dietaryType = EZooAnimalDietaryType.Prey;

        private enum EAnimalState { Dead, Alive };
        private EAnimalState state = EAnimalState.Alive;

        public Vector3 WorldCoords { get { return transform.position; } }

        public void SetDietaryType(EZooAnimalDietaryType updated)
        {
            dietaryType = updated;
        }

        public bool CanMove()
        {
            return IsAlive();
        }

        private void Awake()
        {
            view = GetComponent<IZooAnimalView>();
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

        private bool IsAlive()
        {
            return state != EAnimalState.Dead;
        }

        private void Die()
        {
            // два раза не умрёшь лол
            if (state == EAnimalState.Dead)
                return;

            /// здесь может быть анимация смерти или некромантия какая-нибудь

            view.Die();
            GameObject.Destroy(gameObject, DeathDelaySec);
            MessagingSystem.Instance.Dispatch(new ZooAnimalDeathEvent(dietaryType));
            state = EAnimalState.Dead;
        }

    }
}