using System;
using System.Collections.Generic;
using System.Text;

namespace StandardGraphGameJSON
{

    public class RoadJSON : GameObjectJSON
    {
        #region

        /// <summary>
        /// Первый город дороги
        /// </summary>
        public int City1Id
        {
            get;
            set;
        }

        /// <summary>
        /// Второй город дороги
        /// </summary>
        public int City2Id
        {
            get;
            set;
        }

        /// <summary>
        /// Количество разрешенных проездов
        /// </summary>
        public int CountPermittedPasses
        {
            get;
            set;
        }

        #endregion
    }
}
