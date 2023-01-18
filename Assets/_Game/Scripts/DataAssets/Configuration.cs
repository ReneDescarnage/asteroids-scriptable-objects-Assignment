using System.Runtime.ExceptionServices;
using UnityEngine;
using Variables;

namespace DataAssets {
    [CreateAssetMenu(menuName = "Configuration Files/Configuration",fileName = "Configuration")]
    public class Configuration : ScriptableObject {
        [SerializeField] public ScriptableObject[] Variables;
        [SerializeField] public FloatVariable m_FloatVarthrottlePower;
        [SerializeField] public FloatVariable m_FloatVar_rotationPower;
        [SerializeField] private float _throttlePowerSimple;
        [SerializeField] private float _rotationPowerSimple;



        public ScriptableObject FindSOVariable(string name) {
            for (int i = 0; i < Variables.Length; i++) {
                ScriptableObject obj = Variables[i];
                Debug.Log("Name of variable array member is " + obj.name);
                if (obj.name.Equals(name)) {
                    return Variables[i];
                }
            }
            return null;
        }
    }
}
