using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessLayer.CppInvoke
{
    public class CPPDLL
    {
        [DllImport("CSharpInvokeCPP.CPPDemo.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Add(int x, int y);

        [DllImport("CSharpInvokeCPP.CPPDemo.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sub(int x, int y);
    }
}
