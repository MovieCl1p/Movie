using ExitGames.Client.Photon;
using Core.Services.Network.Specifications;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Core.Services.Network.Messages;
using Core;
using Game.Services.Network.Messages;

namespace Game.Services.Network
{
    public class NetworkService : INetworkService, IOnEventCallback
    {
        private PhotonManager _photon;

        public void Init()
        {
            _photon = PhotonManager.Instance;
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void ConnectAndJoin()
        {
            _photon.ConnectAndJoinRoom();
        }

        public void Disconnect()
        {
            //_photon.Disconnect();
        }

        public void OnEvent(EventData photonEvent)
        {
            MonoLog.Log(MonoLogChannel.MultiPlayer, "photonEvent: " + photonEvent.ToString());

            IMessage message = new Message(photonEvent);
            IMessageResponseHandler<IMessage> root = new MessageResponseHandler<IMessage>();
            IMessageResponseHandler<IMessage> last = new MessageResponseHandler<IMessage>();

            IMessageResponseHandler<IMessage> roomReady = new RoomReadyResponseHandler<IMessage>(); 
            IMessageResponseHandler<IMessage> roomCountdown = new RoomCountdownResponseHandler<IMessage>();
            IMessageResponseHandler<IMessage> goToGame = new GoToGameResponseHandler<IMessage>();

            ISpecification<IMessage> rootSpec = new Specification<IMessage>(o => o.MessageId == MessageId.None);
            ISpecification<IMessage> lastSpec = new Specification<IMessage>(o => true);

            ISpecification<IMessage> roomReadySpec = new Specification<IMessage>(o => o.MessageId == MessageId.RoomReady);
            ISpecification<IMessage> roomCountdownSpec = new Specification<IMessage>(o => o.MessageId == MessageId.RoomReady);
            ISpecification<IMessage> goToGameSpec = new Specification<IMessage>(o => o.MessageId == MessageId.GoToGame);


            root.SetSpecification(rootSpec);
            last.SetSpecification(lastSpec);

            roomReady.SetSpecification(roomReadySpec);
            roomCountdown.SetSpecification(roomCountdownSpec);
            goToGame.SetSpecification(goToGameSpec);

            root.SetSuccessor(roomReady);
            roomReady.SetSuccessor(roomCountdown);
            roomCountdown.SetSuccessor(goToGame);
            goToGame.SetSuccessor(last);

            root.HandleRequest(message);
        }
    }
}