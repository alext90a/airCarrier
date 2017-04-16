using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AircraftInfoGUI : MonoBehaviour {

    [SerializeField]
    Text mAircraftNameText = null;
    [SerializeField]
    Text mSpeedText = null;
    [SerializeField]
    Text mFlyText = null;
    [SerializeField]
    Scrollbar mFlyProgressBar = null;
    [SerializeField]
    Button mCameraButton = null;

    // Use this for initialization
    void Start () {

        mCameraButton.onClick.AddListener(onCameraButtonClicked);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateSpeed(float speed)
    {
        mSpeedText.text = speed.ToString("0.0");
    }

    public void updateFlytime(float currentTime, float totalTime)
    {
        mFlyText.text = currentTime.ToString("0.0");
        mFlyProgressBar.size = currentTime / totalTime;
    }

    void onCameraButtonClicked()
    {

    }

    public void enableCameraButton(bool enable)
    {
        mCameraButton.enabled = enable;
    }
}
