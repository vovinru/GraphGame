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
    /// Объект груза, который надо перевозить
    /// </summary>
    public class Cargo:GameObject
    {
        #region fields

        /// <summary>
        /// Тип груза
        /// </summary>
        EnumCargo _typeCargo;

        /// <summary>
        /// Если груз в городе, то здесь указывается город. Если null груз в транспорте
        /// </summary>
        City _city;

        /// <summary>
        /// Если груз в транспорте, то здесь указывается транспорт, Если null груз в городе
        /// </summary>
        Transport _transport;

        #endregion

        #region propertyes

        /// <summary>
        /// Тип груза
        /// </summary>
        public EnumCargo TypeCargo
        {
            get
            {
                return _typeCargo;
            }
            set
            {
                _typeCargo = value;
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

        public Transport Transport
        {
            get
            {
                return _transport;
            }
            set
            {
                _transport = value;
            }
        }

        #endregion

        #region constructors

        public Cargo() 
            : base()
        {
            XLength = ConstantBase.X_CARGO_LENGTH;
            YLength = ConstantBase.Y_CARGO_LENGTH;
        }

        public Cargo(EnumCargo type)
            : this()
        {
            TypeCargo = type;
        }

        #endregion

        #region methods

        public override void Draw(IDrawEngine engine)
        {
            base.Draw(engine);
            engine.DrawImage(ConstantBase.CARGO_TYPE_IMAGE[TypeCargo], 
                GetXLeft(), GetYTop(), XLength, YLength, 0, false);
        }

        #endregion
    }
}
