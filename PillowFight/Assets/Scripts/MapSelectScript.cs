using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start1"))
        {
            SceneManager.LoadScene("SampleScene");
            SceneManager.UnloadSceneAsync("MapSelection");
            Debug.Log("the 1");
        }

        if (Input.GetButtonDown("Button1"))
        {
            SceneManager.LoadScene("PinkForestMap");
            SceneManager.UnloadSceneAsync("MapSelection");
            Debug.Log("the 1");
        }
    }
}
