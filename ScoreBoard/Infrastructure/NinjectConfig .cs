using Ninject.Modules;

namespace ScoreBoard
{
    internal class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<LoggerToDebug>();
            Bind<IGameRepository>().To<GamesRepositoryOnCollections>();
        }
    }
}
