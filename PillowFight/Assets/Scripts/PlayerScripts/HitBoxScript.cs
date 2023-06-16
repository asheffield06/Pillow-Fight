using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    public Collider[] attackHitBoxes;

    public KeyCode AttackButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Detecting when the player presses the attack button
        if(Input.GetKey(AttackButton))
        {
            LaunchAttack(attackHitBoxes[0]);
        }
    }
    private void LaunchAttack(Collider col)
    {
        
        //Testing whether or not the hitbox hits anything in the "HitBox" layer
        var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitBox"));
        foreach(Collider c in cols)
        {
            //If it hits itself, ignore the detection
            if (c.transform.parent.parent == transform)
                continue;
            
            switch(c.name)
            {
                case "Player1":
                    //Player1 score reduced by amount equal to Player2 score
                    
                    break;
                case "Player2":
                    //Player2 score reduced by amount equal to Player1 score
                    
                    break;
                case "Boss":
                    //Boss health reduced by amount equal to Player score
                    //damage = score;
                default:
                    Debug.Log("");
                    break;
            }
            //What did it hit?
            Debug.Log(c.name);
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        //If the collider interacts with anything what did it hit
        if (otherCollider.GetComponent<PlayerScript>() != null)
        {
            if (otherCollider.GetType() == typeof(SphereCollider))
            {
                Debug.Log("Attack Detected");
                //Code dealing damage to boss
                //Code reducing the other players score and spawning in feathers
            }
        }
    }
}
