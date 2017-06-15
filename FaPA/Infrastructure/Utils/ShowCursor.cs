using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace FaPA.Infrastructure.Utils
{
    public static class ShowCursor
    {
        /// <summary>
        /// Sets the busystate as busy.
        /// </summary>
        public static void Show()
        {
            Show(true);
        }

        /// <summary>
        /// Sets the busystate to busy or not busy.
        /// </summary>
        /// <param name="busy">if set to <c>true</c> the application is now busy.</param>
        private static void Show(bool busy)
        {
            if (busy == IsBusy) return;

            IsBusy = busy;
            Mouse.OverrideCursor = busy ? Cursors.Wait : null;

            if (IsBusy)
            {
                new DispatcherTimer(TimeSpan.FromSeconds(0), DispatcherPriority.ApplicationIdle,
                    DispatcherTimerTick, Application.Current.Dispatcher);
            }
        }

        public static bool IsBusy { get; set; }

        /// <summary>
        /// Handles the Tick event of the dispatcherTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void DispatcherTimerTick(object sender, EventArgs e)
        {
            var dispatcherTimer = sender as DispatcherTimer;
            if (dispatcherTimer == null) return;
            Show(false);
            dispatcherTimer.Stop();
        }
    }
}