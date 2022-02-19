using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGraphGameJSON
{
    //
    public class GameGraphJSON
    {
        #region propertyes

        /// <summary>
        /// Города
        /// </summary>
        public List<CityJSON> Citys
        {
            get;
            set;
        }

        /// <summary>
        /// Дороги
        /// </summary>
        public List<RoadJSON> Roads
        {
            get;
            set;
        }

        /// <summary>
        /// Транспорт
        /// </summary>
        public List<TransportJSON> Transports
        {
            get;
            set;
        }

        #endregion

        #region constructors

        public GameGraphJSON()
        {
            Citys = new List<CityJSON>();
            Roads = new List<RoadJSON>();
            Transports = new List<TransportJSON>();
        }

        #endregion

    }
}
