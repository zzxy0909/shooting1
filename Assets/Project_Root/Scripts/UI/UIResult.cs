using UnityEngine;
using System.Collections;

public class UIResult : MonoBehaviour {
    public UILabel _MessageTitle;
    public UIButton _btnNext;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NextPlayStage()
    {
        bool b = GameWorld.Instance.SetNextStageRound();

        if (b == false)
        {
            // max round.
            Application.LoadLevel(GameWorld._Name_StartMenu);
        }
        else
        {
            Application.LoadLevel(GameWorld._Name_GameStage);
        }
    }
    public void LoadStartMenu()
    {
        Application.LoadLevel(GameWorld._Name_StartMenu);
    }
}
