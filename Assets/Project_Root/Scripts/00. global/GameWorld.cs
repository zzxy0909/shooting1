using UnityEngine;
using System.Collections;

public sealed class GameWorld
{
    public const string _Name_StartMenu = "StartMenu";
    public const string _Name_GameStage = "GameStage_T2";
    public const string _Name_SetupSlot = "SetupSlot";

    static GameWorld instance=null;
    static readonly object padlock = new object();
	GameWorld()
    {
	}
	public static GameWorld Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance==null)
                {
                    instance = new GameWorld();
				}
                return instance;
            }
        }
    }
    public int _CurrentStageNo = 1;
    public int _CurrentRoundNo = 1;

//    public MonsterDataManager _MonsterDataManager;
//    public Monster_DataController _Monster_DataController;
    public BGMController _BGMController;
    public UnitList _UnitList;
    public SlotManager _SlotManager;

    public bool SetNextStageRound()
    {
        int maxRound_stage1 = 3;
        int maxRound_stage6 = 3;
        int maxRound_stage11 = 3;
        int maxRound_stage16 = 3;
        int maxRound_stage21 = 3;
        

        switch (_CurrentStageNo)
        {
            default :
                if (_CurrentRoundNo >= maxRound_stage1)
                {
                    return false;
                }
                else
                {
                    _CurrentRoundNo++;
                }
                break;
        }

        return true;
    }

	public static Camera GetMainCamera()
	{
		return Camera.main;
	}

	public static void SetActiveRecursively(GameObject go, bool active)
	{
    	go.SetActive (active);

	    foreach(Transform t in go.transform) 
		{
     	   SetActiveRecursively (t.gameObject, active);
	   	}
	}

	public static GameObject DoGetChildByName(GameObject root, string name, string tag)
	{
		if (root == null) return null;

		foreach (Transform child in root.transform)
		{
			if (!string.IsNullOrEmpty(name))
			{
				if (child.name == name)
				{
					if (!string.IsNullOrEmpty(tag))
					{
						if (child.tag.Equals(tag))
							return child.gameObject;
					}
					else return child.gameObject;
				}
			}
			if (!string.IsNullOrEmpty((tag)))
				if (child.tag == tag)
					return child.gameObject;

			GameObject returnObject = DoGetChildByName(child.gameObject, name, tag);
			if(returnObject != null)
				return returnObject;
		}

		return null;
	}
	
	public void CameraShake00()
	{
		Camera cam = GameWorld.GetMainCamera();
		if(cam)
		{
			GameObject cam_obj =cam.gameObject;
			if(cam_obj)
			{
				CameraShake shake = cam_obj.GetComponent<CameraShake>();
                if (shake)
                {
                    shake.Shake(0);
                }
                else
                {
                    Debug.LogError("CameraShake == null !");
                }
			}
		}
	}

    
}