using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPhysics : MonoBehaviour
{
    public Vector3 cordAnchorPos;
    private LineRenderer line;
    [SerializeField] private GameObject oxygenOutlet, engineOutlet, shieldOutlet, computerOutlet;
    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        updateChord();
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

    public void checkBatteryMove()
    {
        if (oxygenOutlet.GetComponent<Outlet>().enteredOutletSpace)
        {
            moveIntoBatteryPos(oxygenOutlet);
        }
    }

    private void moveIntoBatteryPos(GameObject movePoint) { }

}

