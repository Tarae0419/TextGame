using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    [SerializeField] string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = theParser.Parse(csv_FileName);
            for(int i = 0;i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]);
            }
            isFinish = true;
        }
    }


    public Dialogue[] GetDialogue(int StartNum, int EndNum)
    {
        List<Dialogue> dialougeList = new List<Dialogue>();

        for(int i = 0; i <= EndNum - StartNum; i++)
        {
            dialougeList.Add(dialogueDic[StartNum + i]);
        }

        return dialougeList.ToArray();
    }
}
