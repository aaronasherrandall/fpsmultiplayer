using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	//Static reference: variable is bound to the class, not the object in Unity
	//References "this" script as set in Awake()
	public static MenuManager Instance;

	//[SerializeField] exposes variable to inspector without making it public; we don't need other classes accessing this field
	[SerializeField] Menu[] menus;

	private void Awake()
	{
		Instance = this;
	}

	
    // Start is called before the first frame update
    
    //Takes string and open a menu
    public void OpenMenu(string menuName)
	{
		for (int i = 0; i < menus.Length; i++)
		{
			if (menus[i].menuName == menuName)
			{
				menus[i].Open();
			}
			else if(menus[i].open)
			{
				CloseMenu(menus[i]);
			}
		}
	}

    //Take a Menu object (Menu script) and open a menu
    public void OpenMenu(Menu menu)
	{
		//If we open a menu with this function, we need it to close the one we currently have open
		//Only want to have one menu open at a time
		for (int i = 0; i < menus.Length; i++)
		{
			if (menus[i].open)
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
