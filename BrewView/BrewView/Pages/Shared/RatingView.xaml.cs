using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BrewView.Pages.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingView : ContentView
    {
        public static readonly BindableProperty MaxRatingProperty =
            BindableProperty.Create(nameof(MaxRating), typeof(int), typeof(RatingView), 5,
                propertyChanged: InvalidateSurface);

        public static readonly BindableProperty StrokeColorProperty =
            BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(RatingView), Color.Black,
                propertyChanged: InvalidateSurface);

        public static readonly BindableProperty EmptyColorProperty =
            BindableProperty.Create(nameof(EmptyColor), typeof(Color), typeof(RatingView), Color.White,
                propertyChanged: InvalidateSurface);

        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(RatingView), Color.Gold,
                propertyChanged: InvalidateSurface);

        public static readonly BindableProperty RatingProperty =
            BindableProperty.Create(nameof(Rating), typeof(int), typeof(RatingView), 0,
                propertyChanged: InvalidateSurface, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty IsSettableProperty =
            BindableProperty.Create(nameof(IsSettable), typeof(bool), typeof(RatingView), true);

        private readonly SKCanvasView m_canvasView;
        private int m_height;
        private int m_width;

        public RatingView()
        {
            m_canvasView = new SKCanvasView();
            m_canvasView.PaintSurface += OnPaintSurface;
            Content = m_canvasView;
        }

        public bool IsSettable
        {
            get => (bool) GetValue(IsSettableProperty);
            set => SetValue(IsSettableProperty, value);
        }

        public int Rating
        {
            get => (int) GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        public int MaxRating
        {
            get => (int) GetValue(MaxRatingProperty);
            set => SetValue(MaxRatingProperty, value);
        }

        public Color StrokeColor
        {
            get => (Color) GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }

        public Color EmptyColor
        {
            get => (Color) GetValue(EmptyColorProperty);
            set => SetValue(EmptyColorProperty, value);
        }

        public Color FillColor
        {
            get => (Color) GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        private static void InvalidateSurface(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is RatingView ratingView) ratingView.m_canvasView.InvalidateSurface();
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            m_height = info.Height;
            m_width = info.Width;

            canvas.Clear();

            var stroke = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = StrokeColor.ToSKColor(),
                StrokeWidth = 5
            };

            var background = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = EmptyColor.ToSKColor()
            };

            var fill = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = FillColor.ToSKColor()
            };

            var starWidth = info.Width / MaxRating;

            for (var i = 0; i < MaxRating; i++)
            {
                var center = new SKPoint(starWidth / 2 + starWidth * i, info.Height / 2);

                var radius = 0.45f * Math.Min(starWidth, info.Height);

                var path = new SKPath();

                path.MoveTo(center.X, info.Height / 2 - radius);

                for (var y = 1; y < 5; y++)
                {
                    // angle from vertical
                    var angle = y * 4 * Math.PI / 5;
                    path.LineTo(center + new SKPoint(radius * (float) Math.Sin(angle),
                        -radius * (float) Math.Cos(angle)));
                }

                path.Close();

                canvas.DrawPath(path, stroke);
                canvas.DrawPath(path, i <= Rating - 1 ? fill : background);
            }
        }

        public void SendPan(float x, int i, GestureStatus running, int pointerId)
        {
            //SetRating(x);
        }

        private void SetRating(float x)
        {
            if (!IsSettable) return;
            var starWidth = m_width / MaxRating;
            for (var i = 0; i < MaxRating; i++)
            {
                if (!(x >= starWidth * i) || !(x <= starWidth * (1 + i))) continue;

                Rating = i + 1;
                break;
            }

            m_canvasView.InvalidateSurface();
        }

        public void SendTapped(float x, float y)
        {
            SetRating(x);
        }
    }
}