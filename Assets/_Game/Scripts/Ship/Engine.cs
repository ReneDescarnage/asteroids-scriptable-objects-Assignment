using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Variables;

namespace Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Engine : MonoBehaviour
    {
        [SerializeField] private FloatVariable _throttlePower;
        [SerializeField] private FloatVariable _rotationPower;
        [SerializeField] private CharVariable[] _commandKeys;

        // private Dictionary<KeyCode, char > chartoKeycode = new Dictionary<KeyCode, char>() {
        //     //-------------------------LOGICAL mappings-------------------------
        //
        //     //Lower Case Letters
        //     {KeyCode.A,'a' },
        //     {KeyCode.B, 'b' },
        //     {KeyCode.C, 'c' },
        //     {KeyCode.D, 'd' },
        //     {KeyCode.E,'e' },
        //     {KeyCode.F,'f' },
        //     {KeyCode.G,'g' },
        //     {KeyCode.H,'h' },
        //     {KeyCode.I,'i' },
        //     {KeyCode.J },
        //     {KeyCode.K },
        //     {KeyCode.L },
        //     {KeyCode.M },
        //     { 'n', KeyCode.N },
        //     { 'o', KeyCode.O },
        //     { 'p', KeyCode.P },
        //     { 'q', KeyCode.Q },
        //     { 'r', KeyCode.R },
        //     { 's', KeyCode.S },
        //     { 't', KeyCode.T },
        //     { 'u', KeyCode.U },
        //     { 'v', KeyCode.V },
        //     { 'w', KeyCode.W },
        //     { 'x', KeyCode.X },
        //     { 'y', KeyCode.Y },
        //     { 'z', KeyCode.Z }
        // };
        private Rigidbody2D _rigidbody;
        
        private void FixedUpdate()
        {
           
                
            if (Input.GetKey(char.ToLowerInvariant(_commandKeys[0].Value).ToString()))
            {
                Throttle();
            }
        
            if (Input.GetKey(_commandKeys[1].Value.ToString()))
            {
                SteerLeft();
            } 
            else if (Input.GetKey(_commandKeys[2].Value.ToString()))
            {
                SteerRight();
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            if (_commandKeys.Length != 3) {
                Debug.Log("Incorrect number of keys");
            }
        }
    
        public void Throttle()
        {
            _rigidbody.AddForce(transform.up * _throttlePower.Value, ForceMode2D.Force);
        }

        public void SteerLeft()
        {
            _rigidbody.AddTorque(_rotationPower.Value, ForceMode2D.Force);
        }

        public void SteerRight()
        {
            _rigidbody.AddTorque(-_rotationPower.Value, ForceMode2D.Force);
        }
    }
    
    
}
