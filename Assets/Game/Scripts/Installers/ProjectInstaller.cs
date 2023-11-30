using Zenject;
using Asteroids.Services;
using UnityEngine.AddressableAssets;
using UnityEngine;

namespace Asteroids.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AssetReference gpSceneAR;

        public override void InstallBindings()
        {
            Container.BindInstance(gpSceneAR).AsSingle();
            Container.BindInterfacesAndSelfTo<AddressableService>().AsSingle();
        }
    }
}
