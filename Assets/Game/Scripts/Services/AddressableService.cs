using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

namespace Asteroids.Services
{
    public class AddressableService : IInitializable
    {
        private AsyncOperationHandle<SceneInstance> _asyncOperationHandleScene;
        private AssetReference _gpSceneAR;

        public string LoggerData { get; private set; } = "";

        public AddressableService(AssetReference gpSceneAR)
        {
            _gpSceneAR = gpSceneAR;
        }

        public async void Initialize()
        {
            Debug.Log("InitializeAddressables");

            AsyncOperationHandle<IResourceLocator> initializationHandle = Addressables.InitializeAsync();
            await initializationHandle.Task;

            Debug.Log("Initialization Completed");

            await DownloadAndLoadGameplayScene();
        }

        public void ReleaseSpawnedObjects()
        {
            ReleaseGameplayScene();
        }

        private void ReleaseGameplayScene()
        {
            LoggerData += "Unloading : " + _asyncOperationHandleScene.Result.Scene.name + " Scene";
            UnloadGameplayScene();
        }

        private async Task DownloadAndLoadGameplayScene()
        {
            var downloadScene = Addressables.LoadSceneAsync(_gpSceneAR, LoadSceneMode.Single);
            await downloadScene.Task;

            DownloadSceneCompleted(downloadScene);
        }

        private void DownloadSceneCompleted(AsyncOperationHandle<SceneInstance> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                LoggerData += "\n " + handle.Result.Scene.name + " Successfully Loaded!";
                _asyncOperationHandleScene = handle;
            }
        }

        private async void UnloadGameplayScene()
        {
            var unloadHandle = Addressables.UnloadSceneAsync(_asyncOperationHandleScene, true);
            await unloadHandle.Task;

            if (unloadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Gameplay Scene Unloaded");
            }
        }
    }
}
