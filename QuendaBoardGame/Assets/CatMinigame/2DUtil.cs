using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _2DUtil
    {

	// Use this for initialization
	void Start ()
{
		
	}
	
	// Update is called once per frame
	void Update ()
{
		
	}
    
        /// <summary>
        /// Returns true if the first value is the same as the second
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsAt(Vector2 first, Vector2 second)
        {
            if (first.x == second.x && first.y == second.y)
            {
                return true;
            }
            return false;
        }


        public static float Distance(Vector2 first, Vector2 second)
        {
            return (float)Mathf.Sqrt(Mathf.Pow(second.x - first.x, 2) + Mathf.Pow(second.y- first.y, 2));
        }
        public static float LookAt(Vector2 source, Vector2 dest)
        {
            Vector2 direction = dest - source;
            return (float)Mathf.Atan2(direction.y, direction.x);

        }
        public static void MoveTowards(GameObject2D obj, Vector2 dest, float step)
        {
            Vector2 direction = (dest - obj.pos2D).normalized;
            obj.SetPos2D(obj.pos2D + direction * step);
            //PrintXnaVec(obj.pos2D);


        if (Distance(obj.pos2D, dest) <= step)
            {
                //Console.WriteLine(obj.pos2D.X + "," + obj.pos2D.Y);
                //Console.WriteLine(dest.X + "," + dest.Y);
                obj.SetPos2D(new Vector2(dest.x, dest.y));

            }

        }


    public static void PrintVec(Vector2 vec)
    {
        Debug.Log("Vector: " +vec.x + "," + vec.y);
    }
    
    /*
    public static bool CheckCollision(GameObject2D obj1, GameObject2D obj2)
    {
        //if(obj1.collisions && obj2.collisions)
        //{
            if (obj1.boundingBox.Intersects(obj2.boundingBox))
            {
                return (true);

            }

        //}
        return false;

    }
    */

    public static bool CheckSphereCollision(Vector2 pos1, float radius1, Vector2 pos2, float radius2)
        {
            if (_2DUtil.Distance(pos1, pos2) <= radius1 + radius2)
            {
                return true;
            }

            return false;
        }


        
        public static bool CheckCollision(Rectangle obj1, Rectangle obj2)
        {

            if (obj1==default(Rectangle) || obj2==default(Rectangle))
            {
                return false;
            }

            if (obj1.Intersects(obj2))
            {
                return (true);

            }
            return (false);
        }

        public static bool CheckCollision(Rectangle obj1, Vector2 obj2)
        {

            if (obj1 == default(Rectangle))
            {
                return false;
            }

            if (obj2.x <= obj1.Right && obj2.x >= obj1.Left && obj2.y >= obj1.Top && obj2.y <= obj1.Bottom)
            {
                return (true);

            }
            return (false);
        }

        public static void ResolveCollision(GameObject2D obj1, GameObject2D obj2)
        {
            obj1.SetPos2D(new Vector2(obj1.lastPosNoCol.x, obj1.lastPosNoCol.y));
            obj2.SetPos2D(new Vector2(obj2.lastPosNoCol.x, obj2.lastPosNoCol.y));
        }

    }
