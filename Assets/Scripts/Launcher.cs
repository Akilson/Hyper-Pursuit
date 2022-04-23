using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
	public static Launcher Instance;
	private static Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

	[SerializeField] TMP_InputField roomNameInputField;
	[SerializeField] TMP_Text errorText;
	[SerializeField] TMP_Text roomNameText;
	[SerializeField] Transform roomListContent;
	[SerializeField] GameObject roomListItemPrefab;
	[SerializeField] Transform playerListContent;
	[SerializeField] GameObject PlayerListItemPrefab;
	[SerializeField] GameObject startGameButton;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Debug.Log("Connecting to Masterserver");
		//Connect to the photon server using the settings defined in unity (in our case we have only change the region)
		PhotonNetwork.ConnectUsingSettings();
	}

	//it is called when we are finally connected to the master (server)
	//it is a callback called by photon when we successfully connect to the master server
	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to Master");
		//the place where we create or join a room
		PhotonNetwork.JoinLobby();
		//use to apply to all the scene modification on the host to all the clients
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	//it is called when we joined the lobby
	public override void OnJoinedLobby()
	{
		MenuManager.Instance.OpenMenu("title");
		Debug.Log("Joined Lobby");
		PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
	}
	//fn used whith the CreateRoom menu
	public void CreateRoom()
	{
		if(string.IsNullOrEmpty(roomNameInputField.text))
		{
			return;
		}
		//RoomOptions options = new RoomOptions();
		//options.MaxPlayers = 1;
		//ask to the photon server to create a new room
		PhotonNetwork.CreateRoom(roomNameInputField.text);//,options); //When we create a room there is 2 possible callbacks: either we successfully create and so join a room(OnJoinedRoom) or  we failed to create a room(OnCreateRoomFailed)
		//We open again the loading scene because it takes some time to create the room
		MenuManager.Instance.OpenMenu("loading");
	}
	//it is called when we succed to join a room
	public override void OnJoinedRoom()
	{
		//open the menu "room"
		MenuManager.Instance.OpenMenu("room");
		//set in the text field of room the current name of the created room
		roomNameText.text = PhotonNetwork.CurrentRoom.Name;
		//put in the players list all the players in the room
		Player[] players = PhotonNetwork.PlayerList;
		//if we don't have this each time we leave and create a new room the player are stacking and not destroyed
		foreach(Transform child in playerListContent)
		{
			Destroy(child.gameObject);
		}

		for(int i = 0; i < players.Count(); i++)
		{
			Debug.Log("player instantiated");
			//instantiate PlayerListItemPrefab in playerListContent and setup the info of the players
			Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
		}
		//the start button is active if only we are the host of the server
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}
	//call when the host leaves the room
	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		//the start button is only accessible to the new host
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room Creation Failed: " + message;
		Debug.LogError("Room Creation Failed: " + message);
		MenuManager.Instance.OpenMenu("error");
	}

	public void StartGame()
	{
		PhotonNetwork.LoadLevel(1);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		MenuManager.Instance.OpenMenu("loading");
	}
	//fn to that try to join the room
	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
		MenuManager.Instance.OpenMenu("loading");
	}

	//called when the room is finally lefted
	public override void OnLeftRoom()
	{ 
		MenuManager.Instance.OpenMenu("title");
		cachedRoomList.Clear();
	}
	//its i called each time the roomlist is updated
	public override void OnRoomListUpdate(List<RoomInfo> roomList) //the it is a list of info where each elt of the list corresponds to the infos of a player
	{
		foreach(Transform trans in roomListContent)
		{
			Destroy(trans.gameObject);
		}

		for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
		foreach (KeyValuePair<string, RoomInfo> entry in cachedRoomList)
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(cachedRoomList[entry.Key]);
        }
	}
	//it is called when another player enters the room
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
	}

	public void QuitGame()
    {
        Application.Quit();
    }
}
