using SkiaSharp;
using SkiaSharp.Views.Forms;
using StandardEngineDraw;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace XamarinGameGraph.EngineDraw
{
    public class XamarinEngineDraw : IDrawEngine
    {
        bool _draw = false;
        List<DrawFunction> _functionsQueue = new List<DrawFunction>();

        SKCanvas Canvas
        {
            get;
            set;
        }

        public XamarinEngineDraw (SKCanvas canvas)
        {
            Canvas = canvas;
            Clear();
        }

        public void ReDraw(SKCanvas canvas)
        {
            Canvas = canvas;
            if (_functionsQueue.Count == 0)
                return;

            _draw = true;

            Clear();

            foreach (DrawFunction function in _functionsQueue)
                function.Start();

            _functionsQueue.Clear();

            _draw = false;
        }

        public void Clear()
        {
            if (_draw)
                Canvas.Clear();
        }

        public void DrawImage(byte[] bitmap, float xCenter, float yCenter, float xLength, float yLength, float angleRotation, bool mirror)
        {
            if (_draw)
            {
                SKImage image = SKImage.FromEncodedData(bitmap);

                SKRect rect = new SKRect(xCenter, yCenter, xCenter + xLength, yCenter + yLength);

                Canvas.DrawImage(image, rect);
            }
            else
            {
                _functionsQueue.Add(new DrawFunction_DrawImage(this, bitmap, xCenter, yCenter, xLength, yLength, angleRotation, mirror));
            }
        }

        public void DrawLine(float x1, float y1, float x2, float y2, float thickness)
        {
            if (_draw)
            {
                SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Black,
                    StrokeWidth = thickness
                };

                Canvas.DrawLine(x1, y1, x2, y2, paint);
            }
            else
            {
                _functionsQueue.Add(new DrawFunction_DrawLine(this, x1, y1, x2, y2, thickness));
            }
        }

        public void DrawRectangle(float x1, float y1, float x2, float y2, float thicknessBorder, bool fill)
        {
            if (_draw)
            {
                SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColors.LightGray,
                    StrokeWidth = thicknessBorder,
                };

                SKRect rect = new SKRect(x1, y1, x2, y2);

                Canvas.DrawRect(rect, paint);
            }
            else
            {
                _functionsQueue.Add(new DrawFucntion_DrawRectangle(this, x1, y1, x2, y2, thicknessBorder, fill));
            }
        }

        public void DrawString(float xCenter, float yCenter, string str)
        {
            if (_draw)
            {
                SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Black,
                    StrokeWidth = 2,
                    TextSize = 20
                };

                Canvas.DrawText(str, xCenter, yCenter, paint);
            }
            else
            {
                _functionsQueue.Add(new DrawFuction_DrawString(this, xCenter, yCenter, str));
            }
        }
    }
}
