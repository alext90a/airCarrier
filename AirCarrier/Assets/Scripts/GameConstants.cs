﻿using UnityEngine;
using System.Collections;

public class GameConstants
{

	public static float kCarrierMaxForwardSpeed = 1f;
    public static float kCarrierMaxBackwardSpeed = -0.5f;
    public static float kCarrierIncreaseStepSpeed = 0.25f;
    public static float kCarrierMaxRoationSpeed = 5;
    public static float kCarrierRotationStepSpeed = 1;
    public static float kAircarrierAcceleration = 0.1f;


    public static float kTimeBetweenAircaftLaunch = 0.5f;
    public static float kAircraftFlytime = 30f;
    public static float kAircraftMinSpeed = 2f;
    public static float kAircraftMaxSpeed = 5f;
    public static float kAircraftAcceleration = 0.5f;
    public static float kAircraftMaxAngleSpeed = 90f;

    public static int kAircaftsAmount = 5;
    public static float kEchelonDistance = 0.1f;

    
    public static string kLandedStateName = "Landed";
    public static string kRunawayStateName = "Runaway";
    public static string kFlyStateName = "Fly";
    public static string kLandingStateName = "Landing";
}
