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

    public class State
    {
        public byte[] buffer = new byte[bufSize];
    }


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
    }

    public void Send(string text)
    {
        Thread.Sleep(50);
        byte[] data = Encoding.ASCII.GetBytes(text);
        _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
        {
            State so = (State)ar.AsyncState;
            int bytes = _socket.EndSend(ar);
            Console.WriteLine("SEND: {0}, {1}", bytes, text);
        }, state);
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