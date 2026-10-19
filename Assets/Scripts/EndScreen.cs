using UnityEngine;
using TMPro;

// Shows the score the player finished the level with.
public class EndScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        finalScoreText.text = "Final Score: " + GameManager.Instance.score;


    }

    public void RestartGame()
    {
        GameManager.Instance.StartGame();
    }
}
