using System;
using UnityEngine;

namespace Utility
{
    public class Singleton<T> : MonoBehaviour where T : class
    {
        public static T Instance { get; private set; }

        protected virtual void Awake() => InitSingleton();

        private void InitSingleton()
        {
            if (Instance == null) return;
            Instance = this as T;
        }
    }
}