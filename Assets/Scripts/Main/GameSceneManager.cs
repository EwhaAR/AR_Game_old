using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    // #JES
    public GameObject dontDestroy;
    public int gameStage;
    public int stageStep;

    public void Start() {
        dontDestroy = GameObject.Find("DontDestroy");
        gameStage = dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage;
        stageStep = dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep;

        // Debug.Log("Stage: " + gameStage);
        // Debug.Log("- Step: " + stageStep);

    }


    public void convertScene()
    {
        gameStage = dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage;
        stageStep = dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep;
        Debug.Log("STAGE: " + gameStage + " - Step: " + stageStep);

        // Stage 1
        if(gameStage == 1)
        {
            switch (stageStep)
            {
                // 1.1. Travel(BackDoor)
                case 1:  
                    // StartMinigame(gameStage);
                    TravelCampus();
                    break;
                // 1.2. Intro Scene
                case 2:
                    dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep++;
                    // GoToIntrox2);
                    SceneManager.LoadScene("Intro_bckup");  
   
                    break;                 
                // 1.3. Minigame
                case 3:
                    StartMinigame(gameStage);
                    break;
                // 1.4. Intro Scene
                case 4: 
                    dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep = 1;
                    dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage++;
                    // GoToIntro(3);
                    SceneManager.LoadScene("Intro_bckup");  

                    break;                 
            }
        }
        // Stage 2
        else if(gameStage == 2)
        {
            switch (stageStep)
            {
                // 2.1. Travel
                case 1:
                    TravelCampus();
                    break;
                // 2.2. Minigame 2
                case 2:
                    StartMinigame(gameStage);  // 거미 슈팅
                    break;
            }
        }
        // Stage 3
        else if(gameStage == 3)
        {
            switch (stageStep)
            {
                // 3.1. Travel
                case 1:
                    TravelCampus();
                    break;
                // 2.3. Minigame 2
                case 2:
                    StartMinigame(gameStage);  // 쓰레기 던지기
                    break;
            }
        }
        // Stage 4
        else if(gameStage == 4)  // 4. Ending
        {
            GoToEnding();
        }

    }

    public void StartMinigame(int gameStage) // throwing food into cat bowl
    {
        Debug.Log("Start Minigame " + gameStage);

        switch (gameStage)
        {
            case 1:
                Debug.Log("Minigame 1");  
                SceneManager.LoadScene("Minigame1");  
                break;

            case 2:
                Debug.Log("Minigame 2");  
                SceneManager.LoadScene("ShootingGame_yw");  
                break;

            case 3:
                Debug.Log("Minigame 3");  
                SceneManager.LoadScene("Minigame2");               
                break;

            default:
                Debug.Log("gameStage: index out of range");  
                break;
        }

        // if(gameStage == 1)
        //     SceneManager.LoadScene("Minigame1");  
        // else if(gameStage == 2)
        //     SceneManager.LoadScene("ShootingGame_yw");  
        // else if(gameStage == 3)
        //     SceneManager.LoadScene("Minigame2");               
    }

    public void TravelCampus()
    {
        SceneManager.LoadScene("Yoora");       
    }

    public void GoToIntro(int intro_num)
    {
        switch (intro_num)
        {
            case 1:
                SceneManager.LoadScene("Intro1"); 
                break;
            case 2:
                SceneManager.LoadScene("Intro2");    
                break;   
            case 3:
                SceneManager.LoadScene("Intro3"); 
                break;

        }
    }

    public void GoToEnding()
    {
        SceneManager.LoadScene("Ending");       
    }


}
