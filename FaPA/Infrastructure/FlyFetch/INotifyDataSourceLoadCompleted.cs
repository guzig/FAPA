namespace FaPA.Infrastructure.FlyFetch
{
    public interface INotifyDataSourceLoadCompleted
    {
        void LoadCompleted(bool loadCompleted);
    }
}