using System;

[System.Serializable]
public record JNationData : JBaseData
{
	public int ID;
	public string Name;
	public int ClearStage;
}