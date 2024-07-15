using Characters.Player;
using UnityEngine;

namespace Map.Token
{
    public class HealthTokenScript : MonoBehaviour
    {
        private PlayerController _playerController;

        // Start is called before the first frame update
        private void Start()
        {
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                _playerController.IncreaseHealth(30);
            }
            // destroy token
            // Destroy(gameObject);
        }
    }
}