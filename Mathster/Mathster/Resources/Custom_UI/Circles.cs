using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Mathster.Resources.Custom_UI
{
    public class Circles
    {
        private readonly Func<SKImageInfo, SKPoint> _centerfunc;
        public SKPoint Center { get; set; }
        public float Redius { get; set; }
        private SKCanvas canvas { get; set; }

        public Circles(float redius, Func<SKImageInfo, SKPoint> centerfunc)
        {
            _centerfunc = centerfunc;
            Redius = redius;
        }

        public void DrawFullProgressBar(SKCanvasView View, string backgroundColorHex, string progressBarColorHex,
            float ProgressBarThickness, float progress, string progressColorHex)
        {
            // Bez tohohle se to nepostaví, buď redundantní kód nebo tahle ošklivost 
            View.PaintSurface += (sender, args) =>
            {
                canvas = args.Surface.Canvas;
                CalculateCenter(args.Info);
                canvas.Clear();
                DrawFullCircle(backgroundColorHex);
                DrawCircleBorder(ProgressBarThickness, progressBarColorHex);
                DrawProgress(progress, ProgressBarThickness, progressColorHex);
            };
        }

        public void DrawFullCircle(SKCanvasView View, string backgroundColorHex)
        {
            // Bez tohohle se to nepostaví, buď redundantní kód nebo tahle ošklivost 
            View.PaintSurface += (sender, args) =>
            {
                canvas = args.Surface.Canvas;
                CalculateCenter(args.Info);
                canvas.Clear();
                DrawFullCircle(backgroundColorHex);
            };
        }

        public void DrawChart(SKCanvasView View, string backgroundColorHex, string progressBarColorHex,
            float ProgressBarThickness, float entry1, float entry2, float entry3, float entryMax, string entry1ColorHex,
            string entry2ColorHex, string entry3ColorHex)
        {
            View.PaintSurface += (sender, args) =>
            {
                float vysledek1, vysledek2, vysledek3;
                VypocetVelikostCastiGrafu(entry1, entry2, entry3, entryMax, out vysledek1, out vysledek2,
                    out vysledek3);
                canvas = args.Surface.Canvas;
                CalculateCenter(args.Info);
                canvas.Clear();
                DrawFullCircle(backgroundColorHex);
                DrawCircleBorder(ProgressBarThickness, progressBarColorHex);
                DrawProgress(vysledek1, ProgressBarThickness, entry1ColorHex);
                DrawProgress(vysledek2, ProgressBarThickness, entry2ColorHex);
                DrawProgress(vysledek3, ProgressBarThickness, entry3ColorHex);
            };
        }

        private void VypocetVelikostCastiGrafu(float entry1, float entry2, float entry3, float max, out float vysledek1,
            out float vysledek2, out float vysledek3)
        {
            vysledek1 = ((entry1 + entry2 + entry3) / max) * 100;
            vysledek2 = ((entry2 + entry3) / max) * 100;
            vysledek3 = (entry3 / max) * 100;
        }

        public SKRect Rect => new SKRect(Center.X - Redius, Center.Y - Redius, Center.X + Redius, Center.Y + Redius);

        public void CalculateCenter(SKImageInfo argsInfo)
        {
            Center = _centerfunc.Invoke(argsInfo);
        }

        private void DrawFullCircle(string backgroundColorHex)
        {
            canvas.DrawCircle(Center, Redius,
                new SKPaint()
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColor.Parse(backgroundColorHex)
                });
        }

        private void DrawCircleBorder(float ProgressBarThickness, string colorHex)
        {
            canvas.DrawCircle(Center, Redius,
                new SKPaint()
                {
                    StrokeWidth = ProgressBarThickness,
                    Color = SKColor.Parse(colorHex),
                    IsStroke = true
                });
        }

        private void DrawProgress(float progress, float ProgressBarThickness, string colorHex)
        {
            Func<float> postup = () => progress;
            var angle = postup.Invoke() * 3.6f;
            canvas.DrawArc(Rect, 270, angle, false,
                new SKPaint() {StrokeWidth = ProgressBarThickness, Color = SKColor.Parse(colorHex), IsStroke = true});
        }
    }
}