using System;
using System.Collections.Generic;
using System.Text;

namespace Device_Repair_Desk.Modles
{
     public abstract class RapairService   // class you can not create object from it but you can inherite
    {
        public abstract String GetServiceName();
    }
}
