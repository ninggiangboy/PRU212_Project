using TMPro;
using UnityEngine;

namespace UI.Coin
{
    public class CoinUIScript : MonoBehaviour
    {
        public TMP_Text coinText;

        // Start is called before the first frame update
        private void Start()
        {
            ShowCoinCount(0);
        }

        public void ShowCoinCount(int coinCount)
        {
            coinText.text = "x " + coinCount;
        }
    }
}