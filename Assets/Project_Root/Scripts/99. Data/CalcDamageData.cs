using UnityEngine;
using System.Collections;

public enum E_Attack_Type{

	_Normal,
	_Big,
	_Evade,
	_Dash,
	_CounterAttack,
	_Rush,
	
	_Skill01=11,
	_Skill02=12,
	_Skill03=13,
	_Skill04=14,
	
	_RunningTypeAttack=200,
	
	
	_Type_Count,
}

[System.Serializable]
public struct CalcDamageData {

	public E_Attack_Type _E_Attack_Type;
	public int _Attack;
	public float _AttackRatio;
	public float _CriticalAttackRatio;
	public GameObject _FromObject;
	
}

