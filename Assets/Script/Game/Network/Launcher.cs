using System;
using Core;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Game.Network
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
	    public event Action OnLauncherConnectRoom;
	    public event Action OnLauncherDisconnectRoom;
	    
        //Need to be in global settings
        private string _gameVersion = "1";
        
        private bool _isConnecting = true;
	    private byte _maxPlayersPerRoom = 4;

	    private void Awake()
        {
            // #Critical
            // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
            //PhotonNetwork.autoJoinLobby = false;

            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        
        public void Connect()
        {
            _isConnecting = true;
            
            if(PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = _gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }

        }
        
        public override void OnConnectedToMaster()
        {
            
            if (_isConnecting)
            {
                Debug.Log("OnConnectedToMaster: Next -> try to Join Random Room");
                PhotonNetwork.JoinRandomRoom();
            }
        }
        
        public override void OnJoinRandomFailed(short returnCode, string message)
		{
			Debug.Log("<Color=Red>OnPhotonRandomJoinFailed</Color>: Next -> Create a new Room");

			// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = _maxPlayersPerRoom });
		}


        public override void OnDisconnected(DisconnectCause cause)
		{
			Debug.Log("<Color=Red>OnDisconnectedFromPhoton</Color>");

			_isConnecting = false;
			if (OnLauncherDisconnectRoom != null)
			{
				OnLauncherDisconnectRoom();
			}
		}

		public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running.");

            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
			{
				// #Critical
				// Load the Room Level. 
				//PhotonNetwork.LoadLevel("PunBasics-Room for 1");
			}

			if (OnLauncherConnectRoom != null)
			{
				OnLauncherConnectRoom();
			}
		}
    }
}