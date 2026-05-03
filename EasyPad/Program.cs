using static EasyPad.EasyPadForm;
using static EasyPad.Program.NativeMethods;

namespace EasyPad {
   internal static partial class Program {
      [STAThread]
      static void Main(string[] pArguments) {
         SetProcessDPIAware();
         Application.SetHighDpiMode(HighDpiMode.SystemAware);
         ApplicationConfiguration.Initialize();
         if (pArguments.Length == 1)
            sArgument = pArguments[0];
         Application.Run(new EasyPadForm());
      }
   }
}