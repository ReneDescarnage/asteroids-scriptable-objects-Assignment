using System;
using System.Collections;
using DataAssets;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class AsteroidSpawner : MonoBehaviour {

        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private float _minSpawnTime;
        [SerializeField] private float _maxSpawnTime;
        [SerializeField] private int _minAmount;
        [SerializeField] private int _maxAmount;
        [SerializeField] private EnumVariable SpawnDirections;
        private int[] _allowedSpawnDirections;
        
        private float _timer;
        private float _nextSpawnTime;
        private Camera _camera;

        private enum SpawnLocation
        {
            Top,
            Bottom,
            Left,
            Right
        }

        private void Start()
        {
            _camera = Camera.main;
            GetAllowedDirections();
            Spawn();
            UpdateNextSpawnTime();
        }

        private void Update()
        {
            UpdateTimer();

            if (!ShouldSpawn())
                return;

            Spawn();
            UpdateNextSpawnTime();
            _timer = 0f;
        }

        private void UpdateNextSpawnTime()
        {
            _nextSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
        }

        private void UpdateTimer()
        {
            _timer += Time.deltaTime;
        }

        private bool ShouldSpawn()
        {
            return _timer >= _nextSpawnTime;
        }

        private void Spawn()
        {
            var amount = Random.Range(_minAmount, _maxAmount + 1);
            
            for (var i = 0; i < amount; i++)
            {
                var location = GetSpawnLocation();
                var position = GetStartPosition(location);
                Instantiate(_asteroidPrefab, position, Quaternion.identity);
            }
        }

        private void OnEnable() {
            //ArrayList
        }

        private SpawnLocation GetSpawnLocation()
        {
            var roll = Random.Range(0, _allowedSpawnDirections.Length);
            int result = _allowedSpawnDirections[roll];
            //Debug.Log("GetSpawnLocation roll was " + result);
            return result switch
            {
                1 => SpawnLocation.Bottom,
                2 => SpawnLocation.Left,
                3 => SpawnLocation.Right,
                _ => SpawnLocation.Top
            };
            
        }

        private void GetAllowedDirections() {
            int flagCount = 0;
            for (int i = 0; i < 4; i++) {
                if(SpawnDirections.AllowedDirections.HasFlag((SpawnDirection)(1 << i)))
                    flagCount++;

            }

            _allowedSpawnDirections = new int[flagCount];
            int current = 0;
            for (int i = 0; i < 4; i++) {
                if (SpawnDirections.AllowedDirections.HasFlag((SpawnDirection)(1 << i))) {
                    _allowedSpawnDirections[current] = i;
                    current++;
                }
            }
        }
        private Vector3 GetStartPosition(SpawnLocation spawnLocation)
        {
            var pos = new Vector3 { z = Mathf.Abs(_camera.transform.position.z) };
            
            const float padding = 5f;
            switch (spawnLocation)
            {
                case SpawnLocation.Top:
                    pos.x = Random.Range(0f, Screen.width);
                    pos.y = Screen.height + padding;
                    break;
                case SpawnLocation.Bottom:
                    pos.x = Random.Range(0f, Screen.width);
                    pos.y = 0f - padding;
                    break;
                case SpawnLocation.Left:
                    pos.x = 0f - padding;
                    pos.y = Random.Range(0f, Screen.height);
                    break;
                case SpawnLocation.Right:
                    pos.x = Screen.width - padding;
                    pos.y = Random.Range(0f, Screen.height);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spawnLocation), spawnLocation, null);
            }
            
            return _camera.ScreenToWorldPoint(pos);
        }
    }
}
