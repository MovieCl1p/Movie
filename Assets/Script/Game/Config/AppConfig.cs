using Core.Binder;
using Core.Dispatcher;
using Game.Services;
using Game.Services.Network;
using Game.Services.PlayerService;

namespace Game.Config
{
    public class AppConfig
    {
        public void Init()
        {
            BindServices();
        }

        private void BindServices()
        {
            BindManager.Bind<INetworkService>().To<NetworkService>().ToSingleton(); 
            BindManager.Bind<IQuizService>().To<QuizService>().ToSingleton(); 
            BindManager.Bind<IPlayerService>().To<PlayerService>().ToSingleton();
            BindManager.Bind<IDispatcher>().To<Dispatcher>().ToSingleton();
        }
    }
}
