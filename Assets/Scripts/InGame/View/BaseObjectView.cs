using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjectView : MonoBehaviour 
{
    [SerializeField] protected SpriteRenderer basicSpriteRenderer;
    protected abstract bool IsInit();
    public virtual void Updated(BaseViewDataClass updateData)
    {
        UpdateSpriteSize();
    }
    protected virtual void UpdateSpriteSize()
    {
        SpriteRenderer spriteRender = GetComponent<SpriteRenderer>();

        if (spriteRender != null)
        {
            Sprite resourceSprite = basicSpriteRenderer.sprite;

            if (resourceSprite == null)
            {
                return;
            }

            Vector2 spriteSize = resourceSprite.bounds.size;

            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                boxCollider.size = spriteSize;
                boxCollider.offset = new Vector2((spriteSize.x / 2), 0);
            }
        }
    }
}
public class BaseViewDataClass
{
}