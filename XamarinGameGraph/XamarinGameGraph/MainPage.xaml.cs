using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using StandardGraphGame;
using XamarinGameGraph.EngineDraw;

namespace XamarinGameGraph
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        XamarinEngineDraw _engineDraw;
        GameGraph _game;

        SKCanvasView _canvasView;

        public MainPage()
        {
            InitializeComponent();

            _canvasView = new SKCanvasView();
            _canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = _canvasView;


            _canvasView.EnableTouchEvents = true;
            _canvasView.Touch += CanvasView_Touch;

        }

        private void CanvasView_Touch(object sender, SKTouchEventArgs e)
        {
            _game.Click(e.Location.X, e.Location.Y);
            _canvasView.InvalidateSurface();
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;


            ConstantBase.X_LENGTH_GAME = info.Width;
            ConstantBase.Y_LENGTH_GAME = info.Height;

            ConstantBase.SCALE_COEFICIENT =
                Math.Min((float)ConstantBase.X_LENGTH_GAME / (float)ConstantBase.X_LENGTH_GAME_BASE,
                            (float)ConstantBase.Y_LENGTH_GAME / (float)ConstantBase.Y_LENGTH_GAME_BASE);

            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            if (_engineDraw == null)
            {
                _engineDraw = new XamarinEngineDraw(canvas);
                
                _game = new GameGraph(_engineDraw, LoadLevel, false);
                _game.ReDraw();
            }

            _engineDraw.ReDraw(canvas);

        }

        private void LoadLevel(int numberLevel)
        {
            _game.ClearLevel();
            _game.LoadLevel(numberLevel);
            _game.StartLevel();
            _game.ReDraw();

        }

    }
}
