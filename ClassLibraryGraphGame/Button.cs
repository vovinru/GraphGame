using ClassLibraryGraphGame.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardEngineDraw;

namespace ClassLibraryGraphGame
{
    public class Button:GameObject
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
            engine.DrawImage(Properties.Resources.Level, GetXLeft(), GetYTop(), XLength, YLength, 0, false);
            engine.DrawString(GetXLeft(), GetYTop(), Caption);
        }

        #endregion
    }
}
