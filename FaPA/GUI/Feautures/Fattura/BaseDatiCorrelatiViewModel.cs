using System;
using System.Linq.Expressions;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class BaseDatiCorrelatiViewModel : BaseTabsViewModel<Core.Fattura, DatiDocumentiCorrelatiType[]>
    {
        protected BaseDatiCorrelatiViewModel( Expression<Func<Core.Fattura, DatiDocumentiCorrelatiType[]>> getter, 
            IRepository repository, Core.Fattura instance, string dispName, bool isClosable ) : 
                base( getter, repository, instance, dispName, isClosable )
        {
        }

        protected override void AddItemToUserCollection()
        {
            AddToArray();
        }

        protected override void RemoveItemFromUserCollection()
        {
            RemoveFromFixedArray();
        }
    }
}