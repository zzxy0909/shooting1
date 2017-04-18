using UnityEngine;
using System.Collections;

public class MonsterDataManager : MonoBehaviour {
	
	public AI_DataRecord[] _arrAI_DataRecord;
	public int _CurrentBossHP = 0;
    public bool _SetupOK = false;
	
	void Awake()
	{
//		GameWorld.Instance._MonsterDataManager = this;
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
		
		for(int i=0; i<_arrAI_DataRecord.Length; i++)
		{
			if(string.IsNullOrEmpty(_arrAI_DataRecord[i]._model_name) )
			{
				continue;
			}
		
			_arrAI_DataRecord[i]._point_min = DataManager.Instance._SqlBalance_mon_game_point.Get_point_min(_arrAI_DataRecord[i]._model_name);	
			_arrAI_DataRecord[i]._point_max = DataManager.Instance._SqlBalance_mon_game_point.Get_point_max(_arrAI_DataRecord[i]._model_name);	
			_arrAI_DataRecord[i]._gold_min = DataManager.Instance._SqlBalance_mon_game_point.Get_gold_min(_arrAI_DataRecord[i]._model_name);	
			_arrAI_DataRecord[i]._gold_max = DataManager.Instance._SqlBalance_mon_game_point.Get_gold_max(_arrAI_DataRecord[i]._model_name);
            _arrAI_DataRecord[i]._Attack = DataManager.Instance._SqlBalance_mon_game_point.Get_attack(_arrAI_DataRecord[i]._model_name);
            _arrAI_DataRecord[i]._HP = DataManager.Instance._SqlBalance_mon_game_point.Get_hp(_arrAI_DataRecord[i]._model_name);	
			
/*          test data  
 *          if(_arrAI_DataRecord[i]._E_AI_Type == E_AI_Type._AIType10
                || _arrAI_DataRecord[i]._E_AI_Type == E_AI_Type._AIType11
                )
            {
                _arrAI_DataRecord[i]._Attack = 50;
                _arrAI_DataRecord[i]._HP = 1000;
            }else{
                _arrAI_DataRecord[i]._Attack = 10;
                _arrAI_DataRecord[i]._HP = 200;
            }
            */

		}
		Debug.Log("~~~~~~~~ set _arrAI_DataRecord " );

        _SetupOK = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public int GetAttack(E_AI_Type a_type)
	{
		for(int i=0; i<_arrAI_DataRecord.Length; i++)
		{
			if(_arrAI_DataRecord[i]._E_AI_Type == a_type)
			{
				return _arrAI_DataRecord[i]._Attack;
			}
		}
		return 0;
	}
	public int GetHP(E_AI_Type a_type)
	{
		for(int i=0; i<_arrAI_DataRecord.Length; i++)
		{
			if(_arrAI_DataRecord[i]._E_AI_Type == a_type)
			{
				return _arrAI_DataRecord[i]._HP;
			}
		}
		return 0;
	}
	public string GetModelName(E_AI_Type a_type)
	{
		for(int i=0; i<_arrAI_DataRecord.Length; i++)
		{
			if(_arrAI_DataRecord[i]._E_AI_Type == a_type)
			{
				return _arrAI_DataRecord[i]._model_name;
			}
		}
		return "";
	}
	public int GetCurrentBossHP()
	{
		return _CurrentBossHP;
	}
	
	public void SetContinueBossHP(int hp)
	{
		_CurrentBossHP = hp;
	}
	
	public void SetupBossHP(E_AI_Type a_type)
	{
		for(int i=0; i<_arrAI_DataRecord.Length; i++)
		{
			if(_arrAI_DataRecord[i]._E_AI_Type == a_type)
			{
				_CurrentBossHP = _arrAI_DataRecord[i]._HP;
				return;
			}
		}
		
		Debug.LogError("Boss Type Error!");
	}
}
