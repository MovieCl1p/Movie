using System;
using Core.UnityUtils;
using ExitGames.Client.Photon;
using Game.Network;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Game.Services
{
    public class NetworkService : INetworkService, IUpdateHandler, IInRoomCallbacks, IOnEventCallback
    {
        public event Action OnConnectToRoom;
        public event Action OnStartGame;
        public event Action<int> OnCountDown;

        private int _secondsToStart = 5;
        private float _counter = 0;

        public bool IsUpdating { get; set; }

        public bool IsRegistered { get; set; }

        public void Init()
        {
            PhotonManager.Instance.OnConnectToRoom += OnConnect;
            PhotonNetwork.AddCallbackTarget(this);
        }
        
        public void Connect()
        {
            PhotonManager.Instance.Connect();
        }

        public void Disconnect()
        {
            PhotonManager.Instance.Disconnect();
        }

        private void OnConnect()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.MaxPlayers = 2;
            }

            if (OnConnectToRoom != null)
            {
                OnConnectToRoom();
            }
        }

        public void OnJoinRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _counter = 0;
                UpdateNotifier.Instance.Register(this);
                return;
            }
        }

        public void OnUpdate()
        {
            _counter += Time.deltaTime;
            if(_counter >= 1.0f)
            {
                _secondsToStart--;
                _counter = 0;

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                SendOptions sendOptions = new SendOptions { Reliability = true };

                PhotonNetwork.RaiseEvent(NetworkEvents.LobbyCountDown, _secondsToStart, raiseEventOptions, sendOptions);
            }

            if (_secondsToStart <= 0)
            {
                UpdateNotifier.Instance.UnRegister(this);

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                SendOptions sendOptions = new SendOptions { Reliability = true };
                PhotonNetwork.RaiseEvent(NetworkEvents.StartGame, null, raiseEventOptions, sendOptions);
            }
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == NetworkEvents.LobbyCountDown)
            {
                if(OnCountDown != null)
                {
                    OnCountDown((int)photonEvent.CustomData);
                }

                return;
            }

            if (photonEvent.Code == NetworkEvents.StartGame)
            {
                if(OnStartGame != null)
                {
                    OnStartGame();
                }

                return;
            }
        }

        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                IsUpdating = false;

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                SendOptions sendOptions = new SendOptions { Reliability = true };
                PhotonNetwork.RaiseEvent(NetworkEvents.StartGame, null, raiseEventOptions, sendOptions);
            }
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {

        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {

        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {

        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {

        }

    }
}