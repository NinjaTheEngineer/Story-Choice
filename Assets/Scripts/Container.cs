using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaTools {
    public abstract class Container<T> : NinjaMonoBehaviour {
        [SerializeField]
        private int _capacity;
        public int Capacity => _capacity;
        protected List<T> _objects;
        public List<T> GameObjects => _objects;

    }
}
