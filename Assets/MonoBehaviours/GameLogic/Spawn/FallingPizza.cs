using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FallingSpriteSpawner : MonoBehaviour
{
    public List<Texture2D> Sprites;
    private List<VisualElement> activeSprites = new List<VisualElement>();
    public float MinWaitTime = 0.1f;
    public float MaxWaitTime = 0.5f;
    public int MaxSprites = 2;
    public float FallSpeed = 50f;
    private UIDocument document;
    private Image image;

    void Start()
    {
        document = GetComponent<UIDocument>();
        // child.AddComponent(new Texture2D)
        StartCoroutine(SpawnSprite());
    }

    private IEnumerator SpawnSprite()
    {
        image = new Image();
        image.image = Sprites[Random.Range(0, Sprites.Count)];
        image.style.position = Position.Absolute;
        image.transform.position = new Vector3(Random.Range(-Screen.width / 2, Screen.width / 2), -Screen.height, 0);
        image.transform.scale *= new Vector2(0.76f, 0.9f);

        document.rootVisualElement.Add(image);
        image.SendToBack();
        activeSprites.Add(image);

        yield return new WaitForSeconds(Random.Range(MinWaitTime, MaxWaitTime));

        if (activeSprites.Count < MaxSprites)
        {
            StartCoroutine(SpawnSprite());
        }
    }

    void FixedUpdate()
    {
        foreach (VisualElement sprite in activeSprites)
        {
            if (sprite.transform.position.y > Screen.height)
            {
                sprite.transform.position = new Vector3(Random.Range(-Screen.width / 2, Screen.width / 2), Random.Range(-Screen.height, -Screen.height - 5000), 0);
            }
            sprite.transform.position += new Vector3(0, FallSpeed, 0);
            sprite.transform.rotation *= Quaternion.Euler(0,0,FallSpeed * 0.1f);
        }
    }
}
