public interface IState
{
    void Construct();
    void Destruct();
    void Update();
    void Transition();
}
