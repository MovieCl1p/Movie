
using Core.Binder;
using Game.Services;

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
        }
    }
}
