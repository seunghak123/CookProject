using System;

[System.Serializable]
public record JFoodObjectData : JBaseData
{
	public int ID;
	public string Name;
	public int Type;
	public string ObjectFile;
	public string IconFile;
}