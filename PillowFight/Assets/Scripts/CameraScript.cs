using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private float cameraHeight;

    // Update is called once per frame
    void Update()
    {
        cameraHeight = UsePythagorus((player1.transform.position.x - player2.transform.position.x), (player1.transform.position.z - player2.transform.position.z)) + 7;
        Vector3 AB = new Vector3(((player1.transform.position.x + player2.transform.position.x) / 2), cameraHeight, ((player1.transform.position.z + player2.transform.position.z) / 2));



        cameraHeight = UsePythagorus((player3.transform.position.x - player4.transform.position.x), (player3.transform.position.z - player4.transform.position.z)) + 7;
        Vector3 CD = new Vector3(((player3.transform.position.x + player4.transform.position.x) / 2), cameraHeight, ((player3.transform.position.z + player4.transform.position.z) / 2));



        cameraHeight = UsePythagorus(UsePythagorus((player1.transform.position.x - player2.transform.position.x), (player1.transform.position.z - player2.transform.position.z)),
        UsePythagorus((player3.transform.position.x - player4.transform.position.x), (player3.transform.position.z - player4.transform.position.z))) + 7;



        gameObject.transform.position = new Vector3(((AB.x + CD.x) / 2), cameraHeight, ((AB.z + CD.z) / 2));

    }

    float UsePythagorus(float a, float b)
    {
        return (Mathf.Sqrt(a * a + b * b));
    }
}
