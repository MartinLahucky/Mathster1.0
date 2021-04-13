using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Mathster.Resources.Custom_UI
{
    public class Circles
    {
        private readonly Func<SKImageInfo, SKPoint> centerFunc;

        public Circles(float radius, Func<SKImageInfo, SKPoint> centerfold)
        {
            centerFunc = centerfold;
            Radius = radius;
        }

        private SKPoint Center { get; set; }
        private float Radius { get; set; }
        private SKCanvas Canvas { get; set; }

        private SKRect Rect => new(Center.X - Radius, Center.Y - Radius, Center.X + Radius, Center.Y + Radius);

        public void DrawFullProgressBar(SKCanvasView view, string backgroundColorHex, string progressBarColorHex,
            float progressBarThickness, float progress, string progressColorHex)
        {
            // This not nice nothing or redundant code 
            view.PaintSurface += (sender, args) =>
            {
                Canvas = args.Surface.Canvas;
                CalculateCenter(args.Info);
                Canvas.Clear();
                DrawFullCircle(backgroundColorHex);
                DrawCircleBorder(progressBarThickness, progressBarColorHex);
                DrawProgress(progress, progressBarThickness, progressColorHex);
            };
        }

        public void DrawFullCircle(SKCanvasView view, string backgroundColorHex)
        {
            // This ugliness or redundant code 
            view.PaintSurface += (sender, args) =>
            {
                Canvas = args.Surface.Canvas;
                CalculateCenter(args.Info);
                Canvas.Clear();
                DrawFullCircle(backgroundColorHex);
            };
        }

        //TODO create a class for entry and colorHex --> All these parameters will be stored in array 
        public void DrawChart(SKCanvasView view, string backgroundColorHex, string progressBarColorHex,
            float progressBarThickness, float entry1, float entry2, float entry3, float entryMax, string entry1ColorHex,
            string entry2ColorHex, string entry3ColorHex)
        {
            view.PaintSurface += (sender, args) =>
            {
                ChartPartCalculations(entry1, entry2, entry3, entryMax,  out float result1, out float result2,
                    out float result3);
                Canvas = args.Surface.Canvas;
                CalculateCenter(args.Info);
                Canvas.Clear();
                DrawFullCircle(backgroundColorHex);
                DrawCircleBorder(progressBarThickness, progressBarColorHex);
                DrawProgress(result1, progressBarThickness, entry1ColorHex);
                DrawProgress(result2, progressBarThickness, entry2ColorHex);
                DrawProgress(result3, progressBarThickness, entry3ColorHex);
            };
        }

        /*
        private ChartPart[] ChartPartCalcualtion(ChartPart[] chartParts, float max)
        {
            ChartPart[] chartPartsCalculated = new ChartPart[chartParts.Length];
            float value = 0;
            for (int i = chartParts.Length; i > 0; i--)
            {
                value += chartParts[i].PartValue;
                chartPartsCalculated[i].PartValue = value / max * 100;
            }

            return chartPartsCalculated;
        }
         */

        private void ChartPartCalculations(float entry1, float entry2, float entry3, float max, out float result1,
            out float result2, out float result3)
        {
            result1 = (entry1 + entry2 + entry3) / max * 100;
            result2 = (entry2 + entry3) / max * 100;
            result3 = entry3 / max * 100;
        }

        private void CalculateCenter(SKImageInfo argsInfo)
        {
            Center = centerFunc.Invoke(argsInfo);
        }

        private void DrawFullCircle(string backgroundColorHex)
        {
            Canvas.DrawCircle(Center, Radius,
                new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColor.Parse(backgroundColorHex)
                });
        }

        private void DrawCircleBorder(float progressBarThickness, string colorHex)
        {
            Canvas.DrawCircle(Center, Radius,
                new SKPaint
                {
                    StrokeWidth = progressBarThickness,
                    Color = SKColor.Parse(colorHex),
                    IsStroke = true
                });
        }

        private void DrawProgress(float progress, float progressBarThickness, string colorHex)
        {
            Func<float> step = () => progress;
            var angle = step.Invoke() * 3.6f;
            Canvas.DrawArc(Rect, 270, angle, false,
                new SKPaint {StrokeWidth = progressBarThickness, Color = SKColor.Parse(colorHex), IsStroke = true});
        }
    }
}