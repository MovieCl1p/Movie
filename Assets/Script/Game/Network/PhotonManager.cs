using System;
using Core;
using UnityEngine;

namespace Game.Network
{
    public class PhotonManager : MonoSingleton<PhotonManager>
    {
        public event Action OnConnectToRoom;
        public event Action OnDisconnectToRoom;

        [SerializeField] 
        private Launcher _launcher;

        private void Awake()
        {
            _launcher.OnLauncherConnectRoom += OnLauncherConnectRoom;
            _launcher.OnLauncherDisconnectRoom += OnLauncherDisconnectRoom;
        }

        private void OnLauncherDisconnectRoom()
        {
            if (OnDisconnectToRoom != null)
            {
                OnDisconnectToRoom();
            }
        }

        public void Connect()
        {
            _launcher.Connect();
        }

        public void Disconnect()
        {
            _launcher.Disconnect();
        }

        private void OnLauncherConnectRoom()
        {
            if (OnConnectToRoom != null)
            {
                OnConnectToRoom();
            }
        }

        protected override void OnReleaseResources()
        {
            _launcher.OnLauncherConnectRoom -= OnLauncherConnectRoom;
            _launcher.OnLauncherDisconnectRoom -= OnLauncherDisconnectRoom;
            base.OnReleaseResources();
        }
    }
}