using System;
using System.Collections.Generic;
using System.Text;
using StandardEngineDraw;

namespace StandardGraphGame
{

    public class GraphRoad : GameObject
    {
        #region fields

        /// <summary>
        /// Первый город дороги
        /// </summary>
        City _city1;

        /// <summary>
        /// Второй город дороги
        /// </summary>
        City _city2;

        /// <summary>
        /// Количество разрешенных проездов
        /// </summary>
        int _countPermittedPasses;

        #endregion

        #region propertyes

        /// <summary>
        /// Первый город дороги
        /// </summary>
        public City City1
        {
            get
            {
                return _city1;
            }
            set
            {
                _city1 = value;
            }
        }

        /// <summary>
        /// Второй город дороги
        /// </summary>
        public City City2
        {
            get
            {
                return _city2;
            }
            set
            {
                _city2 = value;
            }
        }

        /// <summary>
        /// Количество разрешенных проездов
        /// </summary>
        public int CountPermittedPasses
        {
            get
            {
                return _countPermittedPasses;
            }
            set
            {
                _countPermittedPasses = value;

                if (value == 0)
                {
                    XLength = 0;
                    YLength = 0;
                }
            }
        }

        #endregion

        #region constructors

        public GraphRoad() :
            base()
        {
            XLength = 40;
            YLength = 40;
        }

        #endregion

        #region methods

        public override void Draw(IDrawEngine engine)
        {
            base.Draw(engine);

            if (CountPermittedPasses == 0)
                return;

            float x1 = City1.GetXCenter();
            float y1 = City1.GetYCenter();

            float x2 = City2.GetXCenter();
            float y2 = City2.GetYCenter();

            engine.DrawLine(
                x1 * ConstantBase.SCALE_COEFICIENT,
                y1 * ConstantBase.SCALE_COEFICIENT,
                x2 * ConstantBase.SCALE_COEFICIENT,
                y2 * ConstantBase.SCALE_COEFICIENT,
                3 * ConstantBase.SCALE_COEFICIENT);
            
            XCoordinate = (x1 + x2) / 2;
            YCoordinate = (y1 + y2) / 2;

            engine.DrawRectangle(
                (XCoordinate - 10) * ConstantBase.SCALE_COEFICIENT, 
                (YCoordinate - 10) * ConstantBase.SCALE_COEFICIENT, 
                (XCoordinate + 30) * ConstantBase.SCALE_COEFICIENT, 
                (YCoordinate + 40) * ConstantBase.SCALE_COEFICIENT,
                3 * ConstantBase.SCALE_COEFICIENT, true);

            engine.DrawString(
                XCoordinate * ConstantBase.SCALE_COEFICIENT,
                YCoordinate * ConstantBase.SCALE_COEFICIENT, _countPermittedPasses.ToString());
        }

        public override bool CheckPoint(float x, float y)
        {
            x = x / ConstantBase.SCALE_COEFICIENT;
            y = y / ConstantBase.SCALE_COEFICIENT;

            if (x > XCoordinate - 10 && x < XCoordinate + 30)
                if (y > YCoordinate - 10 && y < YCoordinate + 40)
                    return true;

            return false;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", City1, City2);
        }

        #endregion
    }
}
