using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public GameObject youWin;
    public GameObject gameOver;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    public PlayerController playerController;

    public int onEnemyKilledGain = 5;

    private int score = 0;

    private void OnEnable()
    {
        GameManager.EnemyKilled += AddScore;
    }

    private void OnDisable()
    {
        GameManager.EnemyKilled -= AddScore;
    }

    private void AddScore()
    {
        score += onEnemyKilledGain;
    }

    // Why update any faster?
    private void FixedUpdate()
    {
        healthText.text = "Health: " + playerController.health;
        scoreText.text = "Score: " + score;
    }

    public void PlayerDied()
    {
        healthText.text = "Health: " + 0;

        gameOver.SetActive(true);
    }

    public void YouWin()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        youWin.SetActive(true);
    }
}
