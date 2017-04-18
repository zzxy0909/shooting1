using UnityEngine;
using System.Collections;

[System.Serializable]
public class Monster_DataRecord {
	public E_Monster_Type _eType;
	public string _model_name;
	public int _HP;
	public int _Attack;
	public int _point_min;
	public int _point_max;
	public int _gold_min;
	public int _gold_max;

}


public enum E_Monster_Type{

	_eType1,
	_eType2,
	_eType3,
	_eType4,
	_eType5,
	_eType6,
	_eType7,
	_eType8,
	_eType9,
	_eType10,
	_eType11,
	_eType12,
	_eType13,
	_eType14,
	_eType15,

	_Type_Count,
}

