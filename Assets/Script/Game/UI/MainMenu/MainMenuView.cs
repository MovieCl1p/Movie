using Core.Binder;
using Core.Commands;
using Core.States;
using Core.ViewManager;
using Game.Data;
using Game.Services;
using Game.States;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuView : BaseView, IStateMachineContainer
    {
        [SerializeField] 
        private Button _platBtn;

        private StateMachine _stateMachine;
        private StateFlow _flow;

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

        private INetworkService _network;

        protected override void Start()
        {
            base.Start();

            _stateMachine = new StateMachine(this);
            _flow = new StateFlow(this, _stateMachine);

            _flow.Add(new NextStatePair(typeof(MenuState), typeof(LobbyState)));
            _flow.Add(new NextStatePair(typeof(LobbyState), typeof(GameState)));

            _stateMachine.ApplyState<MenuState>();

            _platBtn.onClick.AddListener(OnPlayClick);
            _network = BindManager.GetInstance<INetworkService>();
        }

        private void OnPlayClick()
        {
            _network.OnConnectToRoom += OnConnectToRoom;
            _network.Connect();
        }

        private void OnConnectToRoom()
        {
            _network.OnConnectToRoom -= OnConnectToRoom;

            ViewManager.Instance.SetView(ViewNames.RoomMenuView);
        }

        protected override void OnReleaseResources()
        {
            _network.OnConnectToRoom -= OnConnectToRoom;
            _platBtn.onClick.RemoveListener(OnPlayClick);
            base.OnReleaseResources();
        }
    }
}