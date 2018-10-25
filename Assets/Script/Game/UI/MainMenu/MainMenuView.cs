using System;
using System.Collections;
using Core.Binder;
using Core.Commands;
using Core.States;
using Core.ViewManager;
using Game.Data;
using Game.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuView : BaseView, IStateMachineContainer
    {
        [SerializeField] 
        private Button _platBtn;

        [SerializeField]
        private ConnectionProgressView _progress;

        private StateMachine _stateMachine; 
        private StateFlow _flow;

        private INetworkService _network;

        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }
        }

        public void Next(StateCommand previousState)
        {
            _flow.Next(previousState);
        }

        protected override void Start()
        {
            base.Start();

            //_stateMachine = new StateMachine(this);
            //_flow = new StateFlow(this, _stateMachine);

            //_flow.Add(new NextStatePair(typeof(MenuState), typeof(LobbyState)));
            //_flow.Add(new NextStatePair(typeof(LobbyState), typeof(GameState)));

            //_stateMachine.ApplyState<MenuState>();

            _platBtn.onClick.AddListener(OnPlayClick);
            
        }

        private void OnPlayClick()
        {
            _network = BindManager.GetInstance<INetworkService>();
            _network.OnConnectToRoom += OnConnectToRoom;
            _network.Connect();

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

        private void OnConnectToRoom()
        {
            ShowConnectionProgress(false);

            _network.OnConnectToRoom -= OnConnectToRoom;
            ViewManager.Instance.SetView(ViewNames.RoomMenuView);
        }

        protected override void OnReleaseResources()
        {
            _platBtn.onClick.RemoveListener(OnPlayClick);
            _network.OnConnectToRoom -= OnConnectToRoom;
            base.OnReleaseResources();
        }
    }
}