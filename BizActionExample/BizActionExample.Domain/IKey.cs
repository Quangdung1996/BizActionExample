namespace BizActionExample.Domain
{
    public interface IKey<out TKey>
    {
        TKey Id { get; }
    }
}