using System.IO;

namespace PDF_conversion.src.logic
{
    public static class Logger
    {
        public static void Log(object message)
        {
            using (FileStream stream = new FileStream("log.txt", FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
                writer.WriteLine(message);
        }
    }
}
