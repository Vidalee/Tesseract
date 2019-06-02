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
    private List<string> pinfos = new List<string>();
    private string lastS = "";
    private string lastR = "";

    private TcpClient client;
    private NetworkStream ns;

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
            const int PORT_NO = 27000;
            const string SERVER_IP = "127.0.0.1";
            //---data to send to the server---

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            NetworkStream nwStream = client.GetStream();
            while (true)
            {
                this.client = client;
                this.ns = nwStream;

                //---read back the text---
                byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                string msg = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                string[] p = msg.Split(';');
                foreach(string m in p) {
                    if (m == "") continue;
                    Debug.Log("RAW RECEIVE: " + m);
                    UDPEvent.Receive(m);
                    if (m.StartsWith("SET"))
                    {

                        string[] args = m.Split(' ');
                        if (args.Length != 3) return;
                        if (Coffre.Existe(args[0])) Coffre.Vider(args[0]);
                        Coffre.Remplir(args[1], args[2]);

                    }
                }
            }
            client.Close();



        }).Start();


    }

    public void Send(string text)
    {
        if(text.Contains("PINFO TP"))
        {
            if (pinfos.Contains(text)) return;
            else pinfos.Add(text);
        }
        text += ";";
        if (text == "PING;")
            Thread.Sleep(300);
        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(text);

        //---send the text---
        Console.WriteLine("Sending : " + text);
        ns.Write(bytesToSend, 0, bytesToSend.Length);
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