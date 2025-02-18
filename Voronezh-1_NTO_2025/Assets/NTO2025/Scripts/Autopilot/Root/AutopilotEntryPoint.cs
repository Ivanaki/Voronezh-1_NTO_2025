using BaCon;
using R3;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Autopilot.Root
{
    public class AutopilotEntryPoint : MonoBehaviour
    {
        private Subject<Unit> _exitSignalSub = new();
        
        public Observable<Unit> Run(DIContainer gameplayContainer)
        {
            if (Player.instance != null)
            {
                var player = Player.instance;
                var playerCamera = player.hmdTransforms[0].GetComponent<Camera>();
                player.transform.position = new Vector3(0, 0, 0);
                
                
                
                
                
            }

            Debug.Log($"ENTRY POINT: vr is ");
            return _exitSignalSub;
        }

        public void GoToMenu()
        {
            _exitSignalSub.OnNext(Unit.Default);
        }
    }
}