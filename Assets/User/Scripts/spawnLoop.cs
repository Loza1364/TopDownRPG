using System.Collections;
using UnityEngine;

public class spawnLoop : MonoBehaviour
{
    public int enemyCount = 0;
    public GameObject warning;

    public GameObject[] enemyPrefabs;
    public Transform[] enemySpawn;
    void Start()
    {
        warning.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(Spawn());
    }

    private void EnemySpawner()
    {
        enemyCount = 0;
        for (int i = 0; i < enemySpawn.Length; i++)
        {
            enemyCount++;
            Instantiate(enemyPrefabs[0], enemySpawn[i]);
        }
    }


    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.5f);
        warning.SetActive(true);
        yield return new WaitForSeconds(1f);
        EnemySpawner();
        yield return new WaitForSeconds(0.5f);
        warning.SetActive(false);
        yield return new WaitUntil(() => enemyCount < 1);
        StopAllCoroutines();
        StartCoroutine(Spawn());
    }
}
