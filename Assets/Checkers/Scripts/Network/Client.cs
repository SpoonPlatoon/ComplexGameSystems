using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour
{
    public string clientName;
    public bool isHost;
    public List<GameClient> players = new List<GameClient>();
    public bool socketReady;
    public TcpClient socket;
    public NetworkStream stream;
    public StreamWriter writer;
    public StreamReader reader;
    private void Start()
    {
        // DontDestroyOnLoad
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        // If socket is ready
        if (socketReady)
        {
            // Check if the stream's data is available
            if (stream.DataAvailable)
            {
                // Get the data from reader
                string data = reader.ReadLine();
                // If there is data
                if (data != null)
                {
                    // Call OnIncomingData
                    OnIncomingData(data);
                }
            }
        }
    }
    private void OnApplicationQuit()
    {
        // CloseSocket
        CloseSocket();
    }
    private void OnDisable()
    {
        // CloseSocket
        CloseSocket();
    }
    private void OnIncomingData(string data)
    {
        // Log the packet data
        Debug.Log("Packet data: " + data);
        // Split the packet data to array
        string[] arrayData = data.Split('|');
        // Switch first part of array
        switch (arrayData[0])
        {
            case "SHWO":
                for (int i = 1; i < arrayData.Length - 1; i++)
                {
                    UserConnected(arrayData[1], false);
                }

                break;
            case "SCNN":
                break;
            case "SMOV":
                break;
            case "SMSG":
                break;
            default:
                break;
        }
        // Case SWHO
    }


    private void UserConnected(string name, bool host)
    {

    }
    private void CloseSocket()
    {

    }
    public void ConnectToServer(string host, int port)
    {

    }
    public void Send(string data)
    {

    }
}

