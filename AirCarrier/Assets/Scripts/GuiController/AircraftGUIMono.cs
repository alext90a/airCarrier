using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;

public class AircraftGUIMono : MonoBehaviour,IAircraftGUI {

    [SerializeField]
    Text mAvailableAircraftText = null;
    [SerializeField]
    TextTimer mAircarrierMessageText = null;
    [SerializeField]
    GameObject mAircraftOnLandingMessage = null;
    [SerializeField]
    GameObject mAircraftPanel = null;
    [SerializeField]
    AircraftInfoGUI mAircraftInfoPrefab = null;

    
    

    private List<AircraftInfoGUI> mAircraftInfo = new List<AircraftInfoGUI>();
    private int mNextInfoInd = 0;

    private void Awake()
    {
        for(int i=0; i<GameConstants.kAircaftsAmount; ++i)
        {
            AircraftInfoGUI createdGui = GameObject.Instantiate(mAircraftInfoPrefab, mAircraftPanel.transform) as AircraftInfoGUI;
            createdGui.gameObject.SetActive(true);
            mAircraftInfo.Add(createdGui);
        }
    }

    public void setAvailableAircraft(int amount)
    {
        mAvailableAircraftText.text = amount.ToString();
    }

    public void setAirportMessage(string text)
    {
        mAircarrierMessageText.show(text, GameConstants.kTextShowTime);
    }

    public void showLandingMessage(bool show)
    {
        mAircraftOnLandingMessage.gameObject.SetActive(show);
    }



    public AircraftInfoGUI getNextInfoGui()
    {
        AircraftInfoGUI guiInfo = mAircraftInfo[mNextInfoInd];
        ++mNextInfoInd;
        return guiInfo;
    }
}
