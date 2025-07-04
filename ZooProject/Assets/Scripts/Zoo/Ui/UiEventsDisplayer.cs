using Extensions;
using JGM.MessagingSystem;
using UnityEngine;

namespace Zoo
{
    public abstract class UiEventDisplayer<T> : MonoBehaviourEventReceiver,
         IMessagingSubscriber<T> where T : IZooAnimalEvent
    {
        //[Inject]
        [SerializeField]
        Camera gameCamera;

        //[Inject]
        [SerializeField]
        Canvas uiCanvas;

        [SerializeField]
        SimpleEventText eventTextPrefab;

        void OnEnable()
        {
            Msg.Subscribe<T>(this);
        }

        void OnDisable()
        {
            Msg.Unsubscribe<T>(this);
        }

        public abstract Vector3 Pos(T message);

        public void OnReceiveMessage(T message)
        {
            LogDebug(message);
            ShowSimpleEventText(Pos(message), eventTextPrefab);
        }

        protected Vector3 EventPosition(Vector3 worldPos)
        {
            var pos = gameCamera.WorldToScreenPoint(worldPos);
            pos.z = 0;
            return pos;
        }

        private void ShowSimpleEventText(Vector3 worldPos, SimpleEventText tastyTextPrefab)
        {
            Instantiate(tastyTextPrefab, EventPosition(worldPos), Quaternion.identity, uiCanvas.transform);
        }
    }
}
