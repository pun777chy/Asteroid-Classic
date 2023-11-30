using Asteroids.Bullets;
using Asteroids.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace Asteroids.SpaceShip
{
    public class PlayerShipGun : MonoBehaviour, IGun
    {
        [Inject]
        Bullet.Factory bulletFactory;
        [Inject]
        private AudioContainer audioContainer;
        [Inject]
        private IAudioService audioServicer;
        public void HandleShooting(Rigidbody2D shipRigidBody)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlaySFX();
                Bullet bullet = bulletFactory.Create();
                bullet.transform.position = transform.position;
                Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
                Vector2 shipVelocity = shipRigidBody.velocity;
                Vector2 shipDirection = transform.up;
                float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);
                if (shipForwardSpeed < 0)
                {
                    shipForwardSpeed = 0;
                }

                bulletRigidBody.velocity = shipDirection * shipForwardSpeed;
                bulletRigidBody.AddForce(bullet.BulletSpeed * transform.up, ForceMode2D.Impulse);
            }
        }
        private void PlaySFX()
        {

            audioServicer.Play(audioContainer.playerBullets);
        }

    }
}
