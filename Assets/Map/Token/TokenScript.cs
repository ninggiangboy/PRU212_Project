using UI.Coin;
using UnityEngine;

namespace Map.Token
{
    public class TokenScript : MonoBehaviour
    {
        private GamePlayScript _gamePlay;

        // Start is called before the first frame update
        private void Start()
        {
            _gamePlay = FindObjectOfType<GamePlayScript>();
        }

        // when collision with player
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                _gamePlay.AddCoin();
            }
        }
    }
}