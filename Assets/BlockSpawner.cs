using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public float xBound = 5;
    public float zBound = 5;
    public float yIncrease = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnBlock", 0.5f, 3f);
    }

    void SpawnBlock()
    {
        Instantiate(Prefab, transform.position, Quaternion.identity);
        transform.position = new Vector3(Random.Range(-xBound, xBound), transform.position.y + yIncrease, Random.Range(-xBound, xBound));
    }
}
