using System;
using System.Collections.Generic;
using System.Text;

namespace StandardGraphGameJSON
{

    public class TransportJSON : GameObjectJSON
    {
        /// <summary>
        /// вместимость транспорта
        /// </summary>
        public int Capacity
        {
            get;
            set;
        }

        /// <summary>
        /// Список грузов в транспорте
        /// </summary>
        public List<CargoJSON> Cargos
        {
            get;
            set;
        }

        /// <summary>
        /// Город где сейчас находится транспорт
        /// </summary>
        public int CityId
        {
            get;
            set;
        }

        public TransportJSON()
        {
            Cargos = new List<CargoJSON>();
        }
    }
}
