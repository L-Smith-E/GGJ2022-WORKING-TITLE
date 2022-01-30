using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public static bool startGauntlet = false;
    public GameObject boss;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            startGauntlet = true;
            GameObject spawnBoss = Instantiate(boss);
            boss.transform.position = spawnPoint.position;
            Debug.Log("Start the battle");
        }

    }
}
