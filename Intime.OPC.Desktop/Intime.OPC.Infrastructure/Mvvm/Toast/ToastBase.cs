using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Intime.OPC.Infrastructure.Mvvm.Toast
{
    /// <summary>
    /// Wraps some basic functionality common to an animated "toast" mechanism.
    /// It implements <see cref="IAnimatedToast"/> with some simple behavior 
    /// that can be overriden by inheriting classes.
    /// The animation has the sequence shown below. The steps were not coded as a fully
    /// automatic process to allow the caller to deal with special cases when the toast
    /// needs to be cancelled, recreated, etc.
    /// Steps:
    /// 1) An object (a toast inheriting this class) needs to be instantiated
    /// 2) object.GetInitialPlacement() is called to position the toast at the initial location
    /// 3) object.Show() is called to show the toast
    /// 4) object.AnimatedShow() is called to start the animation as described below:
    /// The "appearing" animation (which can be overriden) starts showing the toast. When it finishes 
    /// the "stay up" timer kicks in to keep the toast up for some time. 
    /// At the end of the "stay up" timer, the "disappearing" animation (which can be overriden also)
    /// begins. Then at the end of it all, the <c>ToastIsDone</c> event is fired.
    /// 
    /// This implementation has a default "slide-in/slide-out" animation.
    /// </summary>
    public class ToastBase : Window, IAnimatedToast
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ToastBase()
        {
            // By definition, an animated toast may have the position be 
            // manipulated manually
            WindowStartupLocation = WindowStartupLocation.Manual;

            // Set the location of this window outside of the viewable area
            Left = -1000;
            Top = -1000;

            // The default behavior is not to take away the focus from
            // any other window the user may be looking at.
            doNotCauseWindowActivation = true;
        }

        public ToastBase(bool doNotCauseWindowActivation, int timeToStayUpMilisec)
            : this()
        {
            this.doNotCauseWindowActivation = doNotCauseWindowActivation;
            this.timeToStayUpMilisec = timeToStayUpMilisec;

            // stay-up timer default
            SetupStayUpTimer();
        }

        #endregion

        #region IAnimatedToast Members

        /// <summary>
        /// This event is to be fired when the toast is about to disappear, therefore
        /// the animations (both appearing and disappearing) must be finished.
        /// This event gives a chance to the caller to clean-up after the toast has done
        /// its job.
        /// </summary>
        public event AnimatedToastEventHandler<IAnimatedToast, EventArgs> ToastIsDone;

        /// <summary>
        /// Gets a value indicating if the toast will cause an activation
        /// event to be fired. If it does, it takes the focus away from
        /// any other window (that the user may be using) that has the focus at 
        /// the time the toast is shown.
        /// </summary>
        public bool DoNotCauseWindowActivation
        {
            get { return doNotCauseWindowActivation; }
            set { doNotCauseWindowActivation = value; }
        }

        /// <summary>
        /// The time the toast will stay up for after it has been shown.
        /// </summary>
        public int TimeToStayUpMilisec
        {
            get { return timeToStayUpMilisec; }
            set
            {
                timeToStayUpMilisec = value;
                SetupStayUpTimer();
            }
        }

        /// <summary>
        /// The toast has to be placed at certain initial location. Calculate what
        /// that location is and return 2 points: the upper left corner's and
        /// the lower right corner's. From that location, the animations will start.
        /// </summary>
        /// <param name="width">The width of the toast.</param>
        /// <param name="height">The height of the toast.</param>
        /// <param name="margin">The margin of the toast. This mayhe useful to adjust the 
        /// location of the toast.</param>
        /// <param name="ptStart">The output starting location.</param>
        /// <param name="ptEnd">The output ending location.</param>
        public virtual void GetInitialPlacement(double width, double height, out Point ptStart, out Point ptEnd)
        {
            ptStart = new Point();
            ptEnd = new Point();

            // show the toast relative to the task bar location
            WindowsTaskBarInfo tbi = WindowsTaskBarInfo.GetWindowsTaskBarInfo();

            switch (tbi.Edge)
            {
                case Win32.TaskBarEdge.Left:
                    ptStart.Y = ptEnd.Y = tbi.WorkArea.Bottom - height;
                    ptEnd.X = tbi.WorkArea.Left;
                    ptStart.X = tbi.ScreenArea.Left - width;

                    // if window is not off the screen (due to multiple monitors), don't do animation
                    if (ptStart.X + width > WindowsTaskBarInfo.ConvertPixelsToDIPixels(SystemParameters.VirtualScreenLeft))
                    {
                        ptStart = ptEnd;
                    }
                    break;

                case Win32.TaskBarEdge.Right:
                    ptStart.Y = ptEnd.Y = tbi.WorkArea.Bottom - height;
                    ptEnd.X = tbi.WorkArea.Right - width;
                    ptStart.X = tbi.ScreenArea.Right;

                    // if window is not off the screen (due to multiple monitors), don't do animation
                    double virtualScreenRight = SystemParameters.VirtualScreenLeft + SystemParameters.VirtualScreenWidth;
                    if (ptStart.X < WindowsTaskBarInfo.ConvertPixelsToDIPixels(virtualScreenRight))
                    {
                        ptStart = ptEnd;
                    }
                    break;

                case Win32.TaskBarEdge.Top:
                    ptStart.X = ptEnd.X = tbi.WorkArea.Right - width;
                    ptEnd.Y = tbi.WorkArea.Top;
                    ptStart.Y = tbi.ScreenArea.Top - height;

                    // if window is not off the screen (due to multiple monitors), don't do animation
                    if (ptStart.Y + height > WindowsTaskBarInfo.ConvertPixelsToDIPixels(SystemParameters.VirtualScreenTop))
                    {
                        ptStart = ptEnd;
                    }
                    break;

                case Win32.TaskBarEdge.Bottom:
                default:
                    ptStart.X = ptEnd.X = tbi.WorkArea.Right - width;
                    ptEnd.Y = tbi.WorkArea.Bottom - height;
                    ptStart.Y = tbi.ScreenArea.Bottom;

                    // if window is not off the screen (due to multiple monitors), don't do animation
                    double virtualScreenBottom = SystemParameters.VirtualScreenTop + SystemParameters.VirtualScreenHeight;
                    if (ptStart.Y < WindowsTaskBarInfo.ConvertPixelsToDIPixels(virtualScreenBottom))
                    {
                        ptStart = ptEnd;
                    }
                    break;
            }
        }

        /// <summary>
        /// Shows the toast with animation effects in certain area defined by the 
        /// two given input locations.
        /// This assumes that the toast has already been "shown" with the native show
        /// Window function. From that point on, the animation will kick in.
        /// </summary>
        /// <param name="startPoint">Start point of the animation.</param>
        /// <param name="endPoint">End point of the animation.</param>
        public void AnimatedShow(Point startPoint, Point endPoint)
        {
            // Start the animation part wehn the toast shows up
            RunAppearanceAnimation(startPoint, endPoint);
        }

        /// <summary>
        /// Cancels a currently animated show operation.
        /// </summary>
        public void CancelAnimatedShow()
        {
            // Cancel the current storyboard
            if (CurrentStoryboard != null)
            {
                CurrentStoryboard.Stop(this);
            }

            // Stop the timer
            StopStayUpTimer();
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the current <see cref="Storyboard"/> that's driving the animation.
        /// </summary>
        protected Storyboard CurrentStoryboard
        {
            get { return currentStoryboard; }
            private set { currentStoryboard = value; }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Implements the animation for when the toast is appearing.
        /// </summary>
        /// <param name="startPoint">Start point of the animation.</param>
        /// <param name="endPoint">End point of the animation.</param>
        protected virtual void RunAppearanceAnimation(Point startPoint, Point endPoint)
        {
            // Store the animation points
            animationStartingPoint = startPoint;
            animationEndingPoint = endPoint;

            // create a storyboard to slide the toast into position
            currentStoryboard = ToastUtility.SlideIn(this, startPoint, endPoint,
                                                                  AppearingAnimation_Completed);
        }

        /// <summary>
        /// Implements the animation for when the toast is disappearing.
        /// </summary>
        /// <param name="startPoint">Start point of the animation.</param>
        /// <param name="endPoint">End point of the animation.</param>
        protected virtual void RunDisappearanceAnimation(Point startPoint, Point endPoint)
        {
            // create animation to slide out the toast
            currentStoryboard = ToastUtility.SlideOut(this, startPoint, endPoint, DisappearingAnimation_Completed);
        }

        /// <summary>
        /// Fires the <see cref="ToastIsDone"/> event.
        /// </summary>
        protected virtual void OnToastIsDone(EventArgs eventArg)
        {
            if (ToastIsDone != null)
            {
                ToastIsDone(this, eventArg);
            }
        }

        /// <summary>
        /// Does the right type of "native" window show depending on whether
        /// the toast will cause activation or not.
        /// </summary>
        protected void ShowInternal()
        {
            //fixed bug 10411
            Topmost = true;

            WindowInteropHelper wih = new WindowInteropHelper(this);
            if (DoNotCauseWindowActivation && wih.Handle != IntPtr.Zero)
            {
                // show window without activating it
                Win32.ShowWindow(wih.Handle, (int)Win32.SHOWWINDOW.SW_SHOWNOACTIVATE);
                Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Visibility = System.Windows.Visibility.Visible;
                Show();
            }
            // Slide under the task bar
            Topmost = false;
        }

        public virtual void ShowToast()
        {
            Point startPoint = new Point(0, 0);
            Point endPoint = new Point(0, 0);

            GetInitialPlacement(Width, Height, out startPoint, out endPoint);

            try
            {
                // show offscreen
                Left = startPoint.X;
                Top = startPoint.Y;
                Opacity = 1;

                // Shows the toast using the correct native Window show call
                ShowInternal();

                // Starts the animation now that the toast is shown and positioned properly
                AnimatedShow(startPoint, endPoint);
            }
            catch // eat all exceptions when showing the toast, since the toast is mainly informational to the user
            {
            }
        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Event handler for the "appearing animation completed" event.
        /// Note that this method is protected so that derived classes overriding the
        /// animations can reuse this event handlers.
        /// </summary>
        protected void AppearingAnimation_Completed(object sender, EventArgs e)
        {
            // Move the toast to the ending location
            Left = animationEndingPoint.X;
            Top = animationEndingPoint.Y;

            // Make sure we're topmost to be shown here
            Topmost = true;

            // Start the stay-up timer now that we're up
            StartStayUpTimer();
        }

        /// <summary>
        /// Event handler for the "disappearing animation completed" event.
        /// Note that this method is protected so that derived classes overriding the
        /// animations can reuse this event handlers.
        /// </summary>
        protected void DisappearingAnimation_Completed(object sender, EventArgs e)
        {
            // Hide the window
            Hide();

            // Reset the storyboard
            CurrentStoryboard = null;

            // Fire the ToastDisappearing event
            OnToastIsDone(EventArgs.Empty);
        }

        /// <summary>
        /// The event handler for the <c>Tick</c> event of the timer we use
        /// to ensure the toast is shown for certain amount of time.
        /// </summary>
        private void StayUpTimer_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            StopStayUpTimer();

            // By default this class runs a fade-out animation when disappearing
            RunDisappearanceAnimation(animationStartingPoint, animationEndingPoint);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Sets up the <c>stayUpTimer</c> to be used as part of the
        /// toast showing process.
        /// </summary>
        private void SetupStayUpTimer()
        {
            // if no time is specified, the toast will stay up forever
            if (timeToStayUpMilisec > 0)
            {
                stayUpTimer = new DispatcherTimer(DispatcherPriority.Send);
                stayUpTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)timeToStayUpMilisec);
                stayUpTimer.IsEnabled = false;
                stayUpTimer.Tick += StayUpTimer_Tick;
            }
        }

        /// <summary>
        /// Starts the <c>stayUpTimer</c> to ensure the toast is 
        /// shown for the given amount of time.
        /// </summary>
        private void StartStayUpTimer()
        {
            if (stayUpTimer != null)
            {
                // Enable the stay-up timer now that we're up
                stayUpTimer.IsEnabled = true;
                stayUpTimer.Start();
            }
        }

        /// <summary>
        /// Stops the <c>stayUpTimer</c> as we are done showing the toast.
        /// </summary>
        private void StopStayUpTimer()
        {
            if (stayUpTimer != null)
            {
                stayUpTimer.Stop();
                stayUpTimer = null;
            }
        }

        /// <summary>
        /// Restarts the <c>stayUpTimer</c> with a new timeout value
        /// </summary>
        public void ResetStayUpTimer(int timeInMilisec)
        {
            if (stayUpTimer != null)
            {
                stayUpTimer.Stop();
                timeToStayUpMilisec = timeInMilisec;
                stayUpTimer.Interval = new TimeSpan(0, 0, 0, 0, timeToStayUpMilisec);

                StartStayUpTimer();
            }
        }

        #endregion

        #region Class Variables

        private int timeToStayUpMilisec = 5000;
        private bool doNotCauseWindowActivation;
        private Point animationStartingPoint, animationEndingPoint;
        private Storyboard currentStoryboard;

        private DispatcherTimer stayUpTimer;

        #endregion
    }
}
