using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.SpaceShip
{
    public interface IShip  
    {
          float ShipAcceleration {set;get;}
          float ShipMaxVelocity {set;get;}
          float ShipRotationSpeed { set;get;}
          Rigidbody2D ShipRigidBody {set;get;}
          bool IsAlive {set;get;}
          bool IsAccelerating{set;get;}
          ParticleSystem DestroyableParticles { get; set; }
          void HandleShipAcceleration();
          void HandleShipRotation();
        

    }
}
