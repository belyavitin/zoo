using Extensions;
using JGM.MessagingSystem;
using UnityEngine;

namespace Zoo
{
    [DisallowMultipleComponent]
    public class UiEventsDisplayer : MonoSingleton<UiEventsDisplayer>,
         IMessagingSubscriber<ZooAnimalEatenEvent>,
         IMessagingSubscriber<ZooAnimalFrustratedEvent>,
         IMessagingSubscriber<ZooAnimalOutOfBoundsEvent>
    {
        //[Inject]
        [SerializeField]
        Camera gameCamera;

        //[Inject]
        [SerializeField]
        Canvas uiCanvas;

        //[Inject]
        MessagingSystem msg;
        MessagingSystem Msg { get { if (msg == null) msg = MessagingSystem.Instance; return msg; } }

        [SerializeField]
        SimpleEventText tastyTextPrefab;

        [SerializeField]
        SimpleEventText outOfBoundsTextPrefab;

        [SerializeField]
        SimpleEventText frustratedTextPrefab;

        void OnEnable()
        {
            Msg.Subscribe<ZooAnimalEatenEvent>(this);
            Msg.Subscribe<ZooAnimalOutOfBoundsEvent>(this);
            Msg.Subscribe<ZooAnimalFrustratedEvent>(this);
        }

        void OnDisable()
        {
            Msg.Unsubscribe<ZooAnimalEatenEvent>(this);
            Msg.Unsubscribe<ZooAnimalOutOfBoundsEvent>(this);
            Msg.Unsubscribe<ZooAnimalFrustratedEvent>(this);
        }

        public void OnReceiveMessage(ZooAnimalEatenEvent message)
        {
            LogDebug(message);
            ShowSimpleEventText(message.PredatorWorldPos, tastyTextPrefab);
        }

        public void OnReceiveMessage(ZooAnimalFrustratedEvent message)
        {
            LogDebug(message);
            ShowSimpleEventText(message.WorldPos, frustratedTextPrefab);
        }

        public void OnReceiveMessage(ZooAnimalOutOfBoundsEvent message)
        {
            LogDebug(message);
            ShowSimpleEventText(message.WorldPos, outOfBoundsTextPrefab);
        }

        private Vector3 EventPosition(Vector3 predatorWorldPos)
        {
            var pos = gameCamera.WorldToScreenPoint(predatorWorldPos);
            pos.z = 0;
            return pos;
        }

        private void ShowSimpleEventText(Vector3 worldPos, SimpleEventText tastyTextPrefab)
        {
            Instantiate(tastyTextPrefab, EventPosition(worldPos), Quaternion.identity, uiCanvas.transform);
        }
    }
}
