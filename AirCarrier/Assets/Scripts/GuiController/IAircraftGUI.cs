using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAircraftGUI
{
    
    AircraftInfoGUI getNextInfoGui();
    void setAvailableAircraft(int amount);
    void setAirportMessage(string text);
    void showLandingMessage(bool show);
}
