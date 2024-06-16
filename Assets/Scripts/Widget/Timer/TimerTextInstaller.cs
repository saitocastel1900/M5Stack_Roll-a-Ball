using Zenject;

public class TimerTextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(TimerTextPresenter),typeof(IInitializable)).To<TimerTextPresenter>().AsCached().NonLazy();
        Container.Bind<ITimerTextModel>().To<TimerTextModel>().AsCached();
    }
}