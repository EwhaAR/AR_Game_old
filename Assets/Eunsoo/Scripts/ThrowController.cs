// 추가한 기능들 (#ES)
// 1. 한 번에 하나의 오브젝트만 들 수 있게끔 (기존에 이미 물체가 holding되어 있는데도 또 두 손가락으로 터치하면 새로운 물체 생성됨, 기존 물체와 부딪혀서 튕겨나감)
// 2. 한 번 떨어진 물체는 다시 움직일 수 없게끔 (한 번 떨어진 물체는 더 이상 충돌 불가하게 수정)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowController : MonoBehaviour
{
    private const float CameraDistance = 7.5f;
    public float positionY = 0.4f;
    public GameObject[] prefab;

    protected Camera mainCamera;
    protected GameObject HoldingObject;
    protected Vector3 InputPosition;

    // #ES1: Make sure to hold one object at a time (Prevent newly held object to hit the previous held one)
    public bool isAlreadyTouched2 = false;

    // Player Guide
    MinigameManager minigameScript; 


    void Start()
    {
        mainCamera = Camera.main; // Get the main camera of the scene
        minigameScript = GameObject.Find("ESGameManager").GetComponent<MinigameManager>();

        // Reset();
        
    }

    void Update()
    {
        // Player Guide

        // Use mouse click if executed in Unity Editor(for testing)
        // #if !UNITY_EDITOR
        // if (Input.touchCount == 0) return;
        // #endif

        InputPosition = TouchHelper.TouchPosition;  // If touched, get the touched position

        // touched with 2 fingers: Create new object to throw
        if(TouchHelper.Touch2)
        {
            if(!isAlreadyTouched2) {  // #ES1
                Reset();

                // Player Guide
                minigameScript.showGuidelines(3);  // 3: Drag the object to throw
            }
            return;
        }

        // dragged: Throw the object 
        if(HoldingObject)  // Check if the object is not null
        {
            // If a touch input has ended/been released
            if(TouchHelper.IsUp)
            {
                isAlreadyTouched2 = false;  // #ES1

                OnPut(InputPosition);  // Throw the object with force
                
                // 충돌을 아예 불가능하게 해버리면 평면에 쌓이질 않음 (평면과 닿자마자 disable하려고 했는데 복잡... Flag를 사용해야할 것 같음)
                // #ES2: Disable all kinds of collision for the thrown object
                // Collider[] colliders = HoldingObject.GetComponentsInChildren<Collider>();
                // foreach (Collider collider in colliders)
                // {
                //     collider.enabled = false;
                // }

                // the object is no longer being held
                HoldingObject = null;  

                // Player Guide
                minigameScript.showGuidelines(2);  // 2: Touch the screen with two fingers to create new object to throw

                return;
            }
            Move(InputPosition);  // Move the object to the touched position
            return;
        }

        if (!TouchHelper.IsDown) return;

        // Check if the casted ray hits the object with tag "ThrownObject"(object thrown by the player)
        if(Physics.Raycast(mainCamera.ScreenPointToRay(InputPosition), out var hits, mainCamera.farClipPlane))
        {
            if(hits.transform.gameObject.tag.Equals("ThrownObject"))
            {
                // Assign the object that was hit
                HoldingObject = hits.transform.gameObject;
                OnHold();
            }
        }
    }

    // Stop holding the object
    protected virtual void OnPut(Vector3 pos)
    {
        HoldingObject.GetComponent<Rigidbody>().useGravity = true;  // Enable gravity for the held object
        HoldingObject.transform.SetParent(null);

        // Rigidbody rb = HoldingObject.GetComponent<Rigidbody>();
        // rb.isKinematic = true; // Make the Rigidbody kinematic to fix its position
    }

    // Move the held object towards the target position based on the camera's perspective
    private void Move(Vector3 pos)
    {
        pos.z = mainCamera.nearClipPlane * CameraDistance;
        HoldingObject.transform.position = Vector3.Lerp(HoldingObject.transform.position, mainCamera.ScreenToWorldPoint(pos), Time.deltaTime * 7f);
    }

    // Hold the object by positioning it in the game world based on the camera's viewpoint
    protected virtual void OnHold()
    {
        HoldingObject.GetComponent<Rigidbody>().useGravity = false;  // Hold the object by disabling gravity for it
        HoldingObject.transform.SetParent(mainCamera.transform);  // Make the object as a child of main camera (to move and rotate relative to the camera)
        HoldingObject.transform.rotation = Quaternion.identity;
        HoldingObject.transform.position = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCamera.nearClipPlane * CameraDistance));
    }

    // Create a new object and reset it to its initial state
    private void Reset()
    {
        var pos = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCamera.nearClipPlane * CameraDistance));
        var obj = Instantiate(prefab[0], pos, Quaternion.identity, mainCamera.transform);
        var rigidbody = obj.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        isAlreadyTouched2 = true;  // #ES1
    }
}