using System;

[System.Serializable]
public record JCharacterData : JBaseData
{
	public int ID;
	public string Name;
	public float MoveSpeed;
	public float Jump;
	public int SkillID;
	public string CharacterFile;
	public string IconFile;
}