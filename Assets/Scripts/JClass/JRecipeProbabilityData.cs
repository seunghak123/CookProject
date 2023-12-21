using System;

[System.Serializable]
public record JRecipeProbabilityData : JBaseData
{
	public int ID;
	public int RecipeID;
	public float Probability;
	public int GroupID;
}