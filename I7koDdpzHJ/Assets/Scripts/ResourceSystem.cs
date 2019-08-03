using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    [SerializeField] private float resource, maxResources, lossRate;
    [SerializeField] private bool broken;
    [SerializeField] private BatteryPowerInteractions plugInfo;
    [SerializeField] public GameObject plug;

    public void setBreak(bool isBroken)
    {
        broken = isBroken;
    }

    public void gainResources()
    {
        if (resource < maxResources)
        {
            resource += plugInfo.drainPower();
        }
        else
        {
            resource = maxResources;
        }
    }

    public void loseResources()
    {
        if (resource > 0)
        {
            resource -= lossRate;
        }
        else
        {
            resource = 0;
        }
    }

    public bool hasPlug()
    {
        return plug != null;
    }
}
