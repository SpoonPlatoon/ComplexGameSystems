using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Server : MonoBehaviour
{
    public int port;
    private List<ServerClient> clients = new List<ServerClient>();
    private List<ServerClient> disconnectList = new List<ServerClient>();
    private TcpListener server;
    private bool serverStarted;
    private void Update()
    {
        // Is the server not started?
        // return
        // Loop through entire list of clients
        // Is the client still connected?
        // Get the client's network stream
        // Check if data is available on the stream
        // Setup a reader for the stream
        // Read the data from the reader of the stream
        // If there is any data
        // Give the client & it's data to the method
        // else ... the client disconnected
        // Close the client's tcp protocol
        // Add to list of disconnected
        // Loop through all the disconnected clients
        // Tell our player somebody has disconnected
        // Clear the disconnected list for another time
    }

    // Runs command to continue listening for a tcp clien to connect
    private void StartListening()
    {
        // Begin Accepting Tcp Client
    }
    // Callback method for listening for tcp clients
    private void AcceptTcpClient(IAsyncResult result)
    {
        // Get the listener
        // Loop through all currently connected clients
        // Append with client name
        // Get the connected client from the listener
        // Add newly connected clien to the list
        // Continue listening for more clients
        // Broadcast to all clients that there is a newly connected client
    }
    // Check if a TcpClient is connected
    private bool IsConnected(TcpClient client)
    {
        return false;
    }

    // Broadcast data to a list of incomingClients
    private void Broadcast(string data, List<ServerClient> incomingClients)
    {
        // Loop through all incoming clients from broadcast
        // Try sending the data to the client
        // Get a writer specifically for the current client
        // Send the data to client with writer
        // Flush the writer when done
        // catch exception e ... the data couldn't be sent
        // Print the error message
    }

    // Broadcast data to a single client - This function exists for simplicity (less syntax)
    private void Broadcast(string data, ServerClient incomingClient)
    {
        // Create a list containing the individual client
        // Broadcast to that list of the client
    }
    private void OnIncomingData(ServerClient client, string data)
    {
        /* Client Commands:
             * CWHO - Client who connected
             * CMOV - Movement data from client
             * CMSG - Message from the client
             * 
             * Server Commands:
             * SCNN - Server new connection
             * SMOV - Server movement broadcast
             * SMSG - Server message broadcast
             */

        // NOTE: The data could be thought of as the "packet"

        // Split the data with in-line | (poles)

        // Switch the header of the packet
        // Client connected. Syntax - "CWHO | clientName | isHost"
        // Get the client's name
        // Check if the client is a host
        // Broadcast the new client to all other clients

        // Client has made a move. Syntax - "CMOV | x1 | y1 | x2 | y2"
        //                                           |   |     |   |
        //                                            \ /       \ /
        //                                       Start Drag   End drag
        // Send this data to all clients
        // Chat message. Syntax - "CMSG | chatMessage" 
    }
    public void Init()
    {
        // DontDestroyOnLoad
        // try
        // set server to new TcpListener(IPAddress.Any, port)
        // start server (server.Start())
        // Call StartListening()
        // set serverStarted to true
        // catch exception e
        // Log Error "Socket error: " + e.Message
    }
}

