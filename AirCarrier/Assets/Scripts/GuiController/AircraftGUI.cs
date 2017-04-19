using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AircraftGUI : MonoBehaviour {

    [SerializeField]
    Text mAvailableAircraftText = null;
    [SerializeField]
    GameObject mAircraftPanel = null;
    [SerializeField]
    AircraftInfoGUI mAircraftInfoPrefab = null;

    List<AircraftInfoGUI> mAircraftInfo = new List<AircraftInfoGUI>();

    int mNextInfoInd = 0;

    private void Awake()
    {
        for(int i=0; i<GameConstants.kAircaftsAmount; ++i)
        {
            AircraftInfoGUI createdGui = GameObject.Instantiate(mAircraftInfoPrefab, mAircraftPanel.transform) as AircraftInfoGUI;
            createdGui.gameObject.SetActive(true);
            mAircraftInfo.Add(createdGui);

        }
    }
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
