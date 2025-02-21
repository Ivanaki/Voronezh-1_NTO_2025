using BaCon;
using Game.Params;
using NTO2025.Scripts.Game;
using R3;
using SaveLaod;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace NTO2025.Scripts.Drom.Root
{
    public class DromEntryPoint : MonoBehaviour
    {
        private Subject<GameplayExitParams> _exitSignalSub = new();
        [SerializeField] private Transform _basePosition;
        
        public Observable<GameplayExitParams> Run(DIContainer gameplayContainer)
        {
            ActiveGoToMenu.ChangeSubject(_exitSignalSub);
            
            if (Player.instance != null)
            {
                var player = Player.instance;
                var playerCamera = player.hmdTransforms[0].GetComponent<Camera>();
                player.transform.position = _basePosition.position;
                 var d = gameplayContainer.Resolve<ILoadSaveService>();



            }

            
            _exitSignalSub.Subscribe(_ => DontDestroyOnLoad(Player.instance.gameObject));
            
            Debug.Log($"ENTRY POINT: vr is ");
            return _exitSignalSub;
        }

        public void GoToMenu()
        {
            _exitSignalSub.OnNext(new GameplayExitParams(false));
        }
    }
}