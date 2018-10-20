using System;
using Core;
using UnityEngine;

namespace Game.Network
{
    public class PhotonManager : SingletonMonoBehaviour<PhotonManager>
    {
        public event Action OnConnectToRoom;
        
        [SerializeField] 
        private Launcher _launcher;

        public override void Awake()
        {
            base.Awake();

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

        private void OnDestroy()
        {
            _launcher.OnLauncherConnectRoom -= OnLauncherConnectRoom;
        }
    }
}