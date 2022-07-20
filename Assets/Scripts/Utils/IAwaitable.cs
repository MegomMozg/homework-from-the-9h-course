namespace Utils
{
    public interface IAwaitable<T>
    {
        IAwaiter<T> GetAwaiter();
    }
}