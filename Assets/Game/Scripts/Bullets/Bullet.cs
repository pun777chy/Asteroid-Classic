using UnityEngine;
using Zenject;

namespace Asteroids.Bullets
{
    /// <summary>
    /// Represents a bullet fired from a weapon.
    /// </summary>
    public class Bullet : MonoBehaviour, IProjectile
    {
        #region Serialized Fields

        [SerializeField] private float bulletLifetime = 1f;
        [SerializeField] private float bulletSpeed = 8f;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the lifetime of the bullet.
        /// </summary>
        public float BulletLifetime { get => bulletLifetime; set => bulletLifetime = value; }

        /// <summary>
        /// Gets or sets the speed of the bullet.
        /// </summary>
        public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }

        #endregion

        /// <summary>
        /// Activated when the bullet is enabled.
        /// </summary>
        public void OnEnable()
        {
            // Schedule the deactivation of the bullet after its lifetime.
            Invoke(nameof(DeactivateGameObject), bulletLifetime);
        }

        /// <summary>
        /// Deactivates the game object associated with the bullet.
        /// </summary>
        private void DeactivateGameObject()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// Factory class for creating bullets.
        /// </summary>
        public class Factory : PlaceholderFactory<Bullet> { }
    }
}
