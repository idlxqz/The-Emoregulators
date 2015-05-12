using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;

public class OpenSignalsSocket : MonoBehaviour
{
    public String host = "127.0.0.1";
    public Int32 port = 30000;

    internal Boolean socket_ready = false;
    internal String input_buffer = "";
    TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;

    void Update()
    {
        string received_data = readSocket();

        //writeSocket("Unity Echo");
        if (received_data != "")
        {
        	// Do something with the received data,
        	// print it in the log for now
            Debug.Log(received_data);
            var splitData = received_data.Split(new[]{':'}, StringSplitOptions.RemoveEmptyEntries);
            var messageType = splitData[0];
            if (messageType.Equals("HR"))
            {
                var hearthRate = float.Parse(splitData[1]);
                if (hearthRate >= 50 && hearthRate <= 220)
                {
                    SessionManager.HeartRate = Convert.ToInt32(hearthRate);
                }
            }
            else if (messageType.Equals("EMG1"))
            {
                var muscleActive = bool.Parse(splitData[1]);
                SessionManager.IsMuscleActive = muscleActive;
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

            socket_ready = true;
        }
        catch (Exception e)
        {
        	// Something went wrong
            Debug.Log("Socket error: " + e);
        }
    }

    public void writeSocket(string line)
    {
        if (!socket_ready)
            return;
            
        line = line + "\r\n";
        socket_writer.Write(line);
        socket_writer.Flush();
    }

    public String readSocket()
    {
        if (!socket_ready)
            return "";

        if (net_stream.DataAvailable)
            return socket_reader.ReadLine();

        return "";
    }

    public void closeSocket()
    {
        if (!socket_ready)
            return;

        socket_writer.Close();
        socket_reader.Close();
        tcp_socket.Close();
        socket_ready = false;
    }
}