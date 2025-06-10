using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TextMeshPro text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Score: {GameManager.Instance.Score}\nLevel: {GameManager.Instance.Level}\n{GameManager.Instance.LevelCount - 1}/{GameManager.Instance.BlocksPerLevel}";    
    }
}
