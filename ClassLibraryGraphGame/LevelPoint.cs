using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using StandardEngineDraw;

namespace ClassLibraryGraphGame
{
    /// <summary>
    /// Точка выбора уровня
    /// </summary>
    public class LevelPoint:GameObject
    {
        #region fields

        int _numberLevel;

        #endregion

        #region propertyes

        /// <summary>
        /// Номер уровня
        /// </summary>
        public int NumberLevel
        {
            get => _numberLevel;
            set => _numberLevel = value;
        }

        #endregion

        #region methods

        public override void Draw(IDrawEngine engine)
        {

            base.Draw(engine);

            engine.DrawImage(Properties.Resources.Level, GetXLeft(), GetYTop(), XLength, YLength, 0, false);
            engine.DrawString(GetXCenter(), GetYCenter(), NumberLevel.ToString());
        }

        #endregion
    }
}
