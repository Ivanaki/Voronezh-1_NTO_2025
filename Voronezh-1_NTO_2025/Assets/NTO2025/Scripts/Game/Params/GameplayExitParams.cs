namespace Game.Params
{
    public class GameplayExitParams
    {
        public bool IsRestart { get;}

        public GameplayExitParams(bool isRestart)
        {
            IsRestart = isRestart;
        }
    }
}