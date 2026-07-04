using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    private int totalScore = 0; 
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI timerText;
    private float timeRemaining = 60f;
    private bool isGameActive = true;



    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        scoreText.text = $"Score: {totalScore}";
        StartCoroutine(CountdownTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int score)
    {
        totalScore += score;
        scoreText.text = $"Score: {totalScore}";
    }
    private IEnumerator CountdownTimer()
    {
        while (timeRemaining > 0)
        {
            timerText.text = $"Time: {Mathf.CeilToInt(timeRemaining)}"; 
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        EndGame(); 
    }

    private void EndGame()
    {
        isGameActive = false;

        List<int> highScores = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey($"HighScore{i}"))
            {
                highScores.Add(PlayerPrefs.GetInt($"HighScore{i}"));
            }
        }

        highScores.Add(totalScore);

        
        highScores.Sort((a, b) => b.CompareTo(a));
        while (highScores.Count > 5)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt($"HighScore{i}", highScores[i]);
        }
        PlayerPrefs.Save();

        SceneManager.LoadScene("start");
    }
}
