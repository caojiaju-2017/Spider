using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetConfig.DbAccess.DataStruct
{
    public enum LoopType
    {
        Loop,
        Single
    }

    public class CalcWay
    {
        public static string Kick = "00";
        public static string PreAppend = "10";
        public static string Append = "11";
        public static string Raw = "99";
    }
}
