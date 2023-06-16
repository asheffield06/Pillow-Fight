using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Camera cam;
    public float speed = 10f;

    private int score = 0;
    public int Score { get { return score; } }

    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Up;
    public KeyCode Down;

    public bool playerChasing;
    public int playerNumber;

    bool AttackButtonPressed;
    /// ///////////////////////
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;

    public AudioSource chickenSnd;
    public static bool GamePaused = false;
    public GameObject PauseMenu;

    //Ewan's Meddling for a winning condition_________________________________________________
    public float GameTimer = 60f;
    float PauseTimer = 3.0f;

    public bool P1Winning = false;
    public bool P2Winning = false;

    //________________________________________________________________________________________

    //Ewan's Meddling for chickens on hit__________________________
    private float spawnHeight;
    private float spawnWidth;

    public GameObject chickenPrefab;

    public int maxChickenNumber;

    public List<ChickenScript> chickens;
    //_____________________________________________________________

    //Hitbox scripts____________________________________________________________________________


    public Collider[] attackHitBoxes;

    public KeyCode AttackButton;

    private float attackCoolDown = 0.0f;

    //___________________________________________________________________________________________

    // Start is called before the first frame update
    void Start()
    {
        //Ewan's meddling______________________________________
        spawnHeight = gameObject.GetComponent<Renderer>().bounds.size.z;
        spawnWidth = gameObject.GetComponent<Renderer>().bounds.size.x;

        chickens = new List<ChickenScript>();

        //_____________________________________________________

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponentInChildren(typeof(Animator)).GetComponent<Animator>().SetFloat("Speed",1.0f);

        //Ewan's and Arnie's Meddling____
        //Come back to this
        gameObject.transform.localScale = new Vector3(1 + (0.05f*score), 1 + (0.05f * score), 1 + (0.05f * score));
        


        //__________

        attackCoolDown += Time.deltaTime;
        //Ewan's Meddling
        if (score < 0)
        {
            score = 0;
            speed = 15f;
            
        }
        speed = 15 - (0.2f * score);
        

        if (speed < 1)
        {
            speed = 1;
        }
        //Ewan's meddling for a win condition
        GameTimer -= Time.deltaTime;
        PauseTimer -= 0.01f;
        

        // Character is on ground (built-in functionality of Character Controller)
        if (controller.isGrounded)
        {
            if (Input.GetAxis("Horizontal" + playerNumber) <= 0.0f)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
            }
            else
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }

            if( moveDirection.x * moveDirection.x > moveDirection.z * moveDirection.z)
            {
                if(moveDirection.x > 0)
                {
                    gameObject.transform.eulerAngles = new Vector3(90.0f, 90.0f, 0.0f);
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(90.0f, 270.0f, 0.0f);
                }
            }
            else
            {
                if (moveDirection.z > 0)
                {
                    gameObject.transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
                }
            }
            

            if (gameObject.transform.eulerAngles == new Vector3(90.0f, 90.0f, 0.0f)) // RIGHT
            {
                moveDirection = new Vector3(Input.GetAxis("Vertical" + playerNumber), Input.GetAxis("Horizontal" + playerNumber), 0);
            }
            else if (gameObject.transform.eulerAngles == new Vector3(90.0f, 0.0f, 0.0f)) // UP
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal" + playerNumber), -Input.GetAxis("Vertical" + playerNumber), 0);
            }
            else if (gameObject.transform.eulerAngles == new Vector3(90.0f, 270.0f, 0.0f)) // LEFT
            {
                moveDirection = new Vector3(-Input.GetAxis("Vertical" + playerNumber), -Input.GetAxis("Horizontal" + playerNumber), 0);
            }
            else if (gameObject.transform.eulerAngles == new Vector3(270.0f, 0.0f, 0.0f)) // DOWN
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal" + playerNumber), Input.GetAxis("Vertical" + playerNumber), 0);
            }


            // Use input up and down for direction, multiplied by speed
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        // Apply gravity manually.
        moveDirection.y -= gravity * Time.deltaTime;
        // Move Character Controller
        controller.Move(moveDirection * Time.deltaTime);


        //Hitbox Scripts___________________________________________________________

        if (GamePaused == false)
        {
            if (Input.GetButtonDown("Start" + playerNumber))
            {

                //The cooldown is universal, I think? which would need to change
                //Attacks feel too quick for the lower end
                if (attackCoolDown >= 0.5f + (0.5f * score))
                {
                    gameObject.GetComponentInChildren(typeof(Animator)).GetComponent<Animator>().SetBool("AttackButtonPressed", true);
                    LaunchAttack(attackHitBoxes[0]);
                    attackCoolDown = 0.0f;
                }
            }
        }

        //Pausing Game:
        if (Input.GetButtonDown("Pausing" + playerNumber) && GamePaused == false && PauseTimer < 0.0f)
        {
            Time.timeScale = 0;
            GamePaused = true;
            PauseMenu.SetActive(true);
            PauseTimer = 3.0f;
        }

        if (Input.GetButtonDown("Pausing" + playerNumber) && GamePaused == true && PauseTimer < 0.0f)
        {
            Time.timeScale = 1;
            GamePaused = false;
            PauseMenu.SetActive(false);
            PauseTimer = 3.0f;
        }

        if (GamePaused == true)
        {
            if (Input.GetButtonDown("Button" + playerNumber))
            {
                SceneManager.LoadScene("menu");
                SceneManager.UnloadSceneAsync("SampleScene");
                SceneManager.UnloadSceneAsync("PinkForestMap");
            }

            if (Input.GetButtonDown("Start" + playerNumber) && GamePaused == true && PauseTimer < 0.0f)
            {
                Time.timeScale = 1;
                GamePaused = false;
                PauseMenu.SetActive(false);
                PauseTimer = 3.0f;
            }
        }

        //_________________________________________________________________________
        //When player falls off map, put them back on the map
        if (gameObject.transform.position.y < -0.1)
        {
            gameObject.transform.position = new Vector3(0, 5, -20);
        }
    }
    void LateUpdate()
    {
        gameObject.GetComponentInChildren(typeof(Animator)).GetComponent<Animator>().SetBool("AttackButtonPressed", false);
        gameObject.GetComponentInChildren(typeof(Animator)).GetComponent<Animator>().SetBool("GotHit?", false);
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<ChickenScript>() != null)
        {
            if (otherCollider.GetType() == typeof(BoxCollider))
            {
                score += 1;
                ChickenScript chicken = otherCollider.GetComponent<ChickenScript>();
                chickenSnd = otherCollider.GetComponent<AudioSource>();
                chickenSnd.Play();
                Destroy(chicken.gameObject);
            }
        }
    }

    //Hitbox Scripts___________________________________________________________________________________
    private void LaunchAttack(Collider col)
    {
        

        //Testing whether or not the hitbox hits anything in the "HitBox" layer
        var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitBox"));
        foreach (Collider c in cols)
        {
            //If it hits itself, ignore the detection
            if (c.transform.parent.parent == transform)
                continue;
            
            switch (c.name)
            {
                case "Player1":
                    //Player1 score reduced by amount 
                    // equal to whichever player's score attacked it
                    if (c.GetComponent<PlayerScript>().score > 0)
                    {
                        c.GetComponent<AudioSource>().Play();
                        c.GetComponentInChildren(typeof(Animator)).GetComponent<Animator>().SetBool("GotHit?", true);

                        //We need to figure out the opponent's direction so
                        //the player can be knocked back in the right direction
                        float knockbackX = c.GetComponent<PlayerScript>().gameObject.transform.position.x - gameObject.transform.position.x;
                       float knockbackZ = c.GetComponent<PlayerScript>().gameObject.transform.position.z - gameObject.transform.position.z;
                                                                                                                                                                        //knockbackY goes here
                                                                                                                                                                        //     \/
                        
                        DroppedChickens(c.GetComponent<PlayerScript>().score, score, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z,knockbackX, knockbackZ);
                        //function for spawning chickens
                        c.GetComponent<PlayerScript>().score -= score;
                       

                        c.GetComponent<PlayerScript>().gameObject.transform.position = new Vector3(c.GetComponent<PlayerScript>().gameObject.transform.position.x + knockbackX, 1, c.GetComponent<PlayerScript>().gameObject.transform.position.z + knockbackZ);

                    }
                    break;
                case "Player2":
                    //Player2 score reduced by amount 
                    // equal to whichever player's score attacked it
                    if (c.GetComponent<PlayerScript>().score > 0)
                    {
                        c.GetComponentInChildren(typeof(Animator)).GetComponent<Animator>().SetBool("GotHit?", true);

                        //We need to figure out the opponent's direction so
                        //the player can be knocked back in the right direction
                        float knockbackX = c.GetComponent<PlayerScript>().gameObject.transform.position.x - gameObject.transform.position.x;
                        float knockbackZ = c.GetComponent<PlayerScript>().gameObject.transform.position.z - gameObject.transform.position.z;

                        
                        //_____________                                                                                                                                            //knockbackY goes here
                        //         \/


                        DroppedChickens(c.GetComponent<PlayerScript>().score, score, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z, knockbackX, knockbackZ);
                        c.GetComponent<PlayerScript>().score -= score;
                        
                        c.GetComponent<PlayerScript>().gameObject.transform.position = new Vector3(c.GetComponent<PlayerScript>().gameObject.transform.position.x + knockbackX, 1, c.GetComponent<PlayerScript>().gameObject.transform.position.z + knockbackZ);
                    }
                    break;
                case "Player3":
                    //Player3 score reduced by amount 
                    // equal to whichever player's score attacked it
                    if (c.GetComponent<PlayerScript>().score > 0)
                    {
                        c.GetComponentInChildren(typeof(Animator)).GetComponent<Animator>().SetBool("GotHit?", true);

                        //We need to figure out the opponent's direction so
                        //the player can be knocked back in the right direction
                        float knockbackX = c.GetComponent<PlayerScript>().gameObject.transform.position.x - gameObject.transform.position.x;
                        float knockbackZ = c.GetComponent<PlayerScript>().gameObject.transform.position.z - gameObject.transform.position.z;
                        //knockbackY goes here
                        //     \/

                        DroppedChickens(c.GetComponent<PlayerScript>().score, score, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z, knockbackX, knockbackZ);
                        //function for spawning chickens
                        c.GetComponent<PlayerScript>().score -= score;


                        c.GetComponent<PlayerScript>().gameObject.transform.position = new Vector3(c.GetComponent<PlayerScript>().gameObject.transform.position.x + knockbackX, 1, c.GetComponent<PlayerScript>().gameObject.transform.position.z + knockbackZ);
                    }
                    break;
                case "Player4":
                    //Player4 score reduced by amount 
                    // equal to whichever player's score attacked it
                    if (c.GetComponent<PlayerScript>().score > 0)
                    {
                        c.GetComponentInChildren(typeof(Animator)).GetComponent<Animator>().SetBool("GotHit?", true);

                        //We need to figure out the opponent's direction so
                        //the player can be knocked back in the right direction
                        float knockbackX = c.GetComponent<PlayerScript>().gameObject.transform.position.x - gameObject.transform.position.x;
                        float knockbackZ = c.GetComponent<PlayerScript>().gameObject.transform.position.z - gameObject.transform.position.z;
                        //knockbackY goes here
                        //     \/

                        DroppedChickens(c.GetComponent<PlayerScript>().score, score, gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z, knockbackX, knockbackZ);
                        //function for spawning chickens
                        c.GetComponent<PlayerScript>().score -= score;


                        c.GetComponent<PlayerScript>().gameObject.transform.position = new Vector3(c.GetComponent<PlayerScript>().gameObject.transform.position.x + knockbackX, 1, c.GetComponent<PlayerScript>().gameObject.transform.position.z + knockbackZ);
                    }
                    break;
                default:
                   break;
            }
        }
    }
    //___________________________________________________________________________________________________
    //Ewan's Attempt at spawning chickens on hit
    void DroppedChickens (int playerHitscore, int playerAttackScore, float playerXPos, float playerYPos, float playerZPos, float KnockbackX, float KnockBackZ)
    {
        if (playerAttackScore > 0)
        {
            int DamageTotal = playerHitscore - playerAttackScore;
            if (DamageTotal < 0)
            {
                for (int i = 0; i < playerHitscore; i++)
                {
                    GameObject prefabInstance = Instantiate(chickenPrefab) as GameObject;

                    prefabInstance.transform.position = new Vector3(playerXPos - (KnockbackX * 2.25f), playerYPos, playerZPos - (KnockBackZ * 2.25f));


                    ChickenScript c = prefabInstance.GetComponent<ChickenScript>();
                    chickens.Add(c);
                }
            }

            else
            {
                for (int i = 0; i < playerAttackScore; i++)
                {
                    GameObject prefabInstance = Instantiate(chickenPrefab) as GameObject;

                    prefabInstance.transform.position = new Vector3(playerXPos - (KnockbackX * 2.25f) , playerYPos, playerZPos - (KnockBackZ * 2.25f));


                    ChickenScript c = prefabInstance.GetComponent<ChickenScript>();
                    chickens.Add(c);
                }
            }
        }
        
    }
}
