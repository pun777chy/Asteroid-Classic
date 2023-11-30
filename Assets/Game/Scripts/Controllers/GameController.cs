using System;
using UnityEngine;
using Zenject;
using Asteroids.Asteroid;
using Asteroids.SpaceShip;
using ModestTree;

namespace Asteroids.Controllers
{
    [System.Serializable]
    public enum GameStates
    {
        WaitingToStart,
        Playing,
        GameOver
    }

    /// <summary>
    /// Manages the overall game state and transitions between different game states.
    /// </summary>
    public class GameController : IController, IInitializable, ITickable, IDisposable
    {
        private readonly SignalBus signalBus;

        [SerializeField] private GameStates currentState = GameStates.WaitingToStart;

        private float timePassed;

        public GameStates CurrentState { get => currentState; set => currentState = value; }
        public float  TimePassed { get { return timePassed; } set { timePassed = value; } }

        /// <summary>
        /// Constructor with dependency injection for SignalBus.
        /// </summary>
        public GameController(SignalBus signal)
        {
            signalBus = signal;
        }

        /// <summary>
        /// Subscribe to the PlayerShipCrashedSignal during initialization.
        /// </summary>
        public void Initialize()
        {
            signalBus.Subscribe<PlayerShipCrashedSignal>(OnShipCrashed);
        }

        /// <summary>
        /// Update the game state based on the current state during each tick.
        /// </summary>
        public void Tick()
        {
            switch (currentState)
            {
                case GameStates.WaitingToStart:
                    GameStart();
                    break;

                case GameStates.Playing:
                    Playing();
                    timePassed += Time.deltaTime;
                    break;

                case GameStates.GameOver:
                    GameOver();
                    break;

                default:
                    Assert.That(false);
                    break;
            }
        }

        /// <summary>
        /// Unsubscribe from the PlayerShipCrashedSignal during disposal.
        /// </summary>
        public void Dispose()
        {
            signalBus.Unsubscribe<PlayerShipCrashedSignal>(OnShipCrashed);
        }

        /// <summary>
        /// Transition the game to the waiting state and reset time passed.
        /// </summary>
        public void GameStart()
        {
            currentState = GameStates.WaitingToStart;
            timePassed = 0;
        }

        /// <summary>
        /// Transition the game to the playing state and update time passed.
        /// </summary>
        public void Playing()
        {
            currentState = GameStates.Playing;
            timePassed += Time.deltaTime;
        }

        /// <summary>
        /// Transition the game to the game over state.
        /// </summary>
        public void GameOver()
        {
            currentState = GameStates.GameOver;
        }

        /// <summary>
        /// Handle the event when the player's ship crashes.
        /// </summary>
        private void OnShipCrashed()
        {
            GameOver();
        }
    }
}
