using System;
using System.Collections.Generic;
using Ship;
using UnityEngine;

namespace RuntimeSets
{
    [CreateAssetMenu]
    public class LaserRuntimeSet : ScriptableObject
    {
        private readonly List<Laser> _lasers = new List<Laser>();

        public int Amount => _lasers.Count;
        
        public void Add(Laser laser)
        {
            _lasers.Add(laser);
        }

        public void Remove(Laser laser)
        {
            _lasers.Remove(laser);
        }

        // TODO Which is the best method to clear?
        private void OnEnable()
        {
            _lasers.Clear();
        }

        public void UpdateLasers() {
            foreach (var laser in _lasers) {
                laser.GameUpdate();
            }
        }
    }
}
