using Meebey.SmartIrc4net;
using System;
using System.Collections;
using System.Text;
using System.Threading;
using UnityEngine;

class IRCBot : ScriptableObject
{
    // make an instance of the high-level API
    public static IrcClient irc = new IrcClient();
    public static string username;
    private string encryptedPassword;

    // this method we will use to analyse queries (also known as private messages)
    public void OnChannelMessage(object sender, IrcEventArgs e)
    {
        ChatEvent.Message(e.Data.Channel, e.Data.Nick, e.Data.Message); 

    }

    public void OnQueryMessage(object sender, IrcEventArgs e)
    {
        ChatEvent.PrivateMessage(e.Data.Nick, e.Data.Message);
    }

    // this method will get all IRC messages
    public void OnRawMessage(object sender, IrcEventArgs e)
    {
        //Debug.Log("Received: " + e.Data.RawMessage);
    }

    public void OnChannelActiveSynced(object sender, IrcEventArgs e)
    {
        ChatEvent.Join(e.Data.Channel);
    }

    public void OnChannelPassiveSynced(object sender, IrcEventArgs e)
    {
        ChatEvent.Quit(e.Data.Channel);
    }

    public void Start()
    {


        // UTF-8 test
        irc.Encoding = Encoding.UTF8;

        // wait time between messages, we can set this lower on own irc servers
        irc.SendDelay = 200;

        // we use channel sync, means we can use irc.GetChannel() and so on
        irc.ActiveChannelSyncing = true;

        // here we connect the events of the API to our written methods
        // most have own event handler types, because they ship different data
        irc.OnChannelMessage += new IrcEventHandler(OnChannelMessage);
        irc.OnRawMessage += new IrcEventHandler(OnRawMessage);
        irc.OnChannelActiveSynced += new IrcEventHandler(OnChannelActiveSynced);
        irc.OnChannelPassiveSynced += new IrcEventHandler(OnChannelPassiveSynced);
        irc.OnQueryMessage += new IrcEventHandler(OnQueryMessage);


        string[] serverlist;
        // the server we want to connect to, could be also a simple string
        serverlist = new string[] { "irc.tesseract-game.net" };
        int port = 7000;

        // here we try to connect to the server and exceptions get handled
        irc.Connect(serverlist, port);


        try
        {
            // here we logon and register our nickname and so on 
            irc.Login(username, username, 1, username);
            irc.RfcPrivmsg("Xelia", "IDENTIFY " + encryptedPassword);

            irc.Listen();


            irc.Disconnect();
        }
        catch (ConnectionException)
        {
            // this exception is handled because Disconnect() can throw a not
            // connected exception
        }
        catch (Exception e)
        {
            // this should not happen by just in case we handle it nicely
            System.Console.WriteLine("Error occurred! Message: " + e.Message);
            System.Console.WriteLine("Exception: " + e.StackTrace);
        }
    }

    public static void Send(string channel, string message)
    {
        irc.SendMessage(SendType.Message, channel, message);
    }

    public IRCBot(string name, string password)
    {
        username = name;
        encryptedPassword = sha256(password);
        new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            Start();
        }).Start();
    }

    static string sha256(string password)
    {
        var crypt = new System.Security.Cryptography.SHA256Managed();
        var hash = new System.Text.StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
        foreach (byte theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }
        return hash.ToString();
    }
}