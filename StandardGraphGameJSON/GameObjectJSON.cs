using System;
using System.Collections.Generic;
using System.Text;

namespace StandardGraphGameJSON
{

    public class GameObjectJSON
    {
        #region propertyes

        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Координата по X (центр)
        /// </summary>
        public float XCoordinate
        {
            get;
            set;
        }

        /// <summary>
        /// Координата по Y (центр)
        /// </summary>
        public float YCoordinate
        {
            get;
            set;
        }

        /// <summary>
        /// Длина по X
        /// </summary>
        public float XLength
        {
            get;
            set;
        }

        /// <summary>
        /// Длина по Y
        /// </summary>
        public float YLength
        {
            get;
            set;
        }

        #endregion
    }
}
