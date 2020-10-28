using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeType
{
    public List<Vector2> coordinates;
    public string shapeName;
    public int blocks;
    public List<Vector2> rotations;
    public List<Vector2> rotationsCounter;
    public ShapeType(List<Vector2> coordinates, string shapeName, List<Vector2> rotations, List<Vector2> rotationsCounter)
    {
        this.coordinates = coordinates;
        this.shapeName = shapeName;
        blocks = coordinates.Count;
        this.rotations = rotations;
        this.rotationsCounter = rotationsCounter;
    }
}
public class ShapeTypes
{
    public List<ShapeType> shapeTypes;
    public ShapeTypes()
    {
            shapeTypes = new List<ShapeType>() { new ShapeType(new List<Vector2> { new Vector2(7, 1), new Vector2(8, 1), new Vector2(7, 0), new Vector2(8, 0) }, "Square",
            new List<Vector2>{ new Vector2(0, 1), new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1)},
            new List<Vector2>{ new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 0)}),

            new ShapeType(new List<Vector2> {new Vector2(8, 2), new Vector2(7, 1), new Vector2(8, 1), new Vector2(8, 0)}, "T",
            new List<Vector2>{ new Vector2(-1, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, -1)},
            new List<Vector2>{ new Vector2(1, 1), new Vector2(1, -1), new Vector2(0, 0), new Vector2(-1, -1)}),

            new ShapeType(new List<Vector2> {new Vector2(7, 2), new Vector2(8, 2), new Vector2(8, 1), new Vector2(8, 0)}, "LBack",
            new List<Vector2>{ new Vector2(0, 2), new Vector2(-1, 1), new Vector2(0, 0), new Vector2(1, -1)},
            new List<Vector2>{ new Vector2(2, 0), new Vector2(1, 1), new Vector2(0, 0), new Vector2(-1, -1)}),

            new ShapeType(new List<Vector2> {new Vector2(8, 2), new Vector2(9, 2), new Vector2(8, 1), new Vector2(8, 0)}, "LFront",
            new List<Vector2>{ new Vector2(-1, 1), new Vector2(-2, 0), new Vector2(0, 0), new Vector2(1, -1)},
            new List<Vector2>{ new Vector2(1, 1), new Vector2(0, 2), new Vector2(0, 0), new Vector2(-1, -1)}),

            new ShapeType(new List<Vector2> {new Vector2(7, 2), new Vector2(7, 1), new Vector2(8, 1), new Vector2(8, 0)}, "ZBack",
            new List<Vector2>{ new Vector2(-1, 1), new Vector2(0, 0), new Vector2(-1, -1), new Vector2(0, -2)},
            new List<Vector2>{ new Vector2(1, 1), new Vector2(0, 0), new Vector2(-1, 1), new Vector2(-2, 0)}),

            new ShapeType(new List<Vector2> {new Vector2(8, 2), new Vector2(7, 1), new Vector2(8, 1), new Vector2(7, 0)}, "ZFront",
            new List<Vector2>{ new Vector2(-1, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(2, 0)},
            new List<Vector2>{ new Vector2(1, 1), new Vector2(1, -1), new Vector2(0, 0), new Vector2(0, -2)}),

            new ShapeType(new List<Vector2> {new Vector2(6, 0), new Vector2(7, 0), new Vector2(8, 0), new Vector2(9, 0)}, "Line",
            new List<Vector2>{ new Vector2(2, 2), new Vector2(1, 1), new Vector2(0, 0), new Vector2(-1, -1)},
            new List<Vector2>{ new Vector2(2, -2), new Vector2(1, -1), new Vector2(0, 0), new Vector2(-1, 1)})};
    }
}
