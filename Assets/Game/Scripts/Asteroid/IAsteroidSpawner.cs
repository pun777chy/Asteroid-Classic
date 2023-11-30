using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace Asteroids.Asteroid
{
    public interface IAsteroidSpawner
    {
        public int AsteroidCount { get; set; }
        public int Level { get; set; }

        public void SpawnAsteroids();
        public void StartSpawningAsteroids();
    }
}
