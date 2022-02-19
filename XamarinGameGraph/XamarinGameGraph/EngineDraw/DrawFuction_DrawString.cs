using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinGameGraph.EngineDraw
{
    public class DrawFuction_DrawString:DrawFunction
    {
        float XCenter
        {
            get;
            set;
        }

        float YCenter
        {
            get;
            set;
        }

        string Str
        {
            get;
            set;
        }

        public DrawFuction_DrawString(XamarinEngineDraw engineDraw, 
            float xCenter, float yCenter, string str):
            base(engineDraw)
        {
            EngineDraw = engineDraw;

            XCenter = xCenter;
            YCenter = yCenter;
            Str = str;
        }

        public override void Start()
        {
            EngineDraw.DrawString(XCenter, YCenter, Str);
        }
    }
}
