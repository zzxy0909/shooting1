using UnityEngine;
using System.Collections;

public class testDB : MonoBehaviour {

	public ST_B_PlayerWeaponRec _rec;
	public PlayerWeapon_DataRecord _PlayerWeapon_DataRecord;

	// Use this for initialization
	void Start () {

		StartCoroutine(TestSql());
	}

	IEnumerator TestSql()
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

		int hp = DataManager.Instance._SqlSavePlayerData.Get_HP();

		_rec = DataManager.Instance._SqlBalance_player_weapon.Get_PlayerWeaponRec("pwp001");

		_PlayerWeapon_DataRecord._DataName = _rec._datacode;
		_PlayerWeapon_DataRecord._Dur = _rec._dur;
		_PlayerWeapon_DataRecord._AttackValue = _rec._attack;

		string str_weapon_datacode = DataManager.Instance._SqlSavedata_player_weapon.Get_weapon_datacode();

		int [] testlist = DataManager.Instance._SqlSavedata_player_weapon.GetList_weapon_idx_where_slot_no(0);

		ST_S_PlayerWeaponRec[] pwplist1 = DataManager.Instance._SqlSavedata_player_weapon.GetList_weapon_all_where_slot_no(1);
		ST_S_PlayerWeaponRec[] pwplist2 = DataManager.Instance._SqlSavedata_player_weapon.GetList_weapon_all_where_slot_no(2);
		ST_S_PlayerWeaponRec[] pwplist3 = DataManager.Instance._SqlSavedata_player_weapon.GetList_weapon_all_where_slot_no(3);


		Debug.Log("~~~~~~~~~~~~ _critical_ratio:" + _rec._critical_ratio.ToString()  + "weapon_datacode : " + str_weapon_datacode 
		          +" list cnt:"+testlist.Length 
		          +" pwplist1 cnt:"+pwplist1.Length 
		          +" pwplist2 cnt:"+pwplist2.Length 
		          +" pwplist3 cnt:"+pwplist3.Length 
		          );
	}

	// Update is called once per frame
	void Update () {
	
	}
}
