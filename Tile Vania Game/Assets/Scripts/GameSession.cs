using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour, IGameSession
{
    private const string SCORE = "Score";
    int playerLives = 3;
    int score;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    public event Action<int> OnScoreChange;


    //private void Awake()
    //{
    //    int numGameSessions = FindObjectsOfType<GameSession>().Length; 
    //    if (numGameSessions > 1)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

   

    private void Start()
    {
        livesText.text = PlayerPrefs.GetInt("Life", 0).ToString();
        scoreText.text = PlayerPrefs.GetInt(SCORE, 0).ToString();      
    }



    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
        if (OnScoreChange != null)
        {
            OnScoreChange.Invoke(score);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    private void OnDestroy()
    {
        //var highScore = PlayerPrefs.GetInt("HighScore", 0);
        //highScore = Math.Max(score, highScore);
        PlayerPrefs.SetInt(SCORE, score);
        PlayerPrefs.SetInt("Life", playerLives);
    }

    public void AddScore(int pointsToAdd)
    {
        throw new NotImplementedException();
    }
}

internal interface IGameSession
{
    event Action<int> OnScoreChange;
    void AddScore(int pointsToAdd);

}