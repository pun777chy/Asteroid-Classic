using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Bullets
{
    public interface IProjectile
    {
        float BulletLifetime { set; get; }
        float BulletSpeed { get; set; }
    }
}
