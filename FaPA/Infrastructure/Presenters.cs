using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using FaPA.GUI.Design.Commands;
using FaPA.Infrastructure.Helpers;
using FaPA.Infrastructure.Utils;

namespace FaPA.Infrastructure
{
    public class Presenters
	{
		public static object Show(string name, params object[] args)
		{
            ShowCursor.Show();

			var instance = CreateInstance(name, args);

			instance.Show();

		    return instance;
		}

        public static object ShowDialog(string name, params object[] args)
		{
			var instance = CreateInstance(name, args);

			instance.ShowDialog();

			return instance;
		}

        public static IPresenter CreateInstance(string name, object[] args)
		{
			var type = Assembly.GetExecutingAssembly().GetType("FaPA.GUI.Feautures." + name + ".Presenter");
			
            if (type == null)
				throw new InvalidOperationException("Could not find presenter: " + name);

			var instance = (IPresenter)Activator.CreateInstance(type);

			instance.SetSessionFactory(BootStrapper.SessionFactory);

			WireEvents(instance);
			WireButtons(instance);
            //WireCommands( instance );
            //WireSplitButtons(instance);
            //WireDataGridsDoubleClick(instance);
            //WireDataGridsSelectionChanged(instance);

            if (args != null && args.Length > 0)
			{
				var init = type.GetMethod("Initialize");

                //if (init == null)
					//throw new InvalidOperationException("Presenter to be shown we argument, but not initialize method found");

				init.Invoke(instance, args);
			}
			return instance;
		}

        //private static void WireDataGridsDoubleClick(IPresenter presenter)
        //{
        //	var presenterType = presenter.GetType();
        //	var methodsAndDataGrids = from method in GetActionMethods(presenterType)
        //	                          where method.Name.EndsWith("Choosen")
        //							  where method.GetParameters().Length == 1
        //	                          let elementName = method.Name.Substring(2, method.Name.Length - 2 /*On*/- 7 /*Choosen*/)
        //                                    let matchingDataGrid = presenter.View.TryFindChild<PodGrid>(t => t.Name == "PoDsGrid")
        //							  where matchingDataGrid != null
        //	                          select new {method, matchingDataGrid};

        //	foreach (var methodAndEvent in methodsAndDataGrids)
        //	{
        //		var parameterType = methodAndEvent.method.GetParameters()[0].ParameterType;
        //		var action = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(parameterType),
        //		                                     presenter, methodAndEvent.method);

        //              methodAndEvent.matchingDataGrid.PoDGridControl.MouseDoubleClick += (sender, args) =>
        //		{

        //                  var item1 = ((DataGrid)sender).CurrentItem;
        //			if(item1 == null)
        //				return;
        //			action.DynamicInvoke(item1);
        //		};
        //	}	
        //}

        //      private static void WireDataGridsSelectionChanged(IPresenter presenter)
        //      {

        //          var presenterType = presenter.GetType();
        //          var methodsAndDataGrids = from method in GetActionMethods(presenterType)
        //                                    where method.Name.EndsWith("Selected")
        //                                    where method.GetParameters().Length == 1
        //                                    let elementName = method.Name.Substring(2, method.Name.Length - 2 /*On*/- 8 /*Selected*/)
        //                                    let matchingDataGrid = presenter.View.TryFindChild<PodGrid>(t => t.Name == "PoDsGrid")
        //                                    where matchingDataGrid != null
        //                                    select new { method, matchingDataGrid };

        //          foreach (var methodAndEvent in methodsAndDataGrids)
        //          {
        //              var parameterType = methodAndEvent.method.GetParameters()[0].ParameterType;
        //              var action = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(parameterType),
        //                                                   presenter, methodAndEvent.method);

        //              methodAndEvent.matchingDataGrid.PoDGridControl.SelectionChanged += (sender, args) =>
        //              {
        //                  var item1 = ((DataGrid)sender).SelectedItem;
        //                  if (item1 == null)
        //                      return;
        //                  action.DynamicInvoke(item1);
        //              };
        //          }
        //      }

        //private static void WireCommands( IPresenter presenter )
        //{
        //    var presenterType = presenter.GetType();
        //    var methodsAndButtons =
        //            from method in GetParameterlessActionMethods( presenterType )
        //            let elementName = method.Name.Substring( 2 )
        //            let matchingControl = presenter.View.TryFindChild<InstanceNullManager>( t => t.Name == elementName )
        //            let fact = presenterType.GetProperty( "Can" + elementName )
        //            where matchingControl != null
        //            select new { method, fact, button = matchingControl };

        //    foreach ( var matching in methodsAndButtons )
        //    {
        //        var action = ( Action ) Delegate.CreateDelegate( typeof( Action ), presenter, matching.method );
        //        Fact fact = null;
        //        if ( matching.fact != null )
        //            fact = ( Fact ) matching.fact.GetValue( presenter, null );
        //        matching.button.ActionLinked.Command = new DelegatingCommand( action, fact );
        //    }
        //}

        private static void WireButtons(IPresenter presenter)
		{
			var presenterType = presenter.GetType();
			var methodsAndButtons =
					from method in GetParameterlessActionMethods(presenterType)
					let elementName = method.Name.Substring(2)
                    let matchingControl = presenter.View.TryFindChild<Button>(t => t.Name == elementName)
					let fact = presenterType.GetProperty("Can" + elementName)
					where matchingControl != null
					select new { method, fact, button = matchingControl  };

			foreach (var matching in methodsAndButtons)
			{
				var action = (Action)Delegate.CreateDelegate(typeof(Action),presenter, matching.method);
				Fact fact = null;
				if (matching.fact != null)
					fact = (Fact)matching.fact.GetValue(presenter, null);
				matching.button.Command = new DelegatingCommand(action, fact);
			}
		}

        //private static void WireSplitButtons(IPresenter presenter)
        //{
        //    var presenterType = presenter.GetType();
        //    var methodsAndButtons =
        //            from method in GetParameterlessActionMethods(presenterType)
        //            let elementName = method.Name.Substring(2)
        //            let matchingControl = presenter.View.TryFindChild<SplitButton>(t => t.Name == elementName)
        //            let fact = presenterType.GetProperty("Can" + elementName)
        //            where matchingControl != null
        //            select new { method, fact, button = matchingControl };

        //    foreach (var matching in methodsAndButtons)
        //    {
        //        var action = (Action)Delegate.CreateDelegate(typeof(Action), presenter, matching.method);
        //        Fact fact = null;
        //        if (matching.fact != null)
        //            fact = (Fact)matching.fact.GetValue(presenter, null);
        //        matching.button.Command = new DelegatingCommand(action, fact);
        //    }
        //}

		/// <summary>
		/// Here we simply match methods on the presenter to events on the view
		/// The convention is that any method started with "On" and having no parameters
		/// will be matched with an event with the same name (without the On prefix)
		/// assuming that the event is of RoutedEventHandler type.
		/// </summary>
		private static void WireEvents(IPresenter presenter)
		{
			var viewType = presenter.View.GetType();
			var presenterType = presenter.GetType();
			var methodsAndEvents =
					from method in GetParameterlessActionMethods(presenterType)
					let matchingEvent = viewType.GetEvent(method.Name.Substring(2))
					where matchingEvent != null
					where matchingEvent.EventHandlerType == typeof(RoutedEventHandler)
					select new { method, matchingEvent };

			foreach (var methodAndEvent in methodsAndEvents)
			{
				var action = (Action)Delegate.CreateDelegate(typeof(Action), presenter, methodAndEvent.method);

				var handler = (RoutedEventHandler)((sender, args) => action());
				methodAndEvent.matchingEvent.AddEventHandler(presenter.View, handler);
			}
		}

		private static IEnumerable<MethodInfo> GetActionMethods(Type type)
		{
			return from method in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
				   where method.Name.StartsWith("On")
				   select method;
		}

		private static IEnumerable<MethodInfo> GetParameterlessActionMethods(Type type)
		{
		    return (from method in GetActionMethods(type)
		        where method.GetParameters().Length == 0
		        select method).ToArray();
		}
	}
}