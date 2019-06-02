using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPSocket
{
    private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    private const int bufSize = 8 * 1024;
    private State state = new State();
    private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
    private AsyncCallback recv = null;
    private List<string> toSend = new List<string>();
    private string lastS = "";
    private string lastR = "";

    public class State
    {
        public byte[] buffer = new byte[bufSize];
    }

    public void resetLastR() => lastR = "";
    public void Server(string address, int port)
    {
        _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
        _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
        Receive();
    }

    public void Client(string address, int port)
    {
        new Thread(() =>
        {
            _socket.Connect(IPAddress.Parse(address), port);
            Receive();

        }).Start();

        new Thread(() =>
        {
            while (true)
            {
                Thread.Sleep(1000);
                lastS = "";
            }

        }).Start();

        new Thread(() =>
        {
            while (true)
            {
                //Thread.Sleep(3);
                if (toSend.Count != 0)
                {
                    
                    string text = toSend[0];
                    toSend.RemoveAt(0);
                    if(lastS != text)
                    {
                        lastS = text;
                        byte[] data = Encoding.ASCII.GetBytes(text);
                        _socket.Send(data, 0, data.Length, SocketFlags.None);
                        _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
                        {

                            State so = (State)ar.AsyncState;
                            int bytes = _socket.EndSend(ar);
                            Debug.Log("SEND: " + text);
                        }, state);
                    } 
                }
            }
        }).Start();


    }

    public void Send(string text)
    {
        if (text == "PING")
            Thread.Sleep(30);
        toSend.Add(text);
    }

    private void Receive()
    {
        _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
        {
            State so = (State)ar.AsyncState;
            int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
            _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
            Console.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));
            string msg = Encoding.ASCII.GetString(so.buffer, 0, bytes);
            if (lastR == msg) return;
            lastR = msg;
            Debug.Log("RAW RECEIVE: " + msg);
            UDPEvent.Receive(msg);
            if (msg.StartsWith("SET"))
            {
                
                string[] args = msg.Split(' ');
                if (args.Length != 3) return;
                if (Coffre.Existe(args[0])) Coffre.Vider(args[0]);
                Coffre.Remplir(args[1], args[2]);
            }
        }, state);

    }
}