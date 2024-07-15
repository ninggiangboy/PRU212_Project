using UI.Coin;
using UnityEngine;

namespace Map.Token
{
    public class TokenScript : MonoBehaviour
    {
        private CoinUIScript _coinUIScript;

        // Start is called before the first frame update
        private void Start()
        {
            _coinUIScript = GameObject.Find("CoinScoreUI").GetComponent<CoinUIScript>();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        // when collision with player
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                _coinUIScript.IncreaseCoinCount();
            }
            // destroy token
            // Destroy(gameObject);
        }
    }
}