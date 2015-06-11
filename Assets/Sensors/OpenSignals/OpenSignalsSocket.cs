using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;

public class OpenSignalsSocket : MonoBehaviour
{
    public String host = "127.0.0.1";
    public Int32 port = 30000;

    public bool SocketReady { get; private set; } 
    internal String input_buffer = "";
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;

    public OpenSignalsSocket()
    {
        this.SocketReady = false;
    }

    void Update()
    {
        string received_data = readSocket();

        //writeSocket("Unity Echo");
        if (received_data != "")
        {
        	// Do something with the received data,
            var splitData = received_data.Split(new[]{':'}, StringSplitOptions.RemoveEmptyEntries);
            var messageType = splitData[0];
            if (messageType.Equals("HR"))
            {
                var hearthRate = float.Parse(splitData[1]);
                if (hearthRate >= 50 && hearthRate <= 220)
                {
                    SensorManager.HeartRate = Convert.ToInt32(hearthRate);
                }
            }
            else if (messageType.Equals("EMG1"))
            {
                var muscle1Active = bool.Parse(splitData[1]);
                //if the muscle is updated we need to log the change in muscle activation
                if (muscle1Active != SensorManager.Muscle1Active)
                {
                    SensorManager.Muscle1Active = muscle1Active;
                    if (muscle1Active)
                    {
                        Logger.Instance.LogInformation("Muscle1 Activated.");
                    }
                    else
                    {
                        Logger.Instance.LogInformation("Muscle1 Relaxed.");
                    }
                }
                
            }
            else if (messageType.Equals("EMG2"))
            {
                var muscle2Active = bool.Parse(splitData[1]);
                //if the muscle is updated we need to log the change in muscle activation
                if (muscle2Active != SensorManager.Muscle2Active)
                {
                    SensorManager.Muscle2Active = muscle2Active;
                    if (muscle2Active)
                    {
                        Logger.Instance.LogInformation("Muscle2 Activated.");
                    }
                    else
                    {
                        Logger.Instance.LogInformation("Muscle2 Relaxed.");
                    }
                }

            }
            else if (messageType.Equals("EDA"))
            {
                var eda = int.Parse(splitData[1]);
                SensorManager.EDA = eda;
            }
            
        }
    }

    void Awake()
    {
        setupSocket();
        DontDestroyOnLoad(this);
    }

    void OnApplicationQuit()
    {
        closeSocket();
    }

    public void setupSocket()
    {
        try
        {
            tcp_socket = new TcpClient(host, port);

            net_stream = tcp_socket.GetStream();
            socket_writer = new StreamWriter(net_stream);
            socket_reader = new StreamReader(net_stream);

            this.SocketReady = true;
        }
        catch (Exception e)
        {
        	// Something went wrong
            Debug.Log("Socket error: " + e);
        }
    }

    public void writeSocket(string line)
    {
        if (!this.SocketReady)
            return;
            
        line = line + "\r\n";
        socket_writer.Write(line);
        socket_writer.Flush();
    }

    public String readSocket()
    {
        if (!this.SocketReady)
            return "";

        if (net_stream.DataAvailable)
            return socket_reader.ReadLine();

        return "";
    }

    public void closeSocket()
    {
        if (!this.SocketReady)
            return;

        socket_writer.Close();
        socket_reader.Close();
        tcp_socket.Close();
        this.SocketReady = false;
    }
}