using UnityEngine;
using System.Collections;

public class UISelectPlay : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LoadSetupSlot()
    {
        Application.LoadLevel(GameWorld._Name_SetupSlot);
    }

    public void PlayStage1_1()
    {
        GameWorld.Instance._CurrentStageNo = 1;
        GameWorld.Instance._CurrentRoundNo = 1;
        Application.LoadLevel(GameWorld._Name_GameStage);
    }
    public void PlayStage1_2()
    {
        GameWorld.Instance._CurrentStageNo = 1;
        GameWorld.Instance._CurrentRoundNo = 2;
        Application.LoadLevel(GameWorld._Name_GameStage);
    }
    public void PlayStage1_3()
    {
        GameWorld.Instance._CurrentStageNo = 1;
        GameWorld.Instance._CurrentRoundNo = 3;
        Application.LoadLevel(GameWorld._Name_GameStage);
    }
    public void PlayStage6_1()
    {
        GameWorld.Instance._CurrentStageNo = 6;
        GameWorld.Instance._CurrentRoundNo = 1;
        Application.LoadLevel(GameWorld._Name_GameStage);
    }
    public void PlayStage6_2()
    {
        GameWorld.Instance._CurrentStageNo = 6;
        GameWorld.Instance._CurrentRoundNo = 2;
        Application.LoadLevel(GameWorld._Name_GameStage);
    }
    public void PlayStage6_3()
    {
        GameWorld.Instance._CurrentStageNo = 6;
        GameWorld.Instance._CurrentRoundNo = 3;
        Application.LoadLevel(GameWorld._Name_GameStage);
    }
    public void PlayStage11_1()
    {
        GameWorld.Instance._CurrentStageNo = 11;
        GameWorld.Instance._CurrentRoundNo = 1;
        Application.LoadLevel(GameWorld._Name_GameStage);
    }
    public void PlayStage11_2()
    {
        GameWorld.Instance._CurrentStageNo = 11;
        GameWorld.Instance._CurrentRoundNo = 2;
        Application.LoadLevel(GameWorld._Name_GameStage);
    }
    public void PlayStage11_3()
    {
        GameWorld.Instance._CurrentStageNo = 11;
        GameWorld.Instance._CurrentRoundNo = 3;
        Application.LoadLevel(GameWorld._Name_GameStage);
    }

}
