using Zenject;

public class ScoreTextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(ScoreTextPresenter),typeof(IInitializable)).To<ScoreTextPresenter>().AsCached().NonLazy();
        Container.Bind<IScoreTextModel>().To<ScoreTextModel>().AsCached();
    }
}