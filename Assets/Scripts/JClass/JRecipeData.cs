using System;

[System.Serializable]
public record JRecipeData : JBaseData
{
	public int ID;
	public string Name;
	public int RecipeType;
	public int Foodtype;
	public int FoodName;
	public int UseObject;
	public int UseObjectOutput;
	public int[] AddFood;
	public int[] ComplexFood;
	public int AddOuput;
	public int Score;
}