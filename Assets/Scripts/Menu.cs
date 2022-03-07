using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public string menuName;
	//we use the bool for the menumanager
	public bool open;

	public void Open()
	{
		open = true;
		gameObject.SetActive(true);
	}

	public void Close()
	{
		open = false;
		gameObject.SetActive(false);
	}
}