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

        if (score >= 100) //100�� ������ ����.
        {
            //Game Success Text ����
            gameSuccessText.gameObject.SetActive(true);

            //�Ź� �� ���ֱ�
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Spider");
            foreach (GameObject enemy in enemies)
                Destroy(enemy); 
        }
    }
}
