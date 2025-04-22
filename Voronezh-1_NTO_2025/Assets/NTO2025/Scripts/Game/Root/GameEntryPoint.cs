using System.Collections;
using Apeks.Root;
using Autopilot.Root;
using BaCon;
using Drift.Root;
using MainMenu.Root;
using MyUtils;
using NTO2025.Scripts.Drom.Root;
using R3;
using SaveLaod;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

namespace Game.Root
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private readonly DIContainer _rootContainer = new DIContainer();
        private DIContainer _cachedSceneContainer;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);
            
            var save = new JsonToFileLoadSaveService();
            _rootContainer.RegisterInstance<ILoadSaveService>(save);

            //_rootContainer.RegisterFactory(_ => new Account(save)).AsSingle();
            //_rootContainer.RegisterFactory(_ => new CursorLocker()).AsSingle();
        }

        private void RunGame()
        {
            string nameScene = "MainMenu";
            
            nameScene = SceneManager.GetActiveScene().name;
            
#if UNITY_EDITOR
            nameScene = SceneManager.GetActiveScene().name;

            if (SceneManager.GetActiveScene().name != Scenes.APEKS &&
                SceneManager.GetActiveScene().name != Scenes.MAIN_MENU &&
                SceneManager.GetActiveScene().name != Scenes.DRIFT &&
                SceneManager.GetActiveScene().name != Scenes.AUTOPILOT &&
                SceneManager.GetActiveScene().name != Scenes.DROM)
            {
                return;
            }
#endif
            _coroutines.StartCoroutine(InitSteamVR(nameScene));
        }

        private bool flag = true;
        private IEnumerator InitSteamVR(string SceneName)
        {
            if (flag)
            {
                yield return LoadScene(Scenes.START_STEAM_VR);
                flag = false;
            }
            
            _coroutines.StartCoroutine(ScenesLoader(SceneName));
        }


        private IEnumerator ScenesLoader(string SceneName)
        {
            yield return null;
            switch (SceneName)
            {
                case "MainMenu":
                    _coroutines.StartCoroutine(LoadAndStartMainMenu());
                    break;
                case "Drift":
                    _coroutines.StartCoroutine(LoadAndStartDrift());
                    break;
                case "Apeks":
                    _coroutines.StartCoroutine(LoadAndStartApeks());
                    break;
                case "Autopilot":
                    _coroutines.StartCoroutine(LoadAndStartAutopilot());
                    break;
                case "Drom":
                    _coroutines.StartCoroutine(LoadAndStartDrom());
                    break;
            }
        }

        private IEnumerator LoadAndStartMainMenu()
        {
            yield return LoadScene(Scenes.MAIN_MENU);

            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);
            sceneEntryPoint.Run(gameplayContainer).Subscribe(mainMenuExitParams =>
            {
                _coroutines.StartCoroutine(ScenesLoader(mainMenuExitParams.SceneName));
            });
        }
        
        private IEnumerator LoadAndStartDrift()
        {
            yield return LoadScene(Scenes.DRIFT);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<DriftEntryPoint>();
            var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer); 
            sceneEntryPoint.Run(gameplayContainer).Subscribe(_ =>
            {
                
                Debug.Log(_.IsRestart);
                if (_.IsRestart)
                {
                    _coroutines.StartCoroutine(LoadAndStartDrift());
                    return;
                }
                
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            });
        }
        private IEnumerator LoadAndStartApeks()
        {
            yield return LoadScene(Scenes.APEKS);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<ApeksEntryPoint>();
            var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer); 
            sceneEntryPoint.Run(gameplayContainer).Subscribe(_ =>
            { 
                if (_.IsRestart)
                {
                    _coroutines.StartCoroutine(LoadAndStartApeks());
                    return;
                }
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            });
        }
        private IEnumerator LoadAndStartAutopilot()
        {
            yield return LoadScene(Scenes.AUTOPILOT);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<AutopilotEntryPoint>();
            var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer); 
            sceneEntryPoint.Run(gameplayContainer).Subscribe(_ =>
            { 
                if (_.IsRestart)
                {
                    _coroutines.StartCoroutine(LoadAndStartAutopilot());
                    return;
                }
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            });
        }
        
        private IEnumerator LoadAndStartDrom()
        {
            yield return LoadScene(Scenes.DROM);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<DromEntryPoint>();
            var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer); 
            sceneEntryPoint.Run(gameplayContainer).Subscribe(_ =>
            { 
                if (_.IsRestart)
                {
                    _coroutines.StartCoroutine(LoadAndStartDrom());
                    return;
                }
                
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            });
        }
        
        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}