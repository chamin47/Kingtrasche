using GoogleSheet.Core.Type;

[UGS(typeof(Scene))]
public enum Scene
{
	Unknown,
	Title,
	StageSelect,
	Game,
	Boss,
	Boss2,
	Boss3
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

public enum BossState
{
	Idle,
	Phase1,
	Phase2,
	Phase3,
	Dead
}