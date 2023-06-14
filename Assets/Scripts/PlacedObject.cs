using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacedObject : MonoBehaviour
{
    [SerializeField]
    private GameObject cubeSelected; // 선택된 객체를 표시하는 큐브 프리팹
    public bool IsSelected
    {
        get => SelectedObject == this;
    }
    private static PlacedObject selectedObject; // 현재 선택된 객체
    public static PlacedObject SelectedObject
    {
        get => selectedObject;
        set
        {
            if (selectedObject == value)
            {
                return;
            }

            if (selectedObject != null)
            {
                selectedObject.cubeSelected.SetActive(false); // 이전에 선택된 객체를 비활성화
            }

            selectedObject = value; // 새로운 객체를 선택

            if (value != null)
            {
                value.cubeSelected.SetActive(true); // 새로운 객체를 활성화
            }
        }
    }
    private void Awake()
    {
        cubeSelected.SetActive(false); // 선택된 큐브를 비활성화
    }
    // 터치 후 드래그하는 위치로 현재 선택된 오브젝트를 이동
    public void OnPointerDrag(BaseEventData bed)
    {
        if (IsSelected) // 현재 객체가 선택된 객체인 경우
        {
            PointerEventData ped = (PointerEventData)bed;
            if (Utility.Raycast(ped.position, out Pose hitPose)) // 터치 위치에 Raycast를 수행하여 plane에 부딪히는 위치를 얻음
            {
                transform.position = hitPose.position; // 현재 선택된 객체를 plane에 부딪힌 위치로 이동시킴
            }
        }
    }
}