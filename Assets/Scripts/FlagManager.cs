using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class FlagManager : MonoBehaviour
    {
        [SerializeField] private List<string> initialFlags;
        
        private HashSet<string> _flags = new HashSet<string>();

        private void Awake()
        {
            foreach (string flag in initialFlags)
            {
                _flags.Add(flag);
            }
        }

        public void AddFlag(string flag)
        {
            _flags.Add(flag);
        }

        public void RemoveFlag(string flag)
        {
            if (_flags.Contains(flag))
            {
                _flags.Remove(flag);
            }
        }

        public bool HasFlag(string flag)
        {
            return _flags.Contains(flag);
        }
    }
}