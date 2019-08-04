using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPhysics : MonoBehaviour
{
    public Vector3 cordAnchorPos;
    private LineRenderer line;
    public bool inOutletSpace;
    private GameObject curOutlet;
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
        }
    }

    public void tryBeginBatteryLeave()
    {
        if (curOutletParent != null && curOutletParent.GetComponent<ResourceSystem>().hasPlug() && this.GetComponent<MouseActions>().holdingPlug)
        {
            unlockBatteryPos();
            curOutletParent.GetComponent<ResourceSystem>().removePlug();
            
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
        curOutletParent.GetComponent<ResourceSystem>().plugIn(this.gameObject);
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

