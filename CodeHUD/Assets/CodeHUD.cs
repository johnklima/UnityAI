using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CodeHUD : MonoBehaviour
{

    const int MAX_ROWS = 300;
    const int MAX_COLUMNS = 300;

    Rect rect = new Rect(0, 0, 3, 3);
    Color color = new Color(1, 0, 0);

    int curRow = 1;
    int curCol = 1;

    //2d array of ints
    int[,] cells;

    //have we intialized cells above?
    bool isCellsInit = false;

    //some dumbass stuff we need to draw a rectangle because
    //unity has no native primitive draw commands (ulike unreal)
    Texture2D backgroundTexture;
    GUIStyle textureStyle;

    // Use this for initialization
    void Start ()
    {
        //some dumbass stuff we need to draw a rectangle because
        //unity has no native primitive draw commands (ulike unreal)
        backgroundTexture = Texture2D.whiteTexture;
        textureStyle = new GUIStyle { normal = new GUIStyleState { background = backgroundTexture } };

        //redimension our array
        cells = new int[MAX_ROWS, MAX_COLUMNS];

    }

    // Update is called once per frame
    void Update () {

		
	}

    //called when it is time to draw the gui
    private void OnGUI()
    {

        //rect.Set(3 * 10, 3 * 10, 3, 3);
        //drawRect(rect, color);

        //drawCA();
        drawGOL();
        //drawRandomWalker();
    }

    //some dumbass stuff we need to draw a rectangle because
    //unity has no native primitive draw commands (ulike unreal)
    private void drawRect(Rect rectangle, Color color, GUIContent content = null)
    {
        
        var backgroundColor = GUI.backgroundColor;
        GUI.backgroundColor = color;
        GUI.Box(rectangle, content ?? GUIContent.none, textureStyle);
        GUI.backgroundColor = backgroundColor;

    }

    //set up the cells array with zeros or random 0,1
    void initCells(bool random )
    {
        for (int row = 0; row < MAX_ROWS; row++)
        {
            for (int col = 0; col < MAX_COLUMNS; col++)
            {
                if (random)
                {
                    cells[row, col] = Random.Range(0, 2);
                }
                else
                    cells[row, col] = 0;

            }
        }

        isCellsInit = true;

    }

    void drawRandomWalker()
    {
        if (!isCellsInit)
            initCells(false);

        int dir = Random.Range(0, 4);

        //change curCol and curRow according to dir above
        if (dir == 0)
            curRow--;       //UP

        if (dir == 1)
            curCol++;       //RIGHT

        if (dir == 2)
            curRow++;       //DOWN

        if (dir == 3)       //LEFT
            curCol--;


        //make sure curCol and curRow are within the bounds of the array 
        if (curRow < 0)
            curRow = 0;

        if (curRow > MAX_ROWS - 1)
            curRow = MAX_ROWS - 1;

        if (curCol < 0)
            curCol = 0;

        if (curCol > MAX_COLUMNS - 1)
            curCol = MAX_COLUMNS - 1;

        //set the new cells
        cells[curRow,curCol] = 1;

        //render the screen
        drawGeneration();
    }

    void drawCA()
    {
        if (!isCellsInit)
            initCells(false);
    }
    void drawGOL()
    {
        if (!isCellsInit)
            initCells(true);

        generateGOL();

        drawGeneration();

    }

    //draws our current array of cells on the GUI canvas
    void drawGeneration()
    {

        for (int row = 0; row < MAX_ROWS; row++)
        {
            for (int col = 0; col < MAX_COLUMNS; col++)
            {
                if (cells[row,col] > 0)
                {
                    rect.Set(3 * row, 3 * col, 3, 3);
                    drawRect(rect, color);
                }

            }
        }

    }

    //next generation game of life
    void generateGOL()
    {
        int [,] next = new int[MAX_ROWS,MAX_COLUMNS];

	    // Loop through every spot in our 2D array and check spots neighbors
	    for (int row = 1; row<MAX_ROWS - 1; row++) {
		    for (int col = 1; col<MAX_COLUMNS - 1; col++) {

			    // Add up all the states in a 3x3 surrounding grid, not including where i am now
			    int neighbors = 0;

                neighbors += cells[row - 1, col];
			    neighbors += cells[row + 1, col];
			    neighbors += cells[row, col - 1];
			    neighbors += cells[row , col + 1];			
			    neighbors += cells[row + 1 , col + 1];
			    neighbors += cells[row + 1 , col - 1];
			    neighbors += cells[row - 1 , col + 1];
			    neighbors += cells[row - 1 , col - 1];


			    // Rules of Life
			    if ((cells[row , col] == 1) && (neighbors<  2))				// Loneliness 
                    next[row , col] = 0;           
			    else if ((cells[row , col] == 1) && (neighbors >  3))		// Overpopulation
				    next[row , col] = 0;           
			    else if ((cells[row , col] == 0) && (neighbors == 3))		// Reproduction 
				    next[row , col] = 1;           
			    else														// Stasis         
				    next[row , col] = cells[row , col];  
		    }
	    }

	    //now swap new values for old
	    for (int row = 1; row<MAX_ROWS - 1; row++) 
	    {
		    for (int col = 1; col<MAX_COLUMNS - 1; col++) 
		    {
		
			    cells[row , col] = next[row , col];
		
		    }
	    }

    }

}
