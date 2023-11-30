using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Asteroids.Settings;
using Asteroids.Asteroid;

public class TestInstaller : Installer<TestInstaller>
{
    [Inject]
    GameSettings settings;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AsteroidSpawner>().AsSingle();
         
        // Bind AsteroidHurdle and Bullet factories
        Container.BindFactory<AsteroidHurdle, AsteroidHurdle.Factory>().FromComponentInNewPrefab(settings.asteroidPrefab);
    }
}
