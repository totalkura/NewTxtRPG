using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STDungeon
{
    internal static class RenderConsole
    {
        /// <summary>
        /// 한 줄 띄우기와 함께 메시지를 출력합니다.
        /// </summary>
        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }

        /// <summary>
        /// 한 줄 띄우기만 출력합니다.
        /// </summary>
        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }
    }
}
