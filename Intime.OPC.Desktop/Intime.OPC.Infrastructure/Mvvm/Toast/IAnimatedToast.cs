using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Intime.OPC.Infrastructure.Mvvm.Toast
{
    /// <summary>
    /// Defines a generic event (and its handler delegate) for toast events.
    /// </summary>
    /// <typeparam name="TEventSource">Type of the event source object.</typeparam>
    /// <typeparam name="TEventArgs">Type of the event arguments.</typeparam>
    /// <param name="eventSource">Object generating the event.</param>
    /// <param name="eventArgs">Arguments describing the event.</param>
    public delegate void AnimatedToastEventHandler<TEventSource, TEventArgs>(TEventSource eventSource, TEventArgs eventArgs)
        where TEventArgs : EventArgs;

    /// <summary>
    /// Defines some behavior for a "toast" UI that animates as it is
    /// being displayed and hidden.
    /// </summary>
    public interface IAnimatedToast
    {
        #region Events

        /// <summary>
        /// This event is to be fired when the toast is about to disappear, therefore
        /// the animations (both appearing and disappearing) must be finished.
        /// This event gives a chance to the caller to clean-up after the toast has done
        /// its job.
        /// </summary>
        event AnimatedToastEventHandler<IAnimatedToast, EventArgs> ToastIsDone;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating if the toast will cause an activation
        /// event to be fired. If it does, it takes the focus away from
        /// any other window (that the user may be using) that has the focus at 
        /// the time the toast is shown.
        /// </summary>
        bool DoNotCauseWindowActivation { get; set; }

        /// <summary>
        /// The time the toast will be displayed for.
        /// </summary>
        int TimeToStayUpMilisec { get; set; }

        #endregion

        #region Methods

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
        void GetInitialPlacement(double width, double height,
                                 out Point ptStart, out Point ptEnd);

        /// <summary>
        /// Shows the toast with animation effects in certain area defined by the 
        /// two given input locations.
        /// This assumes that the toast has already been "shown" with the native show
        /// Window function. From that point on, the animation will kick in.
        /// </summary>
        /// <param name="startPoint">Start point of the animation.</param>
        /// <param name="endPoint">End point of the animation.</param>
        /// <param name="animationDone">Delegate to be called when the animation is done.</param>
        void AnimatedShow(Point startPoint, Point endPoint);

        /// <summary>
        /// Cancels a currently animated show operation.
        /// </summary>
        void CancelAnimatedShow();

        #endregion
    }
}
