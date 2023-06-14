using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARFaceMaterialSwitcher : MonoBehaviour
{
    [SerializeField]
    private Material[] materials; // 사용할 머티리얼 배열
    private ARFaceManager faceManager; // ARFace 관리자
    private int index = 0; // 현재 머티리얼 배열 인덱스

    private void Start()
    {
        faceManager = GetComponent<ARFaceManager>(); // ARFaceManager 구성 요소 가져오기
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // 터치 입력이 감지되면(모바일 기기에서 터치를 input으로 받음)
        {
            index = (index + 1) % materials.Length; // 머티리얼 배열 인덱스 증가 및 범위 내에서 순환

            // 모든 ARFace 오브젝트에 대해 새로운 머티리얼 할당
            foreach (ARFace face in faceManager.trackables)
            {
                face.GetComponent<MeshRenderer>().material = materials[index];
            }
        }
    }
}

