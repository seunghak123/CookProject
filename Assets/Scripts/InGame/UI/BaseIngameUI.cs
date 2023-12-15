using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseIngameUI : MonoBehaviour
{
    [SerializeField] private Transform reciptParent;

    public virtual void CreateRecipe(int recipeId){}
}
