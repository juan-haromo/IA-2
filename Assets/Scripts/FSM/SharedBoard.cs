using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class SharedBoard : MonoBehaviour
{
    BlackBoard sharedBoard;
    public List<StateMachine> sharers;

    private void Awake()
    {
        sharedBoard = new BlackBoard();
        foreach (StateMachine s in sharers) 
        {
            s.blackBoard = sharedBoard;
        }
    }
}
