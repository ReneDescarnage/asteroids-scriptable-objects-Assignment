using System.Collections.Generic;
using Asteroids;
using UnityEditor.UIElements;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "new EnumVariable", menuName = "ScriptableObjects/Variables/EnumVariable")]
    public class EnumVariable : ScriptableObject {
        [Tooltip("")]
        public SpawnDirection AllowedDirections;
        private bool[] validFlags;
        
        



    }
}
