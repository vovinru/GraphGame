using System;
using System.Drawing;

namespace StandardEngineDraw
{
    public interface IDrawEngine
    {
        void DrawLine(float x1, float y1, float x2, float y2, float thickness);
        void DrawImage(byte[] bitmap, float xCenter, float yCenter, float xLength, float yLength, float angleRotation, bool mirror);
        void DrawRectangle(float x1, float y1, float x2, float y2, float thicknessBorder, bool fill);
        void DrawString(float xCenter, float yCenter, string str);

        void Clear();
    }
}
