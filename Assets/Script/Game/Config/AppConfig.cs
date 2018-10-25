using Core.Binder;
using Game.Services;
using Game.Services.Instance;
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
            BindManager.Bind<IInstanceService>().To<InstanceService>().ToSingleton();
            BindManager.Bind<IPlayerService>().To<PlayerService>().ToSingleton();
        }
    }
}
