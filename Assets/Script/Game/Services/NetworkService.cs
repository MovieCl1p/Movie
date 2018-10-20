using System;
using Game.Network;
using UnityEngine;

namespace Game.Services
{
    public class NetworkService : INetworkService
    {
        public event Action OnConnectToRoom;

        public void Init()
        {
            PhotonManager.Instance.OnConnectToRoom += OnConnect;
        }
        
        public void Connect()
        {
            PhotonManager.Instance.Connect();
        }

        private void OnConnect()
        {
            if (OnConnectToRoom != null)
            {
                OnConnectToRoom();
            }
            
            //PhotonNetwork.RaiseEvent(0, "Join room", true, RaiseEventOptions.Default);
        }
    }
}