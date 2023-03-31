using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteManager : MonoBehaviour
{
    public bool IsVote;


    private void Awake()
    {
        IsVote = false;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Vote(int SelectNum)
    {
        if(IsVote == true)
        {
            //Deselect
        }
        else
        {

        }
    }
}
