namespace Scene
{
    public interface ISceneService
    {
        ISceneState CurrentSceneState { get; }
        void CreateStartingScene();
    }
}
