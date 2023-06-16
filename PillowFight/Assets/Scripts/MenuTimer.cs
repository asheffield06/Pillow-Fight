using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTimer : MonoBehaviour
{
    public float Menutimer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Menutimer -= Time.deltaTime;
        if (Menutimer <= 0)
        {
            SceneManager.LoadScene("menu");
            SceneManager.UnloadSceneAsync("player1wins");
            SceneManager.UnloadSceneAsync("player2wins");
        }
    }
}
