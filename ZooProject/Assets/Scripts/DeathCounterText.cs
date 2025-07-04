using JGM.MessagingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Zoo
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public class DeathCounterText : MonoBehaviourEventReceiver, IMessagingSubscriber<ZooAnimalDeathEvent>
    {
        private Text text;

        [SerializeField]
        uint count = 0;

        [SerializeField]
        EZooAnimalDietaryType DietaryType = EZooAnimalDietaryType.Prey;

        [SerializeField]
        string format = "{0}: {1}";

        void OnEnable()
        {
            Msg.Subscribe<ZooAnimalDeathEvent>(this);
        }

        void OnDisable()
        {
            Msg.Unsubscribe<ZooAnimalDeathEvent>(this);
        }

        public void OnReceiveMessage(ZooAnimalDeathEvent message)
        {
            if (message.Dietary == DietaryType)
            {
                ++count;
                SetText();
            }
        }

        private void SetText()
        {
            text.text = string.Format(format, DietaryType, count);
        }

        void Awake()
        {
            text = GetComponent<Text>();
            SetText();
        }
    }
}