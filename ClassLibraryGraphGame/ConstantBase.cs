using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGraphGame
{
    public static class ConstantBase
    {
        /// <summary>
        /// Размер отрисовки целей города
        /// </summary>
        public static float GOAL_IMAGE_SIZE = 25;

        /// <summary>
        /// Соответсвие картинок и типов груза
        /// </summary>
        public static Dictionary<EnumCargo, Bitmap> CARGO_TYPE_IMAGE = new Dictionary<EnumCargo, Bitmap>
        {
            { EnumCargo.apple, Properties.Resources.Apple },
            { EnumCargo.cherry, Properties.Resources.Cherry },
            { EnumCargo.orange, Properties.Resources.Orange },
            { EnumCargo.peach, Properties.Resources.Peach },
            { EnumCargo.plum, Properties.Resources.Plum }
        };

        public static List<EnumCargo> ENUM_CARGO_TYPES_ALL = new List<EnumCargo>()
        {
            EnumCargo.apple,
            EnumCargo.cherry,
            EnumCargo.orange,
            EnumCargo.peach,
            EnumCargo.plum
        };

        public static string LEVEL_FILE_NAME = @"C:\Me\Programming\GraphGame\GraphGame\Levels\Level";

        public static Bitmap LEVEL_POINT_IMAGE = Properties.Resources.Level;
        public static int X_LENGTH_LEVEL_POINT = 50;
        public static int Y_LENGTH_LEVEL_POINT = 50;
        public static int X_INTERVAL_BEETWEEN_LEVEL_POINT = 10;
        public static int Y_INTERVAL_BEETWEEN_LEVEL_POINT = 10;

        public static int X_LENGTH_END_LEVEL_BUTTON = 200;
        public static int Y_LENGTH_END_LEVEL_BUTTON = 50;
        public static int Y_INTERVAL_BEETWEEN_END_LEVEL_BUTTONS = 10;
        public static int Y_START_BUTTONS_END = 50;

        public static int X_CARGO_LENGTH = 20;
        public static int Y_CARGO_LENGTH = 20;
        public static int X_INTERVAL_CARGO_CITY = 10;

        public static int X_CITY_LENGTH = 200;
        public static int Y_CITY_LENGTH = 200;

    }
}
