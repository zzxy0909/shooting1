using UnityEngine;
using System.Collections;


public enum E_BallMoveType
{
    twist360_36_1,
    twist360_36_180,
    twist360_36_Random,
    twist4_40,
}

public class BallController : MonoBehaviour {
    public E_BallMoveType _BallMoveType = E_BallMoveType.twist360_36_1;
    public GameObject _MoverRoot;
    public float _StartDelay = 1f;
    public float _Duration = 18f;

	// Use this for initialization
	void Start () {

        PlayMove();
	}

    public void PlayMove()
    {
        string Name_Func = "";

        switch (_BallMoveType)
        {
            case E_BallMoveType.twist360_36_1:
                //twist360_36_1();
                Name_Func = E_BallMoveType.twist360_36_1.ToString();
                break;
            case E_BallMoveType.twist360_36_Random:
                //twist360_36_1();
                float r = Random.Range(0f, 1f);
                if (r > 0.5f)
                {
                    Name_Func = E_BallMoveType.twist360_36_1.ToString();
                }
                else
                {
                    Name_Func = E_BallMoveType.twist360_36_180.ToString();
                }
                break;
        }

        Invoke(Name_Func, _StartDelay);
    }
    void twist360_36_180()
    {
        twist360_36_x(180f);
    }
    void twist360_36_1()
    {
        twist360_36_x(0f);
    }
    void twist360_36_x(float startR)
    {
        int cnt = 36;
        for (int i = 0; i < cnt; i++)
        {
            GameObject obj = null;
            if (i == cnt - 1)
            {
                obj = _MoverRoot;
            }
            else
            {
                obj = (GameObject)Instantiate(_MoverRoot, transform.position, Quaternion.identity);
            }
            obj.transform.parent = null;
            obj.transform.localRotation = Quaternion.Euler(0f, 0f, startR + i * 10f);
            wt_Mover mov = obj.GetComponentInChildren<wt_Mover>();
            if (mov)
            {
                mov._isLocalMover = true;
                mov._StartDelay = i * 0.1f;
                mov.PlayMove();
            }

            Destroy(obj, _Duration);
        }

        Destroy(this.gameObject, _Duration);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
