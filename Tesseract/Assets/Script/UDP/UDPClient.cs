using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UDPClient : MonoBehaviour, UDPEventListener
{
    public string ip;

    public Text error;
    public Button loginButton;

    public InputField nameInputfield;
    public InputField passwordInputfield;

    // Start is called before the first frame update
    public static UDPSocket _socket = new UDPSocket();

    bool connected = false;
    bool wpass = false;
    bool cpass = false;
    public void OnReceive(string text)
    {
        Debug.Log(text);
        if(text == "PONG") connected = true;
        if (text == "WPASS") wpass = true;
        if (text == "CPASS") cpass = true;
        
    }

    void Start()
    {
        Coffre.Créer();

        UDPEvent.Register(this);
        error.text = "Server not reachable";
        //loginButton.enabled = false;
        loginButton.gameObject.SetActive(false);

        _socket.Client(ip, 27000);
       
        _socket.Send("PING");

    }

    public void Login()
    {
        _socket.Send("CONNECT " + nameInputfield.text + " " + passwordInputfield.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (connected)
        {
            error.gameObject.SetActive(false);
            loginButton.gameObject.SetActive(true);
        }

        if (wpass)
        {
            error.gameObject.SetActive(true);
            error.text = "Wrong password";
        }

        if (cpass)
        {
            new IRCBot(nameInputfield.text, passwordInputfield.text);
            SceneManager.LoadScene("Rooms");
        }
    }

    string sha256(string password)
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
