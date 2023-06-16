using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    private bool playerInRange = false;
    public float speed = 2f;
    private float rndNo = 1;

    private bool toLeft = false;
    private bool toRight = false;

    PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1.1f, gameObject.transform.position.z);

        if (toLeft == true)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        if (toRight == true)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        if (playerInRange)
        {
            gameObject.transform.position += transform.forward * Time.deltaTime * speed;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0);


            ChickenBuzz();
        }
        else
        {
            ChickenBuzz();
        }
    }

    void OnTriggerStay(Collider otherCollider)
    {
        if (otherCollider.GetComponent<PlayerScript>() != null)
        {
            playerInRange = true;
            PlayerScript player = otherCollider.GetComponent<PlayerScript>();
            gameObject.transform.LookAt(2 * transform.position - player.transform.position);

            if (player.transform.position.x < gameObject.transform.position.x)
            {
                toLeft = false;
                toRight = true;
            }
            else
            {
                toLeft = true;
                toRight = false;
            }
        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.GetComponent<PlayerScript>() != null)
        {
            playerInRange = false;
            PlayerScript player = otherCollider.GetComponent<PlayerScript>();
            player.playerChasing = false;
        }
    }

    void ChickenBuzz()
    {
        rndNo = Random.Range(-1, 1);
        if (rndNo >= 0)
        {
            rndNo = Random.Range(-1, 1);
            if (rndNo >= 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + -speed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z + -speed * Time.deltaTime);
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
            }
            else
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + -speed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z + speed * Time.deltaTime);
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
            }
        }
        else
        {
            rndNo = Random.Range(-1, 1);
            if (rndNo >= 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z + -speed * Time.deltaTime);
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
            }
            else
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z + speed * Time.deltaTime);
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
            }
        }
    }
}
