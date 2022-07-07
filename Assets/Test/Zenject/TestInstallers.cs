using Zenject;

public class TestInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ITest>().To<Test>().AsSingle().NonLazy();
    }
}
