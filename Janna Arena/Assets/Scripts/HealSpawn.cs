using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpawn : MonoBehaviour
{
    public GameObject healObj;

    [SerializeField] public float timeBetweenHeals;
    private float curTimer;


    // Start is called before the first frame update
    void Start()
    {
        curTimer = timeBetweenHeals;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (curTimer <= 0)
        {
            GameObject spawnedHeal = Instantiate(healObj,RandomizeV3(transform.position),Quaternion.identity);
            if (!spawnedHeal.activeSelf)
            {
                spawnedHeal.SetActive(true);
            }

            curTimer = timeBetweenHeals;
        }
        else
        {

            curTimer -= Time.deltaTime;
        }



    }

    Vector3 RandomizeV3(Vector3 inp)
    {
        float randomAngle = Random.Range(0, Mathf.PI * 2);
        float offsetX = Mathf.Cos(randomAngle) * 20;
        float offsetY = Mathf.Sin(randomAngle) * 20;
        return inp + new Vector3(offsetX, offsetY, 0);
    }
}
