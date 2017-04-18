using UnityEngine;
using System.Collections;

public class UserExp : MonoBehaviour {
    public int _Value = 0;
    public UILabel _lbValue;

    public wt_PlayerController _PlayerCont = null;
    public UnitInfo _PlayerContInfo = null;
    public void AddExp(int n)
    {
        _Value += n;
        SetValue(_Value);

        if (_PlayerCont != null)
        {
            _PlayerCont.AddUnitExp(n);
        }
    }
 
    void SetValue(int n)
    {
        _Value = n;
        _lbValue.text = _Value.ToString();
    }
	// Use this for initialization
	void Start () {

        if (GamePlayManager.Instance._UserExp == null)
        {
            GamePlayManager.Instance._UserExp = this;
        }
        if (_lbValue == null)
        {
            _lbValue = GetComponent<UILabel>();
        }
       //~~~~~~~~~~~~~~~ data	
        StartCoroutine(IE_StartData());
        
    }
    IEnumerator IE_StartData()
    {
        while (true)
        {
            if (DataManager.Instance != null)
            {
                if (DataManager.Instance._SetupDataManager)
                {
                    if (DataManager.Instance._SetupDataManager._SetupOK == true)
                    {
                        break;
                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }

        _Value = (int) DataManager.Instance.GetTotalPlayScore();
        SetValue(_Value);
    }

	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        if (_PlayerCont == null)
        {
            _PlayerCont = GamePlayManager.Instance._PlayerManager._PlayerController;
        }
    }
}
