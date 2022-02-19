using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StandardEngineDraw;

namespace StandardGraphGame
{
    /// <summary>
    /// Класс города. В который будет поставка груза
    /// </summary>
    public class City : GameObject
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
            : base()
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
        /// Возвращает количество груза в городе
        /// </summary>
        /// <returns></returns>
        public int GetCargosCount()
        {
            return _cargosCity.Values.Sum(v => v.Count);
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

        public int GetCargosGoalCount()
        {
            return _cargosGoal.Values.Sum();
        }

        public EnumCargo GetCargoTypeImage()
        {
            return ConstantBase.CARGO_TYPE_IMAGE.Keys.FirstOrDefault(key =>
                    ConstantBase.CARGO_TYPE_IMAGE[key] == BitmapMain);
        }

        public List<Cargo> GetCargos()
        {
            List<Cargo> cargos = new List<Cargo>();
            foreach (EnumCargo type in _cargosCity.Keys)
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
            foreach (EnumCargo type in _cargosGoal.Keys)
            {
                if (_cargosGoal[type] > GetCargosCount(type))
                    return false;
            }

            return true;
        }

        public override void Draw(IDrawEngine engine)
        {
            base.Draw(engine);
            engine.DrawImage(Properties.Resources.City, 
                GetXLeft() * ConstantBase.SCALE_COEFICIENT, 
                GetYTop() * ConstantBase.SCALE_COEFICIENT,
                XLength * ConstantBase.SCALE_COEFICIENT,
                YLength * ConstantBase.SCALE_COEFICIENT, 0, false);

            float xCargoCurrent = GetXCenter() -
                (ConstantBase.X_CARGO_LENGTH / 2 * GetCargosCount()) -
                (ConstantBase.X_INTERVAL_CARGO_CITY / 2 * GetCargosCount() - 1);
            float yCargoCurrent = GetYCenter() - 5 * ConstantBase.Y_CARGO_LENGTH / 2;

            ///рисуем груз
            foreach (EnumCargo type in _cargosCity.Keys)
            {
                foreach (Cargo cargo in _cargosCity[type])
                {
                    cargo.XCoordinate = xCargoCurrent;
                    cargo.YCoordinate = yCargoCurrent;
                    cargo.Draw(engine);

                    xCargoCurrent += ConstantBase.X_INTERVAL_CARGO_CITY + ConstantBase.X_CARGO_LENGTH;
                }
            }
            ///---

            ///рисуем цель

            xCargoCurrent = GetXCenter() -
                (ConstantBase.GOAL_IMAGE_SIZE / 2 * GetCargosGoalCount()) -
                (ConstantBase.X_INTERVAL_CARGO_CITY / 2 * GetCargosGoalCount() - 1);
            yCargoCurrent = GetYCenter() + 3 * ConstantBase.GOAL_IMAGE_SIZE / 2;

            //float yCargoTextCurrent = yCargoCurrent - ConstantBase.GOAL_IMAGE_SIZE;
            foreach (EnumCargo type in _cargosGoal.Keys)
            {
                for (int i = 0; i < _cargosGoal[type]; i++)
                {
                    engine.DrawImage(ConstantBase.CARGO_TYPE_IMAGE[type],
                        xCargoCurrent * ConstantBase.SCALE_COEFICIENT,
                        yCargoCurrent * ConstantBase.SCALE_COEFICIENT,
                        ConstantBase.GOAL_IMAGE_SIZE * ConstantBase.SCALE_COEFICIENT, 
                        ConstantBase.GOAL_IMAGE_SIZE * ConstantBase.SCALE_COEFICIENT, 0, false);
                    //engine.DrawString(xCargoCurrent, yCargoTextCurrent, string.Format("{0}({1})", GetCargosCount(type), GetCargosGoalCount(type)));

                    xCargoCurrent += ConstantBase.X_INTERVAL_CARGO_CITY + ConstantBase.GOAL_IMAGE_SIZE;
                }
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
