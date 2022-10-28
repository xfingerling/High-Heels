using System;
using System.Collections.Generic;

public abstract class StateBase
{
    public IState currentState;
    public Dictionary<Type, IState> statesMap;

    protected virtual void InitState()
    {
        statesMap = new Dictionary<Type, IState>();
    }

    public void SetState<T>() where T : IState
    {
        var newState = GetState<T>();

        if (currentState != null)
            currentState.Destruct();

        currentState = newState;
        currentState.Construct();
    }

    public IState GetState<T>() where T : IState
    {
        var type = typeof(T);
        return statesMap[type];
    }

    protected void CreateState<T>() where T : IState, new()
    {
        var state = new T();
        var type = typeof(T);
        statesMap[type] = state;
    }
}
