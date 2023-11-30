using UnityEngine;
using Zenject;
using Asteroids.Settings;
[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField]
    public GameSettings gameSetting;
    [SerializeField]
    public AudioContainer audioContainer;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameSettings>().FromInstance(gameSetting).AsSingle();
        Container.BindInterfacesAndSelfTo<AudioContainer>().FromInstance(audioContainer).AsSingle();
    }
}