using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardEngineDraw;

using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;
using System.Drawing;
using Image = System.Windows.Controls.Image;
using System.IO;
using System.Security;
using System.Windows.Interop;
using System.Windows.Media;
using Brushes = System.Drawing.Brushes;

namespace WpfEngineDraw
{
    public class WpfEngineDraw : IDrawEngine
    {
        #region fields

        Canvas _canvas;

        #endregion

        #region constructors

        public WpfEngineDraw(Canvas canvas)
        {
            _canvas = canvas;
        }

        #endregion

        #region methods

        public void DrawImage(byte[] bitmap, float xCenter, float yCenter, float xLength, float yLength, float angleRotation, bool mirror)
        {
            MemoryStream ms = new MemoryStream(bitmap);
            
            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();

            Image image = new Image
            {
                Source = bitmapImage,
                Width = xLength,
                Height = yLength,
                Stretch = Stretch.Fill
            };

            Canvas.SetLeft(image, xCenter);
            Canvas.SetTop(image, yCenter);

            _canvas.Children.Add(image);
            
        }

        public void DrawLine(float x1, float y1, float x2, float y2, float thickness)
        {
            Line line = new Line();
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;
            line.StrokeThickness = thickness;
            line.Stroke = System.Windows.SystemColors.ActiveBorderBrush;

            _canvas.Children.Add(line);
        }

        public void DrawRectangle(float x1, float y1, float x2, float y2, float thicknessBorder, bool fill)
        {
            if (fill)
            {
                System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle();
                rectangle.Fill = System.Windows.Media.Brushes.LightGray;
                rectangle.Height = y2 - y1;
                rectangle.Width = x2 - x1;

                Canvas.SetLeft(rectangle, x1);
                Canvas.SetTop(rectangle, y1);
                _canvas.Children.Add(rectangle);
            }
            else
            {
                DrawLine(x1, y1, x2, y1, thicknessBorder);
                DrawLine(x2, y1, x2, y2, thicknessBorder);
                DrawLine(x2, y2, x1, y2, thicknessBorder);
                DrawLine(x1, y2, x1, y1, thicknessBorder);
            }

        }

        public void DrawString(float xCenter, float yCenter, string str)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = str;
            Canvas.SetLeft(textBlock, xCenter);
            Canvas.SetTop(textBlock, yCenter);
            _canvas.Children.Add(textBlock);
        }

        public void Clear()
        {
            _canvas.Children.Clear();
        }

        #endregion
    }
}
