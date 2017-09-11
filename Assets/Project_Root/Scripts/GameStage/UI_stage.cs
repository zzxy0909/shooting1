using UnityEngine;
using System.Collections;

public class UI_stage : MonoBehaviour {
    public UILabel _lbValue;
   
	// Use this for initialization
	void Start () {
        
        if (_lbValue == null)
        {
            _lbValue = GetComponent<UILabel>();
        }

        if (_lbValue != null)
        {
            _lbValue.text = string.Format("stage:{0}-{1}", GameWorld.Instance._CurrentStageNo, GameWorld.Instance._CurrentRoundNo);
        }
    }

	// Update is called once per frame
	//void Update () {
	
	//}

 //   void LateUpdate()
 //   {
        
 //   }
}
