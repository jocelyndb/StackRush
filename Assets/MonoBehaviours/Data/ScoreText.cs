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
        text.text = $"Score: {GameManager.Instance.score}\nLevel: {GameManager.Instance.level}\n{GameManager.Instance.LevelCount - 1}/{GameManager.Instance.BlocksPerLevel}";    
    }
}
