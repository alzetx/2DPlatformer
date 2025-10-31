public interface IGameStartListener : IGameListener
{
    void OnStartGame();
}

public interface IGamePauseListener : IGameListener
{
    void OnPauseGame();
}
public interface IGameResumeListener : IGameListener
{
    void OnResumeGame();
}
public interface IGameFinishListener : IGameListener
{
    void OnFinishGame();
}
public interface IGameListener
{

}