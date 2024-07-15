using TMPro;
using UnityEngine;

namespace UI.Coin
{
    public class CoinUIScript : MonoBehaviour
    {
        public TMP_Text coinText;
        public int coinCount;

        // Start is called before the first frame update
        private void Start()
        {
            coinCount = 0;
            ShowCoinCount();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void IncreaseCoinCount()
        {
            coinCount++;
            ShowCoinCount();
        }

        private void ShowCoinCount()
        {
            coinText.text = "x " + coinCount;
        }
    }
}