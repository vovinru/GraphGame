using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardGraphGame;

namespace WpfGraphGame.ViewModels
{
    public class WindowCityCreateViewModel
    {
        #region fields

        GameGraph _game;
        City _city;

        #endregion

        #region propertyes

        public float CoordinateX
        {
            get => _city.XCoordinate;
            set => _city.XCoordinate = value;
        }

        public float CoordinateY
        {
            get => _city.YCoordinate;
            set => _city.YCoordinate = value;
        }

        public float LengthX
        {
            get => _city.XLength;
            set => _city.XLength = value;
        }


        public float LengthY
        {
            get => _city.YLength;
            set => _city.YLength = value;
        }

        public string CargosString
        {
            get
            {
                string ret = string.Empty;
                foreach(EnumCargo type in ConstantBase.ENUM_CARGO_TYPES_ALL)
                {
                    int count = _city.GetCargosCount(type);
                    ret += string.Format("{0} -{1}", type, count);
                    ret += "\n";
                }

                return ret;
            }
        }

        public string CargosGoalString
        {
            get
            {
                string ret = string.Empty;
                foreach (EnumCargo type in ConstantBase.ENUM_CARGO_TYPES_ALL)
                {
                    int count = _city.GetCargosGoalCount(type);
                    ret += string.Format("{0} -{1}", type, count);
                    ret += "\n";
                }

                return ret;
            }
        }

        public List<EnumCargo> CargoTypeEnums
        {
            get
            {
                return ConstantBase.ENUM_CARGO_TYPES_ALL;
            }
        }

        public EnumCargo SelectedCargoEnum
        {
            get
            {
                return _city.GetCargoTypeImage();
            }
            set
            {
                _city.BitmapMain = ConstantBase.CARGO_TYPE_IMAGE[value];
            }
        }

        #endregion

        #region constructors

        public WindowCityCreateViewModel(GameGraph game, City city)
        {
            _game = game;
            _city = city;
        }

        #endregion

        #region methods
        #endregion
    }
}
