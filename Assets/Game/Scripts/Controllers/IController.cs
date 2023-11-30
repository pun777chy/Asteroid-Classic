using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Asteroids.Controllers
{
    public interface IController
    {
        public float TimePassed { get; set; }
        public GameStates CurrentState { get; set; }
        public void Playing();
        public void GameStart();
    }
}
