public interface IFactory<T, F>
{
    T Create(F prefab);
}
