using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// static: TouchHelper will be used frequently
public static class TouchHelper
{
    // Use mouse click when executed in Unity Editor(for testing)
    #if UNITY_EDITOR
    public static bool Touch2 => Input.GetMouseButtonDown(1);  // If mouse right is clicked
    public static bool IsDown => Input.GetMouseButtonDown(0);  // If mouse left is clicked
    public static bool IsUp => Input.GetMouseButtonUp(0);
    public static Vector2 TouchPosition => Input.mousePosition;

    // Use touch(tab) when executed in actual devices
    #else
    public static bool Touch2 => Input.touchCount == 2 && (Input.GetTouch(1).phase == TouchPhase.Began);  // if touched by two fingers
    public static bool IsDown => Input.GetTouch(0).phase == TouchPhase.Began;  // true if a touch has just begun (by one finger)
    public static bool IsUp => Input.GetTouch(0).phase == TouchPhase.Ended;  // true if a touch has ended or been released
    public static Vector2 TouchPosition => Input.GetTouch(0).position;
    #endif
}
