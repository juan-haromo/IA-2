using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }
    [SerializeField] State initialState;

    [SerializeField] List<FloatContext> numericalContext;
    [SerializeField] List<TransformContext> spatialContext;
    public NavMeshAgent agent;

    public BlackBoard blackBoard = new BlackBoard();

    void Awake()
    {
        // blackBoard = new BlackBoard();
    }

    void Start()
    {
        ChangeState(initialState);
    }


    public void ChangeState(State state)
    {
        if (CurrentState == state || state == null) { return; }

        if (CurrentState != null)
        {
            CurrentState.Exit(this);
        }
        CurrentState = state;
        CurrentState.Enter(this);
    }

    void Update()
    {
        CurrentState.FrameUpdate(this);
        CurrentState.CheckTransitions(this);
    }


    #region FakeBlackboard
    public bool ContainsNumber(string key)
    {
        return ContainsNumber(key, out FloatContext number);
    }

    public bool ContainsNumber(string key, out FloatContext number)
    {

        number = null;
        foreach (FloatContext c in numericalContext)
        {
            if (c.key == key)
            {
                number = c;
                return true;
            }
        }
        return false;
    }

    public void AddNumber(string key, float value)
    {
        if (ContainsNumber(key))
        {
            UpdateNumber(key, value);
        }
        else
        {
            numericalContext.Add(new FloatContext(key, value));
        }
    }

    public void UpdateNumber(string key, float value)
    {
        for (int i = 0; i < numericalContext.Count; i++)
        {
            if (numericalContext[i].key == key)
            {
                numericalContext[i].value = value;
            }
        }
    }

    public void RemoveNumber(string key)
    {
        if (ContainsNumber(key, out FloatContext toRemove))
        {
            numericalContext.Remove(toRemove);
        }
    }

    public float GetNumber(string key)
    {
        if (ContainsNumber(key, out FloatContext context))
        {
            return context.value;
        }
        return 0;
    }


    public bool ContainsPoint(string key)
    {
        return ContainsPoint(key, out TransformContext point);
    }

    public bool ContainsPoint(string key, out TransformContext point)
    {
        point = null;
        foreach (TransformContext c in spatialContext)
        {
            if (c.key == key)
            {
                point = c;
                return true;
            }
        }
        return false;
    }

    public void AddPoint(string key, Transform point)
    {
        if (ContainsPoint(key))
        {
            UpdatePoint(key, point);
        }
        else
        {
            spatialContext.Add(new TransformContext(key, point));
        }
    }

    public void UpdatePoint(string key, Transform value)
    {
        for (int i = 0; i < spatialContext.Count; i++)
        {
            if (spatialContext[i].key == key)
            {
                spatialContext[i].value = value;
            }
        }
    }

    public void RemovePoint(string key)
    {
        if (ContainsPoint(key, out TransformContext toRemove))
        {
            spatialContext.Remove(toRemove);
        }
    }

    public Transform GetPoint(string key)
    {
        if (ContainsPoint(key, out TransformContext context))
        {
            return context.value;
        }
        return null;
    }
    #endregion
}
