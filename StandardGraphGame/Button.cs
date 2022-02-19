using System;
using System.Collections.Generic;
using System.Text;
using StandardEngineDraw;
using System.Drawing;

namespace StandardGraphGame
{
    public class Button : GameObject
    {
        #region fields

        string _caption;

        #endregion

        #region propertyes

        /// <summary>
        /// Номер уровня
        /// </summary>
        public string Caption
        {
            get => _caption;
            set => _caption = value;
        }

        #endregion

        #region methods

        public override void Draw(IDrawEngine engine)
        {
            base.Draw(engine);
            engine.DrawImage(Properties.Resources.Level, 
                GetXLeft() * ConstantBase.SCALE_COEFICIENT, 
                GetYTop() * ConstantBase.SCALE_COEFICIENT,
                XLength * ConstantBase.SCALE_COEFICIENT,
                YLength * ConstantBase.SCALE_COEFICIENT, 0, false);

            engine.DrawString(
                GetXLeft() * ConstantBase.SCALE_COEFICIENT, 
                GetYTop() * ConstantBase.SCALE_COEFICIENT, Caption);
        }

        #endregion
    }
}
