using System;
using System.Collections;
using Core.Binder;
using Core.Commands;
using Core.Dispatcher;
using Core.States;
using Core.ViewManager;
using Game.Data;
using Game.Services;
using Game.Services.Network;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuView : BaseView
    {
        [SerializeField] 
        private Button _platBtn;

        [SerializeField]
        private ConnectionProgressView _progress;

        private INetworkService _network;

        protected override void Start()
        {
            base.Start();
            _platBtn.onClick.AddListener(OnPlayClick);
        }

        private void OnPlayClick()
        {
            IDispatcher dispatcher = BindManager.GetInstance<IDispatcher>();
            dispatcher.AddListener(NetworkEventType.OnRoomJoin, OnRoomJoin);

            _network = BindManager.GetInstance<INetworkService>();
            _network.ConnectAndJoin();

            HidePlayBtn();
            ShowConnectionProgress(true);
        }

        private void ShowConnectionProgress(bool show)
        {
            _progress.gameObject.SetActive(show);
            _progress.Activate(show);
        }

        private void HidePlayBtn()
        {
            _platBtn.gameObject.SetActive(false);
        }

        private void OnRoomJoin()
        {
            ShowConnectionProgress(false);
            ViewManager.Instance.SetView(ViewNames.RoomMenuView);
        }

        protected override void OnReleaseResources()
        {
            IDispatcher dispatcher = BindManager.GetInstance<IDispatcher>();
            dispatcher.RemoveListener(NetworkEventType.OnRoomJoin, OnRoomJoin);

            _platBtn.onClick.RemoveListener(OnPlayClick);
            base.OnReleaseResources();
        }
    }
}