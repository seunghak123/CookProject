using System;

[System.Serializable]
public record JChapterData : JBaseData
{
	public int ID;
	public string Name;
	public int ClearStage;
}