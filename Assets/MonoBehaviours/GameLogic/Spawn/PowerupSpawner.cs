using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    // public GameObject Prefab;
    public List<GameObject> Prefabs;
    public float XBound = 5;
    public float ZBound = 5;
    public float YInitial = 20f;
    public float YIncrease = 1f;
    public float MinWaitTime = 2f;
    public float MaxWaitTime = 20f;

    void Start()
    {
        StartCoroutine(SpawnPowerup());
    }

    private IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(Random.Range(MinWaitTime, MaxWaitTime));

        Instantiate(Prefabs[Random.Range(0, Prefabs.Count)], transform.position, Quaternion.identity);
        transform.position = new Vector3(Random.Range(-XBound, XBound), YInitial + YIncrease * GameManager.Instance.LevelCount, Random.Range(-XBound, XBound));

        StartCoroutine(SpawnPowerup());
    }
}
