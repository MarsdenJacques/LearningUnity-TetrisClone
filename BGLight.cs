using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLight : MonoBehaviour
{
    public float bottom;
    public float top;
    public float speed;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        if(gameObject.transform.position.y <= bottom)
        {
            gameObject.transform.Translate(0, top - bottom, 0);
        }
    }
}
