using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.PTO.Ex.ExMsg
{
    public interface IExMsgPTO:IExMsgTPTO
    {
        Int32 IdExMsg { get; set; }
        String IdTEx { get; set; }
        Int32 IdStaRg { get; set; }
    }
}
