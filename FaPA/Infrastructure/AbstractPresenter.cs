using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using FaPA.Core;
using FaPA.Infrastructure.Helpers;
using NHibernate;
using NHibernate.Criterion;
using Action = System.Action;

namespace FaPA.Infrastructure
{
    public abstract class AbstractPresenter<TModel, TView> : PropertyChangedBase, IDisposable, IPresenter
		where TView : Window, new()
	{
		private TModel _model;
        private ISession _session;
		private IStatelessSession _statelessSession;

		protected AbstractPresenter()
		{
			View = new TView();
			View.Closed += (sender, args) => Dispose();
		}

		DependencyObject IPresenter.View { get { return View; } }

		public object Result { get; protected set; }

		protected TView View { get; private set; }

        protected ISessionFactory SessionFactory { get; private set; }

        protected ISession Session
		{
			get
			{
			    if ( _session != null ) return _session;
                //new AddPropertyChangedInterceptor()
                return _session = SessionFactory.OpenSession( );

            }
		}

		protected IStatelessSession StatelessSession
		{
			get { return _statelessSession ?? (_statelessSession = SessionFactory.OpenStatelessSession() ); }
		}

		protected TModel Model
		{
			get { return _model; }
			set
			{
				_model = value;
				View.DataContext = _model;
			}
		}

		protected void ReplaceSessionAfterError()
		{
			if(_session!=null)
			{
				_session.Dispose();
				_session = SessionFactory.OpenSession();
				ReplaceEntitiesLoadedByFaultedSession();
			}
		    if (_statelessSession == null) return;
		    _statelessSession.Dispose();
		    _statelessSession = SessionFactory.OpenStatelessSession();
		}

		protected virtual void ReplaceEntitiesLoadedByFaultedSession()
		{
			throw new InvalidOperationException(
				@"ReplaceSessionAfterError was called, but the presenter does not override ReplaceEntitiesLoadedByFaultedSession!
You must override ReplaceEntitiesLoadedByFaultedSession to call ReplaceSessionAfterError.");
		}
        
		public void SetSessionFactory(ISessionFactory theSessionFactory)
		{
			SessionFactory = theSessionFactory;
		}

		public void Show()
		{
			View.Show();
		}

		public void ShowDialog()
		{
			View.ShowDialog();
		}

        protected IList<T> GetExeCriteria<T>( QueryOver criteria )
        {
            IList<T> entities;
            using (var tx = Session.BeginTransaction())
            {
                entities = criteria.DetachedCriteria.GetExecutableCriteria( Session ).List<T>();
                tx.Commit();
            }
            return entities;
        }
        
        protected IList<T> GetExeCriteriaAsReadOnly<T>( DetachedCriteria criteria )
        {
            IList<T> entities;
            using ( var tx = Session.BeginTransaction() )
            {
                entities = criteria.GetExecutableCriteria( Session ).SetReadOnly( true ).List<T>();
                tx.Commit();
            }
            return entities;
        }

        protected IList<T> GetExeCriteria<T>(DetachedCriteria criteria)
        {
            IList<T> entities;
            using (var tx = Session.BeginTransaction())
            {
                entities = criteria.GetExecutableCriteria(Session).SetReadOnly(true).List<T>();
                tx.Commit();
            }
            return entities;
        }
        
		public event Action Disposed = delegate { };

		public virtual void Dispose()
		{
			if(_session!=null)
				_session.Dispose();
			if (_statelessSession != null)
				_statelessSession.Dispose();
			View.Close();
			Disposed();
		}

        private static void WaitForNonStaleResults<T>( bool isfetchCompleted, bool isSelectedAll, Action onLoadedCollection,
            ObservableCollection<T> collection ) where T : BaseEntity
        {
            if ( isfetchCompleted || !isSelectedAll )
                onLoadedCollection();

            ProxyHelpers.UnproxiedCollection( collection );
        }

        protected void UnProxySelectedItems<T>( bool isSelectedAll, ObservableCollection<T> collection,
            DataGrid grid, Action onLoadedCollection ) where T : BaseEntity
        {
            if ( isSelectedAll )
                Application.Current.Dispatcher.Invoke( () => WaitForNonStaleResults( true, true, 
                    onLoadedCollection, collection ) );
            else
                Application.Current.Dispatcher.Invoke( ()=> ProxyHelpers.UnproxiedItems( grid ) );
        }

	}
}