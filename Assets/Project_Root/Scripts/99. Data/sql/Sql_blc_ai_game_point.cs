#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;


public class Sql_blc_ai_game_point {

	private SQLiteDB _db = null;
	string _db_filename;
		
	private string _querySelect_point_min = "SELECT point_min FROM blc_ai_game_point where model_name = '{0}' ;";
	private string _querySelect_point_max = "SELECT point_max FROM blc_ai_game_point where model_name = '{0}' ;";
	private string _querySelect_gold_min = "SELECT gold_min FROM blc_ai_game_point where model_name = '{0}' ;";
	private string _querySelect_gold_max = "SELECT gold_max FROM blc_ai_game_point where model_name = '{0}' ;";
	
	public Sql_blc_ai_game_point()
    {
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
	}
	
	string GetFileName_SaveDB()
	{
		return DataManager.Instance.GetDataFilePath_SaveDB();
		
	}
	
	public int Get_point_min(string a_model_name)
	{
		_db_filename = GetFileName_SaveDB();
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		int rtn = 0;//---
		try{
			_db.Open(_db_filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_point_min, a_model_name); //--- _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rtn = qr.GetInteger("point_min"); //---
			}
			qr.Release();                                     
			_db.Close();			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}		
		return rtn; //---
	}
	public int Get_point_max(string a_model_name)
	{
		_db_filename = GetFileName_SaveDB();
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		int rtn = 0;//---
		try{
			_db.Open(_db_filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_point_max, a_model_name); //--- _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rtn = qr.GetInteger("point_max"); //---
			}
			qr.Release();                                     
			_db.Close();			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}		
		return rtn; //---
	}
	public int Get_gold_min(string a_model_name)
	{
		_db_filename = GetFileName_SaveDB();
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		int rtn = 0;//---
		try{
			_db.Open(_db_filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_gold_min, a_model_name); //--- _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rtn = qr.GetInteger("gold_min"); //---
			}
			qr.Release();                                     
			_db.Close();			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}		
		return rtn; //---
	}
	public int Get_gold_max(string a_model_name)
	{
		_db_filename = GetFileName_SaveDB();
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		int rtn = 0;//---
		try{
			_db.Open(_db_filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_gold_max, a_model_name); //--- _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rtn = qr.GetInteger("gold_max"); //---
			}
			qr.Release();                                     
			_db.Close();			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}		
		return rtn; //---
	}
	
}
