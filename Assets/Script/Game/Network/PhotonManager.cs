using System;
using Core;
using UnityEngine;

namespace Game.Network
{
    public class PhotonManager : MonoSingleton<PhotonManager>
    {
        public event Action OnConnectToRoom;
        
        [SerializeField] 
        private Launcher _launcher;

        private void Awake()
        {
            _launcher.OnLauncherConnectRoom += OnLauncherConnectRoom;
        }

        public void Connect()
        {
            
            _launcher.Connect();
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
        }
    }
}