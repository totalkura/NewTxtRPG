namespace NewTxtRPG.etc
{
    internal static class RenderConsole
    {
        /// <summary>
        /// 한 줄 띄우기와 함께 메시지를 출력합니다.
        /// </summary>
        public static void WriteLineWithSpacing(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ResetColor();
        }

        /// <summary>
        /// 색상에 맞는 메시지를 출력합니다.
        /// </summary>
        public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// 색상에 맞는 메시지를 출력합니다. 줄바꿈 없이 출력합니다.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>
        /// 콘솔에 빈 줄을 출력합니다.
        /// </summary>
        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }
    }
}
