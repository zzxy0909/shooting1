using UnityEngine;
using System.Collections;

public class CallButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadStartMenu_SaveSlotPos()
    {
        GameWorld.Instance._SlotManager.SaveSlotPos();

        Application.LoadLevel(GameWorld._Name_StartMenu);
    }
}
