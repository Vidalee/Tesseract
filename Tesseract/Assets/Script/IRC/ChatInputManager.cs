using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatInputManager : MonoBehaviour, ChatEventListener
{
    public bool inputAlwaysVisible;

    public static bool isTyping = false;
    public InputField inputField;
    public Text channelInfo;
    //public Scrollbar scrollbar;
    public static List<string> channels = new List<string>();
    int lastChan = 0;

    public string username;

    public int maxMessages = 50;

    public GameObject chatPanel, textObject;

    List<Message> messageList = new List<Message>();
    List<string> toAdd = new List<string>();

    public void OnMessage(string channel, string sender, string message)
    {
        if (channel.Length == 7 && channel.ToUpper() == channel) channel = "Party Chat";
        toAdd.Add("[" + channel + "] " + sender + ": " + message);
    }

    public void OnJoin(string channel)
    {
        if (channel != "#announcements")
            channels.Add(channel);
        toAdd.Add("<color=red>Joined " + channel + "</color>");
        lastChan = channels.Count - 1;
    }

    public void OnPrivateMessage(string user, string message)
    {
        toAdd.Add("<color=lightblue><-" + user + ": " + message + "</color>");

        if (user != "Xelia" && message != "Successfully connected.")
        {
            channels.Add(user);
        }
    }

    public void OnQuit(string channel)
    {
        if (channel == "End") return;
        toAdd.Add("<color=red>Left " + channel + "</color>");
        channels.Remove(channel);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!inputAlwaysVisible)
        {
            inputField.gameObject.SetActive(false);
            channelInfo.gameObject.SetActive(false);
        }
        else channelInfo.gameObject.SetActive(true);

        username = IRCBot.username;
        ChatEvent.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        //ChatUpdate
        foreach (string s in toAdd)
        {
            SendMessageToChat(s);
        }
        toAdd.Clear();
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.RemoveAt(0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                if (!inputAlwaysVisible)
                {
                    inputField.gameObject.SetActive(false);
                    channelInfo.gameObject.SetActive(false);
                }

                if (inputField.text == "") return;

                string message = inputField.text;
                if (message == "/tableflip") message = "(╯°□°）╯︵ ┻━┻";
                inputField.text = "";
                IRCBot.Send(channels[lastChan], message);
                string chan = channels[lastChan];
                if (chan.Length == 7 && chan.ToUpper() == chan) chan = "Party";

                if (chan.StartsWith("#"))
                    SendMessageToChat("[" + chan + "] " + username + ": " + message);
                else
                    SendMessageToChat("<color=lightblue>->" + chan + ": " + message + "</color>");

            }
            else
            {
                channelInfo.gameObject.SetActive(true);
                channelInfo.text = "Send to " + channels[lastChan];
                inputField.gameObject.SetActive(true);
                inputField.Select();
                inputField.ActivateInputField();
            }
            isTyping = !isTyping;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && isTyping)
        {
            lastChan++;
            if (lastChan >= channels.Count) lastChan = 0;
            channelInfo.text = "Send to " + channels[lastChan];
        }
    }

    public void SendMessageToChat(string text)
    {
        text = "" + text;
        Message newMessage = new Message();
        newMessage.text = text;
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = text;
        messageList.Add(newMessage);
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}

