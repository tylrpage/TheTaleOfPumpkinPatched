using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class FlagManager : MonoBehaviour
    {
        private HashSet<string> _flags = new HashSet<string>();

        public void AddFlag(string flag)
        {
            _flags.Add(flag);
        }

        public bool HasFlag(string flag)
        {
            return _flags.Contains(flag);
        }
    }
}