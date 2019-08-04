using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPhysics : MonoBehaviour
{
    public Vector3 cordAnchorPos;
    private LineRenderer line;
    public bool inOutletSpace, allowPlay, pluggedIn;
    [SerializeField] private AudioClip hittingSurface, pluggingIn;
    [SerializeField] private AudioSource soundManager;
    [SerializeField] private GameObject curOutletParent;
    // Start is called before the first frame update
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
        if (!pluggedIn && inOutletSpace && Input.GetMouseButtonUp(0))
        {
            lockBatteryPos();
            connectToMachine();
            updateSound(pluggingIn);
        }
    }

    public void tryBeginBatteryLeave()
    {
        if (curOutletParent != null && curOutletParent.tag.Equals("Charging Station"))
        {
            if (curOutletParent != null && curOutletParent.GetComponent<BatteryRechargeSystem>().hasPlug() && this.GetComponent<MouseActions>().holdingPlug)
            {
                unlockBatteryPos();
                curOutletParent.GetComponent<BatteryRechargeSystem>().removePlug();
                pluggedIn = false;

            }
        }
        else
        {
            if (curOutletParent != null && curOutletParent.GetComponent<ResourceSystem>().hasPlug() && this.GetComponent<MouseActions>().holdingPlug)
            {
                unlockBatteryPos();
                curOutletParent.GetComponent<ResourceSystem>().removePlug();
                pluggedIn = false;

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
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void connectToMachine()
    {
        if (curOutletParent.tag.Equals("Charging Station"))
        {
            curOutletParent.GetComponent<BatteryRechargeSystem>().plugIn();
        }
        else
        {
            curOutletParent.GetComponent<ResourceSystem>().plugIn(this.gameObject);
        }
        GetComponent<BatteryPowerInteractions>().connectedTo = curOutletParent;
        pluggedIn = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Outlet"))
        {
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

