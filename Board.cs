using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public List<ShapeType> shapeTypes;
    public GameObject shapePrefab;
    GameObject currentShapeObj;
    public Shape currentShape;
    List<BoardRow> board;
    public List<Shape> oldShapes;
    public Shape ghostShape;
    GameObject ghostShapeObj;
    GameObject nextShapeObj;
    GameObject heldShapeObj;
    int nextShape;
    int nextColor;
    int heldShape;
    int heldColor;
    public GameObject text;
    public GameObject newGameButton;
    public TMP_Text scoreText;
    public TMP_Text levelText;
    public TMP_Text multiplierText;
    public int score;
    public float dropTime;
    private float dropTimer;
    public float slideTime;
    private float slideTimer;
    private int right;
    private int hoverTime = 4;
    public GameObject[] touchPanelObjs;
    private TouchPanel[] touchPanels;
    private int level;
    private int linesToNextLevel;
    private int multiplier;
    private bool notPaused;
    void Start()
    {
        notPaused = true;
        GameManager.manager.newHighScore = false;
        slideTime = 0.1f;
        dropTime = 0.2f;
        slideTimer = slideTime;
        shapeTypes = new ShapeTypes().shapeTypes;
        board = new List<BoardRow>();
        for (int i = 0; i < 48; i++)
        {
            board.Add(new BoardRow());
        }
        dropTimer = dropTime;
        score = 0;
        oldShapes = new List<Shape>();
        nextShape = Random.Range((int)0, shapeTypes.Count);
        nextColor = Random.Range((int)0, (int)2);
        heldShapeObj = null;
        heldShape = 0;
        heldColor = 0;
        level = 1;
        linesToNextLevel = 5;
        multiplier = 1;
        SetupTouchPanels();
        CreateNewShape();
    }
    // Update is called once per frame
    void Update()
    {
         if (notPaused)
         {
            InputLogic();
            GhostShape();
         }
    }
    void SetupTouchPanels()
    {
        touchPanels = new TouchPanel[touchPanelObjs.Length];
        for (int i = 0; i < touchPanelObjs.Length; i++)
        {
            touchPanels[i] = touchPanelObjs[i].GetComponent<TouchPanel>();
        }
    }
    void InputLogic()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || touchPanels[5].pressed)
        {
            touchPanels[5].pressed = false;
            holdNextShape();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || touchPanels[0].pressed)
        {
            touchPanels[0].pressed = false;
            Rotate(true);
        }
        if (Input.GetKeyDown(KeyCode.RightShift) || touchPanels[1].pressed)
        {
            touchPanels[1].pressed = false;
            Rotate(false);
        }
        if (slideTimer <= slideTime / 2)
        {
            if (Input.GetKey(KeyCode.RightArrow) || touchPanels[2].pressed)
            {
                right = 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || touchPanels[3].pressed)
            {
                right = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || touchPanels[4].pressed)
        {
            touchPanels[4].pressed = false;
            DropGhostShape();
        }
        slideTimer -= Time.deltaTime;
        dropTimer -= Time.deltaTime;
        if (slideTimer <= 0)
        {
            if (right != 0)
            {
                Slide(right);
                right = 0;
                hoverTime--;
                if (hoverTime < 0)
                {
                    Drop();
                    hoverTime = 4;
                }
            }
            slideTimer = slideTime;
        }
        if (dropTimer <= 0)
        {
            Drop();
            dropTimer = dropTime;
        }
    }
    void holdNextShape()
    {
        int oldColor = heldColor;
        int oldShape = heldShape;
        GameObject oldHeld = heldShapeObj;
        heldShapeObj = nextShapeObj;
        heldColor = nextColor;
        heldShape = nextShape;
        heldShapeObj.transform.Translate(0, -9, 0);
        nextShapeObj = oldHeld;
        if (nextShapeObj == null)
        {
            int newColor = Random.Range((int)0, (int)currentShape.blockColors.Length);
            if (newColor == nextColor)
            {
                if (newColor == 0)
                {
                    newColor++;
                }
                else
                {
                    newColor--;
                }
            }
            nextColor = newColor;
            int newShape = Random.Range((int)0, shapeTypes.Count);
            if (newShape == nextShape)
            {
                if (newShape == 0)
                {
                    newShape++;
                }
                else
                {
                    newShape--;
                }
            }
            nextShape = newShape;
            nextShapeObj = GameObject.Instantiate(shapePrefab, new Vector3(12.5f, 19, 0), Quaternion.identity);
            Shape nextShapeCode = nextShapeObj.GetComponent<Shape>();
            nextShapeCode.InitShape(shapeTypes[nextShape], nextColor, this);
        }
        else
        {
            nextColor = oldColor;
            nextShape = oldShape;
            nextShapeObj.transform.Translate(0, 9, 0);
        }
    }
    void CreateNewShape()
    {
        if (nextShapeObj != null)
        {
            GameObject.Destroy(nextShapeObj);
        }
        currentShapeObj = Instantiate(shapePrefab);
        currentShape = currentShapeObj.GetComponent<Shape>();
        ShapeType type = shapeTypes[nextShape];
        currentShape.InitShape(type, nextColor, this);
        currentShapeObj.transform.Translate(new Vector3(0, 23, 0));
        for (int i = 0; i < currentShape.blocks.Count; i++)
        {
            Block block = currentShape.blocks[i];
            if (board[block.yCoord].slots[block.xCoord] == 0)
            {
                board[block.yCoord].slots[block.xCoord] = 1;
                board[block.yCoord].filledSlots++;
                board[block.yCoord].blocks.Add(block);
            }
            else
            {
                Debug.Log("GameOver");
                GameOver();
            }
        }
        int newColor = Random.Range((int)0, (int)currentShape.blockColors.Length);
        if (newColor == nextColor)
        {
            if (newColor == 0)
            {
                newColor++;
            }
            else
            {
                newColor--;
            }
        }
        nextColor = newColor;
        int newShape = Random.Range((int)0, shapeTypes.Count);
        if (newShape == nextShape)
        {
            if (newShape == 0)
            {
                newShape++;
            }
            else
            {
                newShape--;
            }
        }
        nextShape = newShape;
        nextShapeObj = GameObject.Instantiate(shapePrefab, new Vector3(12.5f, 19, 0), Quaternion.identity);
        Shape nextShapeCode = nextShapeObj.GetComponent<Shape>();
        nextShapeCode.InitShape(shapeTypes[nextShape], nextColor, this);
        ghostShapeObj = Instantiate(shapePrefab);
        ghostShape = ghostShapeObj.GetComponent<Shape>();
        ghostShape.InitGhostShape(type, this);
        GhostShape();
    }
    bool MoveShapeY()
    {
        //int movedBlocks = -1;
        foreach (Block block in currentShape.blocks)
        {
            board[block.yCoord].blocks.Remove(block);
            board[block.yCoord].slots[block.xCoord] = 0;
            board[block.yCoord].filledSlots--;
        }
        bool canDrop = true;
        foreach (Block block in currentShape.blocks)
        {
            if (block.yCoord + 1 < board.Count)
            {
                if (board[block.yCoord + 1].slots[block.xCoord] == 1)
                {
                    canDrop = false;
                    break;
                }
            }
            else
            {
                canDrop = false;
                break;
            }
        }
        if (canDrop)
        {
            foreach (Block block in currentShape.blocks)
            {
                block.yCoord++;
            }
        }
        foreach (Block block in currentShape.blocks)
        {
            board[block.yCoord].blocks.Add(block);
            board[block.yCoord].slots[block.xCoord] = 1;
            board[block.yCoord].filledSlots++;
        }
        return canDrop;
    }
    bool MoveShapeX(int right)
    {
        foreach (Block block in currentShape.blocks)
        {
            board[block.yCoord].slots[block.xCoord] = 0;
        }
        bool canSlide = true;
        foreach (Block block in currentShape.blocks)
        {
            if (block.xCoord + right < board[0].slots.Count && block.xCoord + right >= 0)
            {
                if (board[block.yCoord].slots[block.xCoord + right] == 1)
                {
                    canSlide = false;
                    break;
                }
            }
            else
            {
                canSlide = false;
                break;
            }
        }
        if (canSlide)
        {
            foreach (Block block in currentShape.blocks)
            {
                block.xCoord += right;
            }
        }
        foreach (Block block in currentShape.blocks)
        {
            board[block.yCoord].slots[block.xCoord] = 1;
        }
        return canSlide;
    }
    public void Slide(int right)
    {
        if (MoveShapeX(right))
        {
            currentShape.Slide(right);
        }
    }
    void OldRowsFall(int rowsDestroyed, int lowestDestroyed)
    {
        foreach (Shape shape in oldShapes)
        {
            foreach (Block block in shape.blocks)
            {
                if (block != null)
                {
                    if (block.yCoord < lowestDestroyed)
                    {
                        block.Fall(rowsDestroyed);
                    }
                }
            }
        }
        for (int i = lowestDestroyed; i > rowsDestroyed; i--)
        {
            board[i].slots = new List<int>(board[i - rowsDestroyed].slots);
            board[i].filledSlots = board[i - rowsDestroyed].filledSlots;
            board[i].blocks = new List<Block>(board[i - rowsDestroyed].blocks);
        }
        for (int i = rowsDestroyed; i >= 0; i--)
        {
            board[i] = new BoardRow();
        }
    }
    public void Drop()
    {
        if (MoveShapeY())
        {
            currentShape.Drop();
        }
        else
        {
            CheckForDestroy();
        }
    }
    bool RotateBoolean(bool clockWise)
    {
        foreach (Block block in currentShape.blocks)
        {
            board[block.yCoord].blocks.Remove(block);
            board[block.yCoord].slots[block.xCoord] = 0;
            board[block.yCoord].filledSlots--;
        }
        bool canRotate = true;
        List<Vector2> rotations;
        if (clockWise)
        {
            rotations = currentShape.rotations;
        }
        else
        {
            rotations = currentShape.rotationsCounter;
        }
        for (int i = 0; i < currentShape.blocks.Count; i++)
        {
            Block block = currentShape.blocks[i];
            if (block.xCoord + Mathf.FloorToInt(rotations[i].x) < board[block.yCoord - Mathf.FloorToInt(rotations[i].y)].slots.Count && block.xCoord + Mathf.FloorToInt(rotations[i].x) >= 0)
            {
                if (block.yCoord - Mathf.FloorToInt(rotations[i].y) < board.Count && block.yCoord - Mathf.FloorToInt(rotations[i].y) >= 0)
                {
                    if (board[block.yCoord - Mathf.FloorToInt(rotations[i].y)].slots[block.xCoord + Mathf.FloorToInt(rotations[i].x)] != 0)
                    {
                        canRotate = false;
                        break;
                    }
                }
                else
                {
                    canRotate = false;
                    break;
                }
            }
            else
            {
                canRotate = false;
                break;
            }
        }
        if (canRotate)
        {
            for (int i = 0; i < currentShape.blocks.Count; i++)
            {
                Block block = currentShape.blocks[i];
                block.yCoord += Mathf.FloorToInt(-rotations[i].y);
                block.xCoord += Mathf.FloorToInt(rotations[i].x);
            }
        }
        foreach (Block block in currentShape.blocks)
        {
            board[block.yCoord].blocks.Add(block);
            board[block.yCoord].slots[block.xCoord] = 1;
            board[block.yCoord].filledSlots++;
        }
        return canRotate;
    }
    public void Rotate(bool clockWise)
    {
        if (RotateBoolean(clockWise))
        {
            currentShape.Rotate(clockWise);
            ghostShape.Rotate(clockWise);
        }
    }
    void ClearRow(int row)
    {
        board[row].filledSlots = 0;
        for (int i = 0; i < board[row].slots.Count; i++)
        {
            board[row].slots[i] = 0;
            board[row].blocks[board[row].blocks.Count - 1].Destroy();
            board[row].blocks.RemoveAt(board[row].blocks.Count - 1);
        }
    }
    void GhostShape()
    {
        int dropVal = -1;
        bool canDrop = true;
        foreach (Block block in currentShape.blocks)
        {
            board[block.yCoord].slots[block.xCoord] = 0;
        }
        for (int i = 0; i < board.Count; i++)
        {
            foreach (Block block in currentShape.blocks)
            {
                if (block.yCoord + i < board.Count)
                {
                    if (board[block.yCoord + i].slots[block.xCoord] == 1)
                    {
                        canDrop = false;
                        break;
                    }
                }
                else
                {
                    canDrop = false;
                    break;
                }
            }
            if (canDrop)
            {
                dropVal++;
            }
            else
            {
                break;
            }
        }
        foreach (Block block in currentShape.blocks)
        {
            board[block.yCoord].slots[block.xCoord] = 1;
        }
        ghostShapeObj.transform.position = new Vector3(currentShapeObj.transform.position.x, currentShapeObj.transform.position.y - dropVal, currentShape.transform.position.z);
        ghostShape.UpdateGhostShapeCoords(dropVal);
    }
    void CheckForDestroy()
    {
        ghostShape = null;
        GameObject.Destroy(ghostShapeObj);
        int rowsDestroyed = 0;
        int scoreVal = 0;
        List<int> clearedRows = new List<int>();
        foreach (Block block in currentShape.blocks)
        {
            if (board[block.yCoord].filledSlots >= board[block.yCoord].slots.Count)
            {
                scoreVal += board[block.yCoord].filledSlots;
                linesToNextLevel--;
                if (linesToNextLevel <= 0)
                {
                    LevelUp();
                }
                ClearRow(block.yCoord);
                clearedRows.Add(block.yCoord);
                rowsDestroyed++;
            }
        }
        if (rowsDestroyed >= 4)
        {
            scoreVal *= 2;
            multiplier++;
            linesToNextLevel -= 4;
            if (linesToNextLevel <= 0)
            {
                LevelUp();
            }
        }
        multiplierText.SetText("Multiplier: " + multiplier);
        AddScore(scoreVal * 100 * multiplier);
        List<Vector2> bottomsAndHeights = new List<Vector2>();
        if (clearedRows.Count > 0)
        {
            clearedRows.Sort();
            clearedRows.Reverse();
            bool start = false;
            Vector2 val = new Vector2();
            val.x = clearedRows[0];
            int rowCount = 1;
            if (clearedRows.Count == 1)
            {
                val.y = rowCount;
                bottomsAndHeights.Add(val);
            }
            else
            {
                if (clearedRows.Count <= 4)
                {
                    GameManager.manager.audioManager.Play(clearedRows.Count);
                }
                else
                {
                    GameManager.manager.audioManager.Play(4);
                }
                for (int i = 1; i < clearedRows.Count; i++)
                {
                    if (start)
                    {
                        start = false;
                    }
                    if (clearedRows[i] != clearedRows[i - 1] - 1)
                    {
                        val.y = rowCount;
                        rowCount = 1;
                        bottomsAndHeights.Add(val);
                        val = new Vector2();
                        start = true;
                        val.x = clearedRows[i];
                    }
                    else
                    {
                        rowCount++;
                        val.y = rowCount;
                    }
                }
                if (start)
                {
                    val.y = 1;
                }
                bottomsAndHeights.Add(val);
            }
        }
        if (currentShape != null)
        {
            oldShapes.Add(currentShape);
        }
        int timesDropped = 0;
        for (int i = 0; i < bottomsAndHeights.Count; i++)
        {
            OldRowsFall(Mathf.FloorToInt(bottomsAndHeights[i].y), Mathf.FloorToInt(bottomsAndHeights[i].x + timesDropped));
            timesDropped++;
        }
        CreateNewShape();
    }
    public void DropGhostShape()
    {
        foreach (Block block in currentShape.blocks)
        {
            board[block.yCoord].slots[block.xCoord] = 0;
            board[block.yCoord].filledSlots--;
            board[block.yCoord].blocks.Remove(block);
        }
        currentShapeObj.transform.position = new Vector3(ghostShapeObj.transform.position.x, ghostShapeObj.transform.position.y, ghostShapeObj.transform.position.z);
        currentShape.DropToGhost();
        foreach (Block block in currentShape.blocks)
        {
            board[block.yCoord].slots[block.xCoord] = 1;
            board[block.yCoord].filledSlots++;
            board[block.yCoord].blocks.Add(block);
        }
        CheckForDestroy();
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.SetText("Score: " + score);
        if (score > GameManager.manager.highScore)
        {
            GameManager.manager.NewHighScore(score);
        }
    }
    void LevelUp()
    {
        linesToNextLevel = 5 + (5 * (level / 5));
        level++;
        multiplier++;
        levelText.SetText("Level: " + level);
        dropTime = Mathf.Max(0.2f - (level * 0.01f), 0.075f);
    }
    public void GameOver()
    {
        Debug.Log("test");
        GameManager.manager.audioManager.Play(1);
        notPaused = false;
        text.SetActive(false);
        newGameButton.SetActive(true);
    }
    public void NewGame()
    {
        newGameButton.SetActive(false);
        GameManager.manager.newHighScore = false;
        board = new List<BoardRow>();
        for (int i = 0; i < 48; i++) 
        {
            board.Add(new BoardRow());
        }
        score = 0;
        GameObject.Destroy(currentShapeObj);
        GameObject.Destroy(ghostShapeObj);
        foreach (Shape shape in oldShapes)
        {
            GameObject.Destroy(shape.gameObject);
        }
        oldShapes = new List<Shape>();
        nextShape = Random.Range((int)0, shapeTypes.Count);
        nextColor = Random.Range((int)0, (int)2);
        heldShapeObj = null;
        heldShape = 0;
        heldColor = 0;
        level = 1;
        linesToNextLevel = 5;
        multiplier = 1;
        AddScore(0);
        CreateNewShape();
        notPaused = true;
    }
}
public class BoardRow
{
    public List<int> slots;
    public int filledSlots;
    public List<Block> blocks;
    public BoardRow()
    {
        slots = new List<int>();
        blocks = new List<Block>();
        for (int i = 0; i < 16; i++)
        {
            slots.Add(0);
        }
        filledSlots = 0;
    }
}