using Core.Binder;
using Core.ViewManager;
using Game.Data;
using Game.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class MainMenuView : BaseView
    {
        [SerializeField] 
        private Button _platBtn;

        private INetworkService _network;

        protected override void Start()
        {
            base.Start();
            
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