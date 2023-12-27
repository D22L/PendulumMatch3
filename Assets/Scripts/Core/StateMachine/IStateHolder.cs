public interface IStateHolder
{
    IState TryGetState<T>() where T: IState;
}
