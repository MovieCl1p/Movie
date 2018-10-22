using Core.Commands;
using Core.States;
using UnityEngine;

public class Test : MonoBehaviour, IStateMachineContainer
{

    private StateMachine _stateMachine;
    private StateFlow _flow;

    public GameObject GameObject
    {
        get
        {
            return gameObject;
        }
    }

    public void Next(StateCommand previousState)
    {
        _flow.Next(previousState);
    }
    
    public void Start ()
    {
        _stateMachine = new StateMachine(this);
        _flow = new StateFlow(this, _stateMachine);


        _flow.Add(new NextStatePair(typeof(GeneralState), typeof(ConnectToServer)));
        _flow.Add(new NextStatePair(typeof(ConnectToServer), typeof(JoinRandomRoom), typeof(CreateRoom)));
        

        _stateMachine.ApplyState<GeneralState>();
    }
}

public class GeneralState : StateCommand
{
    protected override void OnStart(object[] args)
    {
        ScheduleUpdate(1, false);
    }

    protected override void OnScheduledUpdate()
    {
        FinishCommand(true);
    }
}

public class ConnectToServer : StateCommand
{
    protected override void OnStart(object[] args)
    {
        ScheduleUpdate(1, false);
    }

    protected override void OnScheduledUpdate()
    {
        FinishCommand(false);
    }
}

public class JoinRandomRoom : StateCommand
{
    protected override void OnStart(object[] args)
    {

    }
}

public class CreateRoom : StateCommand
{
    protected override void OnStart(object[] args)
    {

    }
}