using System;
using System.Windows;
using NHibernate;

namespace FaPA.Infrastructure
{
    public interface IDispose
    {
        event Action Disposed;
    }
    public interface IPresenter : IDispose
	{
		DependencyObject View { get; }
		void SetSessionFactory(ISessionFactory theSessionFactory);
		void Show();
		object Result{ get;}
		void ShowDialog();
	}
}