using System;
using System.Data.Common;
using Api;
using Core;
using Core.DataModels;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

namespace Infrastructure
{
    public class PlayerProxy : MonoBehaviour
    {
        private const string HubUrl = "https://localhost:7141/WalkersHub";
        private HubConnection _hubConnection;

        private Player _player;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        

        [EasyButtons.Button]
        public async void ConnectToHub()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(HubUrl).Build();
            await _hubConnection.StartAsync();
            
            Debug.Log("Connected");
        }

        [EasyButtons.Button]
        public async void CreateGameSession()
        {
            _player = new Player()
            {
                Id = _hubConnection.ConnectionId,
                Role = PlayerRole.Host,
            };
            
            _hubConnection.On<string>(nameof(OnGameSessionCreated), OnGameSessionCreated);

            _hubConnection.On<string>(nameof(OnGameSessionCreated), OnGameSessionCreated);
            
            await _hubConnection.SendAsync(ServerApi.CreateGameSession, new PlayerDataModel()
            {
                Id = _player.Id,
                Role = _player.Role,
            });
            
            Debug.Log("Sent");
        }

        private void OnGameSessionCreated(string gameSessionId)
        {
             Debug.Log("On game session created: " + gameSessionId);
        }

        private void OnGameSessionStarted(string gameSessionId)
        {
            Debug.Log("On game session created: " + gameSessionId);
        }
        
        public void JoinGameSession()
        {
            _player = new Player()
            {
                Id = _hubConnection.ConnectionId,
                Role = PlayerRole.Client,
            };
            
        }

    }
}