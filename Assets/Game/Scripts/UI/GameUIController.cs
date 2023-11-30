using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Asteroids.Controllers;
using Asteroids.SpaceShip;
using Zenject;
using UnityEngine.SceneManagement;
using System;
using Asteroids.Settings;
namespace Asteroids.UI
{
    public class GameUIController : MonoBehaviour, IDisposable
    {
        private SignalBus signalBus;

        [Inject] 
        private IController controller;
        [Inject]
        private AudioContainer audioContainer;
        [Inject]
        private IAudioService audioServicer;
        [Header("Screen Panels")]
        public List<ScreenUI> screens;

        [Inject]
        public void Construct(SignalBus signal)
        {
            signalBus = signal;
        }

        private void Start()
        {
            InitializeUI();
            SubscribeToSignals();
        }

        private void InitializeUI()
        {
            ChangeState(controller.CurrentState);

            for (int i = 0; i < screens.Count; i++)
            {
                screens[i].SetButtonAction(GetCallbackForState((GameStates)i));
            }
        }

        private void SubscribeToSignals()
        {
            signalBus.Subscribe<PlayerShipCrashedSignal>(OnShipCrashed);
        }
        private void Update()
        {
            UpdateScreenTime();
        }

        private void UpdateScreenTime()
        {
            screens[(int)controller.CurrentState].ShowScreenTime(((int)controller.TimePassed).ToString());
        }
        public void ChangeState(GameStates state)
        {
            // Hide all screens and show the specified screen
            HideAllScreens();
            int stateIndex = (int)state;
            screens[stateIndex].ShowScreen(true, ((int)controller.TimePassed).ToString());
        }

        private void HideAllScreens()
        {
            // Hide all screens with a default value
            foreach (var screen in screens)
            {
                screen.ShowScreen(false, "0");
            }
        }

        private UnityAction GetCallbackForState(GameStates state)
        {
            // Return the appropriate callback for the specified game state
            switch (state)
            {
                case GameStates.WaitingToStart:
                    return StartGame;
                case GameStates.GameOver:
                    return RestartStartGame;
                case GameStates.Playing:
                    return ExitGame;
                default:
                    return StartGame;
            }
        }

        private void StartGame()
        {
            PlaySFX();
            // Start the game and update UI
            controller.Playing();
            ChangeState(controller.CurrentState);
        }

        private void ExitGame()
        {
            PlaySFX();
            // Exit the game and update UI
            Application.Quit();
        }

        private void RestartStartGame()
        {
            PlaySFX();
            // Restart the game and reload the scene
            RestartTheScene();
        }
        private void RestartTheScene()
        {
            controller.GameStart();
            ChangeState(controller.CurrentState);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void OnShipCrashed()
        {
            // Handle UI changes when the ship crashes
            ChangeState(controller.CurrentState);
        }
        private void PlaySFX()
        {

            audioServicer.Play(audioContainer.btnClick);
        }
        public void Dispose()
        {
            // Unsubscribe from the PlayerShipCrashedSignal during disposal
            signalBus.Unsubscribe<PlayerShipCrashedSignal>(OnShipCrashed);
        }
      
    }
}
