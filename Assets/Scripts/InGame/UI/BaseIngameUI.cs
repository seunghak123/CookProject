using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseIngameUI : MonoBehaviour
{
    [SerializeField] protected Transform reciptParent;

    public virtual void CreateRecipe(JRecipeData recipeData){}
    public virtual (bool, int) CheckRecipe(BasicMaterialData reciptResult) { return (false, 0); }
    public virtual void RemoveRecipe(int index = 0)
    {

    }
}
