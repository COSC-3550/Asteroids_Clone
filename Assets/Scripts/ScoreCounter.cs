using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private Text scoreText;
    public GameManager gameManager;

    private void Start()
    {
        scoreText = GetComponent<Text>();
    }

    private void Update()
    {
        this.scoreText.text = gameManager.score.ToString("Score: ,#,0");
    }
}
