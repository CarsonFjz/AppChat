using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Utils.IDCreate
{
    public partial class Generator
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        public static extern int UuidCreateSequential(out Guid guid);
        private const int RPC_S_OK = 0;
        /// <summary>
        /// 顺序结构的GUID
        /// </summary>
        /// <returns></returns>
        public static Guid CreateRpcrt4Guid()
        {
            Guid guid;
            int result = UuidCreateSequential(out guid);
            if (result == RPC_S_OK)
            {
                byte[] guidBytes = guid.ToByteArray();
                Array.Reverse(guidBytes, 0, 4);
                Array.Reverse(guidBytes, 4, 2);
                Array.Reverse(guidBytes, 6, 2);

                return new Guid(guidBytes);
            }
            else
                return Guid.NewGuid();
        }
    }
}
