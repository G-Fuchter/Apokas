using System;

namespace Apokas
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Apokas game = new Apokas())
            {
                game.Run();
            }
        }
    }
#endif
}

