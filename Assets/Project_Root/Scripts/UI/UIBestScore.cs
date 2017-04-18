using UnityEngine;
using System.Collections;

public class UIBestScore : MonoBehaviour
{
    public int _BestScoreVal = 0;
    public UILabel _lbBestScore;
    
    public void SetBestScore(int n)
    {
        int getval = PlayerPrefs.GetInt("Player BestScore");
        if (getval < n)
        {
            PlayerPrefs.SetInt("Player BestScore", n);
            _BestScoreVal = n;
        }
        else
        {
            _BestScoreVal = getval;
        }
        _lbBestScore.text = _BestScoreVal.ToString();
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
