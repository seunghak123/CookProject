using System;

[System.Serializable]
public class JRecipeProbabilityData : JBaseData
{
	public int ID;
	public int RecipeID;
	public float Probability;
	public int GroupID;
}