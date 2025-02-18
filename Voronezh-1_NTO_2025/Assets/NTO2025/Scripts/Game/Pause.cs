using Game.Root;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

namespace NTO2025.Scripts.Game
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;
        private SteamVR_Action_Boolean _pause = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Pause");

        private bool flag = false;
        
        private void Start()
        {
            _menu.SetActive(flag);
        }
        
        private void Update()
        {
            if (_pause.changed)
            {
                flag = !flag;
                if (SceneManager.GetActiveScene().name == Scenes.MAIN_MENU)
                {
                    flag = false;
                }
                _menu.SetActive(flag);
            }
        }

        public void Continue()
        {
            flag = false;
            _menu.SetActive(flag);
        }

        public void RestartScene()
        {
            ActiveGoToMenu.GoToMenu(true);
        }
        
        public void Exit()
        {
            Continue();
            ActiveGoToMenu.GoToMenu(false);
        }
    }
}