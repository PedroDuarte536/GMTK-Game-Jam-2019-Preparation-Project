using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPhysics : MonoBehaviour
{
    public Vector3 cordAnchorPos;
    private LineRenderer line;
    public bool inOutletSpace, allowPlay;
    [SerializeField] private AudioClip hittingSurface, pluggingIn;
    [SerializeField] private AudioSource soundManager;
    private GameObject curOutlet;
    [SerializeField] private GameObject curOutletParent;
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        updateChord();
        tryBeginBatteryPlug();
        tryBeginBatteryLeave();
    }

    private void updateSound(AudioClip clip)
    {
        soundManager.clip = clip;
        StartCoroutine(playSound());
        allowPlay = false;
    }

    private IEnumerator playSound()
    {
        soundManager.Play(0);
        yield return new WaitForSeconds(soundManager.clip.length);
        soundManager.Stop();
        soundManager.clip = null;
        allowPlay = true;
    }

    /*
    private void updateChord()
    {
        line.SetPosition(0, cordAnchorPos);
        line.SetPosition(1, new Vector2(transform.position.x, transform.position.y - 0.4f));
    }*/


    private void updateChord()
    {
        Vector3 endPosition = new Vector3(transform.position.x, transform.position.y - .27f, transform.position.z);
        line.SetPosition(0, cordAnchorPos);
        line.SetPosition(1, endPosition);

    }

    public Vector3 getBatteryPos()
    {
        return transform.position;
    }

    public void setBatteryPos(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void tryBeginBatteryPlug()
    {
        if(inOutletSpace && Input.GetMouseButtonUp(0))
        {
            lockBatteryPos();
            connectToMachine();
            updateSound(pluggingIn);
        }
    }

    //ik the code isnt pretty, but it was assuming the parent had a ResourceSystem class and the charging station doesnt, so had to do this
    public void tryBeginBatteryLeave()
    {
        if (curOutletParent != null && curOutletParent.tag.Equals("Charging Station"))
        {
            if (curOutletParent != null && curOutletParent.GetComponent<BatteryRechargeSystem>().hasPlug() && this.GetComponent<MouseActions>().holdingPlug)
            {
                unlockBatteryPos();
                curOutletParent.GetComponent<BatteryRechargeSystem>().removePlug();

            }
        }
        else
        {
            if (curOutletParent != null && curOutletParent.GetComponent<ResourceSystem>().hasPlug() && this.GetComponent<MouseActions>().holdingPlug)
            {
                unlockBatteryPos();
                curOutletParent.GetComponent<ResourceSystem>().removePlug();

                if (curOutletParent != null && this.GetComponent<MouseActions>().holdingPlug)
                {
                    switch (curOutletParent.gameObject.tag)
                    {
                        case "Resource Station":
                            if (curOutletParent.GetComponent<ResourceSystem>().hasPlug())
                            {
                                unlockBatteryPos();
                                curOutletParent.GetComponent<ResourceSystem>().removePlug();
                            }
                            break;
                        case "Charging Station":
                            if (curOutletParent.GetComponent<BatteryRechargeSystem>().hasPlug())
                            {
                                unlockBatteryPos();
                                curOutletParent.GetComponent<BatteryRechargeSystem>().removePlug();
                            }
                            break;
                    }
                }
            }
        }
    }

    private void unlockBatteryPos()
    {
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void lockBatteryPos()
    {
        transform.position = curOutlet.GetComponent<Outlet>().pluggedPos;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void connectToMachine()
    {

        if (curOutletParent.tag.Equals("Charging Station"))
        {
            curOutletParent.GetComponent<BatteryRechargeSystem>().plugIn(this.gameObject);
        }
        else
        {
            curOutletParent.GetComponent<ResourceSystem>().plugIn(this.gameObject);
        }
        GetComponent<BatteryPowerInteractions>().connectedTo = curOutletParent;



        switch (curOutletParent.gameObject.tag)
        {
            case "Resource Station":
                curOutletParent.GetComponent<ResourceSystem>().plugIn(this.gameObject);
                break;
            case "Charging Station":
                curOutletParent.GetComponent<BatteryRechargeSystem>().plugIn(this.gameObject);
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Outlet"))
        {
            curOutlet = collision.gameObject;
            inOutletSpace = true;
            curOutletParent = collision.transform.parent.gameObject;
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Outlet"))
        {
            inOutletSpace = false;
            curOutletParent = null; 
        }
    }

}

