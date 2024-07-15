using GoogleSheet.Core.Type;

[UGS(typeof(Scene))]
public enum Scene
{
	Unknown,
	Title,
	StageSelect,
	Game,
}

[UGS(typeof(Sound))]
public enum Sound
{
	Bgm,
	Effect,
	MaxCount,
}

public enum UIEvent
{
	Click,
	Drag,
}