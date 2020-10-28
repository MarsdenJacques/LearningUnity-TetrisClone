using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public GameObject blockPrefab;
    public List<Block> blocks;
    public Sprite[] blockColors;
    public string shapeName;
    public List<Vector2> rotations;
    public List<Vector2> rotationsCounter;
    public ShapeType shapeType;
    public Sprite ghostColor;
    private Vector3 dropVal = new Vector3(0, -1, 0);
    private Board board;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InitShape(ShapeType shapeType, Board board)
    {
        this.board = board;
        this.shapeType = shapeType;
        rotations = new List<Vector2>(shapeType.rotations);
        Sprite color = blockColors[Random.Range((int)0, blockColors.Length)];
        shapeName = shapeType.shapeName;
        for (int i = 0; i < shapeType.blocks; i++)
        {
            GameObject blockObj = GameObject.Instantiate(blockPrefab);
            Block block = blockObj.GetComponent<Block>();
            block.InitBlock(shapeType.coordinates[i], gameObject);
            blocks.Add(block);
            blockObj.transform.Translate(gameObject.transform.position.x + (((block.xCoord * 1.0f) - 7.5f)), gameObject.transform.position.y + ((0.5f - block.yCoord * 1.0f)), gameObject.transform.position.z);
            blockObj.GetComponent<SpriteRenderer>().sprite = color;
        }
    }
    private void UpdateRotations(bool clockWise)
    {
        if(clockWise)
        {
            for (int i = 0; i < rotations.Count; i++)
            {
                rotations[i] = new Vector2(rotations[i].y, rotations[i].x * -1);
            }
            for (int i = 0; i < rotationsCounter.Count; i++)
            {
                rotationsCounter[i] = new Vector2(rotationsCounter[i].y, rotationsCounter[i].x * -1);
            }
        }
        else
        {
            for (int i = 0; i < rotations.Count; i++)
            {
                rotations[i] = new Vector2(-rotations[i].y, rotations[i].x);
            }
            for (int i = 0; i < rotationsCounter.Count; i++)
            {
                rotationsCounter[i] = new Vector2(-rotationsCounter[i].y, rotationsCounter[i].x);
            }
        }
    }
    public void InitShape(ShapeType shapeType, int colorInt, Board board)
    {
        this.board = board;
        this.shapeType = shapeType;
        rotations = new List<Vector2>(shapeType.rotations);
        rotationsCounter = new List<Vector2>(shapeType.rotationsCounter);
        Sprite color = blockColors[colorInt];
        shapeName = shapeType.shapeName;
        for (int i = 0; i < shapeType.blocks; i++)
        {
            GameObject blockObj = GameObject.Instantiate(blockPrefab);
            Block block = blockObj.GetComponent<Block>();
            block.InitBlock(shapeType.coordinates[i], gameObject);
            blocks.Add(block);
            blockObj.transform.Translate(gameObject.transform.position.x + (((block.xCoord * 1.0f) - 7.5f)), gameObject.transform.position.y + ((0.5f - block.yCoord * 1.0f)), gameObject.transform.position.z);
            blockObj.GetComponent<SpriteRenderer>().sprite = color;
        }
    }
    public void InitGhostShape(ShapeType shapeType, Board board)
    {
        this.board = board;
        this.shapeType = shapeType;
        rotations = new List<Vector2>(shapeType.rotations);
        rotationsCounter = new List<Vector2>(shapeType.rotationsCounter);
        shapeName = shapeType.shapeName;
        for (int i = 0; i < shapeType.blocks; i++)
        {
            GameObject blockObj = GameObject.Instantiate(blockPrefab);
            Block block = blockObj.GetComponent<Block>();
            block.InitBlock(shapeType.coordinates[i], gameObject);
            blocks.Add(block);
            blockObj.transform.Translate(gameObject.transform.position.x + (((block.xCoord * 1.0f) - 7.5f)), gameObject.transform.position.y + ((0.5f - block.yCoord * 1.0f)), gameObject.transform.position.z);
            blockObj.GetComponent<SpriteRenderer>().sprite = ghostColor;
        }
    }
    public void Rotate(bool clockWise)
    {
        List<Vector2> rotate;
        if(clockWise)
        {
            rotate = rotations;
        }
        else
        {
            rotate = rotationsCounter;
        }
        for (int i = 0; i < blocks.Count; i++)
        {
            Block block = blocks[i];
            GameObject blockObj = block.gameObject;
            blockObj.transform.Translate(rotate[i].x, rotate[i].y, gameObject.transform.position.z);
        }
        UpdateRotations(clockWise);
    }
    public void Drop()
    {
        gameObject.transform.Translate(dropVal);
    }
    public void Slide(int right)
    {
        Vector3 movement = new Vector3(right, 0, 0);
        gameObject.transform.Translate(movement);
    }
    public void Fall(int amount)
    {
        foreach(Block block in blocks)
        {
            block.yCoord += amount;
        }
        Vector3 fall = new Vector3(0, -1 * amount, 0);
        gameObject.transform.Translate(fall);
    }
    public void DropToGhost()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].xCoord = board.ghostShape.blocks[i].xCoord;
            blocks[i].yCoord = board.ghostShape.blocks[i].yCoord;
        }
    }
    public void UpdateGhostShapeCoords(int dropVal)
    {
        for(int i = 0; i < blocks.Count; i++)
        {
            blocks[i].xCoord = board.currentShape.blocks[i].xCoord;
            blocks[i].yCoord = board.currentShape.blocks[i].yCoord + dropVal;
        }
    }
    private void OnDestroy()
    {
        board.oldShapes.Remove(this);
    }
}
