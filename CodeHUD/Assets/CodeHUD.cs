using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeHUD : MonoBehaviour
{
    
    Rect pos = new Rect(10, 10, 10, 10);
    Color color = new Color(1, 0, 0);

    //some dumbass stuff we need to draw a rectangle because
    //unity has no native primitive draw commands (ulike unreal)
    Texture2D backgroundTexture;
    GUIStyle textureStyle;

    // Use this for initialization
    void Start ()
    {
        backgroundTexture = Texture2D.whiteTexture;
        textureStyle = new GUIStyle { normal = new GUIStyleState { background = backgroundTexture } };        
    }

    // Update is called once per frame
    void Update () {

		
	}

    //called when it is time to draw the gui
    private void OnGUI()
    {
        DrawRect(pos, color);
    }

    private void DrawRect(Rect position, Color color, GUIContent content = null)
    {
        
        var backgroundColor = GUI.backgroundColor;
        GUI.backgroundColor = color;
        GUI.Box(position, content ?? GUIContent.none, textureStyle);
        GUI.backgroundColor = backgroundColor;

    }

}
