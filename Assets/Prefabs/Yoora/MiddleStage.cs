using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleStage : MonoBehaviour
{
    public CanvasGroup[] panels; // UI �гε��� ���� �迭

    private int stage = 0; // ���� �������� ����
    private bool isStage2Activated = false; // stage 2���� Ȱ��ȭ�� �г� ���� Ȯ���� ���� ����

    private float fadeTime = 1.5f; // ���̵� �ð� ����
    private float accumTime; // ��� �ð�


    // #JES
    private GameObject dontDestroy;
    private int gameStage;
    private int stageStep;
    public GameSceneManager gameSceneManager;



    public void Start() {
        dontDestroy = GameObject.Find("DontDestroy");
        gameStage = dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage;
        stageStep = dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep;

        // Debug.Log("Stage: " + gameStage);
        // Debug.Log("- Step: " + stageStep);

        // Game Scene Manager
        gameSceneManager = FindObjectOfType<GameSceneManager>();

    }



    private void Update()
    {
        // ��ġ �Է��� �����Ͽ� stage ���� ����

        // #JES: Use mouse in Unity Editor
        #if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))  // if mouse left is clicked 
        {
            int gameStage = dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage;
            dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep++;
            // Debug.Log("Stage: " + dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage);

            gameSceneManager.convertScene();   

        }

        #else
        // If image perception succeded 
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            int gameStage = dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage;
            dontDestroy.GetComponent<DontDestroyOnLoad>().stageStep++;
            // if(dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage < 4)
            // {
            //     dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage++;
            // }
            // Debug.Log("Stage: " + dontDestroy.GetComponent<DontDestroyOnLoad>().gameStage);

            gameSceneManager.convertScene();   

        }
        #endif


        if (gameStage == 2 && !isStage2Activated)
        {
            ActivateRandomPanel(panels.Length);
            isStage2Activated = true;
        }
        else if (gameStage == 3 && isStage2Activated)
        {
            ActivateRandomPanel(panels.Length - 1, gameStage);
            isStage2Activated = false;
        }
    }

    private void checkData()
    {

    }

    private void ActivateRandomPanel(int panelCount, int excludedPanelIndex = -1)
    {
        // ��� �г� ��Ȱ��ȭ
        foreach (CanvasGroup panel in panels)
        {
            panel.alpha = 0f;
        }

        // Ȱ��ȭ�� �г� �ε���
        int panelIndex;

        if (excludedPanelIndex != -1)
        {
            // ������ �г� �ε����� �����ϰ� ������ �г� Ȱ��ȭ
            panelIndex = Random.Range(0, excludedPanelIndex);
            if (panelIndex >= excludedPanelIndex)
            {
                panelIndex += 1;
            }
        }
        else
        {
            // ��� �г� �� ������ �г� Ȱ��ȭ
            panelIndex = Random.Range(0, panelCount);
        }

        // ������ �гο� FadeIn �ִϸ��̼� ����
        CanvasGroup selectedPanel = panels[panelIndex];
        StartCoroutine(FadeIn(selectedPanel, 0f));
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup, float waitTime)
    {
        yield return new WaitForSeconds(0.2f);
        accumTime = 0f;

        float originalAlpha = canvasGroup.alpha;
        float targetAlpha = 1f;

        // Gradually fade in
        while (accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, targetAlpha, accumTime / fadeTime); // ���̵� �� �ִϸ��̼�
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = targetAlpha;

        // Keep it visible for the specified wait time
        yield return new WaitForSeconds(waitTime);

        // Call the FadeOut coroutine
        StartCoroutine(FadeOut(canvasGroup)); // ���̵� �ƿ� �ڷ�ƾ ����
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        accumTime = 0f;

        float originalAlpha = canvasGroup.alpha;
        float targetAlpha = 0f;

        // Gradually fade out
        while (accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(originalAlpha, targetAlpha, accumTime / fadeTime); // ���̵� �ƿ� �ִϸ��̼�
            yield return null;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = targetAlpha;
    }
}
