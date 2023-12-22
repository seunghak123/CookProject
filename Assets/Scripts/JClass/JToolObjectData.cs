using System;

[System.Serializable]
public record JToolObjectData : JBaseData
{
	public int ID;
	public string Name;
	public int Type;
	public string ObjectFile;
	public string IconFile;
	public float[] ToolTimer;
	public int OuputFood;
}