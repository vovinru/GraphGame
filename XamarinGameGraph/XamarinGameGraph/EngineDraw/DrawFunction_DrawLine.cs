using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinGameGraph.EngineDraw
{
    public class DrawFunction_DrawLine : DrawFunction
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

        float Thickness
        {
            get;
            set;
        }

        public DrawFunction_DrawLine(XamarinEngineDraw engineDraw, 
            float x1, float y1, float x2, float y2, float thickness): 
            base(engineDraw)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;

            Thickness = thickness;
        }

        public override void Start()
        {
            EngineDraw.DrawLine(X1, Y1, X2, Y2, Thickness);
        }
    }
}
