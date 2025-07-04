using GameLogging;
using JGM.MessagingSystem;

namespace Zoo
{
    public class MonoBehaviourEventReceiver : MonoBehaviourLogger
    {
        //[Inject]
        MessagingSystem msg;
        protected MessagingSystem Msg { get { if (msg == null) msg = MessagingSystem.Instance; return msg; } }
    }
}