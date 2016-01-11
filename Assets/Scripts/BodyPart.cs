using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour {

    [SerializeField]
    int activeSprite = 0;

    [SerializeField]
    Sprite[] possibleSprites;

    private SpriteRenderer sRenderer;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

	void Update () {
	
	}

    public void ChooseNextSprite()
    {
        activeSprite++;

        if (activeSprite >= possibleSprites.Length)
            activeSprite = 0;
        
        sRenderer.sprite = possibleSprites[activeSprite];
    }

    public void ChoosePreviousSprite()
    {
        activeSprite--;

        if (activeSprite < 0)
            activeSprite = possibleSprites.Length - 1;

        sRenderer.sprite = possibleSprites[activeSprite];
    }
}
