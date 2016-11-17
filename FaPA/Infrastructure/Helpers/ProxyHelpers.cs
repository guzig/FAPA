using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using FaPA.Core;
using FaPA.GUI.Utils;
using NHibernate.Proxy;

namespace FaPA.Infrastructure.Helpers
{
    public static class ProxyHelpers
    {
        public static void UnproxiedCollection<T>(ObservableCollection<T> collection) where T : BaseEntity
        {
            ShowCursor.Show();
            var isStale = false;
            var lastStaleResult = 0;
            do
            {
                isStale = false;
                for (var i = lastStaleResult; i < collection.Count; i++)
                {
                    if (collection[i].Id != 0) continue;
                    collection[i].IsProxy();
                    isStale = true;
                    lastStaleResult = i;
                    break;
                }
                if (isStale)
                    Thread.Sleep(50);
            } while (isStale);
        }

        //public static void UnproxiedCollection<T>(IEnumerable collection) where T : BaseEntityDto
        //{
        //    var isStale = false;
        //    do
        //    {
        //        isStale = false;

        //        foreach (var currentItem in collection.Cast<BaseEntityDto>().Where(OnTryUnproxy))
        //        {
        //            currentItem.IsProxy();
        //            isStale = true;
        //            break;
        //        }

        //        if (isStale)
        //            Thread.Sleep(50);
        //    } while (isStale);
        //}

        public static void UnproxiedItems(DataGrid grid)
        {
            bool isStale;
            var lastStaleResult = 0;
            do
            {
                isStale = false;

                for (var index = lastStaleResult; index < grid.Items.Count; index++)
                {
                    var currentItem = grid.Items[index];
                    var entityDto = currentItem as BaseEntity;
                    if (entityDto == null || entityDto.Id != 0) continue;
                    entityDto.IsProxy();
                    isStale = true;
                    lastStaleResult = index;
                    break;
                }

                if (isStale)
                    Thread.Sleep(50);

            } while (isStale);

        }

        //public static void UnproxiedSelectedItems( DataGrid grid )
        //{
        //    bool isStale;
        //    var lastStaleResult = 0;
        //    do
        //    {
        //        isStale = false;

        //        for ( var index = lastStaleResult; index < grid.SelectedItems.Count; index++ )
        //        {
        //            var selectedItem = grid.SelectedItems[index];
        //            var entityDto = selectedItem as BaseEntityDto;
        //            if ( entityDto == null || entityDto.Id != 0 ) continue;
        //            entityDto.IsProxy();
        //            isStale = true;
        //            lastStaleResult = index;
        //            break;
        //        }

        //        if ( isStale )
        //            Thread.Sleep( 50 );

        //    } while ( isStale );

        //}

        public static PropertyChangedEventHandler TryGetPropChangedEventHandler(this object entity)
        {
            var baseEntity = (BaseEntity)entity;
            PropertyChangedEventHandler changedEventHandler = null;
            if (baseEntity?.PropertyChangedEventHandler != null)
                changedEventHandler = baseEntity.PropertyChangedEventHandler;
            return changedEventHandler;
        }
    }
}