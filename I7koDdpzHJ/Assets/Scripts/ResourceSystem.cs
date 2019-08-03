using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    [SerializeField] private float resource, maxResources, lossAmount, lossRate, gainRate;
    [SerializeField] private bool broken, gainingPower, losingPower;
    [SerializeField] private BatteryPowerInteractions plugInfo;
    [SerializeField] public GameObject plug;

    private void Start()
    {
        resource = maxResources * 0.5f;
    }
    private void Update()
    {
        if(!hasPlug() && !losingPower)
        {
            losingPower = true;
            Invoke("loseResources", lossRate);
        }
        else if(!gainingPower)
        {
            gainingPower = true;
            Invoke("gainResources", gainRate);
        }

        if(hasPlug())
        {
            plugIn();
        }
    }

    public void setBreak(bool isBroken)
    {
        broken = isBroken;
    }

    public void gainResources()
    {
        if (hasPlug())
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
        gainingPower = false;
    }

    public void loseResources()
    {
        if (resource > 0)
        {
            resource -= lossAmount;
        }
        else
        {
            resource = 0;
        }
        losingPower = false;
    }

    public void plugIn()
    {
        plugInfo = plug.GetComponent<BatteryPowerInteractions>();
    }

    public bool hasPlug()
    {
        return plug != null;
    }
}
