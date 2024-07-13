using GoogleSheet.Core.Type;

[UGS(typeof(Scene))]
public enum Scene
{
	Unknown,
	Title,
	Stage,
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