using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnEnemy : MonoBehaviour
{
    public GameObject Enemy;
    public int MaxEnemyCount = 5;

    // Start is called before the first frame update
    private void Update()
    {
        for (int i = 0; i < MaxEnemyCount; i++)
        {
            Instantiate(Enemy);
        }
    }

    // Update is called once per frame
}
