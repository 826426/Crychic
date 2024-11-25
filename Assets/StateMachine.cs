public class StateMachine
{
    public State currentState;
    public void InitializeState(State _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }
    public void ChangeState(State _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}