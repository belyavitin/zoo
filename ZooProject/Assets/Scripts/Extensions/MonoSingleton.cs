using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using GameLogging;

namespace Extensions
{
    public interface IMonoSingleton
    {
        void Touch();
    }
    /// <summary>
    /// Searches for active GameObjects everywhere and for inactive in the scene root
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviourLogger, IMonoSingleton where T : MonoSingleton<T>
    {
        private static T _instance;
        private static bool _isDying = false;
        private static bool _isCreating = false;
        private bool _isInit = false;
        protected virtual bool IsDontDestroyOnLoad { get { return true; } }

        public static bool HasInstance
        {
            get
            {
                return !_isDying && _instance != null;
            }
        }

        protected bool IsCurrentInstance(T obj)
        {
            return _instance == obj;
        }

        public static T Instance
        {
            get
            {
                if (_isDying)
                    return null;
                if (_isCreating)
                    return null;
                if (_instance != null)
                    return _instance;
                _instance = FindInstance() as T;
                if (_instance == null)
                {
                    Debug.LogError("FindInstance for " + typeof(T).Name + " returned null!");
                }
                return _instance;
            }
        }

        protected static T FindInstance()
        {
            string source = "";

            var instance = GameObject.FindObjectOfType<T>();

            if (instance == null)
            {
                _isCreating = true;
                var everythingOfThisType = Resources.FindObjectsOfTypeAll<T>();
                foreach (var found in everythingOfThisType)
                {
                    bool isPersistent = false;
#if UNITY_EDITOR
                    isPersistent = EditorUtility.IsPersistent(found.transform.root.gameObject);
#endif
                    if (isPersistent)
                        continue;
                    if (found.hideFlags == HideFlags.NotEditable)
                        continue;
                    if (found.hideFlags == HideFlags.HideAndDontSave)
                        continue;
                    instance = found;
                    if (instance.gameObject.activeInHierarchy)
                        source = "Found ";
                    else
                        source = "Found (disabled) ";
                }
            }

            if (instance == null)
            {
                var resourcesInstance = Resources.Load(typeof(T).Name) as GameObject;

                GameObject go = null;
                if (resourcesInstance != null)
                {
                    go = Instantiate(resourcesInstance);
                    source = "Instantiated from Resources ";
                }
                else
                {
                    go = new GameObject(typeof(T).Name, typeof(T));
                    source = "Created from scratch ";
                }
                go.name = typeof(T).Name;

                var result = go.GetComponent<T>();

                _isCreating = false;
                instance = result;
            }

            DontDestroyOnLoadIfApplicable(instance);

            if (!instance._isInit)
            {
                instance.Init();
                instance._isInit = true;
            }

            Debug.Log(source + instance.GetInstanceID(), instance);

            return instance;
        }

        protected virtual void Init() { }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;

                if (!_isInit)
                {
                    Init();
                    _isInit = true;
                }
                else
                {
                    LogError("Multithreading error:", nameof(Awake), "but", nameof(_isInit), "is", _isInit, "already when (_instance == null)");
                }
                DontDestroyOnLoadIfApplicable(_instance);
                return;
            }
            else if (ReferenceEquals(_instance, this))
            {
                LogWarning("Awake call while instance", GetInstanceID(), "is already init");
            }
            else
            {
                LogError("Another instance", GetInstanceID(), "of", GetType().Name, "on", gameObject.Hierarchy(), "is trying to replace existing instance", _instance.GetInstanceID(), "on", _instance.gameObject.Hierarchy());
                Destroy(gameObject);
            }
        }

        private static void DontDestroyOnLoadIfApplicable(T instance)
        {
            bool isInEditMode = false;
#if UNITY_EDITOR
            isInEditMode = !EditorApplication.isPlaying;
#endif
            if (instance.IsDontDestroyOnLoad && !isInEditMode)
            {
                if (instance.transform.parent == null)
                    DontDestroyOnLoad(instance.gameObject);
                else
                    Debug.LogError("Can't DontDestroyOnLoad for child MonoSingleton at " + instance.gameObject.Hierarchy(), instance);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _isDying = true;
        }

        protected void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
            else if (_instance == null)
                LogError("OnDestroy for", GetInstanceID(), "while instance is null already");
            else
                LogError("OnDestroy for", GetInstanceID(), "while instance is", _instance.GetInstanceID());
        }

        public void Touch()
        {
            LogDebug("Touched");
        }

        public void Die()
        {
            if (HasInstance)
            {
                LogDebug(nameof(MonoSingleton<T>), typeof(T).Name, "destroy call");
                Destroy(gameObject);
            }
        }
    }
}