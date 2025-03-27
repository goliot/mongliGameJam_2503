using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fsm<T>
{
    protected SortedList<T, FsmState<T>> stateList = new SortedList<T, FsmState<T>>();
    protected FsmState<T> curState = null;
    protected FsmState<T> nextState = null;

    public T currentState;
    public T nextStateValue;

    #region - virtual
    public virtual void Clear()
    {
        stateList.Clear();
        curState = null;
        nextState = null;
        currentState = default(T);
        nextStateValue = default(T);
    }

    public virtual void AddFsm(FsmState<T> _state)
    {
        if (true == stateList.ContainsKey(_state.getState))
            return;
        stateList.Add(_state.getState, _state);
    }

    public virtual void SetState(T _state)
    {
        if (false == stateList.ContainsKey(_state))
            return;
        nextState = stateList[nextStateValue = _state];
    }

    public virtual void Update()
    {
        if (null != nextState)
        {
            if (null != curState)
                curState.End();

            curState = nextState;
            currentState = nextStateValue;
            nextState = null;
            nextStateValue = default(T);
            curState.Enter();
        }

        if (null != curState)
            curState.Update();
    }
    #endregion
}
