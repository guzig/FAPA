using System.Collections.ObjectModel;
using FaPA.Infrastructure.FlyFetch;

namespace FaPA.GUI.Feautures.Fattura
{

    public class SearchFatturaQueryObject : BaseQueryPaginatorObject<Core.Fattura, Core.Fattura>, ICountProvider, 
        IPageProvider<Core.Fattura, ObservableCollection<Core.Fattura>>
        
    {

        public SearchFatturaQueryObject(INotifyDataSourceHit notifyHit, INotifyDataSourceLoadCompleted notifyDataSourceLoad)
            : base(notifyHit, notifyDataSourceLoad)
        {}

        public SearchFatturaQueryObject(INotifyDataSourceHit notifyHit) : base(notifyHit)
        {}
    }
}
