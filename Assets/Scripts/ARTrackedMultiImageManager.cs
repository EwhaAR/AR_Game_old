using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackedMultiImageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] trackedPrefabs; // 추적하고자 하는 이미지들에 대한 게임 오브젝트 프리팹들을 담는 배열
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>(); // 추적하고 있는 이미지와 연결된 게임 오브젝트들을 담는 딕셔너리
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>(); // ARTrackedImageManager 컴포넌트를 가져와서 할당

        // 추적하고자 하는 이미지들에 대한 게임 오브젝트를 생성하고, 비활성화한 뒤 spawnedObjects 딕셔너리에 추가
        foreach (GameObject prefab in trackedPrefabs)
        {
            GameObject clone = Instantiate(prefab);
            clone.name = prefab.name;
            clone.SetActive(false);
            spawnedObjects.Add(clone.name, clone);
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged; // ARTrackedImageManager의 추적 이미지 변경 이벤트를 처리하는 함수를 할당
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged; // ARTrackedImageManager의 추적 이미지 변경 이벤트 처리 함수를 제거
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // 이미지가 추가되거나 업데이트될 때마다 UpdateImage 함수 호출
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        // 이미지가 삭제될 때 해당 이미지와 연결된 게임 오브젝트를 비활성화
        foreach (var trackedImage in eventArgs.removed)
        {
            spawnedObjects[trackedImage.name].SetActive(false);
        }
    }
    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name; // 추적 중인 이미지의 이름을 가져옴
        GameObject trackedObject = spawnedObjects[name]; // 추적 중인 이미지와 연결된 게임 오브젝트를 가져옴

        // 추적 중인 상태이면 게임 오브젝트의 위치와 회전 값을 업데이트하고 활성화
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            trackedObject.transform.position = trackedImage.transform.position;
            trackedObject.transform.rotation = trackedImage.transform.rotation;
            trackedObject.SetActive(true);
        }
        // 추적 중이 아니면 게임 오브젝트를 비활성화
        else
        {
            trackedObject.SetActive(false);
        }
    }
}