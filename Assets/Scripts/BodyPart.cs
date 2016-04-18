using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour {

    [SerializeField]
    int activeSprite = 0;

    [SerializeField]
    Sprite[] possibleSprites;

    public SpriteRenderer sRenderer;

    void Start()
    {
        if(sRenderer == null)
            sRenderer = GetComponent<SpriteRenderer>();
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

    public void SetActiveSprite(int spriteNum)
    {
        if(spriteNum >= 0 && spriteNum < possibleSprites.Length)
        {
            activeSprite = spriteNum;
            //print(sRenderer);
            if (sRenderer == null)
                sRenderer = GetComponent<SpriteRenderer>();

            sRenderer.sprite = possibleSprites[activeSprite];
        }
    }

    public int GetActiveSprite()
    {
        return activeSprite;
    }
}
