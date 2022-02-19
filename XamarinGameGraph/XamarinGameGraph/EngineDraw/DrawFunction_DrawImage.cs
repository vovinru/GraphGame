using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinGameGraph.EngineDraw
{
    public class DrawFunction_DrawImage:DrawFunction
    {
        byte[] Bitmap
        {
            get;
            set;
        }

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

        float XLength
        {
            get;
            set;
        }

        float YLength
        {
            get;
            set;
        }

        float AngleRotation
        {
            get;
            set;
        }

        bool Mirror
        {
            get;
            set;
        }

        public DrawFunction_DrawImage(XamarinEngineDraw engineDraw, 
            byte[] bitmap, float xCenter, float yCenter, float xLength, float yLength, float angleRotation, bool mirror):
            base(engineDraw)
        {
            Bitmap = bitmap;
            XCenter = xCenter;
            YCenter = yCenter;
            XLength = xLength;
            YLength = yLength;
            AngleRotation = angleRotation;
            Mirror = mirror;
        }

        public override void Start()
        {
            EngineDraw.DrawImage(Bitmap, XCenter, YCenter, XLength, YLength, AngleRotation, Mirror);
        }
    }
}
