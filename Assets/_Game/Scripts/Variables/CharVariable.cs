using UnityEngine;

namespace Variables {
    [CreateAssetMenu(fileName = "new CharVariable", menuName = "ScriptableObjects/Variables/CharVariable")]

    public class CharVariable : ScriptableObject
    {
        [SerializeField] private char _value;

        public char Value => _value;
    }
}
