using System.Collections.Generic;
using UnityEngine;

namespace Characters.Slime
{
    public class SlimePool : MonoBehaviour
    {
        [SerializeField] private GameObject slimePrefab;
        [SerializeField] private int initialPoolSize = 10;

        private readonly Queue<GameObject> _pool = new();

        private void Awake()
        {
            for (var i = 0; i < initialPoolSize; i++)
            {
                var slime = Instantiate(slimePrefab);
                slime.SetActive(false);
                _pool.Enqueue(slime);
            }
        }

        public GameObject GetSlime()
        {
            if (_pool.Count > 0)
            {
                var slime = _pool.Dequeue();
                slime.SetActive(true);
                return slime;
            }
            else
            {
                var slime = Instantiate(slimePrefab);
                return slime;
            }
        }

        public void ReturnSlime(GameObject slime)
        {
            slime.SetActive(false);
            _pool.Enqueue(slime);
        }
    }
}