using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;


public class ARFaceRegionManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] regionPrefabs;    // Face Region을 표시하기 위한 GameObject 배열
    private ARFaceManager faceManager;    // ARFace  관리자
    private ARSessionOrigin sessionOrigin;


    private NativeArray<ARCoreFaceRegionData> faceRegions;    // Face Region 데이터를 담을 NativeArray


    private void Awake()
    {
        faceManager = GetComponent<ARFaceManager>();
        sessionOrigin = GetComponent<ARSessionOrigin>();

        for (int i = 0; i < regionPrefabs.Length; i++)
        {
            regionPrefabs[i] = Instantiate(regionPrefabs[i], sessionOrigin.trackablesParent);
        }
    }
    private void Update()
    {
        ARCoreFaceSubsystem subsystem = (ARCoreFaceSubsystem)faceManager.subsystem;

        foreach (ARFace face in faceManager.trackables)
        {
            subsystem.GetRegionPoses(face.trackableId, Allocator.Persistent, ref faceRegions);    // 해당 얼굴에서 각 Face Region의 위치와 회전을 얻어 faceRegions NativeArray에 저장

            foreach (ARCoreFaceRegionData faceRegion in faceRegions)
            {
                ARCoreFaceRegion regionType = faceRegion.region;

                regionPrefabs[(int)regionType].transform.localPosition = faceRegion.pose.position;    // Face Region 프리팹의 위치 변경
                regionPrefabs[(int)regionType].transform.localRotation = faceRegion.pose.rotation;    // Face Region 프리팹의 회전 변경
            }
        }
    }


}