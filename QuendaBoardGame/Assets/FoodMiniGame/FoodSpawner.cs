using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {
    //chance to spawn an item out of 100, total of all should be below 100
    public float goodFoodChance;
    public float specialFoodChance;
    public float buriedFoodChance;
    public float badFoodChance;
    public GameObject[] goodFoods;
    public GameObject[] badFoods;
    public GameObject[] specialFoods;
    public GameObject[] buriedFoods;

    // Use this for initialization
    void Start () {
		if(goodFoodChance + specialFoodChance + buriedFoodChance + badFoodChance > 100)
        {
            goodFoodChance = 1.0f;
            specialFoodChance = 1.0f;
            buriedFoodChance = 1.0f;
            badFoodChance = 1.0f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        float roll = Random.Range(0.0f, 100.0f);
        float xPos = Random.Range(-25.0f, 25.0f);
        float yPos = Random.Range(-15.0f, 15.0f);

        if (roll < goodFoodChance)
        {
            int prefab = Random.Range(0, goodFoods.Length);
            Instantiate(goodFoods[prefab], new Vector3(xPos, yPos, 0), Quaternion.identity);
        }else{
            roll -= goodFoodChance;
            if (roll < specialFoodChance)
            {
                int prefab = Random.Range(0, specialFoods.Length);
                Instantiate(specialFoods[prefab], new Vector3(xPos, yPos, 0), Quaternion.identity);
            }
            else{
                roll -= specialFoodChance;
                if (roll < buriedFoodChance)
                {
                    int prefab = Random.Range(0, buriedFoods.Length);
                    Instantiate(buriedFoods[prefab], new Vector3(xPos, yPos, 0), Quaternion.identity);
                }
                else{
                    roll -= buriedFoodChance;
                    if (roll < badFoodChance)
                    {
                        int prefab = Random.Range(0, badFoods.Length);
                        Instantiate(badFoods[prefab], new Vector3(xPos, yPos, 0), Quaternion.identity);
                    }
                }
            }
        }
    }
}
