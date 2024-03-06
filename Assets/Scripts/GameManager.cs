using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void OnEnemyKill();
    public static OnEnemyKill EnemyKilled;

    public static GameManager instance;

    public GameObject player;

    private void Awake()
    {
        Time.timeScale = 1f;
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera"); // Player is the camera
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
