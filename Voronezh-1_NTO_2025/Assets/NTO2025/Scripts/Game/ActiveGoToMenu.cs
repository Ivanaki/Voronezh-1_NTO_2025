using Game.Params;
using R3;
using UnityEngine;
using UnityEngine.Events;

namespace NTO2025.Scripts.Game
{
    public class ActiveGoToMenu
    {
        private static Subject<GameplayExitParams> ActiveGoToMenuChanged;
        
        public static void GoToMenu(bool isRestart)
        {
            if (ActiveGoToMenuChanged != null)
            {
                ActiveGoToMenuChanged.OnNext(new GameplayExitParams(isRestart));
            }
            else
            {
                Debug.LogError("No subject for ActiveGoToMenuChanged");
            }
        }

        public static void ChangeSubject(Subject<GameplayExitParams> subject)
        {
            ActiveGoToMenuChanged = subject;
        }
    }
}