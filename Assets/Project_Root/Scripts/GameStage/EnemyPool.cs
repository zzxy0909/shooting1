using UnityEngine;
using System.Collections;

public class EnemyPool : MonoBehaviour {
    public Enemy[] _pfArrEnemy;
    public Enemy[] _pfArrEnemy_Boss;

    // Use this for initialization
	void Start () {

        GamePlayManager.Instance._EnemyPool = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
