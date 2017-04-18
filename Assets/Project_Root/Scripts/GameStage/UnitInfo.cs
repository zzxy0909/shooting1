using UnityEngine;
using System.Collections;

public class UnitInfo : MonoBehaviour {
    public int _Table_idx;
    public string _unit_code;
    public int _HP;
    public int _HP_Default;
    public int _Attack = 1;
    public bool _DeathFlag = false;
    public int _Level;
    public int _TotalExp;
    public int _NextExp;
    public int _ClassNo = 1;
    public E_FireType _FireType = E_FireType.range;
    
	// Use this for initialization
	void Start () {
	
	}

    float _CheckLevelTime = 1f;
    float _NextCheckLevelTime = 0f;
	// Update is called once per frame
	void Update () {
        if (_TotalExp > 0 && _Table_idx > 0 && _Level > 0
            && _NextCheckLevelTime < Time.time
            )
        {
            _NextCheckLevelTime += _CheckLevelTime;
            CheckLevel();
        }
	
	}

    void CheckLevel()
    {
        if (_NextExp == 0)
        {
            int val = DataManager.Instance._SqlBalance_level_exp.Get_total_exp(_Level + 1, _ClassNo);
            if(val > 0)
            {
                _NextExp = val;
            }else {
                _NextExp = -1; // max..
                return;
            }
        }else if(_NextExp < 0) // max
        {
            return;
        }
        if (_NextExp < _TotalExp)
        {
            // lavel up !!!!!!!!!!!!!!!!
            _Level = DataManager.Instance._SqlBalance_level_exp.Get_level(_TotalExp, _ClassNo);
            _NextExp = DataManager.Instance._SqlBalance_level_exp.Get_total_exp(_Level + 1, _ClassNo);
            SetBalanceData();
        }
    }

    void SetBalanceData()
    {
        ST_B_UnitRec balanceUnit = new ST_B_UnitRec();
        balanceUnit = DataManager.Instance._SqlBalance_unit.Get_UnitRec(_unit_code, _Level, _ClassNo);
        if (balanceUnit.class_no > 0)
        {
            _HP_Default = _HP = balanceUnit.hp;
            _Attack = balanceUnit.attack;
        }
    }
}
