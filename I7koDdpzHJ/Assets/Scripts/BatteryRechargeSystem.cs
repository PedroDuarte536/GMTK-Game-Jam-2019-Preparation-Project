using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryRechargeSystem : MonoBehaviour
{
    [SerializeField] private float chargeRate;
    //broken determines wether or not the machine is functioning or requires fixing, gainingPower and LosingPower are purly logical operators
    public bool charging;
    //contains the script for the plug
    [SerializeField] private BatteryPowerInteractions plugInfo;
    //contains the plug game object
    public GameObject plug;

    //initializes pluginfo to the plugs script
    public void plugIn()
    {
        plugInfo = plug.GetComponent<BatteryPowerInteractions>();
    }

    // determines wheter the plug gameobject is attached
    public bool hasPlug()
    {
        return plug != null;
    }
}
