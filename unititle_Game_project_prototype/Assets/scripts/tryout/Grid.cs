using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{
    private int height;
    private int width;
    private int[,] gridArray;

    public Grid(int width, int height) {
        this.height = height;
        this.width  = width;
        this.gridArray = new int[width,height];

        for(int x=0; x < gridArray.GetLength(0); x++)
        {
            for(int y=0; y<gridArray.GetLength(1); y++)
            {
                Debug.Log(x + ", " + y);
            }
        }
    }
}
