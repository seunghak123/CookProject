using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

public class BaseIngameUI : MonoBehaviour
{
    [SerializeField] protected Transform reciptParent;
    public virtual void UpdateIngameData() { }
    public virtual void RefreshIngameUI() { }
    public virtual void CreateRecipe(JRecipeData recipeData){}
    public virtual (bool, int) CheckRecipe(BasicMaterialData reciptResult) { return (false, 0); }
    public virtual void RemoveRecipe(bool isSuccess = false,int index = 0)
    {

    }
    public virtual void RepositionRecipe() { }
    public virtual async UniTask StartDirection()
    {
        await WaitTimeManager.WaitForRealTimeSeconds(Time.fixedDeltaTime);
    }
    public virtual void FailRecipe() { }
}
