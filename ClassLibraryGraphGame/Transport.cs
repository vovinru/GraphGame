using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardEngineDraw;

namespace ClassLibraryGraphGame
{
    /// <summary>
    /// Класс грузовика который будет груз перевозить
    /// </summary>
    public class Transport:GameObject
    {
        #region fields

        /// <summary>
        /// вместимость транспорта
        /// </summary>
        int _capacity;

        /// <summary>
        /// Список грузов в транспорте
        /// </summary>
        List<Cargo> _cargos;

        /// <summary>
        /// Город где сейчас находится транспорт
        /// </summary>
        City _city;

        #endregion

        #region propertyes

        /// <summary>
        /// вместимость транспорта
        /// </summary>
        public int Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                _capacity = value;
            }
        }

        public City City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }

        public List<Cargo> Cargos
        {
            get
            {
                return _cargos;
            }
            set
            {
                _cargos = value;
            }
        }

        #endregion

        #region constructors

        public Transport() :
            base()
        {
            XLength = 30;
            YLength = 30;

            Cargos = new List<Cargo>();

            Capacity = 1;
        }

        #endregion

        #region methods

        /// <summary>
        /// Добавляем в грузовик груз
        /// </summary>
        /// <param name="cargo"></param>
        public void AddCargo(Cargo cargo)
        {
            _cargos.Add(cargo);
            cargo.City = null;
            cargo.Transport = this;
        }

        /// <summary>
        /// Удалить с траспорта весь груз
        /// </summary>
        public void RemoveAllCargo()
        {
            Cargos.ForEach(c => c.Transport = null);
            Cargos.Clear();
        }

        /// <summary>
        /// Движение транспорта по дороге
        /// </summary>
        /// <param name="road"></param>
        public void DriveRoad(GraphRoad road)
        {
            if (City == road.City1)
                City = road.City2;
            else
                City = road.City1;

            road.CountPermittedPasses--;
            
        }

        /// <summary>
        /// Есть ли в грузовике место для погрузки
        /// </summary>
        /// <returns></returns>
        public bool GetIsFree()
        {
            return Capacity > Cargos.Count;
        }

        public override void Draw(IDrawEngine engine)
        {
            base.Draw(engine);
            XCoordinate = City.GetXCenter();
            YCoordinate = City.GetYCenter();
            engine.DrawImage(Properties.Resources.Trunk,
                GetXLeft() , GetYTop(), XLength, YLength, 0, false);

            float xCargoCurrent = XCoordinate;
            foreach (Cargo cargo in _cargos)
            {
                cargo.XCoordinate = xCargoCurrent;
                cargo.YCoordinate = YCoordinate + cargo.YLength;
                cargo.Draw(engine);

                xCargoCurrent += cargo.XLength * 1.5f;
            }
        }

        #endregion
    }
}
