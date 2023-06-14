using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Monologue
{
    public string name; // 대화주인
    public string sentence; // 문장
    public int bubbleType; // 말풍선 타입 (0: 초록배경 안내문구, 1: 반투명 배경, 2: 생각말풍선 3:말풍선 )
    public GameObject clip; // 부연 이미지
    public Vector2 transform; // 말풍선 위치 조정 가중치 
}

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Monologue> info = new List<Monologue>(); 

 

}
