using System.Runtime.ExceptionServices;
using UnityEngine;
using Variables;

namespace DataAssets {
    [CreateAssetMenu(menuName = "Configuration Files/Configuration",fileName = "Configuration")]
    public class Configuration : ScriptableObject {
        [SerializeField] public ScriptableObject[] Variables;
    

        public ScriptableObject FindSOVariable(string variableName) {
            for (int i = 0; i < Variables.Length; i++) {
                ScriptableObject obj = Variables[i];
                if (obj.name.Equals(variableName)) {
                    return Variables[i];
                }
            }
            return null;
        }
    }
}
