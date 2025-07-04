using UnityEngine;
using UnityEngine.UI;

namespace Zoo
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public class SimpleEventText : MonoBehaviour
    {
        [SerializeField]
        float timeout = 0.5f;

        void Awake()
        {
            Destroy(gameObject, timeout);
        }

    }
}
