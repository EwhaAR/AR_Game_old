using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameObject scoreBoardUI;
    TextMeshProUGUI gameSuccessText;
    public static int score;

    private void Start()
    {
        gameObject.GetComponent<Shoot>().enabled = true;
        scoreBoardUI = GameObject.FindGameObjectWithTag("ScoreCanvas");
        scoreText = GameObject.FindGameObjectWithTag("ScoreOnBanner").GetComponent<TextMeshProUGUI>();
        gameSuccessText = GameObject.FindGameObjectWithTag("GameSuccessText").GetComponent<TextMeshProUGUI>();
        gameSuccessText.gameObject.SetActive(false);
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();

        if (score >= 100) //100점 넘으면 성공.
        {
            //Game Success Text 띄우기
            gameSuccessText.gameObject.SetActive(true);

            //거미 다 없애기
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Spider");
            foreach (GameObject enemy in enemies)
                Destroy(enemy); 
        }
    }
}
