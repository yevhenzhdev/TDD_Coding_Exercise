using Ninject.Modules;

namespace ScoreBoard
{
    /// <summary>
    /// Class for resolving dependencies
    /// </summary>
    internal class NinjectConfig : NinjectModule
    {
        /// <summary>
        /// Loads the module into the Ninject kernel
        /// </summary>
        public override void Load()
        {
            Bind<ILogger>().To<LoggerToDebug>();
            Bind<IGameRepository>().To<GamesRepositoryOnCollections>();
        }
    }
}
