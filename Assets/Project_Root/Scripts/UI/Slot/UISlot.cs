using UnityEngine;
using System.Collections;

public class UISlot : MonoBehaviour {
    public int _SlotNo; // SlotManager 시작 시 slotController._SlotNo 로 가져옮.
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectSlot()
    {
        GameWorld.Instance._SlotManager.SelectSlotNo(_SlotNo);
    }
}
