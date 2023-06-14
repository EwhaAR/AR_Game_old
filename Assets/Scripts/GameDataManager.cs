using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static int[] stageClearInfo = { 0, 0, 0, 0, 0 }; // save only in run time

    // #JES
    public static int[] visitedPlaceInfo = { 0, 0, 0, 0, 0, 0, 0, 0 };


    public GameObject dontDestroy;


    private void Awake()
    {

        // #JES
        int[] placeSerializedData = Load();


        // #JES
        if (placeSerializedData != null)
        {
            visitedPlaceInfo = placeSerializedData;
        }

        Debug.Log(stageClearInfo[0]);


        dontDestroy = GameObject.Find("DontDestroy");
        dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage = 1;
        dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep = 1;

    }
    public int[] Load()
    {

        string visitedPlaces = PlayerPrefs.GetString("visitedPlacesData");
        string[] stringArray = visitedPlaces.Split(',');

        if (visitedPlaces != string.Empty)
        {
            Debug.Log("Load the visited places data");
            // Convert data type of the array from string to int
            int[] serializedData = new int[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                int.TryParse(stringArray[i], out serializedData[i]);
            }

            return serializedData;
        }

        Debug.Log("Cannot find the data of visited places");

        return null;

    }


    public void ClearStage(int stage_num)
    {
        stageClearInfo[stage_num] = 1;

    }

    // #JES: 
    public void ClearStage(int stage_num, int place_num)
    {
        stageClearInfo[stage_num] = place_num;
    }


    public void Save()
    {

        Debug.Log("saving data for visited places");
        string placeSerializedData = string.Join(",", visitedPlaceInfo.Select(x => x.ToString()).ToArray());
        PlayerPrefs.SetString("visitedPlacesData", placeSerializedData);
    }


    public int isCleared(int stage_num)
    {
        return stageClearInfo[stage_num];
    }

    // #JES
    public int[] getVisitedPlaces()
    {

        return visitedPlaceInfo;

    }
    public void VisitPlace(int place_num)
    {
        visitedPlaceInfo[place_num] = 1;

    }


    public int isVisited(int place_num)
    {
        return visitedPlaceInfo[place_num];
    }

    public int getNumOfVisitedPlace()
    {
        int cnt = 0;
        for(int i=0;i<visitedPlaceInfo.Length;i++)
        {
            if (visitedPlaceInfo[i] != 0)
            {
                cnt++;
            }
        }

        return cnt;
    }
}
