using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Asteroid
{
    public interface IAsteroidHurdle 
    {
         int Size { get; set; }
        ParticleSystem DestroyableParticles { get; set; }
    }
}
