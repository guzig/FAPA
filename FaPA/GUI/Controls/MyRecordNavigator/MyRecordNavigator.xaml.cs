using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FaPA.Infrastructure.Utils;


namespace FaPA.GUI.Controls.MyRecordNavigator
{
    /// <summary>
    /// Logica di interazione per MyRecordNavigator.xaml
    /// </summary>
    public partial class MyRecordNavigator : UserControl, INotifyPropertyChanged
    {
        public class IsVisibleEventArgs : EventArgs
        {
            public IsVisibleEventArgs(bool isVisible)
            {
                IsVisible = isVisible;
            }

            public bool IsVisible;

        }
        #region - Constructors -

        public MyRecordNavigator()
        {

            InitializeComponent();
            LstItems = PART_List;
            PART_2.DataContext = this;
            IsVisibleChangedHandler += IsVisibleHandler;

        }

        private void IsVisibleHandler(IsVisibleEventArgs eventArgs)
        {
            if (eventArgs.IsVisible)
                ScrollIntoView(GridSource,LstItems);
        }

        public delegate void IsVisibleEventHandler(IsVisibleEventArgs e);

        public static event IsVisibleEventHandler IsVisibleChangedHandler;

        private static void OnIsVisibleChanged(IsVisibleEventArgs e)
        {
            if (IsVisibleChangedHandler != null)
                IsVisibleChangedHandler(e);
        }

        #endregion

        #region public methods

        //public IPresenter Presenter { get; set; }

        #endregion

        #region - Static Methods -

        #endregion

        #region - Dependency Properties -

        public static readonly DependencyProperty GridSourceProperty =
            DependencyProperty.Register("GridSource", typeof(DataGrid),
               typeof(MyRecordNavigator), new FrameworkPropertyMetadata(null, 
                   OnGridSourceChanged));

        private static void OnGridSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldGrid = e.OldValue as DataGrid;
            if (oldGrid != null) 
                oldGrid.IsVisibleChanged -= GridIsVisibleChanged;

            var grid = e.NewValue as DataGrid;
            if (grid != null) 
                grid.IsVisibleChanged += GridIsVisibleChanged;
        }
        
        private static void GridIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isVisible = e.NewValue is bool && (bool) e.NewValue;
            OnIsVisibleChanged(new IsVisibleEventArgs(isVisible));
        }
        
        public static readonly DependencyProperty ItemsSourceProperty=
            DependencyProperty.Register("ItemsSource", typeof(ICollectionView),
               typeof(MyRecordNavigator), new FrameworkPropertyMetadata(null,
               OnItemsSourceChanged));

        private int _currentPos;
        public int CurrentPos
        {
            get { return _currentPos; }
            set
            {
                _currentPos = value;
                OnPropertyChanged("CurrentPos");
            }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var navigator = d as MyRecordNavigator;

            if (navigator != null)
                navigator.LstItems.ItemsSource = (ICollectionView) e.NewValue;
        }

        public ICollectionView ItemsSource
        {
            get { return (ICollectionView)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataGrid GridSource
        {
            get { return (DataGrid)GetValue(GridSourceProperty); }
            set
            {
                SetValue(GridSourceProperty, value);
            }
        }

        public static DependencyProperty IsSynchronizedWithCurrentItemProperty;
        public bool IsSynchronizedWithCurrentItem
        {
            get { return (bool)GetValue(IsSynchronizedWithCurrentItemProperty); }
            set { SetValue(IsSynchronizedWithCurrentItemProperty, value); }
        }
        #endregion

        #region - Routed Commands -


        private ICommand _moveFirstCommand;
        public ICommand MoveFirstCommand
        {
            get
            {
                if (_moveFirstCommand == null)
                {
                    _moveFirstCommand = new RelayCommand(
                        param => MoveFirstCommandExecuted(),
                        param => MoveFirstCommandCanExecute()
                        );
                }
                return _moveFirstCommand;
            }

        }

        private ICommand _moveLastCommand;
        public ICommand MoveLastCommand
        {
            get
            {
                if (_moveLastCommand == null)
                {
                    _moveLastCommand = new RelayCommand(
                        param => MoveLastCommandExecuted(),
                        param => MoveLastCommandCanExecute()
                        );
                }
                return _moveLastCommand;
            }

        }

        private ICommand _movePreviousCommand;
        public ICommand MovePreviousCommand
        {
            get
            {
                if (_movePreviousCommand == null)
                {
                    _movePreviousCommand = new RelayCommand(
                        param => MovePreviousCommandExecuted(),
                        param => MovePreviousCommandCanExecute()
                        );
                }
                return _movePreviousCommand;
            }

        }

        private ICommand _moveNextCommand;
        public ICommand MoveNextCommand
        {
            get
            {
                if (_moveNextCommand == null)
                {
                    _moveNextCommand = new RelayCommand(param => MoveNextCommandExecuted(),
                        param => MoveNextCommandCanExecute());
                }
                return _moveNextCommand;
            }

        }

        #endregion

        #region - Routed Command Callbacks -
        private void MoveFirstCommandExecuted()
        {
            if (LstItems != null && LstItems != null && LstItems.Items.Count > 0)
                LstItems.Items.MoveCurrentToFirst();
            //ScrollIntoView(GridSource,LstItems);
        }
        private bool MoveFirstCommandCanExecute()
        {
            if (LstItems != null && LstItems != null && LstItems.Items.Count > 0)
                return LstItems.Items.CurrentPosition > 0;
            
            return false;
        }

        private void MovePreviousCommandExecuted()
        {
            if (LstItems != null && LstItems != null && LstItems.Items.Count > 0)
                LstItems.Items.MoveCurrentToPrevious();
            //ScrollIntoView(GridSource, LstItems); ;
        }
        private bool MovePreviousCommandCanExecute()
        {
            if (LstItems != null && LstItems != null && LstItems.Items.Count > 0)
                return LstItems.Items.CurrentPosition > 0;
            
            return false;
        }
        
        private void MoveLastCommandExecuted()
        {
            if (LstItems != null && LstItems != null && LstItems.Items.Count > 0)
                LstItems.Items.MoveCurrentToLast();
            ScrollToEnd();
        }
        private bool MoveLastCommandCanExecute()
        {
            if (LstItems != null && LstItems != null && LstItems.Items.Count > 0)
                return LstItems.Items.CurrentPosition < LstItems.Items.Count - 1;
            
            return false;
        }
        
        private void MoveNextCommandExecuted()
        {
            if (LstItems != null && LstItems != null && LstItems.Items.Count > 0)
                LstItems.Items.MoveCurrentToNext();
            //ScrollIntoView(GridSource, LstItems); ;
        }
        private bool MoveNextCommandCanExecute()
        {
            if (LstItems != null && LstItems != null && LstItems.Items.Count > 0)
                return LstItems.Items.CurrentPosition < LstItems.Items.Count - 1;

            return false;
        }

        #endregion

        private static void ScrollIntoView( dynamic source, ListBox lstItems )
        {
            ScrollIntoView( source, lstItems );
        }
        private static void ScrollIntoView( ListBox source, ListBox lstItems )
        {
            var newItemCount = lstItems.Items.CurrentItem;

            if ( newItemCount != null )
                source.ScrollIntoView( newItemCount );
        }
        private static void ScrollIntoView(DataGrid gridSource, ListBox lstItems)
        {
            if (gridSource == null) return;
            gridSource.Focus();
            //datagrig issue with scrolling
            try
            {

                if ((lstItems.Items.Count < 2) || (lstItems.Items.CurrentItem == null)
                || (lstItems.Items.CurrentPosition < 0)) return;
               
                gridSource.UpdateLayout();

                if ( gridSource.Items.Count < lstItems.Items.Count )
                    return;
                gridSource.CurrentItem = lstItems.Items.CurrentItem;
                gridSource.ScrollIntoView(gridSource.CurrentItem);
            }
            catch{}
        }

        #region - Private Fields -

        private ListBox _lstItems;
        public ListBox LstItems
        {
            get { return _lstItems; }
            private set
            {
                _lstItems = value;
                OnPropertyChanged("LstItems");
                _lstItems.SelectionChanged += (s, o) =>
                    {
                        CurrentPos = _lstItems.Items.CurrentPosition + 1;
                        ScrollIntoView(s, LstItems);
                    };
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members

        void ScrollToEnd()
        {
            if (GridSource == null) return;
            GridSource.Focus();
            if (GridSource.Items.Count > 0)
            {
                var border = VisualTreeHelper.GetChild(GridSource, 0) as Decorator;
                if (border != null)
                {
                    var scroll = border.Child as ScrollViewer;
                    if (scroll != null) scroll.ScrollToEnd();
                }
            }            
        }
    }
}
