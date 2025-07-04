using System;
using Extensions;
using JGM.MessagingSystem;
using UnityEngine;

namespace Zoo
{
    [DisallowMultipleComponent]
    public class UiEventsDisplayer : MonoSingleton<UiEventsDisplayer>,
         IMessagingSubscriber<ZooAnimalEatenEvent>
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
        TastyText tastyTextPrefab;

        void OnEnable()
        {
            Msg.Subscribe<ZooAnimalEatenEvent>(this);
        }

        void OnDisable()
        {
            Msg.Unsubscribe<ZooAnimalEatenEvent>(this);
        }

        public void OnReceiveMessage(ZooAnimalEatenEvent message)
        {
            LogDebug(message);
            var tasty = Instantiate(tastyTextPrefab, TastyPosition(message.PredatorWorldPos), Quaternion.identity, uiCanvas.transform);
        }

        private Vector3 TastyPosition(Vector3 predatorWorldPos)
        {
            var pos = gameCamera.WorldToScreenPoint(predatorWorldPos);
            pos.z = 0;
            return pos;
        }
    }
}
