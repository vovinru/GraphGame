using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardEngineDraw;

namespace ClassLibraryGraphGame
{
    public class GraphRoad:GameObject
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

        public GraphRoad():
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

            engine.DrawLine(x1, y1, x2, y2, 3);

            XCoordinate = (x1 + x2) / 2;
            YCoordinate = (y1 + y2) / 2;

            engine.DrawString(XCoordinate, YCoordinate, _countPermittedPasses.ToString());
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", City1, City2);
        }

        #endregion
    }
}
