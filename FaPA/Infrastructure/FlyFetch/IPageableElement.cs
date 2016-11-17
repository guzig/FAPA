namespace FaPA.Infrastructure.FlyFetch
{
    public interface IPageableElement
    {
        int PageIndex { get; set; }
        bool Loaded { get; set; }
    }
}
