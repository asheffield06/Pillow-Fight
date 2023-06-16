using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(90.0f, gameObject.transform.rotation.y, 0.0f);
        //needs to be changed when we have sprites of other angles
    }
}
