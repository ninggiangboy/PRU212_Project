using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static int slimeCount = 0; // Static variable to keep track of defeated slimes
    [SerializeField]
    public Text slimeCountText; // Reference to the UI Text to display slime count
    // Start is called before the first frame update
    void Start()
    {
        slimeCountText = GetComponent<Text>();
        ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        slimeCountText.text = "Score: " + slimeCount;
    }


    public static void ResetScore()
    {
        slimeCount = 0; 
    }
}
