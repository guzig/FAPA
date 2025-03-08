using System;
using System.Linq.Expressions;
using WAFEL.Core.CORE.DomainServices.Helpers;
using FaPA.GUI.Controls;
using FaPA.Infrastructure;
using WAFEL.Core.FaPa;

namespace FaPA.GUI.Feautures.Fattura
{
    public class BaseDatiCorrelatiViewModel : CrudListViewModel<DatiGeneraliType, DatiDocumentiCorrelatiType[]>
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

        public override DatiGeneraliType ReadInstance()
        {
            var root = Repository.Read();
            return  ( ( WAFEL.Core.CORE.Fattura ) root ).DatiGenerali;
        }
    }
}