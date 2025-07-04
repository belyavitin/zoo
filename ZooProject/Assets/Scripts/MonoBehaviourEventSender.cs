using GameLogging;

public class MonoBehaviourEventSender : MonoBehaviourLogger
{
    protected virtual void Awake()
    {
        BindToBus();
    }

    // тут может быть что-то посложнее но не сегодня
    private void BindToBus()
    {
    }

    protected void SendEvent(IBusEvent e)
    {

    }
}
