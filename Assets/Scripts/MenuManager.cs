using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	//we create this variable to have access to the fn of the menumanager everywhere without the need to have a reference of it
	public static MenuManager Instance;

	[SerializeField] Menu[] menus;

	void Awake()
	{
		Instance = this;
	}

	public void OpenMenu(string menuName)
	{
		for(int i = 0; i < menus.Length; i++)
		{
			// if the menu we are trying to open exist in the menu list
			if(menus[i].menuName == menuName)
			{
				//call the fn open from the class Menu created in Menu.cs
				//open the i-th menu in the list menus that correspond to the menu with the name string menuName      
				menus[i].Open();
			}
			else if(menus[i].open)
			{
				CloseMenu(menus[i]);
			}
		}
	}

	public void OpenMenu(Menu menu)
	{
		for(int i = 0; i < menus.Length; i++)
		{
			if(menus[i].open)
			{
				CloseMenu(menus[i]);
			}
		}
		menu.Open();
	}

	public void CloseMenu(Menu menu)
	{
		menu.Close();
	}
}