using Core.Binder;
using Core.Dispatcher;
using Core.Services.Network.Messages;

namespace Game.Services.Network.Messages
{
    public class GoToGameResponseHandler<T> : MessageResponseHandler<T> where T : IMessage
    {
        public override void Process(T o)
        {
            base.Process(o);

            IDispatcher dispatcher = BindManager.GetInstance<IDispatcher>();
            dispatcher.Dispatch(NetworkEventType.GoToGame);
        }
    }
}
