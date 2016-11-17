namespace FaPA.Infrastructure.FlyFetch
{
    public interface INotifyDataSourceHit
    {
        void QueryInProgress(bool inProgress);
    }
}
