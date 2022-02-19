using System;
using System.Collections.Generic;
using System.Text;

namespace StandardGraphGameJSON
{

    public class CargoJSON : GameObjectJSON
    {
        #region propertyes

        /// <summary>
        /// Тип груза
        /// </summary>
        public int TypeCargo
        {
            get;
            set;
        }

        /// <summary>
        /// Если груз в городе, то здесь указывается город. Если null груз в транспорте
        /// </summary>
        public int? CityId
        {
            get;
            set;
        }

        /// <summary>
        /// Если груз в транспорте, то здесь указывается транспорт, Если null груз в городе
        /// </summary>
        public int? TransportId
        {
            get;
            set;
        }

        #endregion

    }
}
