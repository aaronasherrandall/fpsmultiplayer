using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

/// <summary>
/// MonoBehaviourPunCallbacks gives us access to callbacks for room creation, errors, joining lobbies, etc.
/// </summary>
public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to Master Server");
        //Connect to master server using settings in Photon Server Settings SO
        PhotonNetwork.ConnectUsingSettings();
    }

    // A callback called by Photon when we succesfully connect to the master server
	public override void OnConnectedToMaster()
	{
        Debug.Log("Connected to Master Server");
        //Lobbies are where we find and create rooms
        PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined Lobby");
	}

    public void CreateRoom()
	{
        //This is to make sure that we enter a name for a room before we create one
        //So we don't just create a room with no name
        if(string.IsNullOrEmpty(roomNameInputField.text))
		{
            return;
		}
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        
        //Do the following to stop users from clicking other buttons while photon is creating room
        MenuManager.Instance.OpenMenu("loading");
	}

	public override void OnJoinedRoom()
	{
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");
	}
    
    public void LeaveRoom()
	{
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
	}

    public override void OnLeftRoom()
	{
        MenuManager.Instance.OpenMenu("title");
	}
}
