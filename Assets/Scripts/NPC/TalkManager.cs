using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public NPCManager npcManager;
    public GameObject talkPanel;
    public QuestManager Qmanager;
    public Text talkText;
    public Text questText;
    public GameObject questBar;
    public GameObject scanNpc;
    public int talkIndex;
    public bool isAction;

    // Update is called once per frame
    public void Action(GameObject scanObj)
    {
        scanNpc = scanObj;
        objData objData = scanNpc.GetComponent<objData>();
        Talk(objData.id, objData.isNpc);
        talkPanel.SetActive(isAction);   
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = Qmanager.GetQuestTalkIndex(id);
        string talkData = npcManager.GetTalk(id+ questTalkIndex, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(Qmanager.CheckQuest(id));
            questBar.gameObject.SetActive(true);
            if (RayZ.cnt >=5 && RayZ.cnt <=8)
            {
                questText.text = "코인 2개 수집!";
            }
            else if (RayZ.cnt >= 9)
            {
                questText.text = "쪽지를 찾아 떠나라!";
            }
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            Debug.Log("NPC가 아닙니다.");
        }
        isAction = true;
        talkIndex++;
    }
}
