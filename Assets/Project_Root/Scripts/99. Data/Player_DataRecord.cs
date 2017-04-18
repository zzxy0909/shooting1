using UnityEngine;
using System.Collections;


[System.Serializable]
public class Player_DataRecord {
	public int _CurrentHeroIx = 0;
	public int _CurrentLevel = 1;
	
	public int _HP;
	public int _Attack;
    public int _Def;
	public float _RunningAttackRatio = 8.0f;
	public float _CriticalAttackRatio = 0.1f;
    public float _AttackSpeed = 0.4f;
	public int _SP;
    //스킬포인트 100으로 default
	public int _SkillPoint = 100;

}

