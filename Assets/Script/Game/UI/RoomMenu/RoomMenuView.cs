using System;
using Core.Binder;
using Core.ViewManager;
using Game.Data;
using Game.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.RoomMenu
{
    public class RoomMenuView : BaseView
    {
        [SerializeField] 
        private Text _countDownLabel;
        
        [SerializeField] 
        private Text _text;

        private INetworkService _network;
        
        protected override void Start()
        {
            base.Start();

            _network = BindManager.GetInstance<INetworkService>();
            _network.OnCountDown += OnCountDown;
            _network.OnStartGame += OnStartGame;

            _network.OnJoinRoom();
        }

        private void OnStartGame()
        {
            ViewManager.Instance.SetView(ViewNames.GameView);
        }

        private void OnCountDown(int value)
        {
            _countDownLabel.text = value.ToString();
        }

        protected override void OnReleaseResources()
        {
            _network.OnCountDown -= OnCountDown;
            _network.OnStartGame -= OnStartGame;
            base.OnReleaseResources();
        }
    }
}