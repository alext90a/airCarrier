using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTimer : MonoBehaviour {

    [SerializeField]
    Text mMessageText = null;

    float mTimeSinceActive = 0f;
    float mShowTime = GameConstants.kTextShowTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        mTimeSinceActive += Time.deltaTime;
        if(mTimeSinceActive >= mShowTime)
        {
            
            gameObject.SetActive(false);

        }

	}

    public void show(string text, float showTime)
    {
        mShowTime = showTime;
        gameObject.SetActive(true);
        mMessageText.text = text;
        mTimeSinceActive = 0f;
    }
}
