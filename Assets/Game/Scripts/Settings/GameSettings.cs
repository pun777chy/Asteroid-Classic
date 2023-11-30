using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.Asteroid;
using Asteroids.Bullets;
namespace Asteroids.Settings
{
    [System.Serializable]
    public class GameSettings
    { 
        public AsteroidHurdle asteroidPrefab;  
        public Bullet bulletPrefab;
 
    }
    [System.Serializable]
    public class AudioContainer
    {
        public AudioClip btnClick;
        public AudioClip rockExplode;
        public AudioClip shipExplode;
        public AudioClip playerBullets;
    }
}
