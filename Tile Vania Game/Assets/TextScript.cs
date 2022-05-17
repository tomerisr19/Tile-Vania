using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScript : MonoBehaviour
{
    int highScore;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScore.ToString();
        gameSession.OnScoreChange += OnScoreChange;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        gameSession.OnScoreChange -= OnScoreChange;
    }

    private void OnScoreChange(int score)
    {
        if (highScore < score)
        {
            highScoreText.text = score.ToString();
            highScore = score;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
