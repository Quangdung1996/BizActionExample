namespace BizActionExample.Domain
{
    public interface ICompareChange<in T> where T : IDomainObject
    {
        ChangeValueCollection GetChanges(T other);
    }
}