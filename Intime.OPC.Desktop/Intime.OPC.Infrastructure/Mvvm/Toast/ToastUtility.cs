using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Intime.OPC.Infrastructure.Mvvm.Toast
{
    public class ToastUtility
    {
        #region Private Fields

        private const int DefaultAnimationRefreshRateMilisec = 300;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// This is a utility function that emulates a "toast" slide-in effect when
        /// showing the toast.
        /// </summary>
        /// <param name="toast">The toast to animate.</param>
        /// <param name="startPoint">The starting location of the animation.</param>
        /// <param name="endPoint">The ending location of the animation.</param>
        /// <param name="animationCompleted">An event delegate to be called when the animation
        /// is completed.</param>
        /// <returns>The story board used for the animation</returns>
        public static Storyboard SlideIn(Window toast, Point startPoint, Point endPoint, EventHandler animationCompleted)
        {
            // create a storyboard to slide the toast into position
            Storyboard currentStoryboard = new Storyboard();
            Duration duration = new TimeSpan(0, 0, 0, 0, DefaultAnimationRefreshRateMilisec);

            // create animations for left and top 
            DoubleAnimation animLeft = new DoubleAnimation(startPoint.X, endPoint.X, duration, FillBehavior.Stop);
            currentStoryboard.Children.Add(animLeft);
            Storyboard.SetTargetName(animLeft, toast.Name);
            Storyboard.SetTargetProperty(animLeft, new PropertyPath(Window.LeftProperty));

            DoubleAnimation animTop = new DoubleAnimation(startPoint.Y, endPoint.Y, duration, FillBehavior.Stop);
            currentStoryboard.Children.Add(animTop);
            Storyboard.SetTargetName(animTop, toast.Name);
            Storyboard.SetTargetProperty(animTop, new PropertyPath(Window.TopProperty));

            // run the storyboard
            currentStoryboard.Completed += animationCompleted;
            currentStoryboard.Begin(toast, true);

            return currentStoryboard;
        }

        /// <summary>
        /// This is a utility function that emulates a "toast" fade-out effect when
        /// hiding the toast.
        /// </summary>
        /// <param name="toast">The toast to animate.</param>
        /// <param name="animationCompleted">An event delegate to be called when the animation
        /// is completed.</param>
        /// <returns>The story board used for the animation</returns>
        public static Storyboard FadeOut(Window toast, EventHandler animationCompleted)
        {
            // create animation to fade out the toast
            Storyboard currentStoryboard = new Storyboard();
            Duration duration = new TimeSpan(0, 0, 0, 0, DefaultAnimationRefreshRateMilisec);

            // create animations for opacity
            DoubleAnimation anim = new DoubleAnimation(1, 0, duration, FillBehavior.Stop);
            currentStoryboard.Children.Add(anim);
            Storyboard.SetTargetName(anim, toast.Name);
            Storyboard.SetTargetProperty(anim, new PropertyPath(Window.OpacityProperty));

            // run the animation
            currentStoryboard.Completed += new EventHandler(animationCompleted);
            currentStoryboard.Begin(toast, true);

            return currentStoryboard;
        }

        /// <summary>
        /// This is a utility function that emulates a "toast" slide-in effect when
        /// showing the toast.
        /// </summary>
        /// <param name="toast">The toast to animate.</param>
        /// <param name="startPoint">The starting location of the animation.</param>
        /// <param name="endPoint">The ending location of the animation.</param>
        /// <param name="animationCompleted">An event delegate to be called when the animation
        /// is completed.</param>
        /// <returns>The story board used for the animation</returns>
        public static Storyboard SlideOut(Window toast, Point startPoint, Point endPoint,
                                                        EventHandler animationCompleted)
        {
            // create a storyboard to slide the toast into position
            Storyboard animationStoryBoard = new Storyboard();
            Duration duration = new TimeSpan(0, 0, 0, 0, DefaultAnimationRefreshRateMilisec);

            DoubleAnimation animTop = new DoubleAnimation(endPoint.Y, startPoint.Y, duration, FillBehavior.Stop);
            animationStoryBoard.Children.Add(animTop);
            Storyboard.SetTargetName(animTop, toast.Name);
            Storyboard.SetTargetProperty(animTop, new PropertyPath(Window.TopProperty));

            // run the storyboard
            animationStoryBoard.Completed += new EventHandler(animationCompleted);
            animationStoryBoard.Begin(toast, true);

            return animationStoryBoard;
        }

        #endregion
    }
}
