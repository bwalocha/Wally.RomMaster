namespace Wally.Database
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
