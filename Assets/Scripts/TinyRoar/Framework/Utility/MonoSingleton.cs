﻿using UnityEngine;

namespace TinyRoar.Framework
{

    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {

        private static T _instance;
        private static bool applicationIsQuitting = false;

        // get / set Instance
        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                    return null;

                if (_instance == null)
                    _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "Create singleton: " + typeof(T).ToString();
                    Debug.LogWarning(singleton.name + " created");
                }

                return _instance;
            }
            protected set
            {
                _instance = value;
            }
        }

        // initialization
        public virtual void Awake () {
	        if (_instance == null)
	        {
                _instance = this as T;
            }
            applicationIsQuitting = false;
        }

        public virtual void OnDestroy()
        {
            applicationIsQuitting = true;
        }

    }

}