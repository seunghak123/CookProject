using System;

[System.Serializable]
public class JRecipeData : JBaseData
{
	public int ID;
	public string Name;
	public int RecipeType;
	public string Foodtype;
	public int FoodName;
	public int UseObject;
	public int UseObjectOutput;
	public int[] AddFoodName;
	public int AddOuput;
	public int Score;
}