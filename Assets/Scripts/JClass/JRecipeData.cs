using System;

[System.Serializable]
public record JRecipeData : JBaseData
{
	public int ID;
	public string Name;
	public int RecipeType;
	public string Foodtype;
	public int FoodName;
	public int UseObject;
	public int UseObjectOutput;
	public int[] AddFood1;
	public int[] AddFood2;
	public int AddOuput;
	public int Score;
}