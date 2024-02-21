using System;
using CodeBase.Infrastructure.State;
using CodeBase.StaticData.Level;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Menu
{
    public class MenuWindows : MonoBehaviour
    {
        public Button CloseButton;
        public Button NewGameButton;
        public Button RestartGameButton;
        
        private IGameStateMachine _gameStateMachine;
        
        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            CloseButton.onClick.AddListener(() => Destroy(gameObject));
            NewGameButton.onClick.AddListener(NewGame);
            RestartGameButton.onClick.AddListener(RestartGame);
        }

        public void NewGame()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            _gameStateMachine.Enter<BootstrapState>();
        }

        public void RestartGame()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}