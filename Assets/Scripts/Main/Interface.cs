using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[SerializeField]
public class Interface : MonoBehaviour
{
    public GameObject[] steps;
    public AudioSource audioSource;
    public GameObject soundOnImage;
    public GameObject soundOffImage;
    public static bool musicState = true; // 0:off, 1: on  

    public TextMeshProUGUI numOfVisitedPlace;


    private void Start()
    {



        UpdateProgress();
        updateVisitedPlace();


    }
    public void CheckClearStage(int stage_num)
    {
        if (GameDataManager.stageClearInfo[stage_num] != 0)
        {


            Transform childTransform = transform.Find("completed");

            Debug.Log(childTransform);
            if (childTransform != null)
            {
                GameObject completed = childTransform.gameObject;

                completed.SetActive(true);
            }


        }
    }

    public void onClickHome()
    {
        SceneManager.LoadScene("Main");
    }

    public void onSound()
    {
        audioSource.mute = false; 
        musicState = true;

        soundOnImage.SetActive(true);
        soundOffImage.SetActive(false);
    }

    public void offSound()
    {

        audioSource.mute = true;
        musicState = false;

        soundOnImage.SetActive(false);
        soundOffImage.SetActive(true);
    }

    public void updateVisitedPlace()
    {
        GameDataManager gameDataManager = new GameDataManager();

        numOfVisitedPlace.text = "Visited: " + gameDataManager.getNumOfVisitedPlace() + " / 10";


    }

    public void UpdateProgress() // update cleared stage 
    {

  
        for (int i = 0; i < steps.Length; i++)
        {
            if (GameDataManager.stageClearInfo[i+1] != 0)
            {
              
              
                Transform childTransform = steps[i].transform.Find("completed");

                Debug.Log(childTransform);
                if (childTransform != null)
                {
                    GameObject completed = childTransform.gameObject;

                    completed.SetActive(true);
                }
             

            }
        }
    }
}
