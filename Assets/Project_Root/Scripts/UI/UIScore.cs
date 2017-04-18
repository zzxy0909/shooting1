using UnityEngine;
using System.Collections;

public class UIScore : MonoBehaviour
{
    public int _ScoreVal = 0;
    public UILabel _lbScore;

    public void AddScore(int n)
    {
        _ScoreVal += n;

        _lbScore.text = _ScoreVal.ToString();

        GamePlayManager.Instance._BestScore.SetBestScore(GamePlayManager.Instance._Score._ScoreVal);

        GamePlayManager.Instance._UserExp.AddExp(n);

    }
    public void SetScore(int n)
    {
        _ScoreVal = n;

        _lbScore.text = _ScoreVal.ToString();
    }

    void Awake()
    {
        // Setting up the reference.

    }

	void Update ()
	{
		
	}


}
