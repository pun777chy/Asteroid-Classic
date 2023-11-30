using UnityEngine;
using Asteroids.Helpers;
using Asteroids.Settings;
using Zenject;

namespace Asteroids.Asteroid
{
    /// <summary>
    /// Represents an asteroid obstacle in the game.
    /// </summary>
    public class AsteroidHurdle : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private int size = 3;
        [SerializeField] private ParticleSystem destroyableParticles;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the size of the asteroid.
        /// </summary>
        public int Size { get => size; set => size = value; }

        /// <summary>
        /// Gets or sets the particle system for destruction effects.
        /// </summary>
        public ParticleSystem DestroyableParticles { get => destroyableParticles; set => destroyableParticles = value; }

        #endregion

        #region Dependencies

        [Inject] private IAsteroidSpawner asteroidSpawner;
        [Inject] private AsteroidHurdle.Factory asteroidFactory;
        [Inject]
        private AudioContainer audioContainer;
        [Inject]
        private IAudioService audioServicer;
        #endregion

        /// <summary>
        /// Initializes the asteroid's size, velocity, and increases the asteroid count.
        /// </summary>
        void Start()
        {
            InitializeAsteroid();
        }

        /// <summary>
        /// Handles collisions with bullets, updates asteroid count, and spawns smaller asteroids if applicable.
        /// </summary>
        /// <param name="collision">The collider involved in the collision.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            HandleBulletCollision(collision);
        }

        /// <summary>
        /// Factory class for creating asteroids.
        /// </summary>
        public class Factory : PlaceholderFactory<AsteroidHurdle> { }

        #region Helper Methods

        /// <summary>
        /// Initializes the asteroid's size, velocity, and increases the asteroid count.
        /// </summary>
        private void InitializeAsteroid()
        {
            transform.localScale = 0.5f * size * Vector3.one;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(Random.value, Random.value).normalized;
            float spawnSpeed = Random.Range(4f - size, 5f - size);
            rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
            asteroidSpawner.AsteroidCount++;
        }

        /// <summary>
        /// Handles collisions with bullets, updates asteroid count, and spawns smaller asteroids if applicable.
        /// </summary>
        /// <param name="collision">The collider involved in the collision.</param>
        private void HandleBulletCollision(Collider2D collision)
        {
            if (collision.CompareTag(GameConstants.Bullet))
            {
                asteroidSpawner.AsteroidCount--;
                PlaySFX();
                Destroy(collision.gameObject);

                if (size > 1)
                {
                    SpawnSmallerAsteroids();
                }

                Instantiate(destroyableParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Spawns smaller asteroids upon collision.
        /// </summary>
        private void SpawnSmallerAsteroids()
        {
            for (int i = 0; i < 2; i++)
            {
                AsteroidHurdle asteroid = asteroidFactory.Create();
                asteroid.Size = size - 1;
            }
        }
        private void PlaySFX()
        {

            audioServicer.Play(audioContainer.rockExplode);
        }

        #endregion
    }
}
