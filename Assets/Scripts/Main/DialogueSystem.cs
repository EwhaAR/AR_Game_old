using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;



public class DialogueSystem : MonoBehaviour
{

    public GameObject translucentBubble; // ������ ��ǳ��
    public GameObject greenBubble; // �ʷ� ��ǳ��
    public GameObject thinkingBubble; // ���� ��ǳ�� 
    public GameObject speechBubble; //  �����̿� ��ǳ�� 
    private Dialogue DialogueInfo; 
    private GameObject BeforeMonologueImage;
    Queue<Monologue> dialogues = new Queue<Monologue>();
    // ��ųʸ� ����
    Dictionary<int, GameObject> bubbleMap = new Dictionary<int, GameObject>();
    // ��ǳ�� Ÿ�� (0: ������ ���, 1: �ʷϹ�� �ȳ�����, 2: ������ǳ, 3: ��ǳ��)


    // Start is called before the first frame update

    private void Awake()
    {
        // ��ųʸ��� ������ �߰�
        bubbleMap.Add(0, translucentBubble);
        bubbleMap.Add(1, greenBubble);
        bubbleMap.Add(2, thinkingBubble);
        bubbleMap.Add(3, speechBubble);
    }
    public void Begin(Dialogue dialogue)

    {  

        this.DialogueInfo = dialogue;


        dialogues.Clear();


        foreach(var monologue in dialogue.info)
        {
            dialogues.Enqueue(monologue);

        }

        // ù���� init 
        Next();
       
    }

    public void DeactivateAllTxtGui()
    {
        translucentBubble.SetActive(false);
        greenBubble.SetActive(false);
        thinkingBubble.SetActive(false);
        speechBubble.SetActive(false);
    }

    public void Next()
    {
        if (dialogues.Count == 0)
        {
            End();
        }
        else
        {

            // ���� ��ȭ���� Ȱ��ȭ�� �̹����� �ִٸ� ���� 

            DeactivateAllTxtGui();

            if (BeforeMonologueImage != null && BeforeMonologueImage.tag != "Maintain")
            {
                
                    BeforeMonologueImage.SetActive(false);
                    BeforeMonologueImage = null;
              
               
            }

            Monologue monologue = dialogues.Dequeue();

            TextMeshProUGUI txtSentence;

        
          
            switch (monologue.bubbleType)
            {

                // ��ǳ�� ������ ���� switch
                case 0:
                    translucentBubble.SetActive(true);
                   txtSentence = translucentBubble.GetComponentInChildren<TextMeshProUGUI>();
                    txtSentence.text = monologue.sentence;
                    break;


                case 1:
                    greenBubble.SetActive(true);
                    txtSentence = greenBubble.GetComponentInChildren<TextMeshProUGUI>();
                    txtSentence.text = monologue.sentence;
                    break;

                case 2:
                    thinkingBubble.SetActive(true);
                    txtSentence = thinkingBubble.GetComponentInChildren<TextMeshProUGUI>();
                    txtSentence.text = monologue.sentence;
                    break;

                case 3:
                    speechBubble.SetActive(true);
                    txtSentence = speechBubble.GetComponentInChildren<TextMeshProUGUI>();
                    txtSentence.text = monologue.sentence;
                    break;
    
             
            }
           
           
            // ���� ������ ������ ���� Ȱ��ȭ
            if (monologue.clip!=null)
            {
                monologue.clip.SetActive(true);
                BeforeMonologueImage = monologue.clip;
            }

            // ��ǳ�� ��ġ ������ ������ ��ǳ�� ��ġ ����
            if(monologue.transform!=null)
            {
                Vector2 movement = monologue.transform;
                bubbleMap[monologue.bubbleType].transform.Translate(new Vector3(movement.x, movement.y, 0f));
                    
            }
           
        }
    }

    public void End()
    {

        if (BeforeMonologueImage != null )
        {

            BeforeMonologueImage.SetActive(false);
   

        }
      
        DeactivateAllTxtGui();
        IntroGameManager introGameManager = FindObjectOfType<IntroGameManager>();
        GameSceneManager gameSceneManager = FindObjectOfType<GameSceneManager>();

        // if(gameStage == 1 && stageStep == 1)
        // {
        //     IntroGameManager.doesDialogue1End = true;
        //     introGameManager.Dialogue1End();
        //     introGameManager.Dialogue2End();

        // }

        if (IntroGameManager.doesDialogue1End == false)
        {
            IntroGameManager.doesDialogue1End = true;
            introGameManager.Dialogue1End();
        }
        else if (IntroGameManager.doesDialogue2End == false)
        {
            IntroGameManager.doesDialogue2End = true;
            introGameManager.Dialogue2End();
        }
        else
        {
            // Convert to minigame scene (throwing food into feed the cat)
            gameSceneManager.convertScene();
            
        }
        


    }
}
