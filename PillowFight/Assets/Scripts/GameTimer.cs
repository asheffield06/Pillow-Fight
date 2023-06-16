using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [Header("Players")]
    public PlayerScript player1;
    public PlayerScript player2;
    public PlayerScript player3;
    public PlayerScript player4;

    public float Gametimer = 60f;
    public float Menutimer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gametimer -= Time.deltaTime;

        if (Gametimer <= 0)
        {
            if (player1.Score > player2.Score && player1.Score > player3.Score && player1.Score > player4.Score)
            {
                SceneManager.LoadScene("player1wins");
                SceneManager.UnloadSceneAsync("SampleScene");
                SceneManager.UnloadSceneAsync("PinkForestMap");
                
            }

            if (player2.Score > player1.Score && player2.Score > player3.Score && player2.Score > player4.Score)
            {
                SceneManager.LoadScene("player2wins");
                SceneManager.UnloadSceneAsync("SampleScene");
                SceneManager.UnloadSceneAsync("PinkForestMap");
            }
            if (player3.Score > player1.Score && player3.Score > player2.Score && player3.Score > player4.Score)
            {
                SceneManager.LoadScene("player3wins");
                SceneManager.UnloadSceneAsync("SampleScene");
                SceneManager.UnloadSceneAsync("PinkForestMap");
            }
            if (player4.Score > player1.Score && player4.Score > player2.Score && player4.Score > player3.Score)
            {
                SceneManager.LoadScene("player4wins");
                SceneManager.UnloadSceneAsync("SampleScene");
                SceneManager.UnloadSceneAsync("PinkForestMap");
            }
        }
    }
}
