using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawningScript : MonoBehaviour
{
    private float spawnHeight;
    private float spawnWidth;

    public GameObject chickenPrefab;

    public int maxChickenNumber;

    public bool ShouldchickenSpawn = true;

    public List<ChickenScript> chickens;

    // Start is called before the first frame update
    void Start()
    {
        spawnHeight = gameObject.GetComponent<Renderer>().bounds.size.z;
        spawnWidth = gameObject.GetComponent<Renderer>().bounds.size.x;

        chickens = new List<ChickenScript>();
    }

    // Update is called once per frame
    void Update()
    {
        ChickenSpawn();
    }

    void ChickenSpawn()
    {
        chickens.Clear();
        GameObject[] allActiveChickens = GameObject.FindGameObjectsWithTag("Chicken");
        foreach (GameObject chicken in allActiveChickens)
        {
            if (chicken.transform.position.y < -10)
            {
                Destroy(chicken);
            }
            ChickenScript c = chicken.GetComponent<ChickenScript>();
            chickens.Add(c);
        }
        if (chickens.Count <= maxChickenNumber && ShouldchickenSpawn == true)
        {
            GameObject prefabInstance = Instantiate(chickenPrefab);

            prefabInstance.transform.position = new Vector3(gameObject.GetComponent<Renderer>().bounds.center.x + Random.Range(-(spawnWidth / 2) + 1, spawnWidth / 2 - 1),
                                                            gameObject.GetComponent<Renderer>().bounds.center.y,
                                                            gameObject.GetComponent<Renderer>().bounds.center.z + Random.Range(-(spawnHeight / 2) + 1, (spawnHeight / 2) - 1));


            ChickenScript c = prefabInstance.GetComponent<ChickenScript>();
            chickens.Add(c);
        }
        if (chickens.Count == maxChickenNumber)
        {
            ShouldchickenSpawn = false;
        }
    }
}
