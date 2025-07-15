namespace NewTxtRPG.etc
{
    internal static class RenderConsole
    {
        /// <summary>
        /// 한 줄 띄우기와 함께 메시지를 출력합니다.
        /// </summary>
        public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public static void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }
    }
}
