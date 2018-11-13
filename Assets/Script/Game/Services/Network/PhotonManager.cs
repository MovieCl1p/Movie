using Core;
using Core.Binder;
using Core.Dispatcher;
using Photon.Pun;

namespace Game.Services.Network
{
    public class PhotonManager : MonoBehaviourPunCallbacks
    {
        private string _gameVersion = "1";

        private static PhotonManager _instance;

        public static PhotonManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        public void ConnectAndJoinRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                JoinRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = _gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            MonoLog.Log(MonoLogChannel.MultiPlayer, "OnConnectedToMaster: Next -> try to Join Random Room");
            JoinRoom();
        }

        private void JoinRoom()
        {
            PhotonNetwork.JoinRoom("default");
        }

        public override void OnJoinedRoom()
        {
            IDispatcher dispatcher = BindManager.GetInstance<IDispatcher>();
            dispatcher.Dispatch(NetworkEventType.OnRoomJoin);
        }
    }
}