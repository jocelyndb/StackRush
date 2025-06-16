using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject shadowPrefab;
    public float XBound = 5;
    public float ZBound = 5;
    public float YInitial = 40f;
    public float YIncrease = 1f;
    public float TimeBetweenBlocks = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnBlock", 0.5f, TimeBetweenBlocks);
    }

    void SpawnBlock()
    {
        Prefab.transform.localScale = GameManager.Instance.BlockSize;
        GameObject block = Instantiate(Prefab, transform.position, Quaternion.identity);

        shadowPrefab.transform.localScale = new Vector3(GameManager.Instance.BlockSize.x * 1.1f, transform.position.y, GameManager.Instance.BlockSize.z * 1.1f);
        GameObject shadow = Instantiate(shadowPrefab, transform.position - new Vector3(0, transform.position.y / 2f + GameManager.Instance.BlockSize.y / 2f, 0), Quaternion.identity);
        shadow.transform.parent = block.transform;

        transform.position = new Vector3(Random.Range(-XBound, XBound), YInitial + YIncrease * GameManager.Instance.LevelCount, Random.Range(-XBound, XBound));
    }
}
