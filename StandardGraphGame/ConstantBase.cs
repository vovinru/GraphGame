using System;
using System.Collections.Generic;
using System.Text;

namespace StandardGraphGame
{
    public static class ConstantBase
    {

        /// <summary>
        /// Соответсвие картинок и типов груза
        /// </summary>
        public static Dictionary<EnumCargo, byte[]> cARGO_TYPE_IMAGE = new Dictionary<EnumCargo, byte[]>
        {
            { EnumCargo.apple, Properties.Resources.Apple },
            { EnumCargo.cherry, Properties.Resources.Cherry  },
            { EnumCargo.orange, Properties.Resources.Orange  },
            { EnumCargo.peach, Properties.Resources.Peach  },
            { EnumCargo.plum, Properties.Resources.Plum  },
            { EnumCargo.watermelon, Properties.Resources.Watermelon },
            { EnumCargo.pear, Properties.Resources.Pear }
        };

        public static List<EnumCargo> ENUM_CARGO_TYPES_ALL = new List<EnumCargo>()
        {
            EnumCargo.apple,
            EnumCargo.cherry,
            EnumCargo.orange,
            EnumCargo.peach,
            EnumCargo.plum,
            EnumCargo.watermelon,
            EnumCargo.pear
        };

        public static string LEVEL_FILE_NAME = @"C:\Me\Programming\GraphGame\GraphGame\Levels\Level";

        //Базовые параметры ширины и длины объектов
        public static int X_LENGTH_GAME_BASE = 720;
        public static int Y_LENGTH_GAME_BASE = 1394;
        //---

        //Показатели текущего поля игрового
        public static int X_LENGTH_GAME = 350;
        public static int Y_LENGTH_GAME = 700;
        //---

        //Коэффициент масштаба
        public static float SCALE_COEFICIENT =
            Math.Min( (float)X_LENGTH_GAME / (float)X_LENGTH_GAME_BASE, 
                (float)Y_LENGTH_GAME / (float)Y_LENGTH_GAME_BASE);
        //---

        //Параметры расстановки уровней
        public static int X_COUNT_LEVEL_ROW = 4;
        public static int X_COUNT_LEVEL = 20;

        public static int X_LENGTH_END_LEVEL_BUTTON = 200;
        public static int Y_LENGTH_END_LEVEL_BUTTON = 50;
        public static int Y_INTERVAL_BEETWEEN_END_LEVEL_BUTTONS = 10;
        public static int Y_START_BUTTONS_END = 50;

        public static int X_CARGO_LENGTH = 40;
        public static int Y_CARGO_LENGTH = 40;
        public static int X_INTERVAL_CARGO_CITY = 10;        
        /// <summary>
        /// Размер отрисовки целей города
        /// </summary>
        public static float GOAL_IMAGE_SIZE = 30;

        public static int X_CITY_LENGTH = 200;
        public static int Y_CITY_LENGTH = 200;

        public static int X_LENGTH_TRANSPORT = 60;
        public static int Y_LENGTH_TRANSPORT = 60;

        public static Dictionary<EnumCargo, byte[]> CARGO_TYPE_IMAGE { get => cARGO_TYPE_IMAGE; set => cARGO_TYPE_IMAGE = value; }
    }
}
