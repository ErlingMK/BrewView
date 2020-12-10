using System;
using Android.Content;
using Android.Views;
using BrewView.Droid.Renderers;
using BrewView.Pages.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;


[assembly: ExportRenderer(typeof(RatingView), typeof(RatingContentView))]
namespace BrewView.Droid.Renderers
{
    public class RatingContentView : ViewRenderer, GestureDetector.IOnGestureListener
    {
        public RatingContentView(Context context) : base(context)
        {
            m_random = new Random();
            m_detector = new GestureDetector(context, this);
            m_density = context.Resources.DisplayMetrics.Density;
        }

        private readonly GestureDetector m_detector;
        private RatingView m_elem;
        private bool m_isScrolling;
        private int m_pointerId;
        private readonly Random m_random;
        private readonly int m_scaledTouchSlop = 5;
        private float m_startX;
        private float m_density;


        public bool OnDown(MotionEvent e)
        {
            return true;
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            return false;
        }

        public void OnLongPress(MotionEvent e)
        {
        }

        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            if (m_isScrolling)
            {
                var (x, y) = ToDip(e2.RawX, e2.RawY);
                m_elem?.SendPan(x - m_startX, 0, GestureStatus.Running, m_pointerId);
            }
            return true;
        }

        public void OnShowPress(MotionEvent e)
        {
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            return false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is RatingView contentView) m_elem = contentView;
        }

        private bool StartScroll(MotionEvent ev)
        {
            if (ev.HistorySize < 1) return false;
            var historicalX = ev.GetHistoricalX(0);
            if (Math.Abs(historicalX - ev.GetX()) > m_scaledTouchSlop) // Increase value if we require a longer drag before scrolling starts.
            {
                m_isScrolling = true;
                m_elem?.SendPan(0, 0, GestureStatus.Started, m_pointerId);
                return true;
            }

            return false;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            var action = ev.ActionMasked;
            var (x, y) = (ev.RawX, ev.RawY);

            var loc = new int[2];
            GetLocationInWindow(loc);

            x -= loc[0];
            y -= loc[1];

            switch (action)
            {
                case MotionEventActions.Up:
                    if (!m_isScrolling)
                    {
                        m_elem.SendTapped(x, y);
                    }
                    break;
                case MotionEventActions.Move:
                    return m_isScrolling || StartScroll(ev);
                case MotionEventActions.Down: // This case is the only case that is always intercepted no matter the view hierarchy. (I think) 
                    m_startX = x;
                    m_pointerId = m_random.Next();
                    return false;
            }

            return false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.ActionMasked == MotionEventActions.Up || e.ActionMasked == MotionEventActions.Cancel)
            {
                var (x, y) = (e.RawX, e.RawY);

                var loc = new int[2];
                GetLocationInWindow(loc);

                x -= loc[0];
                y -= loc[1];

                if (m_isScrolling) m_elem?.SendPan(x - m_startX, 0, GestureStatus.Completed, m_pointerId);
                else
                {
                    m_elem.SendTapped(x, y);
                }
                m_isScrolling = false;
                return false;
            }

            if (!m_isScrolling && e.ActionMasked == MotionEventActions.Move) // Is needed when this does not contain any children that can handle touch events. 
            {
                StartScroll(e);
            }

            return m_detector.OnTouchEvent(e);
        }

        private (float, float) ToDip(float rawX, float rawY) => (rawX / m_density, rawY / m_density);
    }

}