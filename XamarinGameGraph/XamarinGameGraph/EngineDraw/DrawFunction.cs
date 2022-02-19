using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XamarinGameGraph.EngineDraw
{
    public class DrawFunction
    {
        protected XamarinEngineDraw EngineDraw
        {
            get;
            set;
        }

        public DrawFunction(XamarinEngineDraw engineDraw)
        {
            EngineDraw = engineDraw;
        }

        public virtual void Start()
        {

        }
    }
}
