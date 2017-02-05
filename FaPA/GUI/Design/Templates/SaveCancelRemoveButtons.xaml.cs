using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FaPA.GUI.Design.Templates
{
    /// <summary>
    /// Logica di interazione per SaveCancelRemoveButtons.xaml
    /// </summary>
    public partial class SaveCancelRemoveButtons : UserControl
    {
        public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register(
            "SaveCommand", typeof ( ICommand ), typeof ( SaveCancelRemoveButtons ), new PropertyMetadata( default ( ICommand ) ) );

        public ICommand SaveCommand
        {
            get { return ( ICommand ) GetValue( SaveCommandProperty ); }
            set { SetValue( SaveCommandProperty, value ); }
        }

        public static readonly DependencyProperty AddItemCommandProperty = DependencyProperty.Register(
            "AddItemCommand", typeof ( ICommand ), typeof ( SaveCancelRemoveButtons ), new PropertyMetadata( default ( ICommand ) ) );

        public ICommand AddItemCommand
        {
            get { return ( ICommand ) GetValue( AddItemCommandProperty ); }
            set { SetValue( AddItemCommandProperty, value ); }
        }

        public static readonly DependencyProperty PerformCancelCommandProperty = DependencyProperty.Register(
            "PerformCancelCommand", typeof ( ICommand ), typeof ( SaveCancelRemoveButtons ), new PropertyMetadata( default ( ICommand ) ) );

        public ICommand PerformCancelCommand
        {
            get { return ( ICommand ) GetValue( PerformCancelCommandProperty ); }
            set { SetValue( PerformCancelCommandProperty, value ); }
        }

        public static readonly DependencyProperty DeleteEntityCommandProperty = DependencyProperty.Register(
            "DeleteEntityCommand", typeof ( ICommand ), typeof ( SaveCancelRemoveButtons ), new PropertyMetadata( default ( ICommand ) ) );

        public ICommand DeleteEntityCommand
        {
            get { return ( ICommand ) GetValue( DeleteEntityCommandProperty ); }
            set { SetValue( DeleteEntityCommandProperty, value ); }
        }
        
        public static readonly DependencyProperty UserCollectionViewProperty = DependencyProperty.Register(
            "UserCollectionView", typeof ( ICollectionView ), typeof ( SaveCancelRemoveButtons ), new PropertyMetadata( default ( ICollectionView ) ) );

        public ICollectionView UserCollectionView
        {
            get { return ( ICollectionView ) GetValue( UserCollectionViewProperty ); }
            set { SetValue( UserCollectionViewProperty, value ); }
        }

        public SaveCancelRemoveButtons()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }
    }
}
