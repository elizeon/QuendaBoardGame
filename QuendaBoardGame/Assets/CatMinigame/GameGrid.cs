using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {


        public CatGame catGame { get; private set; }


        // Use this for initialization
        void Start()
        {
            catGame = FindObjectOfType<CatGame>();

        }

        // Update is called once per frame
        void Update()
        {

        }
        Vector2[,] centrePoints;
        /// <summary>
        /// Creates a grid
        /// </summary>
        /// <param name="xsize">number of squares along screen x axis</param>
        /// <param name="ysize">number of squares along screen y axis</param>
        /// <param name="squareLength">square length as proportion of screen</param>
        public void Set(int xsize, int ysize, float squareLength)
        {
            centrePoints = new Vector2[xsize, ysize];

            for (int x = 0; x < xsize; x++)
            {
                for (int y = 0; y < ysize; y++)
                {
                    centrePoints[x, y] = new Vector2(x * squareLength * Screen.width, y * squareLength * Screen.height);
                }
            }

        }

        /*
        /// <summary>
        /// Returns point in centre of chosen grid square.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>

        Vector2 GetPoint(int x, int y)
        {
            return new Vector2(centrePoints[x,y].x, centrePoints[x,y].y);
        }

        */


        /// <summary>
        /// Gets point as proportion of game screen
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector2 GetPoint(float x, float y)
        {
            if (x <= 1 && y <= 1)
            {
                return new Vector2(x * Screen.width, y * Screen.height);
            }
            else
            {
                return default(Vector2);
            }
        }

        public Vector2 GetPointWorld(Camera cam, float x, float y)
        {
            if (x <= 1 && y <= 1)
            {
            Vector2 myVec = cam.ViewportToWorldPoint(new Vector3(x, y, 0.01f));
            return myVec;
            //return myVec;
            }
            else
            {
                return default(Vector2);
            }
        }


}
