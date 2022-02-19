using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinGameGraph.EngineDraw
{
    public class DrawFucntion_DrawRectangle: DrawFunction
    {
        float X1
        {
            get;
            set;
        }

        float Y1
        {
            get;
            set;
        }

        float X2
        {
            get;
            set;
        }

        float Y2
        {
            get;
            set;
        }

        float ThicknessBorder
        {
            get;
            set;
        }

        bool Fill
        {
            get;
            set;
        }

        public DrawFucntion_DrawRectangle(XamarinEngineDraw engineDraw,
            float x1, float y1, float x2, float y2, float thicknessBorder, bool fill):
            base(engineDraw)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;

            ThicknessBorder = thicknessBorder;
            Fill = fill;
        }

        public override void Start()
        {
            EngineDraw.DrawRectangle(X1, Y1, X2, Y2, ThicknessBorder, Fill);
        }
    }
}
