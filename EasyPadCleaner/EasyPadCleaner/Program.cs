using System.Runtime.InteropServices;

namespace EasyPadCleaner {
   internal static class Program {
      public static bool silent = false;
      private static readonly string
         easy_pad_Data = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Easy_Pad_Data"),
         easy_pad_themes = Path.Combine(easy_pad_Data, @"EasyPad_Themes.txt"),
         easy_pad_recent = Path.Combine(easy_pad_Data, @"EasyPad_Recent_File_History.txt");
      private static readonly uint gTimedMessageBoxFlags = /*MB_OK*/ 0x00000000 | /*MB_SETFOREGROUND*/  0x00010000 |
         /*MB_SYSTEMMODAL*/ 0x00001000 | (uint)MessageBoxIcon.Stop;

      [DllImport("user32.dll", CharSet = CharSet.Unicode)]
      public static extern int MessageBoxTimeout(IntPtr hWnd, string lpText, string lpCaption,
         uint uType, short wLanguageId, int dwMilliseconds);

      [STAThread]
      static void Main(string[] args) {
         if (args.Length == 1) {
            silent = true;
            if (string.Equals(args[0], "all", StringComparison.OrdinalIgnoreCase)) {
               CleanCache();
               CleanThemes();
               CleanRecent();
            }
            else if (args[0].Contains("cache", StringComparison.OrdinalIgnoreCase))
               CleanCache();
            else if (args[0].Contains("themes", StringComparison.OrdinalIgnoreCase))
               CleanThemes();
            else if (args[0].Contains("recent", StringComparison.OrdinalIgnoreCase))
               CleanRecent();
         }
         else {
            ApplicationConfiguration.Initialize();
            Application.Run(new EasyPadCleanerForm());
         }
      }

      public static void CleanCache() {
         try {
            string cache = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                  @"EFM5");
            if (Directory.Exists(cache)) {
               string[] folders = Directory.GetDirectories(cache);
               foreach (string folder in folders) {
                  if (folder.Contains("EasyPad"))
                     Directory.Delete(folder, true);
               }
            }
            if (!silent)
               _ = MessageBoxTimeout(IntPtr.Zero, "The cache was cleaned.", "Success", gTimedMessageBoxFlags,
                  0, 2000);
         }
         catch (Exception pException) {
            _ = MessageBoxTimeout(IntPtr.Zero, string.Format(
               "Cleaning the cache failed. {0}The error was: {0} {1}", Environment.NewLine, pException.ToString()),
               "ERROR", gTimedMessageBoxFlags, 0, 0);
         }
      }

      public static void CleanRecent() {
         try {
            if (Directory.Exists(easy_pad_Data)) {
               if (File.Exists(easy_pad_recent))
                  File.Delete(easy_pad_recent);
               if ((Directory.GetFiles(easy_pad_Data).Length) == 0)
                  Directory.Delete(easy_pad_Data, false);
            }
            if (!silent)
               _ = MessageBoxTimeout(IntPtr.Zero, "Easy Pad’s recent history file was removed.", "Success",
                   gTimedMessageBoxFlags, 0, 2000);
         }
         catch (Exception pException) {
            _ = MessageBoxTimeout(IntPtr.Zero, string.Format(
               "Removing the recent history failed. {0}The error was: {0} {1}", Environment.NewLine,
               pException.ToString()), "ERROR", gTimedMessageBoxFlags, 0, 0);
         }
      }

      public static void CleanThemes() {
         try {
            if (Directory.Exists(easy_pad_Data)) {
               if (File.Exists(easy_pad_themes))
                  File.Delete(easy_pad_themes);
               if ((Directory.GetFiles(easy_pad_Data).Length) == 0)
                  Directory.Delete(easy_pad_Data, false);
            }
            if (!silent)
               _ = MessageBoxTimeout(IntPtr.Zero, "Easy Pad’s Themes file was removed.", "Success",
                   gTimedMessageBoxFlags, 0, 2000);
         }
         catch (Exception pException) {
            _ = MessageBoxTimeout(IntPtr.Zero, string.Format(
               "Removing the themes failed. {0}The error was: {0} {1}", Environment.NewLine,
               pException.ToString()), "ERROR", gTimedMessageBoxFlags, 0, 0);
         }
      }
   }
}