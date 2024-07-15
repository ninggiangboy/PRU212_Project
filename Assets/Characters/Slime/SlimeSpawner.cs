using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.Slime
{
    public class SlimeSpawner : MonoBehaviour
    {
        [SerializeField] private float minimumSpawnTime;
        [SerializeField] private float maximumSpawnTime;
        [SerializeField] private int maxSlimes;
        private int _currentSlimeCount;
        private Transform _playerTransform;
        private SlimePool _slimePool;
        public LayerMask obstacleLayer;
        private float _timeUntilSpawn;

        private void Awake()
        {
            SetTimeUntilSpawn();
        }

        private void Start()
        {
            _slimePool = FindObjectOfType<SlimePool>();
            _playerTransform = GameObject.Find("Player").transform; // Initialize playerTransform
            AddSlime();
        }

        private void Update()
        {
            _timeUntilSpawn -= Time.deltaTime;

            if (_timeUntilSpawn < 0 &&
                _currentSlimeCount < maxSlimes &&
                IsPlayerNear()) // Check if current slime count is less than max limit
            {
                AddSlime();
            }
        }

        public void AddSlime() {
            var slime = _slimePool.GetSlime();
            if (slime)
            {
                slime.transform.position = transform.position;
                _currentSlimeCount++; // Increment current slime count
                SetTimeUntilSpawn();
            }
        }

        private bool IsPlayerNear()
        {
            Vector2 direction = _playerTransform.position - transform.position;
            direction.Normalize(); // Normalize the direction vector to have a magnitude of 1

            // Check if there is an obstacle between slime and player
            var hit = Physics2D.Raycast(transform.position, direction,
                Vector2.Distance(transform.position, _playerTransform.position), obstacleLayer);
            return hit.collider == null;
        }

        private void SetTimeUntilSpawn()
        {
            _timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
        }
    }
}