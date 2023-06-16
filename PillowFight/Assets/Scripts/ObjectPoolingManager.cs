using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance { get { return instance; } }

    public GameObject chickenPrefab;
    public int chickenAmount = 3;

    private List<GameObject> chickens;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        chickens = new List<GameObject>(chickenAmount);

        for (int i = 0; i < chickenAmount; i++)
        {
            GameObject prefabInstance = Instantiate(chickenPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            chickens.Add(prefabInstance);
        }
    }

    public GameObject GetBullet(bool shotByPlayer)
    {
        foreach (GameObject bullet in chickens)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                //bullet.GetComponent<ChickenScript>().ShotByPlayer = shotByPlayer;
                return bullet;
            }
        }
        GameObject prefabInstance = Instantiate(chickenPrefab);
        prefabInstance.transform.SetParent(transform);
        //prefabInstance.GetComponent<ChickenScript>().ShotByPlayer = shotByPlayer;
        chickens.Add(prefabInstance);

        return prefabInstance;
    }
}
