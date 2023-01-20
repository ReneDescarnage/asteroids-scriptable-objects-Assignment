using DefaultNamespace.ScriptableEvents;
using DefaultNamespace.Vars;
using UnityEngine;
using Variables;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private ScriptableEventInt _onAsteroidDestroyed;
        [Header("Config:")]
        // [SerializeField] private float _minForce;
        // [SerializeField] private float _maxForce;
        // [SerializeField] private float _minSize;
        // [SerializeField] private float _maxSize;
        // [SerializeField] private float _minTorque;
        // [SerializeField] private float _maxTorque;
        [SerializeField] private FloatVar _minForce;
        [SerializeField] private FloatVar _maxForce;
        [SerializeField] private FloatVar _minSize;
        [SerializeField] private FloatVar _maxSize;
        [SerializeField] private FloatVar _minTorque;
        [SerializeField] private FloatVar _maxTorque;

        [Header("References:")]
        [SerializeField] private Transform _shape;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;
        private int _instanceId;

        private void Awake() {
            
            
        }
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _instanceId = GetInstanceID();
            
            SetDirection();
            AddForce();
            AddTorque();
            SetSize();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (string.Equals(other.tag, "Laser"))
            {
               HitByLaser();
            }
        }

        private void HitByLaser()
        {
            _onAsteroidDestroyed.Raise(_instanceId);
            Destroy(gameObject);
        }

        // TODO Can we move this to a single listener, something like an AsteroidDestroyer?
        public void OnHitByLaser(IntReference asteroidId)
        {
            if (_instanceId == asteroidId.GetValue())
            {
                Destroy(gameObject);
            }
        }
        
        public void OnHitByLaserInt(int asteroidId)
        {
            if (_instanceId == asteroidId)
            {
                Destroy(gameObject);
            }
        }
        
        private void SetDirection()
        {
            var size = new Vector2(3f, 3f);
            var target = new Vector3
            (
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce()
        {
            float force;
            force = _minForce.Value > _maxForce.Value ? Random.Range(_maxForce.Value, _minForce.Value) : 
                                                        Random.Range(_minForce.Value, _maxForce.Value);
            _rigidbody.AddForce( _direction * force, ForceMode2D.Impulse);
        }

        private void AddTorque() {
            float torque;
            torque = _minTorque.Value > _maxTorque.Value ? Random.Range(_maxTorque.Value, _minTorque.Value) : 
                                                           Random.Range(_minTorque.Value, _maxTorque.Value);
            var roll = Random.Range(0, 2);

            if (roll == 0)
                torque = -torque;
            
            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }

        private void SetSize()
        {
            float size;
            size = _minSize.Value > _maxSize.Value ? Random.Range(_maxSize.Value, _minSize.Value) : 
                Random.Range(_minSize.Value, _maxSize.Value);
            _shape.localScale = new Vector3(size, size, 0f);
        }
    }
}
