using Seunghak.Common;
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
                boxCollider.offset = new Vector2(0, 0);
            }
        }
    }
    public void SetBaseSprite(JToolObjectData toolData)
    {
        SpriteRenderer spriteRender = GetComponent<SpriteRenderer>();

        if (spriteRender != null)
        {
            spriteRender.sprite = SpriteManager.Instance.LoadSprite(toolData.IconFile);
            spriteRender.sortingOrder = 10;
        }

        UpdateSpriteSize();
    }
}
public class BaseViewDataClass
{
}