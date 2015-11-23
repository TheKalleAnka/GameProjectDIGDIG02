using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class MenuPage : MonoBehaviour {

    [SerializeField]
    Sprite background;

    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            if (renderer.sprite != background)
                renderer.sprite = background;
        }
    }
}
