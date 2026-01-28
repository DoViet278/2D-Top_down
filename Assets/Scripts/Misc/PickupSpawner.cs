using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefabs;
    [SerializeField] private GameObject healthPrefabs;
    [SerializeField] private GameObject staminaPrefabs;

    public void DropItems()
    {
        int randomNum = Random.Range(1,5);
        if(randomNum == 1)
        {
           Instantiate(healthPrefabs, transform.position, Quaternion.identity);  
        }
        if(randomNum == 2)
        {
            Instantiate(staminaPrefabs, transform.position, Quaternion.identity);  
        }
        if(randomNum == 3)
        {
            int randomAmountOfGold = Random.Range(1,4);

            for(int i = 0; i < randomAmountOfGold; i++)
            {
                Instantiate(coinPrefabs, transform.position, Quaternion.identity);  
            }
        }

    }
}
