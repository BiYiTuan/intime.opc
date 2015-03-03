using System;
using System.Runtime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Intime.OPC.CustomControls
{
    public sealed class ImageButtonChrome : Decorator
    {
        public static readonly DependencyProperty BackgroundProperty;
        public static readonly DependencyProperty BorderBrushProperty;
        public static readonly DependencyProperty RenderDefaultedProperty;
        public static readonly DependencyProperty RenderMouseOverProperty;
        public static readonly DependencyProperty RenderPressedProperty;
        public static readonly DependencyProperty RoundCornersProperty;
        private static Pen _commonBorderPen;
        private static Pen _commonInnerBorderPen;
        private static Pen _commonDisabledBorderOverlay;
        private static SolidColorBrush _commonDisabledBackgroundOverlay;
        private static Pen _commonDefaultedInnerBorderPen;
        private static LinearGradientBrush _commonHoverBackgroundOverlay;
        private static Pen _commonHoverBorderOverlay;
        private static LinearGradientBrush _commonPressedBackgroundOverlay;
        private static Pen _commonPressedBorderOverlay;
        private static LinearGradientBrush _commonPressedLeftDropShadowBrush;
        private static LinearGradientBrush _commonPressedTopDropShadowBrush;
        private static object _resourceAccess;
        private LocalResources _localResources;

        static ImageButtonChrome()
        {
            BackgroundProperty = Control.BackgroundProperty.AddOwner(typeof (ImageButtonChrome),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
            BorderBrushProperty = Border.BorderBrushProperty.AddOwner(typeof (ImageButtonChrome),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
            RenderDefaultedProperty = DependencyProperty.Register("RenderDefaulted", typeof (bool),
                typeof (ImageButtonChrome), new FrameworkPropertyMetadata(false, OnRenderDefaultedChanged));
            RenderMouseOverProperty = DependencyProperty.Register("RenderMouseOver", typeof (bool),
                typeof (ImageButtonChrome), new FrameworkPropertyMetadata(false, OnRenderMouseOverChanged));
            RenderPressedProperty = DependencyProperty.Register("RenderPressed", typeof (bool),
                typeof (ImageButtonChrome), new FrameworkPropertyMetadata(false, OnRenderPressedChanged));
            RoundCornersProperty = DependencyProperty.Register("RoundCorners", typeof (bool), typeof (ImageButtonChrome),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
            _resourceAccess = new object();
            IsEnabledProperty.OverrideMetadata(typeof (ImageButtonChrome),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public ImageButtonChrome()
        {
        }

        public Brush Background
        {
            get { return (Brush) base.GetValue(BackgroundProperty); }
            set { base.SetValue(BackgroundProperty, value); }
        }

        public Brush BorderBrush
        {
            get { return (Brush) base.GetValue(BorderBrushProperty); }
            set { base.SetValue(BorderBrushProperty, value); }
        }

        public bool RenderDefaulted
        {
            get { return (bool) base.GetValue(RenderDefaultedProperty); }
            set { base.SetValue(RenderDefaultedProperty, value); }
        }

        public bool RenderMouseOver
        {
            get { return (bool) base.GetValue(RenderMouseOverProperty); }
            set { base.SetValue(RenderMouseOverProperty, value); }
        }

        public bool RenderPressed
        {
            get { return (bool) base.GetValue(RenderPressedProperty); }
            set { base.SetValue(RenderPressedProperty, value); }
        }

        public bool RoundCorners
        {
            get { return (bool) base.GetValue(RoundCornersProperty); }
            set { base.SetValue(RoundCornersProperty, value); }
        }

        internal int EffectiveValuesInitialSize
        {
            get { return 9; }
        }

        private bool Animates
        {
            get
            {
                return SystemParameters.PowerLineStatus == PowerLineStatus.Online &&
                       SystemParameters.ClientAreaAnimation && RenderCapability.Tier > 0 && base.IsEnabled;
            }
        }

        private static LinearGradientBrush CommonHoverBackgroundOverlay
        {
            get
            {
                if (_commonHoverBackgroundOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonHoverBackgroundOverlay == null)
                        {
                            var linearGradientBrush = new LinearGradientBrush();
                            linearGradientBrush.StartPoint = new Point(0.0, 0.0);
                            linearGradientBrush.EndPoint = new Point(0.0, 1.0);
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 234, 246, 253),
                                0.0));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 217, 240, 252),
                                0.5));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 190, 230, 253),
                                0.5));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 167, 217, 245),
                                1.0));
                            linearGradientBrush.Freeze();
                            _commonHoverBackgroundOverlay = linearGradientBrush;
                        }
                    }
                }
                return _commonHoverBackgroundOverlay;
            }
        }

        private static LinearGradientBrush CommonPressedBackgroundOverlay
        {
            get
            {
                if (_commonPressedBackgroundOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonPressedBackgroundOverlay == null)
                        {
                            var linearGradientBrush = new LinearGradientBrush();
                            linearGradientBrush.StartPoint = new Point(0.0, 0.0);
                            linearGradientBrush.EndPoint = new Point(0.0, 1.0);
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 194, 228, 246),
                                0.5));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 171, 218, 243),
                                0.5));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 144, 203, 235),
                                1.0));
                            linearGradientBrush.Freeze();
                            _commonPressedBackgroundOverlay = linearGradientBrush;
                        }
                    }
                }
                return _commonPressedBackgroundOverlay;
            }
        }

        private static SolidColorBrush CommonDisabledBackgroundOverlay
        {
            get
            {
                if (_commonDisabledBackgroundOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonDisabledBackgroundOverlay == null)
                        {
                            var solidColorBrush = new SolidColorBrush(Color.FromRgb(244, 244, 244));
                            solidColorBrush.Freeze();
                            _commonDisabledBackgroundOverlay = solidColorBrush;
                        }
                    }
                }
                return _commonDisabledBackgroundOverlay;
            }
        }

        private Brush BackgroundOverlay
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return Brushes.Transparent;
                    //return ImageButtonChrome.CommonDisabledBackgroundOverlay;
                }
                if (!Animates)
                {
                    if (RenderPressed)
                    {
                        return CommonPressedBackgroundOverlay;
                    }
                    if (RenderMouseOver)
                    {
                        return CommonHoverBackgroundOverlay;
                    }
                    return null;
                }
                if (_localResources != null)
                {
                    if (_localResources.BackgroundOverlay == null)
                    {
                        _localResources.BackgroundOverlay = CommonHoverBackgroundOverlay.Clone();
                        _localResources.BackgroundOverlay.Opacity = 0.0;
                    }
                    return _localResources.BackgroundOverlay;
                }
                return null;
            }
        }

        private static Pen CommonHoverBorderOverlay
        {
            get
            {
                if (_commonHoverBorderOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonHoverBorderOverlay == null)
                        {
                            var pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromRgb(60, 127, 177));
                            pen.Freeze();
                            _commonHoverBorderOverlay = pen;
                        }
                    }
                }
                return _commonHoverBorderOverlay;
            }
        }

        private static Pen CommonPressedBorderOverlay
        {
            get
            {
                if (_commonPressedBorderOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonPressedBorderOverlay == null)
                        {
                            var pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromRgb(44, 98, 139));
                            pen.Freeze();
                            _commonPressedBorderOverlay = pen;
                        }
                    }
                }
                return _commonPressedBorderOverlay;
            }
        }

        private static Pen CommonDisabledBorderOverlay
        {
            get
            {
                if (_commonDisabledBorderOverlay == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonDisabledBorderOverlay == null)
                        {
                            var pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromRgb(173, 178, 181));
                            pen.Freeze();
                            _commonDisabledBorderOverlay = pen;
                        }
                    }
                }
                return _commonDisabledBorderOverlay;
            }
        }

        private Pen BorderOverlayPen
        {
            get
            {
                if (!base.IsEnabled)
                {
                    if (RoundCorners)
                    {
                        return CommonDisabledBorderOverlay;
                    }
                    return null;
                }
                if (!Animates)
                {
                    if (RenderPressed)
                    {
                        return CommonPressedBorderOverlay;
                    }
                    if (RenderMouseOver)
                    {
                        return CommonHoverBorderOverlay;
                    }
                    return null;
                }
                if (_localResources != null)
                {
                    if (_localResources.BorderOverlayPen == null)
                    {
                        _localResources.BorderOverlayPen = CommonHoverBorderOverlay.Clone();
                        _localResources.BorderOverlayPen.Brush.Opacity = 0.0;
                    }
                    return _localResources.BorderOverlayPen;
                }
                return null;
            }
        }

        private static Pen CommonInnerBorderPen
        {
            get
            {
                if (_commonInnerBorderPen == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonInnerBorderPen == null)
                        {
                            var pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new LinearGradientBrush
                            {
                                StartPoint = new Point(0.0, 0.0),
                                EndPoint = new Point(0.0, 1.0),
                                GradientStops =
                                {
                                    new GradientStop(Color.FromArgb(250, 255, 255, 255), 0.0),
                                    new GradientStop(Color.FromArgb(133, 255, 255, 255), 1.0)
                                }
                            };
                            pen.Freeze();
                            _commonInnerBorderPen = pen;
                        }
                    }
                }
                return _commonInnerBorderPen;
            }
        }

        private static Pen CommonDefaultedInnerBorderPen
        {
            get
            {
                if (_commonDefaultedInnerBorderPen == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonDefaultedInnerBorderPen == null)
                        {
                            var pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromArgb(249, 0, 204, 255));
                            pen.Freeze();
                            _commonDefaultedInnerBorderPen = pen;
                        }
                    }
                }
                return _commonDefaultedInnerBorderPen;
            }
        }

        private Pen InnerBorderPen
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return CommonInnerBorderPen;
                }
                if (!Animates)
                {
                    if (RenderPressed)
                    {
                        return null;
                    }
                    if (RenderDefaulted)
                    {
                        return CommonDefaultedInnerBorderPen;
                    }
                    return CommonInnerBorderPen;
                }
                if (_localResources != null)
                {
                    if (_localResources.InnerBorderPen == null)
                    {
                        _localResources.InnerBorderPen = CommonInnerBorderPen.Clone();
                    }
                    return _localResources.InnerBorderPen;
                }
                return CommonInnerBorderPen;
            }
        }

        private static LinearGradientBrush CommonPressedLeftDropShadowBrush
        {
            get
            {
                if (_commonPressedLeftDropShadowBrush == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonPressedLeftDropShadowBrush == null)
                        {
                            var linearGradientBrush = new LinearGradientBrush();
                            linearGradientBrush.StartPoint = new Point(0.0, 0.0);
                            linearGradientBrush.EndPoint = new Point(1.0, 0.0);
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(128, 51, 51, 51), 0.0));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0, 51, 51, 51), 1.0));
                            linearGradientBrush.Freeze();
                            _commonPressedLeftDropShadowBrush = linearGradientBrush;
                        }
                    }
                }
                return _commonPressedLeftDropShadowBrush;
            }
        }

        private LinearGradientBrush LeftDropShadowBrush
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return null;
                }
                if (!Animates)
                {
                    if (RenderPressed)
                    {
                        return CommonPressedLeftDropShadowBrush;
                    }
                    return null;
                }
                if (_localResources != null)
                {
                    if (_localResources.LeftDropShadowBrush == null)
                    {
                        _localResources.LeftDropShadowBrush = CommonPressedLeftDropShadowBrush.Clone();
                        _localResources.LeftDropShadowBrush.Opacity = 0.0;
                    }
                    return _localResources.LeftDropShadowBrush;
                }
                return null;
            }
        }

        private static LinearGradientBrush CommonPressedTopDropShadowBrush
        {
            get
            {
                if (_commonPressedTopDropShadowBrush == null)
                {
                    lock (_resourceAccess)
                    {
                        if (_commonPressedTopDropShadowBrush == null)
                        {
                            var linearGradientBrush = new LinearGradientBrush();
                            linearGradientBrush.StartPoint = new Point(0.0, 0.0);
                            linearGradientBrush.EndPoint = new Point(0.0, 1.0);
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(128, 51, 51, 51), 0.0));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0, 51, 51, 51), 1.0));
                            linearGradientBrush.Freeze();
                            _commonPressedTopDropShadowBrush = linearGradientBrush;
                        }
                    }
                }
                return _commonPressedTopDropShadowBrush;
            }
        }

        private LinearGradientBrush TopDropShadowBrush
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return null;
                }
                if (!Animates)
                {
                    if (RenderPressed)
                    {
                        return CommonPressedTopDropShadowBrush;
                    }
                    return null;
                }
                if (_localResources != null)
                {
                    if (_localResources.TopDropShadowBrush == null)
                    {
                        _localResources.TopDropShadowBrush = CommonPressedTopDropShadowBrush.Clone();
                        _localResources.TopDropShadowBrush.Opacity = 0.0;
                    }
                    return _localResources.TopDropShadowBrush;
                }
                return null;
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            UIElement child = Child;
            Size desiredSize;
            if (child != null)
            {
                Size availableSize2 = default(Size);
                bool flag = availableSize.Width < 4.0;
                bool flag2 = availableSize.Height < 4.0;
                if (!flag)
                {
                    availableSize2.Width = availableSize.Width - 4.0;
                }
                if (!flag2)
                {
                    availableSize2.Height = availableSize.Height - 4.0;
                }
                child.Measure(availableSize2);
                desiredSize = child.DesiredSize;
                if (!flag)
                {
                    desiredSize.Width += 4.0;
                }
                if (!flag2)
                {
                    desiredSize.Height += 4.0;
                }
            }
            else
            {
                desiredSize = new Size(Math.Min(4.0, availableSize.Width), Math.Min(4.0, availableSize.Height));
            }
            return desiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect finalRect = default(Rect);
            finalRect.Width = Math.Max(0.0, finalSize.Width - 4.0);
            finalRect.Height = Math.Max(0.0, finalSize.Height - 4.0);
            finalRect.X = (finalSize.Width - finalRect.Width)*0.5;
            finalRect.Y = (finalSize.Height - finalRect.Height)*0.5;
            UIElement child = Child;
            if (child != null)
            {
                child.Arrange(finalRect);
            }
            return finalSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var rect = new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight);
            DrawBackground(drawingContext, ref rect);
            DrawDropShadows(drawingContext, ref rect);
            if (IsEnabled)
            {
                DrawBorder(drawingContext, ref rect);
            }
            DrawInnerBorder(drawingContext, ref rect);
        }

        private static void OnRenderDefaultedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var buttonChrome = (ImageButtonChrome) o;
            if (buttonChrome.Animates)
            {
                if (!buttonChrome.RenderPressed)
                {
                    if ((bool) e.NewValue)
                    {
                        if (buttonChrome._localResources == null)
                        {
                            buttonChrome._localResources = new LocalResources();
                            buttonChrome.InvalidateVisual();
                        }
                        var duration = new Duration(TimeSpan.FromSeconds(0.3));
                        var animation = new ColorAnimation(Color.FromArgb(249, 0, 204, 255), duration);
                        GradientStopCollection gradientStops =
                            ((LinearGradientBrush) buttonChrome.InnerBorderPen.Brush).GradientStops;
                        gradientStops[0].BeginAnimation(GradientStop.ColorProperty, animation);
                        gradientStops[1].BeginAnimation(GradientStop.ColorProperty, animation);
                        var doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame(1.0,
                            TimeSpan.FromSeconds(0.5)));
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new DiscreteDoubleKeyFrame(1.0,
                            TimeSpan.FromSeconds(0.75)));
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame(0.0,
                            TimeSpan.FromSeconds(2.0)));
                        doubleAnimationUsingKeyFrames.RepeatBehavior = RepeatBehavior.Forever;
                        Timeline.SetDesiredFrameRate(doubleAnimationUsingKeyFrames, 10);
                        buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty,
                            doubleAnimationUsingKeyFrames);
                        buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty,
                            doubleAnimationUsingKeyFrames);
                        return;
                    }
                    if (buttonChrome._localResources == null)
                    {
                        buttonChrome.InvalidateVisual();
                        return;
                    }
                    var duration2 = new Duration(TimeSpan.FromSeconds(0.2));
                    var doubleAnimation = new DoubleAnimation();
                    doubleAnimation.Duration = duration2;
                    buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
                    buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
                    var colorAnimation = new ColorAnimation();
                    colorAnimation.Duration = duration2;
                    GradientStopCollection gradientStops2 =
                        ((LinearGradientBrush) buttonChrome.InnerBorderPen.Brush).GradientStops;
                    gradientStops2[0].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
                    gradientStops2[1].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
                }
            }
            else
            {
                buttonChrome._localResources = null;
                buttonChrome.InvalidateVisual();
            }
        }

        private static void OnRenderMouseOverChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var buttonChrome = (ImageButtonChrome) o;
            if (buttonChrome.Animates)
            {
                if (!buttonChrome.RenderPressed)
                {
                    if ((bool) e.NewValue)
                    {
                        if (buttonChrome._localResources == null)
                        {
                            buttonChrome._localResources = new LocalResources();
                            buttonChrome.InvalidateVisual();
                        }
                        var duration = new Duration(TimeSpan.FromSeconds(0.3));
                        var animation = new DoubleAnimation(1.0, duration);
                        buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
                        buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, animation);
                        return;
                    }
                    if (buttonChrome._localResources == null)
                    {
                        buttonChrome.InvalidateVisual();
                        return;
                    }
                    if (buttonChrome.RenderDefaulted)
                    {
                        double opacity = buttonChrome.BackgroundOverlay.Opacity;
                        double num = (1.0 - opacity)*0.5;
                        var doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame(1.0,
                            TimeSpan.FromSeconds(num)));
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new DiscreteDoubleKeyFrame(1.0,
                            TimeSpan.FromSeconds(num + 0.25)));
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame(0.0,
                            TimeSpan.FromSeconds(num + 1.5)));
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame(opacity,
                            TimeSpan.FromSeconds(2.0)));
                        doubleAnimationUsingKeyFrames.RepeatBehavior = RepeatBehavior.Forever;
                        Timeline.SetDesiredFrameRate(doubleAnimationUsingKeyFrames, 10);
                        buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty,
                            doubleAnimationUsingKeyFrames);
                        buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty,
                            doubleAnimationUsingKeyFrames);
                        return;
                    }
                    var duration2 = new Duration(TimeSpan.FromSeconds(0.2));
                    var doubleAnimation = new DoubleAnimation();
                    doubleAnimation.Duration = duration2;
                    buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
                    buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
                }
            }
            else
            {
                buttonChrome._localResources = null;
                buttonChrome.InvalidateVisual();
            }
        }

        private static void OnRenderPressedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var buttonChrome = (ImageButtonChrome) o;
            if (!buttonChrome.Animates)
            {
                buttonChrome._localResources = null;
                buttonChrome.InvalidateVisual();
                return;
            }
            if ((bool) e.NewValue)
            {
                if (buttonChrome._localResources == null)
                {
                    buttonChrome._localResources = new LocalResources();
                    buttonChrome.InvalidateVisual();
                }
                var duration = new Duration(TimeSpan.FromSeconds(0.1));
                var animation = new DoubleAnimation(1.0, duration);
                buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, animation);
                buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
                buttonChrome.LeftDropShadowBrush.BeginAnimation(Brush.OpacityProperty, animation);
                buttonChrome.TopDropShadowBrush.BeginAnimation(Brush.OpacityProperty, animation);
                animation = new DoubleAnimation(0.0, duration);
                buttonChrome.InnerBorderPen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
                var animation2 = new ColorAnimation(Color.FromRgb(194, 228, 246), duration);
                GradientStopCollection gradientStops =
                    ((LinearGradientBrush) buttonChrome.BackgroundOverlay).GradientStops;
                gradientStops[0].BeginAnimation(GradientStop.ColorProperty, animation2);
                gradientStops[1].BeginAnimation(GradientStop.ColorProperty, animation2);
                animation2 = new ColorAnimation(Color.FromRgb(171, 218, 243), duration);
                gradientStops[2].BeginAnimation(GradientStop.ColorProperty, animation2);
                animation2 = new ColorAnimation(Color.FromRgb(144, 203, 235), duration);
                gradientStops[3].BeginAnimation(GradientStop.ColorProperty, animation2);
                animation2 = new ColorAnimation(Color.FromRgb(44, 98, 139), duration);
                buttonChrome.BorderOverlayPen.Brush.BeginAnimation(SolidColorBrush.ColorProperty, animation2);
                return;
            }
            if (buttonChrome._localResources == null)
            {
                buttonChrome.InvalidateVisual();
                return;
            }
            bool renderMouseOver = buttonChrome.RenderMouseOver;
            var duration2 = new Duration(TimeSpan.FromSeconds(0.1));
            var doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = duration2;
            buttonChrome.LeftDropShadowBrush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
            buttonChrome.TopDropShadowBrush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
            buttonChrome.InnerBorderPen.Brush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
            if (!renderMouseOver)
            {
                buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
                buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
            }
            var colorAnimation = new ColorAnimation();
            colorAnimation.Duration = duration2;
            buttonChrome.BorderOverlayPen.Brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            GradientStopCollection gradientStops2 = ((LinearGradientBrush) buttonChrome.BackgroundOverlay).GradientStops;
            gradientStops2[0].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
            gradientStops2[1].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
            gradientStops2[2].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
            gradientStops2[3].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
        }

        private void DrawBackground(DrawingContext dc, ref Rect bounds)
        {
            if (!base.IsEnabled && !RoundCorners)
            {
                return;
            }
            Brush brush = Background;
            if (bounds.Width > 4.0 && bounds.Height > 4.0)
            {
                var rectangle = new Rect(bounds.Left + 1.0, bounds.Top + 1.0, bounds.Width - 2.0, bounds.Height - 2.0);
                if (brush != null)
                {
                    dc.DrawRectangle(brush, null, rectangle);
                }
                brush = BackgroundOverlay;
                if (brush != null)
                {
                    dc.DrawRectangle(brush, null, rectangle);
                }
            }
        }

        private void DrawDropShadows(DrawingContext dc, ref Rect bounds)
        {
            if (bounds.Width > 4.0 && bounds.Height > 4.0)
            {
                Brush leftDropShadowBrush = LeftDropShadowBrush;
                if (leftDropShadowBrush != null)
                {
                    dc.DrawRectangle(leftDropShadowBrush, null, new Rect(1.0, 1.0, 2.0, bounds.Bottom - 2.0));
                }
                Brush topDropShadowBrush = TopDropShadowBrush;
                if (topDropShadowBrush != null)
                {
                    dc.DrawRectangle(topDropShadowBrush, null, new Rect(1.0, 1.0, bounds.Right - 2.0, 2.0));
                }
            }
        }

        private void DrawBorder(DrawingContext dc, ref Rect bounds)
        {
            if (bounds.Width >= 5.0 && bounds.Height >= 5.0)
            {
                Brush brush = BorderBrush;
                Pen pen = null;
                if (brush != null)
                {
                    if (_commonBorderPen == null)
                    {
                        lock (_resourceAccess)
                        {
                            if (_commonBorderPen == null)
                            {
                                if (!brush.IsFrozen && brush.CanFreeze)
                                {
                                    brush = brush.Clone();
                                    brush.Freeze();
                                }
                                var pen2 = new Pen(brush, 1.0);
                                if (pen2.CanFreeze)
                                {
                                    pen2.Freeze();
                                    _commonBorderPen = pen2;
                                }
                            }
                        }
                    }
                    if (_commonBorderPen != null && brush == _commonBorderPen.Brush)
                    {
                        pen = _commonBorderPen;
                    }
                    else
                    {
                        if (!brush.IsFrozen && brush.CanFreeze)
                        {
                            brush = brush.Clone();
                            brush.Freeze();
                        }
                        pen = new Pen(brush, 1.0);
                        if (pen.CanFreeze)
                        {
                            pen.Freeze();
                        }
                    }
                }
                Pen borderOverlayPen = BorderOverlayPen;
                if (pen != null || borderOverlayPen != null)
                {
                    if (RoundCorners)
                    {
                        var rectangle = new Rect(bounds.Left + 0.5, bounds.Top + 0.5, bounds.Width - 1.0,
                            bounds.Height - 1.0);
                        if (base.IsEnabled && pen != null)
                        {
                            dc.DrawRoundedRectangle(null, pen, rectangle, 2.75, 2.75);
                        }
                        if (borderOverlayPen != null)
                        {
                            dc.DrawRoundedRectangle(null, borderOverlayPen, rectangle, 2.75, 2.75);
                        }
                    }
                    else
                    {
                        var pathFigure = new PathFigure();
                        pathFigure.StartPoint = new Point(0.5, 0.5);
                        pathFigure.Segments.Add(new LineSegment(new Point(0.5, bounds.Bottom - 0.5), true));
                        pathFigure.Segments.Add(new LineSegment(new Point(bounds.Right - 2.5, bounds.Bottom - 0.5), true));
                        pathFigure.Segments.Add(new ArcSegment(new Point(bounds.Right - 0.5, bounds.Bottom - 2.5),
                            new Size(2.0, 2.0), 0.0, false, SweepDirection.Counterclockwise, true));
                        pathFigure.Segments.Add(new LineSegment(new Point(bounds.Right - 0.5, bounds.Top + 2.5), true));
                        pathFigure.Segments.Add(new ArcSegment(new Point(bounds.Right - 2.5, bounds.Top + 0.5),
                            new Size(2.0, 2.0), 0.0, false, SweepDirection.Counterclockwise, true));
                        pathFigure.IsClosed = true;
                        var pathGeometry = new PathGeometry();
                        pathGeometry.Figures.Add(pathFigure);
                        if (base.IsEnabled && pen != null)
                        {
                            dc.DrawGeometry(null, pen, pathGeometry);
                        }
                        if (borderOverlayPen != null)
                        {
                            dc.DrawGeometry(null, borderOverlayPen, pathGeometry);
                        }
                    }
                }
            }
        }

        private void DrawInnerBorder(DrawingContext dc, ref Rect bounds)
        {
            if (!base.IsEnabled && !RoundCorners)
            {
                return;
            }
            if (bounds.Width >= 4.0 && bounds.Height >= 4.0)
            {
                Pen innerBorderPen = InnerBorderPen;
                if (innerBorderPen != null)
                {
                    dc.DrawRoundedRectangle(null, innerBorderPen,
                        new Rect(bounds.Left + 1.5, bounds.Top + 1.5, bounds.Width - 3.0, bounds.Height - 3.0), 1.75,
                        1.75);
                }
            }
        }

        private class LocalResources
        {
            public LinearGradientBrush BackgroundOverlay;
            public Pen BorderOverlayPen;
            public Pen InnerBorderPen;
            public LinearGradientBrush LeftDropShadowBrush;
            public LinearGradientBrush TopDropShadowBrush;
        }
    }
}