using System;

namespace Game.Services
{
    public interface INetworkService
    {
        event Action OnConnectToRoom;
        
        void Connect();
        
        void Init();
    }
}