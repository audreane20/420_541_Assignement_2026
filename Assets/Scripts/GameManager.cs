using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private Coroutine scoreBoostTimer;

    public int ScoreMultiplier { get; private set; } = 1;
    public int score = 0;
    public int health = 3;
    public int levelStartScore = 0;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        ResetLevel();
        
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
        Debug.Log("Health: " + health);

        if(health <= 0)
        {
            GameOver();
        }
    }

    public void SaveLevelStartScore()
    {
        levelStartScore = score;
    }

    public void ResetLevel()
    {
        score = levelStartScore;
        health = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ClearScoreBoost();
    }

    public void LoadNextLevel()
    {
        SaveLevelStartScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        score = 0;
        health = 3;
        levelStartScore = 0;
        SceneManager.LoadScene("Level1");
        ClearScoreBoost();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void ActivateScoreBoost()
    {
        if (scoreBoostTimer != null)
        {
            StopCoroutine(scoreBoostTimer);
        }
        scoreBoostTimer = StartCoroutine(ScoreBoostCountdown());
    }

    private IEnumerator ScoreBoostCountdown()
    {
        ScoreMultiplier = 2;
        yield return new WaitForSeconds(10f);
        ScoreMultiplier = 1;
        scoreBoostTimer = null;
    }


    private void ClearScoreBoost()
    {
        if(scoreBoostTimer != null)
        {
            StopCoroutine(scoreBoostTimer);
            scoreBoostTimer = null;
        }

        ScoreMultiplier = 1;
    }

}
