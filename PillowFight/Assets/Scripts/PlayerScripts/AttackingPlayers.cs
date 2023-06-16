using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingPlayers : MonoBehaviour
{
    
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;

    public PlayerScript player1Script;
    //This script doesn't do anything yet
    //I'm not sure it'll be necessary at the end

    // Start is called before the first frame update
    void Start()
    {
       player1Script = playerOne.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
