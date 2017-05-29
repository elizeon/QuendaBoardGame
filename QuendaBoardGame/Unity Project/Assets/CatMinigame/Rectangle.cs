using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Rectangle class
 * 
 * By Elizabeth Haynes
 * */
public struct Rectangle
{
    public Vector2 topLeft { get; set; }
    public Vector2 bottomRight { get; set; }

    public Vector2 topRight { get { return new Vector2(topLeft.x + bottomRight.x, topLeft.y); } }
    public Vector2 bottomLeft { get { return new Vector2(bottomRight.x - topLeft.x, bottomRight.y); } }


    public float Top { get { return topRight.y; } }
    public float Bottom { get { return bottomRight.y; } }
    public float Right { get { return topRight.x; } }
    public float Left { get { return topLeft.x; } }

    public float Width { get { return topRight.x - topLeft.x; } }
    public float Height { get { return topLeft.y - bottomLeft.y; } }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle"/> struct.
    /// </summary>
    /// <param name="start">The start.</param>
    /// <param name="end">The end.</param>
    public Rectangle(Vector2 start, Vector2 end)
    {
        topLeft = start;
        bottomRight = end;
    }

    public Rectangle(float startx, float starty, float width, float height)
    {
        topLeft = new Vector2(startx, starty);
        bottomRight = new Vector2(startx + width, starty + height);
    }


    /// <summary>
    /// Checks if this rectangle intersects with another given rectangle.
    /// Formula from http://www.geeksforgeeks.org/find-two-rectangles-overlap/
    /// </summary>
    /// <param name="rect2">The second rectangle.</param>
    /// <returns></returns>
    public bool Intersects(Rectangle rect2)
    {
        if (topLeft.x > rect2.bottomRight.x || rect2.topLeft.x > bottomRight.x)
        {
            return false;
        }

        if (topLeft.y > rect2.bottomRight.y || rect2.topLeft.y > bottomRight.y)
        {
            return false;
        }



        return true;


        //if(topLeft.x>rect2.bottomRight.x || bottomLeft)
    }

    public static bool operator ==(Rectangle r1, Rectangle r2)
    {
        if (r1.topLeft == r2.topLeft && r1.bottomRight == r2.bottomRight)
        {
            return true;
        }
        return false;
    }

    public static bool operator !=(Rectangle r1, Rectangle r2)
    {
        if (!(r1.topLeft == r2.topLeft && r1.bottomRight == r2.bottomRight))
        {
            return true;
        }
        return false;
    }



}