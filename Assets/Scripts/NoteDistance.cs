using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDistance : MonoBehaviour
{
    public GameObject Player;
    public GameObject NoteInfo; //노트 읽기 메시지
    public GameObject Note; // 노트 UI
    public GameObject NoteBtn; //노트의 버튼
    public GameObject NoteText; //노트 내용
    public GameObject Allcan; // 전체 캔버스 


    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, 3f, LayerMask.GetMask("Note")))
        { //플레이어로부터 발사된 레이저의 3f 거리내에 쪽지 오브젝트가 감지된다면 쪽지에 대한 클릭 메시지가 팝업된다.
            NoteInfo.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.T))
            { //키보드 T 버튼으로 쪽지의 내용을 볼 수 있다.
                Note.gameObject.SetActive(true);
                NoteBtn.gameObject.SetActive(true);
                NoteText.gameObject.SetActive(true);
                Allcan.gameObject.SetActive(false);
                NoteInfo.gameObject.SetActive(false);
            }
        }
        else
            NoteInfo.gameObject.SetActive(false);
    }
}
