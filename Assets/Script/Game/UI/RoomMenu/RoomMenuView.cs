using System;
using Core.Binder;
using Core.ViewManager;
using Game.Data;
using Game.Services;
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

        private int _secondsToStart = 3;
        
        protected override void Start()
        {
            base.Start();
            
            //PhotonNetwork.OnEventCall += OnEventCall;

            //if (PhotonNetwork.isMasterClient)
            //{
            //    PhotonNetwork.room.MaxPlayers = 2;
            //}
        }

        protected override void OnScheduledUpdate()
        {
            //if (PhotonNetwork.isMasterClient)
            //{
            //    _secondsToStart--;

            //    _countDownLabel.text = _secondsToStart.ToString();
            //    PhotonNetwork.RaiseEvent(7, _secondsToStart, true, RaiseEventOptions.Default);

            //    if (_secondsToStart <= 0)
            //    {
            //        StartGame();
            //    }
            //}
        }

        private void StartGame()
        {
            //UnscheduleUpdate();
            //PhotonNetwork.RaiseEvent(8, _secondsToStart, true, RaiseEventOptions.Default);
            //ViewManager.Instance.SetView(ViewNames.GameView);
        }

        private void OnPlayerConnected(int senderId)
        {
            //Debug.Log("Player connected " + senderId);

            //if (PhotonNetwork.isMasterClient && PhotonNetwork.room.playerCount == PhotonNetwork.room.MaxPlayers)
            //{
            //    ScheduleUpdate(1, true);
            //}
        }

        private void OnEventCall(byte eventCode, object content, int senderId)
        {
            _text.text = string.Format("code: {0}; content: {1}; senderId: {2}", eventCode, content, senderId);

            if (eventCode == 0)
            {
                OnPlayerConnected(senderId);
                return;
            }

            if (eventCode == 7)
            {
                _countDownLabel.text = content.ToString();
                return;
            }

            if (eventCode == 8)
            {
                ViewManager.Instance.SetView(ViewNames.GameView);
                return;
            }
        }
    }
}