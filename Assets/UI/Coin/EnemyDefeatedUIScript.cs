using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDefeatedUIScript : MonoBehaviour
{

    public TMP_Text coinText;

    internal void ShowEnemyDefeatedCount(int totaEnemy)
    {
        coinText.text = "x " + totaEnemy;
    }

    private void Start()
    {
        ShowEnemyDefeatedCount(0);
    }
}
