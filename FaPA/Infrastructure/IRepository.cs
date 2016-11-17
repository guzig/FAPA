namespace FaPA.Infrastructure
{
    public interface IRepository
    {
        object Read();
        bool Persist(object entity);
        bool Delete();
    }
}
