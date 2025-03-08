using System.Collections.ObjectModel;
using FaPA.Infrastructure.FlyFetch;

namespace FaPA.GUI.Feautures.SearchFattura
{

    public class SearchFatturaQueryObject : BaseQueryPaginatorObject<WAFEL.Core.CORE.Fattura, WAFEL.Core.CORE.Fattura>, ICountProvider, 
        IPageProvider<WAFEL.Core.CORE.Fattura, ObservableCollection<WAFEL.Core.CORE.Fattura>>
        
    {

        public SearchFatturaQueryObject(INotifyDataSourceHit notifyHit, INotifyDataSourceLoadCompleted notifyDataSourceLoad)
            : base(notifyHit, notifyDataSourceLoad)
        {}

        public SearchFatturaQueryObject(INotifyDataSourceHit notifyHit) : base(notifyHit)
        {}
    }
}
