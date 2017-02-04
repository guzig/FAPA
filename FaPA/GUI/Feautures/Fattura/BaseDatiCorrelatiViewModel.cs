using System;
using System.Linq.Expressions;
using FaPA.Core.FaPa;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;

namespace FaPA.GUI.Feautures.Fattura
{
    public class BaseDatiCorrelatiViewModel : BaseTabsViewModel<DatiGeneraliType, DatiDocumentiCorrelatiType[]>
    {
        protected BaseDatiCorrelatiViewModel( Expression<Func<DatiGeneraliType, DatiDocumentiCorrelatiType[]>> getter, 
            IRepository repository, DatiGeneraliType instance, string dispName, bool isClosable ) : 
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