using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;
    private int targetScore = 1;
    private Text scoreText;

    private void Awake()
    {
        scoreText = GameObject.Find("SuccessScoreText").GetComponent<Text>();
        scoreText.text = "성공: " + currentScore + "/" + targetScore;
    }

    private void Update() {
        checkScore();
    }

    //(충돌이 일어나는 동안 점수가 무한 증가하지 않게 쓰레기통 안에 투명 box collider 추가)
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("ThrownObject"))
        {
            UpdateScoreText();
        }
    }
    
    // Increase score by one for one collision 
    private void UpdateScoreText()
    {
        currentScore++;
        scoreText.text = "성공: " + currentScore + "/" + targetScore;
    }

    private void checkScore()
    {
        if(currentScore >= targetScore)
        {
            GameObject.Find("ESGameManager").GetComponent<MinigameManager>().endMinigame();
        }
    }
}
