using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Interop;

namespace FaPA.Infrastructure.Utils
{
    public class WindowBase : Window
    {
        private const Int32 WM_SYSCOMMAND = 0x112;
        private const Int32 SC_MAXIMIZE = 0xF030;
        Int32 _currentSc;

        public WindowBase()
        {
            IObservable<SizeChangedEventArgs> ObservableSizeChanges = Observable.
                FromEventPattern<SizeChangedEventArgs>(this, "SizeChanged").
                Select(x => x.EventArgs).
                Throttle(TimeSpan.FromMilliseconds(50));

            IDisposable SizeChangedSubscription = ObservableSizeChanges.
                ObserveOn(SynchronizationContext.Current).
                Subscribe(x => { Size_Changed(x); });

            Closing += (o, args) => { SizeChangedSubscription.Dispose(); };
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private void Size_Changed(SizeChangedEventArgs sizeChangedEventArgs)
        {
            var windows = (Window)sizeChangedEventArgs.Source;

            var maxHeigth = SystemParameters.WorkArea.Height;

            string tag = (string)windows.Tag;
            if (tag != null && tag.Contains("noresize"))
            {
                //sizeChangedEventArgs.Handled = true;
                return;
            }

            if (windows.Height > maxHeigth)
            {
                if (_currentSc != SC_MAXIMIZE)
                    MaxHeight = maxHeigth;
            }

            CenterView();

            windows.Height = double.NaN;
            windows.Width = double.NaN;

            windows.SizeToContent = SizeToContent.WidthAndHeight;

        }

        protected void View_OnLoaded(object sender, RoutedEventArgs e)
        {
            CenterView();
        }

        private void CenterView()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = screenWidth / 2 - windowWidth / 2;
            this.Top = double.IsNaN(windowHeight) ? 0D : screenHeight / 2 - windowHeight / 2;
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SYSCOMMAND)
            {
                _currentSc = wParam.ToInt32();

                if (wParam.ToInt32() == SC_MAXIMIZE)
                {
                    MaxHeight = double.PositiveInfinity;

                    //handled = true;
                }
            }

            return IntPtr.Zero;
        }
    }
}
