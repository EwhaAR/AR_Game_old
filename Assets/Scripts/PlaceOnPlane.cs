using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placedPrefab; // 바닥에 터치할 때 프리팹 생성
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private LayerMask placedObjectLayerMask; // 레이캐스트를 수행할 레이어 마스크 변수
    private Vector2 touchPosition;
    private Ray ray;
    private RaycastHit hit;
    private void Update()
    {
        // 화면이 터치되지 않았거나 터치가 Began 상태가 아니면 함수를 종료합니다.
        if (!Utility.TryGetInputPosition(out touchPosition)) return;

        // 오브젝트 선택(select)
        ray = arCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placedObjectLayerMask))
        {
            PlacedObject.SelectedObject = hit.transform.GetComponentInChildren<PlacedObject>();
            return;
        }

        PlacedObject.SelectedObject = null; // 오브젝트 선택 취소

        // 바닥에 Raycast가 충돌하면 객체를 생성합니다.
        if (Utility.Raycast(touchPosition, out Pose hitPose))
        {
            int index = Random.Range(0, placedPrefab.Length); // 생성할 프리팹의 인덱스를 랜덤하게 선택합니다.
            Instantiate(placedPrefab[index], hitPose.position, hitPose.rotation); // 선택한 프리팹을 위치와 회전값을 기반으로 생성합니다.
        }
    }

}