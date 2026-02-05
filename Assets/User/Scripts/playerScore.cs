using System;
using TMPro;
using UnityEngine;

public class playerScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;
    void Start()
    {
        score = 0;
        UpdateScore(score.ToString());
    }

    void UpdateScore(string txt)
    {
        string displayText = "";
        for (int i = 0; i < txt.Length; i++)
        {
            displayText = displayText + "<sprite=" + txt[i]+ ">";
        }
        scoreText.text = displayText;
    }

    public void AddScore(int add)
    {
        score += add;
        UpdateScore(score.ToString());
    }
}
