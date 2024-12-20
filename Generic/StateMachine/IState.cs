
public interface IState
{
    public void OnStateEnter();

    public void OnStateUpdate();

    public IState OnStateProcessState();

    public void OnStateExit();
}
