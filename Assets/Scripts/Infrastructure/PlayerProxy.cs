using System;
using Api;
using Core;
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

        private void Start()
        {
            ConnectToHub();
        }

        private void ConnectToHub()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(HubUrl).Build();
            _hubConnection.StartAsync();
        }

        public void CreateGameSession()
        {
            _player = new Player()
            {
                Id = _hubConnection.ConnectionId,
                Role = PlayerRole.Host,
            };
            
            _hubConnection.SendAsync(ServerApi.CreateGameSession, _player);
            _hubConnection.On<string>(nameof(OnGameSessionCreated), OnGameSessionCreated);
        }

        private void OnGameSessionCreated(string gameSessionId)
        {
            
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