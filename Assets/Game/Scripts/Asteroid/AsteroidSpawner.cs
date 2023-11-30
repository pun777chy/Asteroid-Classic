using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Asteroids.Controllers;

namespace Asteroids.Asteroid
{ 
    // AsteroidSpawner class implementing IAsteroidSpawner and ITickable interfaces
    public class AsteroidSpawner : IAsteroidSpawner, ITickable
    {
        private int asteroidCount = 0;
        private int level = 0;

        // Property to get and set the current asteroid count
        public int AsteroidCount { get { return asteroidCount; } set { asteroidCount = value; } }

        // Property to get and set the current level
        public int Level { get { return level; } set { level = value; } }

        [Inject] AsteroidHurdle.Factory asteroidFactory;
        [Inject] private IController controller;

        // Method to spawn asteroids
        public void SpawnAsteroids()
        {
            // Generate a random spawn position on one of the edges of the screen
            float offset = Random.Range(0f, 1f);
            Vector2 viewportSpawnPosition = Vector2.zero;
            int edge = Random.Range(0, 4);

            // Set the spawn position based on the selected edge
            if (edge == 0)
                viewportSpawnPosition = new Vector2(offset, 0);
            else if (edge == 1)
                viewportSpawnPosition = new Vector2(offset, 1);
            else if (edge == 2)
                viewportSpawnPosition = new Vector2(0, offset);
            else if (edge == 3)
                viewportSpawnPosition = new Vector2(1, offset);

            // Convert the viewport position to world position
            Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);

            // Create an asteroid using the factory and set its position and rotation
            AsteroidHurdle asteroid = asteroidFactory.Create();
            asteroid.transform.position = worldSpawnPosition;
            asteroid.transform.rotation = Quaternion.identity;
        }

        // Method to start spawning asteroids based on the current game state
        public void StartSpawningAsteroids()
        {
            if (asteroidCount == 0)
            {
                level++;
                int numAsteroids = 2 + (2 * level);

                // Spawn the specified number of asteroids
                for (int i = 0; i < numAsteroids; i++)
                {
                    SpawnAsteroids();
                }
            }
        }

        // ITickable interface method, called every frame
        public void Tick()
        {
            // Check if the game state is Playing before spawning asteroids
            if (controller.CurrentState == GameStates.Playing)
            {
                StartSpawningAsteroids();
            }
        }
    }
}
