#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

public struct ST_B_PlayerWeaponRec
{
	public string _datacode;
	public int _dur;
	public int _attack;
	public int _level;
	public float _critical_ratio;
}

public class SqlBalance_player_weapon {

	private SQLiteDB _db = null;

	private string _querySelect_PlayerWeaponRec = "SELECT datacode, dur, attack, level, critical_ratio FROM balance_player_weapon where datacode = '{0}' and level = 1 ;";
	
	public SqlBalance_player_weapon()
    {
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
	}
	
	string GetFileName_DB()
	{
		return DataManager.Instance.GetDataFilePath_BalanceDB();
		
	}
	
	public ST_B_PlayerWeaponRec Get_PlayerWeaponRec(string a_datacode)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		ST_B_PlayerWeaponRec rec = new ST_B_PlayerWeaponRec();

		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_PlayerWeaponRec, a_datacode); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rec._datacode = qr.GetString("datacode");
				rec._dur = qr.GetInteger("dur");
				rec._attack = qr.GetInteger("attack");
				rec._level = qr.GetInteger("level");
				rec._critical_ratio = (float) qr.GetDouble("critical_ratio");
			}
			qr.Release();                                     
			_db.Close();
			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
				_db = null;
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}
		
		return rec;
	}
	

}
