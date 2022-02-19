using System;
using System.Collections.Generic;
using System.Text;

namespace StandardGraphGameJSON
{

    public class CityJSON : GameObjectJSON
    {
        #region propertyes

        public List<CargoJSON> Cargos
        {
            get;
            set;
        }

        /// <summary>
        /// цель грузов для уровня. Ключ это тип груза, значение это количество
        /// </summary>
        public Dictionary<int, int> CagrosGoalJSON
        {
            get;
            set;
        }

        public List<int> GraphRoadIds
        {
            get;
            set;
        }

        #endregion

        #region constructors

        public CityJSON()
        {
            Cargos = new List<CargoJSON>();
            CagrosGoalJSON = new Dictionary<int, int>();
        }

        #endregion
    }
}
