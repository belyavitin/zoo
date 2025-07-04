using UnityEngine;

namespace Zoo
{
    ///<seealso cref="SnakeMoveBehaviour"/> <seealso cref="ToadJumpBehaviour"/>

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ZooAnimal))]
    [DisallowMultipleComponent]
    public class BoundedMoveBehaviour : MonoBehaviourEventSender, IZooBehaviour
    {
        //[Inject]
        protected IBoundsChecker boundsChecker;

        protected new Rigidbody rigidbody;
        protected ZooAnimal animal;

        protected Vector3 AnimalPosition => transform.position;
        protected float AnimalDirection => transform.rotation.y;

        private bool wasOutOfBounds = false;

        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animal = GetComponent<ZooAnimal>();

            // предположим что наличие класса для проверки границ здесь обязательно, свяжу через синглтон пока
            boundsChecker = BoundsControllerByCamera.Instance;
        }

        protected virtual void Start() { }

        protected virtual void Update()
        {
            bool isOutOfBounds = !IsInBounds();
            if (isOutOfBounds && !wasOutOfBounds)
                Msg.Dispatch(new ZooAnimalOutOfBoundsEvent(AnimalPosition));
            wasOutOfBounds = isOutOfBounds;
        }

        protected float ValidateDirectionWithinBounds(float direction)
        {
            if (!IsInBounds())
                return ReturnDirection();

            return direction;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            var animal = collision.collider.GetComponent<ZooAnimal>();
            if (animal == null)
            {
                Vector3 commonNormal = Vector3.zero;
                for (int i = 0; i < collision.contactCount; i++)
                {
                    var contact = collision.GetContact(i);
                    commonNormal += contact.normal;
                }
                LogDebug(nameof(OnCollisionEnter), gameObject.name, "with", collision.collider.gameObject.name, "with", collision.contactCount, collision.contactCount > 1 ? "contacts" : "contact", commonNormal);
            }
        }

        protected float ReturnDirection()
        {
            return Quaternion.LookRotation(boundsChecker.ReturnDirection(AnimalPosition), Vector3.up).eulerAngles.y;
        }

        protected void TurnBackInstantly()
        {
            var dir = ReturnDirection();
            var wantedRotation = Quaternion.Euler(0, dir, 0);
            rigidbody.MoveRotation(wantedRotation);
        }

        protected bool IsInBounds()
        {
            return boundsChecker.IsInBounds(AnimalPosition);
        }
    }
}