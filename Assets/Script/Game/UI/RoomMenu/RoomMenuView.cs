using System;
using Core.Binder;
using Core.Dispatcher;
using Core.ViewManager;
using Game.Data;
using Game.Services;
using Game.Services.Network;
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

        [SerializeField]
        private PlayerRoomView _playerPrefab;

        [SerializeField]
        private Transform _list;

        private INetworkService _network;
        
        protected override void Start()
        {
            base.Start();

            //PlayerRoomView playerView = Instantiate(_playerPrefab, _list);
            //playerView.UpdateView(player);

            IDispatcher dispatcher = BindManager.GetInstance<IDispatcher>();
            dispatcher.AddListener(NetworkEventType.GoToGame, OnStartGame);
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
            IDispatcher dispatcher = BindManager.GetInstance<IDispatcher>();
            dispatcher.RemoveListener(NetworkEventType.GoToGame, OnStartGame);
            base.OnReleaseResources();
        }
    }
}