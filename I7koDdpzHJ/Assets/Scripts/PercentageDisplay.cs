using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageDisplay : MonoBehaviour
{
    public int percentage;

    private SpriteRenderer displaySR;
    private float maxWidth;
    // Start is called before the first frame update
    void Start()
    {
        displaySR = GetComponent<SpriteRenderer>();
        maxWidth = displaySR.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        updateDisplay();
    }

    private void updateDisplay()
    {
        float newWidth = maxWidth * ((float)percentage / 100);
        Debug.Log(newWidth);
        Vector2 newSize = new Vector2(newWidth, displaySR.size.y);
        displaySR.size = newSize;
    }

    public void setPercentage(int p)
    {
        if (p < 0)
            p = 0;
        else
            percentage = p;
    }
}
