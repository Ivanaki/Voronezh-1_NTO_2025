﻿using BaCon;
using Electrical.Scripts._3MainMenu.Root;
using Game.Params;
using Game.Root;
using Gameplay;
using R3;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace MainMenu.Root
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private scanerController _scanerController;
        
        [SerializeField] private UIMainMenuBinder _binder;
        private bool _isExam = false;

        //private string a = Scenes.APEKS;
        
        public Observable<MainMenuExitParams> Run(DIContainer gameplayContainer)
        {
            if (Player.instance != null)
            {
                var player = Player.instance;
                player.transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                var eventSystem = new EventSystemCreator();
            }
            
            //bind UI
            var exitToResultsSignalSubj = new Subject<Unit>();
            _binder.Bind(exitToResultsSignalSubj);
            
            Debug.Log($"MainMenu ENTRY POINT: save file name = , level to load = ");
            
            var e = _scanerController.end;
            var exit = e.Select(nameScene => new MainMenuExitParams(nameScene));
            
            return exit;
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}