using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public int questId;
    public int questActionIndex;
    

    Dictionary<int, QuestData> questList;

    // Start is called before the first frame update
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        questList.Add(10, new QuestData("코인 2개 수집!", new int[] { 1000 }));
        //{}에는 해당 퀘스트와 관련된 npcID 기입
        questList.Add(20, new QuestData("쪽지를 찾아 떠나라!", new int[] { 1000 }));

    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        if (questActionIndex == questList[questId].npcId.Length)
            Next();

        return questList[questId].questName;
    }

    void Next()
    {
        questId =20;
        questActionIndex = 0;
        
    }
}
