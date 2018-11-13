using System;

namespace Game.Services
{
    public interface INetworkService
    {
        //event Action OnConnectToRoom;
        //event Action OnStartGame;
        //event Action<int> OnCountDown;

        void ConnectAndJoin();

        void Disconnect();

        void Init();
    }
}