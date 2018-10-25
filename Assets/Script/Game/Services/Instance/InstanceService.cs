using Core.Binder;
using ExitGames.Client.Photon;
using Game.Services.PlayerService;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace Game.Services.Instance
{
    public class InstanceService : IInstanceService, IOnEventCallback
    {
        public event Action OnFinishRound;

        private Dictionary<string, string> _map;

        public void StartGame()
        {
            _map = new Dictionary<string, string>();
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void PlayerAnswer(string data)
        {
            IPlayerService playerService = BindManager.GetInstance<IPlayerService>();

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };

            PhotonNetwork.RaiseEvent(NetworkEvents.PlayerAnswer, new string[] { playerService.UserName, data }, raiseEventOptions, sendOptions);
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == NetworkEvents.PlayerAnswer)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    string[] data = (string[])photonEvent.CustomData;
                    RegisterAnswer(data);
                }

                return;
            }

            if (photonEvent.Code == NetworkEvents.FinishRound)
            {
                if(OnFinishRound != null)
                {
                    OnFinishRound();
                }

                return;
            }
        }

        private void RegisterAnswer(string[] data)
        {
            if(_map.ContainsKey(data[0]))
            {

                return;
            }

            _map[data[0]] = data[1];

            if(_map.Count >= PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    SendOptions sendOptions = new SendOptions { Reliability = true };

                    PhotonNetwork.RaiseEvent(NetworkEvents.FinishRound, null, raiseEventOptions, sendOptions);
                }

                _map.Clear();
            }
        }
    }
}
