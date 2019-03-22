using System;
using UnityEngine;
using static DiscordRpc;

[Serializable]
public class DiscordJoinEvent : UnityEngine.Events.UnityEvent<string> { }

[Serializable]
public class DiscordSpectateEvent : UnityEngine.Events.UnityEvent<string> { }

[Serializable]
public class DiscordJoinRequestEvent : UnityEngine.Events.UnityEvent<DiscordUser> { }

public class DiscordController : MonoBehaviour
{
    public RichPresence presence = new RichPresence();
    public string applicationId = "535756355395649556";
    public string optionalSteamId;
    public DiscordUser joinRequest;
    public UnityEngine.Events.UnityEvent onConnect;
    public UnityEngine.Events.UnityEvent onDisconnect;
    public UnityEngine.Events.UnityEvent hasResponded;
    public DiscordJoinEvent onJoin;
    public DiscordJoinEvent onSpectate;
    public DiscordJoinRequestEvent onJoinRequest;

    EventHandlers handlers;

    public void RequestRespondYes()
    {
        Debug.Log("Discord: responding yes to Ask to Join request");
        Respond(joinRequest.userId, Reply.Yes);
        hasResponded.Invoke();
    }

    public void RequestRespondNo()
    {
        Debug.Log("Discord: responding no to Ask to Join request");
        Respond(joinRequest.userId, Reply.No);
        hasResponded.Invoke();
    }

    public void ReadyCallback(ref DiscordUser connectedUser)
    {
        Debug.Log(string.Format("Discord: connected to {0}#{1}: {2}", connectedUser.username, connectedUser.discriminator, connectedUser.userId));
        onConnect.Invoke();
    }

    public void DisconnectedCallback(int errorCode, string message)
    {
        Debug.Log(string.Format("Discord: disconnect {0}: {1}", errorCode, message));
        onDisconnect.Invoke();
    }

    public void ErrorCallback(int errorCode, string message)
    {
        Debug.Log(string.Format("Discord: error {0}: {1}", errorCode, message));
    }

    public void JoinCallback(string secret)
    {
        Debug.Log(string.Format("Discord: join ({0})", secret));
        onJoin.Invoke(secret);
    }

    public void SpectateCallback(string secret)
    {
        Debug.Log(string.Format("Discord: spectate ({0})", secret));
        onSpectate.Invoke(secret);
    }

    public void RequestCallback(ref DiscordRpc.DiscordUser request)
    {
        Debug.Log(string.Format("Discord: join request {0}#{1}: {2}", request.username, request.discriminator, request.userId));
        joinRequest = request;
        onJoinRequest.Invoke(request);
    }

    void Start()
    {
    }

    void Update()
    {
        RunCallbacks();
    }

    void OnEnable()
    {
        Debug.Log("Discord: init");
        handlers = new EventHandlers();
        handlers.readyCallback += ReadyCallback;
        handlers.disconnectedCallback += DisconnectedCallback;
        handlers.errorCallback += ErrorCallback;
        handlers.joinCallback += JoinCallback;
        handlers.spectateCallback += SpectateCallback;
        handlers.requestCallback += RequestCallback;
        Initialize(applicationId, ref handlers, true, optionalSteamId);
        RichPresence rp = new RichPresence();
        rp.details = "Campagne";
        rp.largeImageKey = "logo";
        rp.startTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
       /* rp.partyId = "oui";
        rp.partySize = 1;
        rp.partyMax = 6;
        rp.joinSecret = "e7eb30d2ee025ed05c71ea495f770b76454ee4e0";*/
        rp.state = "En jeu";
      
        UpdatePresence(rp);
    }

    void OnDisable()
    {
        Debug.Log("Discord: shutdown");
        Shutdown();
    }

    void OnDestroy()
    {

    }
}