using UnityEngine;
using System.Collections;

public class Monster_DataController : MonoBehaviour {
	
	public Monster_DataRecord[] _arr_DataRecord;

	void Awake()
	{
//		GameWorld.Instance._Monster_DataController = this;
	}
	
	// Use this for initialization
	void Start () {
	
		StartupData();
	}
	
	void StartupData()
	{
		StartCoroutine(IE_StartupData());
		
	}
	IEnumerator IE_StartupData()
	{
		while(true)
		{
			if(DataManager.Instance != null)
			{
				if(DataManager.Instance._SetupDataManager)
				{
					if(DataManager.Instance._SetupDataManager._SetupOK == true)
					{
						break;
					}
				}
			}
			yield return new WaitForFixedUpdate();
		}
		
		
		for(int i=0; i<_arr_DataRecord.Length; i++)
		{
			if(string.IsNullOrEmpty(_arr_DataRecord[i]._model_name) )
			{
				continue;
			}
		
			_arr_DataRecord[i]._point_min = DataManager.Instance._SqlBalance_mon_game_point.Get_point_min(_arr_DataRecord[i]._model_name);	
			_arr_DataRecord[i]._point_max = DataManager.Instance._SqlBalance_mon_game_point.Get_point_max(_arr_DataRecord[i]._model_name);	
			_arr_DataRecord[i]._gold_min = DataManager.Instance._SqlBalance_mon_game_point.Get_gold_min(_arr_DataRecord[i]._model_name);	
			_arr_DataRecord[i]._gold_max = DataManager.Instance._SqlBalance_mon_game_point.Get_gold_max(_arr_DataRecord[i]._model_name);	
			
		}
		Debug.Log("~~~~~~~~ set _arr_DataRecord " );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public int GetHP(E_Monster_Type a_type)
	{
		for(int i=0; i<_arr_DataRecord.Length; i++)
		{
			if(_arr_DataRecord[i]._eType == a_type)
			{
				return _arr_DataRecord[i]._HP;
			}
		}
		return 0;
	}
	public string GetModelName(E_Monster_Type a_type)
	{
		for(int i=0; i<_arr_DataRecord.Length; i++)
		{
			if(_arr_DataRecord[i]._eType == a_type)
			{
				return _arr_DataRecord[i]._model_name;
			}
		}
		return "";
	}

}
