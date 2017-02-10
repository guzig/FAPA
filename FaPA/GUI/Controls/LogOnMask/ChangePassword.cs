using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using FaPA.Infrastructure.Helpers;

namespace FaPA.GUI.Controls.LogOnMask
{

	[TemplatePart( Name = "PART_TextBlockHint", Type = typeof( TextBlock ) )]
	[TemplatePart( Name = "PART_RevealButton", Type = typeof( Button ) )]
	[TemplatePart( Name = "PART_SubmitButton", Type = typeof( Button ) )]
	public partial class ChangePassword
	{
		#region Fields

		private TextBlock _textBlockHint;
	    private TextBlock _confirmedPwdTextBlockHint;
        private Brush _savedBackgroundBrush;
		private DependencyObject _visualRoot;

		#endregion

		#region Public Events

		/// <summary>
		/// Occurs when the sumbit button is clicked or the enter key is pressed while the control has the focus.
		/// </summary>
		public event RoutedEventHandler SubmitRequested;

		#endregion

		#region Dependency Properties

		#region DependencyProperty - Watermark (type of "String")

		/// <summary>
		/// Gets or sets the Watermark in the PasswordBox which is shown when the PasswordBox is empty.
		/// This is a dependency property. The default value is "Digita la password".
		/// </summary>
		public string Watermark
		{
			get { return (string)GetValue( WatermarkProperty ); }
			set { SetValue( WatermarkProperty, value ); }
		}
		public static readonly DependencyProperty WatermarkProperty =
						DependencyProperty.Register( "Watermark", typeof( string ), typeof( ChangePassword ),
							new PropertyMetadata( "Digita la nuova password",
								(dpo, ea) =>
								{
									// ReSharper disable ConvertToLambdaExpression
									((ChangePassword)dpo).OnWatermarkChanged( (string)ea.OldValue, (string)ea.NewValue );
									// ReSharper restore ConvertToLambdaExpression
								} ) );

        //public string CurrentPasswordWatermark
        //{
        //    get { return ( string ) GetValue( CurrentPasswordWatermarkProperty ); }
        //    set { SetValue( CurrentPasswordWatermarkProperty, value ); }
        //}
        //public static readonly DependencyProperty CurrentPasswordWatermarkProperty =
        //                DependencyProperty.Register( "CurrentPasswordWatermark", typeof( string ), typeof( ChangePassword ),
        //                    new PropertyMetadata( "Digita la password corrente",
        //                        ( d, ea ) =>
        //                        {
        //                            // ReSharper disable ConvertToLambdaExpression
        //                            ( ( ChangePassword ) d ).OnCurrentPwdWatermarkChanged( ( string ) ea.OldValue, ( string ) ea.NewValue );
        //                            // ReSharper restore ConvertToLambdaExpression
        //                        } ) );

        public string ConfirmedPasswordWatermark
        {
            get { return ( string ) GetValue( ConfirmedPasswordWatermarkProperty ); }
            set { SetValue( ConfirmedPasswordWatermarkProperty, value ); }
        }
        public static readonly DependencyProperty ConfirmedPasswordWatermarkProperty =
                        DependencyProperty.Register( "ConfirmedPasswordWatermark", typeof( string ), typeof( ChangePassword ),
                            new PropertyMetadata( "Conferma la nuova password",
                                ( d, ea ) =>
                                {
                                    // ReSharper disable ConvertToLambdaExpression
                                    ( ( ChangePassword ) d ).OnConfirmedPwdWatermarkChanged( ( string ) ea.OldValue, ( string ) ea.NewValue );
                                    // ReSharper restore ConvertToLambdaExpression
                                } ) );

        private void OnWatermarkChanged(string oldValue, string newValue)
		{
			if (oldValue != newValue && this._textBlockHint != null)
			{
				this._textBlockHint.Text = newValue;
			}
		}

        //private void OnCurrentPwdWatermarkChanged( string oldValue, string newValue )
        //{
        //    if ( oldValue != newValue && this._currentPwdTextBlockHint != null )
        //    {
        //        this._currentPwdTextBlockHint.Text = newValue;
        //    }
        //}

        private void OnConfirmedPwdWatermarkChanged( string oldValue, string newValue )
        {
            if ( oldValue != newValue && this._confirmedPwdTextBlockHint != null )
            {
                this._confirmedPwdTextBlockHint.Text = newValue;
            }
        }

        #endregion

        #region DependencyProperty - CurrentPassword (type of "String")

        /// <summary>
        /// Gets or sets the Current Password which is inputted by the user. This is a dependency property (BindsTwoWayByDefault).
        /// (Because it is a dependency property it is bindable contrary to the not bindable Password property of the standard PasswordBox)
        /// </summary>
        //public string CurrentPassword
        //{
        //    get { return ( string ) GetValue( CurrentPasswordProperty ); }
        //    set { SetValue( CurrentPasswordProperty, value ); }
        //}
        //public static readonly DependencyProperty CurrentPasswordProperty =
        //                DependencyProperty.Register( "CurrentPassword", typeof( string ), typeof( ChangePassword ),
        //                                                        new FrameworkPropertyMetadata( default( string ) )
        //                                                        {
        //                                                            BindsTwoWayByDefault = true
        //                                                        } );
        #endregion

        #region DependencyProperty - Password (type of "String")

        /// <summary>
        /// Gets or sets the Password which is inputted by the user. This is a dependency property (BindsTwoWayByDefault).
        /// (Because it is a dependency property it is bindable contrary to the not bindable Password property of the standard PasswordBox)
        /// </summary>
        public string Password
		{
			get { return (string)GetValue( PasswordProperty ); }
			set { SetValue( PasswordProperty, value ); }
		}
		public static readonly DependencyProperty PasswordProperty =
						DependencyProperty.Register( "Password", typeof( string ), typeof( ChangePassword ),
																new FrameworkPropertyMetadata( default( string ) )
																	{
																		BindsTwoWayByDefault = true
																	} );
        #endregion

        #region DependencyProperty - Confirmed Password (type of "String")

        /// <summary>
        /// Gets or sets the Password which is inputted by the user. This is a dependency property (BindsTwoWayByDefault).
        /// (Because it is a dependency property it is bindable contrary to the not bindable Password property of the standard PasswordBox)
        /// </summary>
        public string ConfirmedPassword
        {
            get { return ( string ) GetValue( ConfirmedPasswordProperty ); }
            set { SetValue( ConfirmedPasswordProperty, value ); }
        }
        public static readonly DependencyProperty ConfirmedPasswordProperty =
                        DependencyProperty.Register( "ConfirmedPassword", typeof( string ), typeof( ChangePassword ),
                                                                new FrameworkPropertyMetadata( default( string ) )
                                                                {
                                                                    BindsTwoWayByDefault = true
                                                                } );
        #endregion

        #region DependencyProperty - AccessPassword (type of "String")

        /// <summary>
        /// Gets or sets the password which will be accepted by the intern validation process.
        /// This is a dependency property (BindsTwoWayByDefault).
        /// </summary>
        public string AccessPassword
		{
			get { return (string)GetValue( AccessPasswordProperty ); }
			set { SetValue( AccessPasswordProperty, value ); }
		}
		public static readonly DependencyProperty AccessPasswordProperty =
						DependencyProperty.Register( "AccessPassword", typeof( string ), typeof( ChangePassword ),
																new FrameworkPropertyMetadata( default( string ) )
																{
																	BindsTwoWayByDefault = true
																} );
		#endregion

		#region DependencyProperty - UserName (type of "String")

		/// <summary>
		/// Gets or sets the name of the user which will be displayed as the recent user.
		/// This is a dependency property (BindsTwoWayByDefault and DefaultUpdateSourceTrigger=PropertyChanged).
		/// </summary>
		public string UserName
		{
			get { return (string)GetValue( UserNameProperty ); }
			set { SetValue( UserNameProperty, value ); }
		}
		public static readonly DependencyProperty UserNameProperty =
					DependencyProperty.Register( "UserName", typeof( string ), typeof( ChangePassword ),
															new FrameworkPropertyMetadata( default( string ) )
															{
																BindsTwoWayByDefault = true,
																DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
															} );
		#endregion

		#region DependencyProperty - UserImageSource (type of "String")

		/// <summary>
		/// Gets or sets the URI to the image which will be displayed for the recent user.
		/// This is a dependency property. 
		/// </summary>
		public string UserImageSource
		{
			get { return (string)GetValue( UserImageSourceProperty ); }
			set { SetValue( UserImageSourceProperty, value ); }
		}
		public static readonly DependencyProperty UserImageSourceProperty =
						DependencyProperty.Register( "UserImageSource", typeof( string ), typeof( ChangePassword ),
							new PropertyMetadata( default( string ),
								(dpo, ea) =>
								{
									// ReSharper disable ConvertToLambdaExpression
									(( ChangePassword ) dpo).OnUserImageSourceChanged( (string)ea.OldValue, (string)ea.NewValue );
									// ReSharper restore ConvertToLambdaExpression
								} ) );

		protected virtual void OnUserImageSourceChanged(string oldValue, string newValue)
		{
			if (oldValue == null && newValue == null || oldValue != null && oldValue.Equals( newValue )) return;

			try
			{
				if (newValue != String.Empty)
				{
					Uri uri = new Uri( newValue, UriKind.RelativeOrAbsolute );

					if (!uri.IsAbsoluteUri || (uri.IsAbsoluteUri && uri.IsFile))
					{
						this.imgUser.Source = new BitmapImage( uri );
					}
				}
				else
				{
					this.imgUser.Source = new BitmapImage( new Uri( "/Design/Styles/Images/lightbulb-icon-39x39.png", UriKind.Relative ) );
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine( ex.Message );
			}
		}

		#endregion

		#region DependencyProperty - IsUserOptionAvailable (type of "String")

		/// <summary>
		/// Gets or sets the option for entering the user name. This is a dependency property.
		/// </summary>
		public bool IsUserOptionAvailable
		{
			get { return (bool)GetValue( IsUserOptionAvailableProperty ); }
			set { SetValue( IsUserOptionAvailableProperty, value ); }
		}
		public static readonly DependencyProperty IsUserOptionAvailableProperty =
						DependencyProperty.Register( "IsUserOptionAvailable", typeof( bool ),
																typeof( ChangePassword ),
							new PropertyMetadata( false,
								(dpo, ea) =>
								{
									// ReSharper disable ConvertToLambdaExpression
									(( ChangePassword ) dpo).OnIsUserOptionAvailableChanged( (bool)ea.OldValue, (bool)ea.NewValue );
									// ReSharper restore ConvertToLambdaExpression
								} ) );

		protected virtual void OnIsUserOptionAvailableChanged(bool oldValue, bool newValue)
		{
			if (oldValue.Equals( newValue )) return;

			if (newValue == true)
			{
				this.FaultMessagePanel.Margin = new Thickness( 12, 40, 12, 5 );
			}
			else
			{
				this.FaultMessagePanel.Margin = new Thickness( 12, 18, 12, 5 );
			}
		}

		#endregion

		#region DependencyProperty - AdditionalUserInfo (type of "String")

		/// <summary>
		/// Gets or sets the additional user information which will be displayed under the UserName with a smaller font size.
		/// This is a dependency property.
		/// </summary>
		public string AdditionalUserInfo
		{
			get { return (string)GetValue( AdditionalUserInfoProperty ); }
			set { SetValue( AdditionalUserInfoProperty, value ); }
		}
		public static readonly DependencyProperty AdditionalUserInfoProperty =
						DependencyProperty.Register( "AdditionalUserInfo", typeof( string ), typeof( ChangePassword ),
																new PropertyMetadata( default( string ) ) );
		#endregion

		#region DependencyProperty - AdditionalSystemInfo (type of "String")

		/// <summary>
		/// Gets or sets the additional system information which will be displayed under the AdditionalUserInfo
		/// with a smaller font size and a opacity of 0.6. This is a dependency property.
		/// </summary>
		public string AdditionalSystemInfo
		{
			get { return (string)GetValue( AdditionalSystemInfoProperty ); }
			set { SetValue( AdditionalSystemInfoProperty, value ); }
		}
		public static readonly DependencyProperty AdditionalSystemInfoProperty =
						DependencyProperty.Register( "AdditionalSystemInfo", typeof( string ), typeof( ChangePassword ),
																new PropertyMetadata( default( string ) ) );
		#endregion

		#region DependencyProperty - SubmitButtonTooltip (type of "String")

		/// <summary>
		/// Gets or sets the tooltip of the submit button. This is a dependency property. The default value is "Submit".
		/// </summary>
		public string SubmitButtonTooltip
		{
			get { return (string)GetValue( SubmitButtonTooltipProperty ); }
			set { SetValue( SubmitButtonTooltipProperty, value ); }
		}
		public static readonly DependencyProperty SubmitButtonTooltipProperty =
						DependencyProperty.Register( "SubmitButtonTooltip", typeof( string ), typeof( ChangePassword ),
																new PropertyMetadata( "Conferma credenziali" ) );
		#endregion

		#region DependencyProperty - DisappearAnimation (type of "DisappearAnimationType")

		/// <summary>
		/// Gets or sets the type of the disappear animation. This is a dependency property.
		/// The default value is DisappearAnimationType.MoveAndFadeOutToRight.
		/// </summary>
		public DisappearAnimationType DisappearAnimation
		{
			get { return (DisappearAnimationType)GetValue( DisappearAnimationProperty ); }
			set { SetValue( DisappearAnimationProperty, value ); }
		}
		public static readonly DependencyProperty DisappearAnimationProperty =
						DependencyProperty.Register( "DisappearAnimation", typeof( DisappearAnimationType ), typeof( ChangePassword ),
																new PropertyMetadata( DisappearAnimationType.MoveAndFadeOutToRight ) );
		#endregion

		#region DependencyProperty - CapsLockInfo (type of "String")

		/// <summary>
		/// Gets or sets the hint which will be displayed when CapsLock is active.
		/// This is a dependency property. The default value is "Caps Lock is active".
		/// </summary>
		public string CapsLockInfo
		{
			get { return (string)GetValue( CapsLockInfoProperty ); }
			set { SetValue( CapsLockInfoProperty, value ); }
		}
		public static readonly DependencyProperty CapsLockInfoProperty =
						DependencyProperty.Register( "CapsLockInfo", typeof( string ), typeof( ChangePassword ),
																new PropertyMetadata( "Maiuscole attivate" ) );

		#endregion


		#endregion

		#region Constructor

		public ChangePassword()
		{
			InitializeComponent();
		}

		#endregion

		#region Events related to the Control

		private void ChangePassword_Loaded( object sender, RoutedEventArgs e)
		{
			this.InitializeBaseClass( this );

			this._visualRoot = SmartVisualTreeHelper.FindAncestor<Window>( this );

			// The initialization of the "Dependency Properties" in the "Loaded Event" is very important for
			// the properly visualisation of the control in "Design Mode" (when it is loaded)
			this.SetFullSpan( this.FullSpan );

			this.Lock();
        }

        #endregion

        #region Overriding FrameworkElement Methods

        public override void OnApplyTemplate()
		{
			// ReSharper disable JoinDeclarationAndInitializer
			object dpo;
			// ReSharper restore JoinDeclarationAndInitializer

			// This invoke is very important, because i have realized that it is not guaranteed that the PasswordBox
			// Styles and Templates are fully applied at this moment (maybe because they are within a Style ?!)
			PasswordBoxControl.ApplyTemplate();

			// Find the TextBlock in the template, so we are able to apply the watermark text
			dpo = PasswordBoxControl.Template.FindName( "PART_TextBlockHint", PasswordBoxControl );
			if (dpo is TextBlock)
			{
				this._textBlockHint = dpo as TextBlock;
				this._textBlockHint.Text = this.Watermark;
			}

            ConfirmedPasswordBoxControl.ApplyTemplate();
            dpo = ConfirmedPasswordBoxControl.Template.FindName( "PART_TextBlockHint", ConfirmedPasswordBoxControl );
            if ( dpo is TextBlock )
            {
                this._confirmedPwdTextBlockHint = dpo as TextBlock;
                this._confirmedPwdTextBlockHint.Text = this.ConfirmedPasswordWatermark;
            }

            dpo = PasswordBoxControl.Template.FindName( "PART_RevealButton", PasswordBoxControl );
            if ( dpo is Button )
            {
                ( dpo as Button ).LostMouseCapture += ( sender, e ) => RevealButton_LostMouseCapture( sender, e, PasswordBoxControl );
            }

            // Connect the Click event, so we can handle a mouseclick on the submit button
            dpo = PasswordBoxControl.Template.FindName( "PART_SubmitButton", PasswordBoxControl );
            if ( dpo is Button )
            {
                Button submitButton = dpo as Button;
                submitButton.ToolTip = this.SubmitButtonTooltip;
                submitButton.Click += SubmitButton_Click;
            }

            dpo = ConfirmedPasswordBoxControl.Template.FindName( "PART_RevealButton", ConfirmedPasswordBoxControl );
            if ( dpo is Button )
            {
                ( dpo as Button ).LostMouseCapture += ( sender, e ) => RevealButton_LostMouseCapture( sender, e, ConfirmedPasswordBoxControl );
            }

            // Connect the Click event, so we can handle a mouseclick on the submit button
            dpo = ConfirmedPasswordBoxControl.Template.FindName( "PART_SubmitButton", ConfirmedPasswordBoxControl );
			if (dpo is Button)
			{
				Button submitButton = dpo as Button;
				submitButton.ToolTip = this.SubmitButtonTooltip;
				submitButton.Click += SubmitButton_Click;
			}

			base.OnApplyTemplate();
		}

        #endregion

        #region Event Handler

        #region current current password
        ///// <summary>
        ///// If the user press the Return (resp. Enter) Key perform the "submit process"
        ///// </summary>
        //private void CurrentPasswordBoxControl_OnKeyDown( object sender, KeyEventArgs e )
        //{
        //    if ( e.Key == Key.Return || e.Key == Key.Enter )
        //    {
        //        //this.SubmitButton_Click( sender, new RoutedEventArgs() );
        //        PasswordBoxControl.Focus();
        //        return;
        //    }

        //    this.lblCapsLockInfo.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Hidden;
        //}

        ///// <summary>
        ///// Check whether the CapsLock is active or not and display or hide the CapsLockInfo
        ///// </summary>
        //private void CurrentPasswordBoxControl_OnGotFocus( object sender, RoutedEventArgs e )
        //{
        //    this.lblCapsLockInfo.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Hidden;
        //}

        ///// <summary>
        ///// The CapsLockInfo is only visible when the PasswordBox Control got the focus
        ///// </summary>
        //private void CurrentPasswordBoxControl_OnLostFocus( object sender, RoutedEventArgs e )
        //{
        //    this.lblCapsLockInfo.Visibility = Visibility.Hidden;
        //}
        #endregion

        #region Password Box event handler

        /// <summary>
        /// If the user press the Return (resp. Enter) Key perform the "submit process"
        /// </summary>
        private void PasswordBoxControl_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return || e.Key == Key.Enter)
			{
				//this.SubmitButton_Click( sender, new RoutedEventArgs() );
			    ConfirmedPasswordBoxControl.Focus();
				return;
			}

			this.lblCapsLockInfo.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Hidden;
		}

		/// <summary>
		/// Check whether the CapsLock is active or not and display or hide the CapsLockInfo
		/// </summary>
		private void PasswordBoxControl_OnGotFocus(object sender, RoutedEventArgs e)
		{
			this.lblCapsLockInfo.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Hidden;
		}

		/// <summary>
		/// The CapsLockInfo is only visible when the PasswordBox Control got the focus
		/// </summary>
		private void PasswordBoxControl_OnLostFocus(object sender, RoutedEventArgs e)
		{
			this.lblCapsLockInfo.Visibility = Visibility.Hidden;
		}

        #endregion

        #region ConfirmedPassword Box event handler

        /// <summary>
        /// If the user press the Return (resp. Enter) Key perform the "submit process"
        /// </summary>
        private void ConfirmedPasswordBoxControl_OnKeyDown( object sender, KeyEventArgs e )
        {
            if ( e.Key == Key.Return || e.Key == Key.Enter )
            {
                this.SubmitButton_Click( sender, new RoutedEventArgs() );
                return;
            }

            this.lblCapsLockInfo.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Check whether the CapsLock is active or not and display or hide the CapsLockInfo
        /// </summary>
        private void ConfirmedPasswordBoxControl_OnGotFocus( object sender, RoutedEventArgs e )
        {
            this.lblCapsLockInfo.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// The CapsLockInfo is only visible when the ConfirmedPasswordBox Control got the focus
        /// </summary>
        private void ConfirmedPasswordBoxControl_OnLostFocus( object sender, RoutedEventArgs e )
        {
            this.lblCapsLockInfo.Visibility = Visibility.Hidden;
        }

        #endregion



        #region Event handler of all buttons

        /// <summary>
        /// Set the focus to the PasswordBox Control when the RevealButton lost mouse capture.
        /// </summary>
        void RevealButton_LostMouseCapture(object sender, MouseEventArgs e, PasswordBox passwordBoxControl)
		{
			passwordBoxControl.Focus();
		}

		/// <summary>
		/// Perform the "submit process".
		/// </summary>
		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
            if (this.SubmitRequested != null)
			{
				this.SubmitRequested( this, e );
				return;
			}

			if (this.Command != null && this.Command.CanExecute( this.CommandParameter ))
			{
			    btnOK.Focus();
				this.Command.Execute( this.CommandParameter );
			}

		}

		/// <summary>
		/// Hide the Info Overlay an set the focus to the PasswordBox Control. 
		/// </summary>
		private void btnOK_OnClick(object sender, RoutedEventArgs e)
		{
			this.PasswordBoxControl.Visibility = Visibility.Visible;
			this.FaultMessagePanel.Visibility = Visibility.Hidden;

			this.PasswordBoxControl.Focus();
		}

		#endregion

		#endregion

		#region Private helper methods

		/// <summary>
		/// Assimilate the background from the visual root (window) and save the background brush
		/// </summary>
		protected void assimilateBackground()
		{
			if (this._savedBackgroundBrush != null)
			{
				this.Background = this._savedBackgroundBrush.Clone();
				return;
			}

			if (this._savedBackgroundBrush == null && this.Background != null)
			{
				this._savedBackgroundBrush = this.Background.Clone();
				return;
			}

			if (this._visualRoot is Window)
			{
				Window window = this._visualRoot as Window;

				this._savedBackgroundBrush = window.Background.Clone();
				this.Background = this._savedBackgroundBrush;
			}
		}

		/// <summary>
		/// Disable resp. enable all childs of the parent control (if it is a Panel)
		/// </summary>
		/// <param name="isEnabled"></param>
		private void setAvailabilityOfParentChilds(bool isEnabled)
		{
			var panel = this.ParentElement as Panel;
			if (panel != null)
			{
				foreach (var child in panel.Children)
				{
					if (!child.Equals( this ))
					{
						// ReSharper disable PossibleNullReferenceException
						(child as UIElement).IsEnabled = isEnabled;
						// ReSharper restore PossibleNullReferenceException
					}
				}
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Perform the defined (or the default) DisappearAnimation and a FadeOut animation to make the overlay disappear.
		/// </summary>
		public void Unlock()
		{
			Storyboard storyboard;
			bool simultaneous = false;

			switch (DisappearAnimation)
			{
				case DisappearAnimationType.FadeOut:
					storyboard = null;
					break;
				case DisappearAnimationType.MoveAndFadeOutToRight:
					storyboard = this.TryFindResource( "MoveOutToRightStoryboard" ) as Storyboard;
					break;
				case DisappearAnimationType.MoveAndFadeOutToTop:
					storyboard = this.TryFindResource( "MoveOutToTopStoryboard" ) as Storyboard;
					break;
				case DisappearAnimationType.MoveAndFadeOutToRightSimultaneous:
					storyboard = this.TryFindResource( "MoveOutToRightSimultaneousStoryboard" ) as Storyboard;
					simultaneous = true;
					break;
				case DisappearAnimationType.MoveAndFadeOutToTopSimultaneous:
					storyboard = this.TryFindResource( "MoveOutToTopSimultaneousStoryboard" ) as Storyboard;
					simultaneous = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			if (storyboard != null) storyboard.Begin();

			this.setAvailabilityOfParentChilds( true );

			if (!simultaneous)
			{
				storyboard = this.TryFindResource( "FadeOutStoryboard" ) as Storyboard;
				if (storyboard != null)
				{
					storyboard.Completed += (os, ea) =>
					{
						this.Visibility = Visibility.Hidden;
						var panel = this.ParentElement as Panel;
						if (panel != null)
						{
							panel.Opacity = 0.0;
							DoubleAnimation doubleAnimation =
													new DoubleAnimation()
													{
														From = 0.0,
														To = 1.0,
														Duration = new Duration( ((KeyTime)this.TryFindResource( "FadeInDurationKeyTime" )).TimeSpan )
													};
							panel.BeginAnimation( OpacityProperty, doubleAnimation );
						}
					};
				}
			}
			else
			{
				storyboard = this.TryFindResource( "FadeOutSimultaneousStoryboard" ) as Storyboard;
			}

			if (storyboard != null)
			{
				storyboard.Begin();
			}
		}

		/// <summary>
		/// Reset all animations, assimilate the background if neccessary, disable all childs of the parent Control
		/// and set the focus to the PasswordBox Control.
		/// </summary>
		public void Lock()
		{
			this.BeginAnimation( OpacityProperty, null );
			this.BeginAnimation( BackgroundProperty, null );
			this.BeginAnimation( VisibilityProperty, null );
			this.VisualRootTranslateTransform.BeginAnimation( TranslateTransform.XProperty, null );
			this.VisualRootTranslateTransform.BeginAnimation( TranslateTransform.YProperty, null );

			this.LayoutRoot.BeginAnimation( OpacityProperty, null );
			this.LayoutRoot.BeginAnimation( VisibilityProperty, null );
			this.LayoutRootTranslateTransform.BeginAnimation( TranslateTransform.XProperty, null );
			this.LayoutRootTranslateTransform.BeginAnimation( TranslateTransform.YProperty, null );

			this.assimilateBackground();

			this.setAvailabilityOfParentChilds( false );

			this.Visibility = Visibility.Visible;
			this.Password = String.Empty;
			if (this.IsUserOptionAvailable)
			{
				this.tbUserName.Focus();
			}
			else
			{
				this.PasswordBoxControl.Focus();
			}
		}



        #endregion

    }
}
