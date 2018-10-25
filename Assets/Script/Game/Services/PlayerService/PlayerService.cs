using System;

namespace Game.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private string _userName;

        public string UserName
        {
            get
            {
                return _userName;
            }
        }

        public void CreateUser()
        {
            _userName = Guid.NewGuid().ToString();
        }
    }
}
