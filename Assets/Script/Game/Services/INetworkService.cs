using System;

namespace Game.Services
{
    public interface INetworkService
    {
        event Action OnConnectToRoom;
        event Action OnStartGame;
        event Action<int> OnCountDown;

        void Connect();

        void Disconnect();

        void Init();

        void OnJoinRoom();
    }
}