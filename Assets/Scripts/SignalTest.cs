﻿using System;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;


public class SignalTest : MonoBehaviour
{
    
    private HubConnection _hubConnection;
    
    private void Start()
    {
        Debug.Log("Hello");
    }

    [EasyButtons.Button]
    private async void Connect()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7141/WalkersHub")
            .Build();
        
        _hubConnection.On<string>("Ping", message =>
        {
            Debug.Log("Received: " + message);
        });
        
        await _hubConnection.StartAsync();

        await _hubConnection.SendAsync("Send", "my msg");
    }
    
    

}