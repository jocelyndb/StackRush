using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    // public GameObject Prefab;
    public List<GameObject> Prefabs;
    public float xBound = 5;
    public float zBound = 5;
    public float yInitial = 20f;
    public float yIncrease = 1f;
    public float minWaitTime = 2f;
    public float maxWaitTime = 20f;

    void Start()
    {

        StartCoroutine(SpawnPowerup());
    }

    private IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        Instantiate(Prefabs[Random.Range(0, Prefabs.Count)], transform.position, Quaternion.identity);
        transform.position = new Vector3(Random.Range(-xBound, xBound), yInitial + yIncrease * GameManager.Instance.LevelCount, Random.Range(-xBound, xBound));
        // yield return new WaitForSeconds(0.5f);
        StartCoroutine(SpawnPowerup());
    }
}
