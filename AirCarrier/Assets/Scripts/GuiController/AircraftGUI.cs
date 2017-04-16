using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AircraftGUI : MonoBehaviour {

    [SerializeField]
    Text mAvailableAircraftText = null;
    [SerializeField]
    AircraftInfoGUI[] mAircraftInfo;

    int mNextInfoInd = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public AircraftInfoGUI getNextInfoGui()
    {
        AircraftInfoGUI guiInfo = mAircraftInfo[mNextInfoInd];
        ++mNextInfoInd;
        return guiInfo;
    }

    public void setAvailableAircraft(int amount)
    {
        mAvailableAircraftText.text = amount.ToString();
    }
}
