using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGraphGameJSON
{
    public class RoadJSON:GameObjectJSON
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
