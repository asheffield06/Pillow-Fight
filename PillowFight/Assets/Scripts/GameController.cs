using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Game")]
    public PlayerScript player;

    [Header("UI")]
    public Text scoreText;
    public Text Timer;
    public float GameTimer = 60f;

    public int PlayerScore;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + player.Score;
        PlayerScore = player.Score;
        Timer.text = "" + Mathf.RoundToInt(GameTimer);
        GameTimer -= Time.deltaTime;

        


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
