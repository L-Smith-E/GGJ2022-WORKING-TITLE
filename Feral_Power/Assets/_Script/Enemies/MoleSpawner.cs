using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    public float spawnTimer = 0.0f;
    public GameObject[] Mole;

    private float timer = 0.0f;
    private int randomInt = 0;
    private bool stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTimer && !stopSpawning)
        {
            randomInt = Random.Range(0, Mole.Length);
            GameObject spawnedMole = Instantiate(Mole[randomInt]);
            spawnedMole.transform.position = transform.position;
            timer = 0.0f;
        }

        if (UserHUD.timeUp == true)
        {
            stopSpawning = true;
        }
    }
}
