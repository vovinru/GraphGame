using System;
using System.Collections.Generic;
using System.Text;
using StandardEngineDraw;
using System.Drawing;
using Plugin.Settings;

namespace StandardGraphGame
{
    /// <summary>
    /// Точка выбора уровня
    /// </summary>
    public class LevelPoint : GameObject
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

        /// <summary>
        /// Проверяем есть ли данные для загрузки этого уровня
        /// </summary>
        /// <returns></returns>
        public bool IsData()
        {
            object ret = Properties.Resources.ResourceManager.GetObject(string.Format("level{0}", NumberLevel));

            return ret != null;
        }

        /// <summary>
        /// Открыт ли уровень для пользователя
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            string key = string.Format("OpenLevel{0}", NumberLevel);

            bool open = CrossSettings.Current.GetValueOrDefault(key, false);

            return open;
        }

        public override void Draw(IDrawEngine engine)
        {

            base.Draw(engine);

            engine.DrawImage(Properties.Resources.Level, 
                GetXLeft() * ConstantBase.SCALE_COEFICIENT, 
                GetYTop() * ConstantBase.SCALE_COEFICIENT, 
                XLength * ConstantBase.SCALE_COEFICIENT, 
                YLength * ConstantBase.SCALE_COEFICIENT, 
                0, false);

            if(!IsOpen())
            {
                engine.DrawImage(Properties.Resources.Lock,
                    GetXLeft() * ConstantBase.SCALE_COEFICIENT,
                    GetYTop() * ConstantBase.SCALE_COEFICIENT,
                    XLength * ConstantBase.SCALE_COEFICIENT,
                    YLength * ConstantBase.SCALE_COEFICIENT,
                    0, false);
            }

            engine.DrawString(
                GetXCenter() * ConstantBase.SCALE_COEFICIENT, 
                GetYCenter() * ConstantBase.SCALE_COEFICIENT, 
                NumberLevel.ToString());
        }

        #endregion
    }
}
