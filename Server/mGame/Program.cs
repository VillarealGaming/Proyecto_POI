using System;

namespace mGame {
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LevelState state = new LevelState(1, "Homero Simpson", "Bart Simpson");
            using (var game = new POIGame(state))
                game.Run();
        }
    }
#endif
}
