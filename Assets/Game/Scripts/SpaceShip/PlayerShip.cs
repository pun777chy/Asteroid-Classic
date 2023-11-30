using UnityEngine;
using Asteroids.Helpers;
using Asteroids.Settings;
using Zenject;

namespace Asteroids.SpaceShip
{
    /// <summary>
    /// Signal indicating that the player ship has crashed.
    /// </summary>
    public class PlayerShipCrashedSignal { }

    /// <summary>
    /// Represents the player's spaceship.
    /// </summary>
    public class PlayerShip : MonoBehaviour, IShip
    {
        private SignalBus playerShipCrashedSignal;

        #region Serialized Fields

        [Header("Player Properties")]
        [SerializeField] private float shipAcceleration = 10;
        [SerializeField] private float shipMaxVelocity = 10;
        [SerializeField] private float shipRotationSpeed = 180;
        [SerializeField] private Rigidbody2D shipRigidBody;
        [SerializeField] private bool isAlive;
        [SerializeField] private bool isAccelerating;
        [SerializeField] private ParticleSystem destroyableParticles;

        [Header("Player Gun References")]
        [SerializeField] private PlayerShipGun playerShipGun;

        #endregion

        #region Properties

        public float ShipAcceleration { get => shipAcceleration; set => shipAcceleration = value; }
        public float ShipMaxVelocity { get => shipMaxVelocity; set => shipMaxVelocity = value; }
        public float ShipRotationSpeed { get => shipRotationSpeed; set => shipRotationSpeed = value; }
        public Rigidbody2D ShipRigidBody { get => shipRigidBody; set => shipRigidBody = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }
        public bool IsAccelerating { get => isAccelerating; set => isAccelerating = value; }
        public ParticleSystem DestroyableParticles { get => destroyableParticles; set => destroyableParticles = value; }

        #endregion

        /// <summary>
        /// Injects dependencies using Zenject.
        /// </summary>
        [Inject]
        private AudioContainer audioContainer;
        [Inject]
        private IAudioService audioServicer;
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            playerShipCrashedSignal = signalBus;
        }

        /// <summary>
        /// Handles user input for ship acceleration.
        /// </summary>
        public void HandleShipAcceleration()
        {
            isAccelerating = Input.GetKey(KeyCode.UpArrow);
        }

        /// <summary>
        /// Handles user input for ship rotation.
        /// </summary>
        public void HandleShipRotation()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(shipRotationSpeed * Time.deltaTime * transform.forward);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(-shipRotationSpeed * Time.deltaTime * transform.forward);
            }
        }

        private void Start()
        {
            shipRigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (isAlive)
            {
                HandleShipAcceleration();
                HandleShipRotation();
                playerShipGun.HandleShooting(shipRigidBody);
            }
        }

        private void FixedUpdate()
        {
            if (isAlive && IsAccelerating)
            {
                shipRigidBody.AddForce(transform.up * shipAcceleration);
                shipRigidBody.velocity = Vector2.ClampMagnitude(shipRigidBody.velocity, shipMaxVelocity);
            }
            else
            {
                shipRigidBody.velocity = Vector2.Lerp(shipRigidBody.velocity, Vector2.zero, 0.1f);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GameConstants.Asteroid))
            {
                isAlive = false;
                playerShipCrashedSignal.Fire<PlayerShipCrashedSignal>();
                PlaySFX();
                Instantiate(destroyableParticles, transform.position, Quaternion.identity);
                Destroy(gameObject,0.3f);
            }
        }
        private void PlaySFX()
        {
            
            audioServicer.Play(audioContainer.shipExplode);
        }
    }
}
