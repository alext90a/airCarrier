using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControlledUnit
{

    void IncreaseSpeed();
    void DecreaseSpeed();
    void RotateLeft();
    void RotateRight();
    void LaunchAircraft();
}
