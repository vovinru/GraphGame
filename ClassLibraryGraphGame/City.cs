using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardEngineDraw;

namespace ClassLibraryGraphGame
{
    /// <summary>
    /// Класс города. В который будет поставка груза
    /// </summary>
    public class City:GameObject
    {
        #region fields

        /// <summary>
        /// Груз который находится в городе
        /// </summary>
        Dictionary<EnumCargo, List<Cargo>> _cargosCity;

        /// <summary>
        /// Цель города по грузу
        /// </summary>
        Dictionary<EnumCargo, int> _cargosGoal;

        /// <summary>
        /// Дороги города
        /// </summary>
        List<GraphRoad> _graphRoads;

        #endregion

        #region propertyes
        #endregion

        #region constructors

        public City()
            :base()
        {
            _cargosCity = new Dictionary<EnumCargo, List<Cargo>>();
            _cargosGoal = new Dictionary<EnumCargo, int>();
            _graphRoads = new List<GraphRoad>();

            XLength = ConstantBase.X_CITY_LENGTH;
            YLength = ConstantBase.Y_CITY_LENGTH;

            BitmapMain = ConstantBase.CARGO_TYPE_IMAGE[EnumCargo.apple];
        }

        #endregion

        #region methods

        /// <summary>
        /// Добавляет цель уровня, если такая цель уже существует то прибавляет количество
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        public void AddCargoGoal(EnumCargo type, int count)
        {
            if (!_cargosGoal.ContainsKey(type))
                _cargosGoal.Add(type, 0);

            _cargosGoal[type] += count;
        }

        public void RemoveAllGargoGoal()
        {
            _cargosGoal.Clear();
        }

        public void RemoveAllCargoGoal()
        {
            _cargosGoal.Clear();
        }

        /// <summary>
        /// Добавить груз в город
        /// </summary>
        /// <param name="cargo"></param>
        public void AddCargo(Cargo cargo)
        {
            if (!_cargosCity.ContainsKey(cargo.TypeCargo))
                _cargosCity.Add(cargo.TypeCargo, new List<Cargo>());

            _cargosCity[cargo.TypeCargo].Add(cargo);

            cargo.City = this;
            cargo.Transport = null;
        }

        /// <summary>
        /// Удалить груз из города
        /// </summary>
        /// <param name="cargo"></param>
        public void RemoveCargo(Cargo cargo)
        {
            if (!_cargosCity.ContainsKey(cargo.TypeCargo))
                return;

            _cargosCity[cargo.TypeCargo].Remove(cargo);
            if (_cargosCity[cargo.TypeCargo].Count == 0)
                _cargosCity.Remove(cargo.TypeCargo);

            cargo.City = null;
        }

        public void RemoveAllCargo()
        {
            _cargosCity.Clear();
        }

        /// <summary>
        /// Количество грузов определенного типа в городе
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetCargosCount(EnumCargo type)
        {
            if (_cargosCity.ContainsKey(type))
                return _cargosCity[type].Count;

            return 0;
        }

        /// <summary>
        /// Количество определенного груза цель
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetCargosGoalCount(EnumCargo type)
        {
            if (_cargosGoal.ContainsKey(type))
                return _cargosGoal[type];

            return 0;
        }

        public EnumCargo GetCargoTypeImage()
        {
            return ConstantBase.CARGO_TYPE_IMAGE.Keys.FirstOrDefault(key =>
                    ConstantBase.CARGO_TYPE_IMAGE[key] == BitmapMain);
        }

        public List<Cargo> GetCargos()
        {
            List<Cargo> cargos = new List<Cargo>();
            foreach(EnumCargo type in _cargosCity.Keys)
            {
                cargos.AddRange(_cargosCity[type]);
            }

            return cargos;
        }

        public Dictionary<EnumCargo, int> GetCargosGoal()
        {
            return _cargosGoal;
        }

        public bool CheckWin()
        {
            foreach(EnumCargo type in _cargosGoal.Keys)
            {
                if (_cargosGoal[type] > GetCargosCount(type))
                    return false;
            }

            return true;
        }

        public override void Draw(IDrawEngine engine)
        {
            base.Draw(engine);
            engine.DrawImage(BitmapMain, GetXLeft(), GetYTop(), XLength, YLength, 0, false);

            float xCargoCurrent = GetXRight();
            float yCargoCurrent = GetYTop();

            ///рисуем груз
            foreach(EnumCargo type in _cargosCity.Keys)
            {
                foreach(Cargo cargo in _cargosCity[type])
                {
                    cargo.XCoordinate = xCargoCurrent;
                    cargo.YCoordinate = yCargoCurrent + cargo.YLength;
                    cargo.Draw(engine);

                    xCargoCurrent += ConstantBase.X_INTERVAL_CARGO_CITY;
                }
            }
            ///---

            ///рисуем цель

            xCargoCurrent = GetXRight();
            yCargoCurrent = GetYBottom();
            float yCargoTextCurrent = yCargoCurrent - ConstantBase.GOAL_IMAGE_SIZE;
            foreach(EnumCargo type in _cargosGoal.Keys)
            {
                engine.DrawImage(ConstantBase.CARGO_TYPE_IMAGE[type], xCargoCurrent, yCargoCurrent,
                    ConstantBase.GOAL_IMAGE_SIZE, ConstantBase.GOAL_IMAGE_SIZE, 0, false);
                engine.DrawString(xCargoCurrent, yCargoTextCurrent, string.Format("{0}({1})", GetCargosCount(type), GetCargosGoalCount(type)));

                xCargoCurrent += ConstantBase.GOAL_IMAGE_SIZE;
            }

            ///---
        }

        public override string ToString()
        {
            return string.Format("{0}  X:{1} Y{2}", GetCargoTypeImage(), XCoordinate, YCoordinate);
        }

        #endregion
    }
}
