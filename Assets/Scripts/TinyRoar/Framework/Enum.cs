
public enum GameEnvironment
{
    None,
    Default,
    Bla,
}

public enum Layer {
    None,
    MainMenu,
    InGame,
}

public enum GameplayStatus
{
    None,
    MatchStart,
    GameplayIdle,
    UnitSelected,
    UnitMoving,
    UnitAttacking,
    BattleStart,
    RoundEnd,
}

public enum UIAction
{
    None,
    Show,
    Hide,
    Toggle,
}

public enum PowerUp
{
    None,
    Flame,
    Bomb,
    Speed,
    Shit,
    Football,
    Shield,
    ChuckNorris,
    Remote,
    Glitch,
    BombDown,
    FlameDown,
    Jump,
    Boxing,
    Joystick,
    BuildCrate,
}

public enum Direction
{
    None = -2,
    Left = -1,
    Right = 1,
    Top = 0,
    Bottom = 2,
}
