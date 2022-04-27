using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
	[SerializeField] TMP_Text text;
	[SerializeField] Menu error;

	public RoomInfo info;

	public void SetUp(RoomInfo _info)
	{
		info = _info;
		if (info.PlayerCount >= 2)
		{
			text.text = "room is full";
			info.RemovedFromList = true;
		}
		else
			text.text = _info.Name;
	}

	public void OnClick()
	{
		if (info.PlayerCount < 2)
		{
			Launcher.Instance.JoinRoom(info);
		}
	}
}