namespace FaPA.Infrastructure.Finder
{
    public interface INumsSearchProperty : ISearchProperty
    {
        NumOperatorEnums OperatorType { get;  }
    }
}