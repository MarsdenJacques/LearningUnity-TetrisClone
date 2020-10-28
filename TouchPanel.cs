using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPanel : MonoBehaviour
{
    public bool pressed;
    private void Start()
    {
        pressed = false;
    }
    // Start is called before the first frame update
    protected void OnMouseDown()
    {
        pressed = true;
    }
    protected void OnMouseUp()
    {
        pressed = false;
    }
}
