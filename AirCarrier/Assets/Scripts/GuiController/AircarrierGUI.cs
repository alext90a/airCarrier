using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AircarrierGUI : MonoBehaviour {

    [SerializeField]
    Button mCameraButton = null;
    [SerializeField]
    Image mCurForwardSpeedImage = null;
    [SerializeField]
    Image mTargetForwardSpeedImage = null;
    [SerializeField]
    Image mMaxForwardSpeedImage = null;
    [SerializeField]
    Text mCurSpeedText = null;
    [SerializeField]
    Image mCurBackwardSpeedImage = null;
    [SerializeField]
    Image mTargetBackwardSpeedImage = null;
    [SerializeField]
    Image mMaxBackwardSpeedImage = null;
    [SerializeField]
    Image mAngleImage = null;
    [SerializeField]
    Text mAngleText = null;

    float mMaxForwardKoeff = 1f;
    float mMaxBackwardKoeff = 1f;
	// Use this for initialization
	void Start () {
        mMaxForwardKoeff = mMaxForwardSpeedImage.fillAmount;
        mMaxBackwardKoeff = mMaxBackwardSpeedImage.fillAmount;

        mCameraButton.onClick.AddListener(setCamera);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void setCamera()
    {

    }

    public void updatedSpeed(float curSpeed, float targetSpeed)
    {
        if(curSpeed>=0f)
        {
            mCurBackwardSpeedImage.gameObject.SetActive(false);
            mTargetBackwardSpeedImage.gameObject.SetActive(false);
            mMaxBackwardSpeedImage.gameObject.SetActive(false);

            mCurForwardSpeedImage.gameObject.SetActive(true);
            mTargetForwardSpeedImage.gameObject.SetActive(true);
            mMaxForwardSpeedImage.gameObject.SetActive(true);

            mCurForwardSpeedImage.fillAmount = mMaxForwardKoeff * curSpeed / GameConstants.kCarrierMaxForwardSpeed;
            mTargetForwardSpeedImage.fillAmount = mMaxForwardKoeff * targetSpeed / GameConstants.kCarrierMaxForwardSpeed;
            mCurSpeedText.text = curSpeed.ToString("0.0");
        }
        else
        {
            mCurForwardSpeedImage.gameObject.SetActive(false);
            mMaxForwardSpeedImage.gameObject.SetActive(false);
            mTargetForwardSpeedImage.gameObject.SetActive(false);

            mCurBackwardSpeedImage.gameObject.SetActive(true);
            mMaxBackwardSpeedImage.gameObject.SetActive(true);
            mTargetBackwardSpeedImage.gameObject.SetActive(true);

            mTargetBackwardSpeedImage.fillAmount = mMaxBackwardKoeff * targetSpeed / GameConstants.kCarrierMaxBackwardSpeed;
            mCurBackwardSpeedImage.fillAmount = mMaxBackwardKoeff * curSpeed / GameConstants.kCarrierMaxBackwardSpeed;
            mCurSpeedText.text = curSpeed.ToString("0.0");
        }
    }

    public void updateRotaiton(float angle)
    {
        if(angle>0f)
        {
            mAngleImage.fillClockwise = true;
        }
        else
        {
            mAngleImage.fillClockwise = false;
        }
        mAngleImage.fillAmount = Mathf.Abs(angle) / 360f;
        mAngleText.text = angle.ToString("0.0");
    }
}
