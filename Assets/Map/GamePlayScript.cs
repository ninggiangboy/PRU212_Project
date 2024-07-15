using System;
using System.Collections.Generic;
using UI.Coin;
using UnityEngine;

namespace Map
{
    public class GamePlayScript : MonoBehaviour
    {
        public int TotalRooms { get; set; }
        public HashSet<RoomController> RoomsEntered { get; set; } = new();
        private CoinUIScript _coinUIScript;
        private EnemyDefeatedUIScript _enemyDefeatedUIScript;

        public Canvas WinCanvas;
        public Canvas LoseCanvas;
        private int _totaCoins;
        private int _totaEnemy;

        void Start()
        {
            _coinUIScript = GameObject.Find("CoinScoreUI").GetComponent<CoinUIScript>();
            _totaCoins = 0;
            _totaEnemy  = 0;
            _enemyDefeatedUIScript = GameObject.Find("EnemyDefeatedUI").GetComponent<EnemyDefeatedUIScript>();
        }

        public void WinGame()
        {
            Time.timeScale = 0;
            WinCanvas.gameObject.SetActive(true);
            var tokenScoreText = GameObject.Find("TotalEnemyScore").GetComponent<TMPro.TMP_Text>();
            tokenScoreText.text = "x " + _totaCoins;
            var slimeDefeatedText = GameObject.Find("TotalEnemyScore").GetComponent<TMPro.TMP_Text>();
            slimeDefeatedText.text = "x " + _totaEnemy;
        }

        public void LoseGame()
        {
            Time.timeScale = 0;
            LoseCanvas.gameObject.SetActive(true);
        }

        public void AddCoin()
        {
            _totaCoins++;
            _coinUIScript.ShowCoinCount(_totaCoins);
        }

        public void AddEnemyDefeated()
        {
            _totaEnemy++;
            _enemyDefeatedUIScript.ShowEnemyDefeatedCount(_totaEnemy);
        }
    }
}