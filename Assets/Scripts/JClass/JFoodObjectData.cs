using System;

[System.Serializable]
public record JFoodObjectData : JBaseData
{
	public int ID;
	public string Name;
	public string ObjectFile;
	public string IconFile;
}