using UnityEngine;
using System.Collections;

[System.Serializable]
public class AI_DataRecord {
	public E_AI_Type _E_AI_Type;
	public string _model_name;
	public int _HP;
	public int _Attack;
	public int _point_min;
	public int _point_max;
	public int _gold_min;
	public int _gold_max;

}


public enum E_AI_Type{

	_AIType1,
	_AIType2,
	_AIType3,
	_AIType4,
	_AIType5,
	_AIType6,
	_AIType7,
	_AIType8,
	_AIType9,
	_AIType10,
	_AIType11,
	_AIType12,
	_AIType13,
	
	_Type_Count,
}

