using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Asteroids.Controllers;
using Asteroids.Asteroid;
using Asteroids.Bullets;
using Asteroids.Settings;
using Asteroids.SpaceShip;
using Asteroids.Services;
namespace Asteroids.Installers
{
    public class GameControllerInstaller : MonoInstaller
    {
        [Inject]
        GameSettings settings;
 
        public override void InstallBindings()
        {
            // Install SignalBus
            SignalBusInstaller.Install(Container);

            // Declare signals
            Container.DeclareSignal<PlayerShipCrashedSignal>();

            // Bind AsteroidSpawner as a single instance
            Container.BindInterfacesAndSelfTo<AsteroidSpawner>().AsSingle();

            // Bind IShip interface to PlayerShip as a single instance
            Container.Bind<IShip>().To<PlayerShip>().AsSingle();

            // Bind AsteroidHurdle and Bullet factories
            Container.BindFactory<AsteroidHurdle, AsteroidHurdle.Factory>().FromComponentInNewPrefab(settings.asteroidPrefab);
            Container.BindFactory<Bullet, Bullet.Factory>().FromComponentInNewPrefab(settings.bulletPrefab);

            // Bind GameController as a single instance
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();

            // Bind AudioHandler as a single instance
            Container.Bind<IAudioService>().To<AudioService>().AsSingle();
        }
      
    }
}
