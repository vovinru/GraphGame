using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using StandardEngineDraw;

namespace StandardGraphGame
{
    /// <summary>
    /// Класс базовых параметров игрового объекта
    /// </summary>
    public class GameObject
    {
        #region fields

        public static int NEXT_ID = 1;

        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        protected int _id;

        /// <summary>
        /// Координата по X
        /// </summary>
        protected float _xCoordinate;

        /// <summary>
        /// Координата по Y
        /// </summary>
        protected float _yCoordinate;

        /// <summary>
        /// Файл Картинка для отображения 
        /// </summary>
        protected byte[] _bitmapMain;

        /// <summary>
        /// Длина по X
        /// </summary>
        protected float _xLength;

        /// <summary>
        /// Длина по Y
        /// </summary>
        protected float _yLength;

        #endregion

        #region propertyes

        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Координата по X (по левому краю)
        /// </summary>
        public float XCoordinate
        {
            get
            {
                return _xCoordinate;
            }
            set
            {
                _xCoordinate = value;
            }
        }

        /// <summary>
        /// Координата по Y (вверху)
        /// </summary>
        public float YCoordinate
        {
            get
            {
                return _yCoordinate;
            }
            set
            {
                _yCoordinate = value;
            }
        }

        /// <summary>
        /// Длина по X
        /// </summary>
        public float XLength
        {
            get
            {
                return _xLength;
            }
            set
            {
                _xLength = value;
            }
        }

        /// <summary>
        /// Длина по Y
        /// </summary>
        public float YLength
        {
            get
            {
                return _yLength;
            }
            set
            {
                _yLength = value;
            }
        }


        /// <summary>
        /// Файл Картинка для отображения 
        /// </summary>
        public byte[] BitmapMain
        {
            get
            {
                return _bitmapMain;
            }
            set
            {
                _bitmapMain = value;
            }
        }

        #endregion

        #region constructors

        public GameObject()
        {
            Id = NEXT_ID;
            NEXT_ID++;
        }

        #endregion

        #region methods

        /// <summary>
        /// Получить координату X левого угла
        /// </summary>
        /// <returns></returns>
        public float GetXLeft()
        {
            return XCoordinate;
        }

        /// <summary>
        /// Получить координату X правого угла
        /// </summary>
        /// <returns></returns>
        public float GetXRight()
        {
            return XCoordinate + XLength;
        }

        /// <summary>
        /// Получить координату Y верхнего угла
        /// </summary>
        /// <returns></returns>
        public float GetYTop()
        {
            return YCoordinate;
        }

        /// <summary>
        /// Получить координату Y нижнего угла
        /// </summary>
        /// <returns></returns>
        public float GetYBottom()
        {
            return YCoordinate + YLength;
        }

        public float GetXCenter()
        {
            return XCoordinate + XLength / 2;
        }

        public float GetYCenter()
        {
            return YCoordinate + YLength / 2;
        }

        /// <summary>
        /// Проверка на то, что точка находится внутри объекта
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual bool CheckPoint(float x, float y)
        {
            x = x / ConstantBase.SCALE_COEFICIENT;
            y = y / ConstantBase.SCALE_COEFICIENT;

            if (x >= GetXLeft() && x <= GetXRight() && y >= GetYTop() && y <= GetYBottom())
                return true;

            return false;
        }

        /// <summary>
        /// Базовый метод отрисовки для игровых объектов
        /// </summary>
        public virtual void Draw(IDrawEngine engine)
        {
        }

        #endregion

    }
}
