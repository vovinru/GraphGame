using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardGraphGame;

namespace WpfGraphGame.ViewModels
{

    /// <summary>
    /// Вспомогательный класс пара количество тип
    /// </summary>
    public class WindowCreateCargoViewModel_CargoCount
    {
        public EnumCargo Type
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }
    }

    public class WindowCreateCargoViewModel
    {

        City _city;
        bool _goal;

        public List<WindowCreateCargoViewModel_CargoCount> CargoCountList
        {
            get;
            set;
        }

        public WindowCreateCargoViewModel(City city, bool goal)
        {
            _city = city;
            _goal = goal;

            CargoCountList = new List<WindowCreateCargoViewModel_CargoCount>();

            foreach (EnumCargo type in ConstantBase.ENUM_CARGO_TYPES_ALL)
            {
                WindowCreateCargoViewModel_CargoCount c = new WindowCreateCargoViewModel_CargoCount();
                c.Type = type;
                c.Count = goal ? city.GetCargosGoalCount(type) : city.GetCargosCount(type);
                CargoCountList.Add(c);
            }
        }


            
            

    }
}
