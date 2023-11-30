using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids.Bullets;
namespace Asteroids.SpaceShip
{
    public interface IGun 
    {
 
        void HandleShooting(Rigidbody2D shipRigidBody);
    }
}
