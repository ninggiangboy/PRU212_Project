using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static int slimeCount; // Static variable to keep track of defeated slimes

    [SerializeField] public Text slimeCountText; // Reference to the UI Text to display slime count

    // Start is called before the first frame update
    private void Start()
    {
        slimeCountText = GetComponent<Text>();
        ResetScore();
    }

    // Update is called once per frame
    private void Update()
    {
        slimeCountText.text = "Score: " + slimeCount;
    }


    public static void ResetScore()
    {
        slimeCount = 0;
    }
}