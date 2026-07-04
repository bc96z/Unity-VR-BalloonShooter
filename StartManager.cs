using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.XR;

public class StartManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; 
    public XRNode controllerNode = XRNode.RightHand; 
    private bool gameStarted = false;

    private List<int> highScores = new List<int>(); 

    private void Start()
    {
        highScores.Clear();
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey($"HighScore{i}"))
            {
                highScores.Add(PlayerPrefs.GetInt($"HighScore{i}"));
            }
        }
        while (highScores.Count < 5)
        {
            highScores.Add(0);
        }
        
        UpdateHighScoreUI(); 
    }

    private void Update()
    {
        
        InputDevice controller = InputDevices.GetDeviceAtXRNode(controllerNode);
        if (controller.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed && !gameStarted)
        {
            StartGame();
        }
    }


    private void UpdateHighScoreUI()
    {
        highScores.Sort((a, b) => b.CompareTo(a));

        string highScoreDisplay = "High Scores:\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            highScoreDisplay += $"{i + 1}. {highScores[i]}\n";
        }

        highScoreText.text = highScoreDisplay;
    }


    public void StartGame()
    {
        gameStarted = true;
        SceneManager.LoadScene("SampleScene"); 
    }
    public void SaveHighScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt($"HighScore{i}", highScores[i]);
        }
        PlayerPrefs.Save();
    }
}
