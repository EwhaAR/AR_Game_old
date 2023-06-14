using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;

public class Utility
{
    private static ARRaycastManager raycastManager; // ARRaycastManager 컴포넌트를 담을 변수
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>(); // Raycast 결과를 저장할 리스트

    // 정적 생성자에서 ARRaycastManager 컴포넌트를 찾아서 변수에 할당
    static Utility()
    {
        raycastManager = GameObject.FindObjectOfType<ARRaycastManager>();
    }

    // 화면 터치 입력 위치에 대한 Raycast 수행하는 함수
    // 화면 위치, Raycast 성공 시 결과 값을 out 파라미터로 받음
    public static bool Raycast(Vector2 screenPosition, out Pose pose)
    {
        if (raycastManager.Raycast(screenPosition, hits, TrackableType.AllTypes)) // Raycast 수행
        {
            pose = hits[0].pose; // 결과값 중 첫 번째 오브젝트의 Pose 값을 반환
            return true; // Raycast 성공
        }
        else
        {
            pose = Pose.identity; // Pose 값을 기본값으로 설정
            return false; // Raycast 실패
        }
    }

    // 터치 입력 위치를 가져오는 함수
    // 터치 입력이 없으면 false, 있으면 true를 반환하고 입력 위치를 out 파라미터로 받음
    public static bool TryGetInputPosition(out Vector2 position)
    {
        position = Vector2.zero; // 입력 위치를 초기화

        if (Input.touchCount == 0) // 터치 입력이 없으면 false 반환
        {
            return false;
        }

        position = Input.GetTouch(0).position; // 첫 번째 터치 입력 위치를 가져옴

        if (Input.GetTouch(0).phase != TouchPhase.Began) // 첫 번째 터치 입력이 Began 상태가 아니면 false 반환
        {
            return false;
        }

        return true; // 터치 입력이 있고 Began 상태이면 true 반환
    }
}