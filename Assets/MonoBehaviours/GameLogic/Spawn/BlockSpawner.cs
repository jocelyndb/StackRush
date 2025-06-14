using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject shadowPrefab;
    public float xBound = 5;
    public float zBound = 5;
    public float yInitial = 40f;
    public float yIncrease = 1f;
    public float timeBetweenBlocks = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnBlock", 0.5f, timeBetweenBlocks);
    }

    void SpawnBlock()
    {
        Prefab.transform.localScale = GameManager.Instance.BlockSize;
        GameObject block = Instantiate(Prefab, transform.position, Quaternion.identity);
        shadowPrefab.transform.localScale = new Vector3(GameManager.Instance.BlockSize.x * 1.1f, transform.position.y, GameManager.Instance.BlockSize.z * 1.1f);
        GameObject shadow = Instantiate(shadowPrefab, transform.position - new Vector3(0, transform.position.y / 2f + GameManager.Instance.BlockSize.y / 2f, 0), Quaternion.identity);
        shadow.transform.parent = block.transform;
        transform.position = new Vector3(Random.Range(-xBound, xBound), yInitial + yIncrease * GameManager.Instance.LevelCount, Random.Range(-xBound, xBound));
    }
}
