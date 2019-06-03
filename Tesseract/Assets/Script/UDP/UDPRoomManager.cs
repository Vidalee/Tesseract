using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UDPRoomManager : MonoBehaviour, UDPEventListener
{
    public Button refreshButton, playButton;
    public GameObject roomPanel, canvasObject;
    public InputField inputRoomName, inputSeed;
    public Text currentPartyText;

    private static string currentCode = "";
    private List<RoomInfo> rooms = new List<RoomInfo>();
    private List<GameObject> cList = new List<GameObject>();

    private List<string> toAdd = new List<string>();
    private bool start = false;
    
    // Start is called before the first frame update
    public static UDPSocket _socket;


    static bool refresh, joined;
    public void OnReceive(string text)
    {
        string[] args = text.Split(' ');
        if (text == "CPASS") refresh = true;
        if (text.StartsWith("RINFO")) toAdd.Add(text.Substring(6));
        if (args[0] == "PINFO" && args[2] == "START") start = true;
    }

    void Start()
    {
        Coffre.Remplir("mode", "multi");

        playButton.gameObject.SetActive(false);
        _socket = UDPClient._socket;
        UDPEvent.Register(this);
        refresh = true;
    }

    public static void Join(string code)
    {
        joined = true;
        if(currentCode != "") ChatEvent.Quit("#" + currentCode);
        _socket.Send("QUIT " + currentCode);
        _socket.Send("JOIN " + code);
        currentCode = code;
        refresh = true;
    }

    public void Create()
    {
        if(inputRoomName.text != "")
        {
            if (inputSeed.text == "") inputSeed.text = "" + UnityEngine.Random.Range(1, 999999999);
            _socket.Send("CREATE " + inputSeed.text + " " + inputRoomName.text);
            refresh = true;
        }
    }

    public void Refresh()
    {
        refresh = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            SceneManager.LoadScene("MultiGame");
        }
        if (refresh)
        {
            _socket.resetLastR();
            refresh = false;

            foreach (GameObject c in cList) Destroy(c);
            _socket.Send("");
            _socket.Send("LIST");
        }
        
        if (joined)
        {
            joined = false;
            playButton.gameObject.SetActive(true);
            currentPartyText.text = "Current: " + rooms.Where(r => r.code == currentCode).First().name;
        }

        foreach (string rinfo in toAdd)
        {
            string[] args = rinfo.Split(' ');
            RoomInfo ri = new RoomInfo();
            ri.code = args[0];
            int p = int.Parse(args[1]);


            ri.p = p;
            ri.desc = "(" + p + "/4)";
            for (int i = 2; i < 2 + p; i++) ri.desc += " " + args[i];
            for (int i = 2 + p; i < args.Length; i++) ri.name += args[i] + " ";

            GameObject canvas = Instantiate(canvasObject, roomPanel.transform);
            canvas.name = ri.code;
            ri.nameObject = canvas.GetComponentsInChildren<Text>()[1];
            ri.descObject = canvas.GetComponentsInChildren<Text>()[0];
            ri.nameObject.text = ri.name;
            ri.descObject.text = ri.desc;
            cList.Add(canvas);
            rooms.Add(ri);
        }
        toAdd.Clear();
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


[System.Serializable]
public class RoomInfo
{
    public string code;
    public string name = "";
    public Text nameObject;
    public string desc;
    public Text descObject;
    public int p;
}
