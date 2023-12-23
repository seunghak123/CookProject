using System;

[System.Serializable]
public record JStageData : JBaseData
{
	public int ID;
	public string Name;
	public int NationID;
	public int NextStageID;
	public int ProbabilityGroupID;
	public int score1;
	public int score2;
	public int score3;
	public string StageFile;
	public string StagePrefab;
}