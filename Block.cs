using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int xCoord;
    public int yCoord;
    public GameObject explosion;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitBlock(Vector2 coords, GameObject obj)
    {
        xCoord = Mathf.FloorToInt(coords.x);
        yCoord = Mathf.FloorToInt(coords.y);
        gameObject.transform.SetParent(obj.transform);
    }
    public void Destroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        if (gameObject.transform.parent.childCount <= 1)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    public void Fall(int amount)
    {
        Vector3 fall = new Vector3(0, -1 * amount, 0);
        gameObject.transform.Translate(fall);
        yCoord += amount;
    }
}
