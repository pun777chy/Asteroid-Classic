using Zenject;
using NUnit.Framework;
using Asteroids.Asteroid;

[TestFixture]
public class AsteroidSpawnerUnitTest : ZenjectUnitTestFixture
{
    private string testSettingspath = "GameSettingsInstaller";
    [SetUp]
    public void BindInterfaces()
    {
        GameSettingsInstaller.InstallFromResource(testSettingspath, Container);
        TestInstaller.Install(Container);
    }
    [Test]
    public void WillAsteroidSpawnerResolve()
    {
        IAsteroidSpawner spawnerObject = Container.Resolve<IAsteroidSpawner>();
        Assert.NotNull(spawnerObject);
    }
    [Test]
    public void IAsteroidSpawnerResolvesSpawningObject()
    {
        AsteroidSpawner obj = Container.Resolve<IAsteroidSpawner>() as AsteroidSpawner;
        Assert.NotNull(obj);
    }
    [Test]
    public void IAsteroidSpawnerResolvesAsteroidCount()
    {
        int count = Container.Resolve<IAsteroidSpawner>().AsteroidCount;
        Assert.AreEqual(count, 0);
    }
    [TearDown]
    public void TearDown()
    {
        Container.UnbindAll();
    }
}