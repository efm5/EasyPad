using System.Drawing.Printing;
using System.Text.RegularExpressions;
using EasyPad.Properties;

namespace EasyPad {
   public partial class EasyPadForm : Form {
#pragma warning disable IDE0052 // unsued var - efm5 only during developement
#pragma warning disable IDE0051 // unsued var - efm5 only during developement
#pragma warning disable IDE0044 // unsued var - efm5 only during developement  
      // The "Box" that will hold the user's choice once they click a button
      private TaskCompletionSource<ResponseType>? _qrTaskCompletionSource, _infoTaskCompletionSource;

      #region private static, readonly & constant variables
      private const int THEMES_VERSION = 1, DARK_THEME = 0, LIGHT_THEME = 1, CURRENT_THEME = 2, FIND_WIDTH = 200;
      private static bool sIsCntrolKeyDown = false, sNeedsSizing = false, sDoing = false, sQRClosed = false, sDoSave = false;
      private static readonly Color darkGray = Color.FromArgb(255, 50, 50, 50),
         darkWhite = Color.FromArgb(255, 205, 205, 205),
         lightRed = Color.FromArgb(255, 255, 200, 200),
         darkRed = Color.FromArgb(255, 50, 0, 0);
      private static ColorUsage sColorUsage = ColorUsage.Text;
      private static List<Control> controlList = new List<Control>();
      private static float sZoomLevel = 0;
      private static Form? sForm = null;
      private const int WINDOW_REDUCER = 7, CLIPBOARD_DELAY = 300;
      private static EscapeFrom sEscapeFrom = EscapeFrom.Main;
      private static float sScaling;
      private static FontUsage sFontUsage = FontUsage.TextBox;
      private static FontSizeUsage sFontSizeUsage = FontSizeUsage.TextBox;
      private static int sSelectionStart, sSelectionLength, sMenuLeftOffset = 30, sEM = 10, sIndent = 10, sCancelOffset = 15,
         sOkOffset = 15, sTitleBarHeight, sGroupTopPad = 2, sOpenedFilterIndex = 16, sScalingGroupBoxTopLinePad = 0,
         sFindPosition, sFindLength, sWidgetHorizontalSpace = 3, sWidgetBigHorizontalSpace = 10, sWidgetVerticalOffset = 2,
         sWidgetBigVerticalOffset = 7, sGroupRightPad = 10, sGroupBottomPad = 10, sGroupLeftPad = 25,
         sMenuHeight, sToolbarHeight, sToolbarScale = Settings.Default.ToolBarSize, sStatusStripHeight,
         sAssociatedButtonPostTextBoxVerticalOffset = 0,
         sAssociatedUpDownPostButtonHorizontalSpace = 0,
         sAssociatedUpDownPostButtonVerticalOffset = 4,
         sAssociatedLabelPostUpDownHorizontalSpace = 0,
         sAssociatedLabelPostUpDownVerticalOffset = 0,
         sAssociatedTextBoxPostPrefixHorizontalSpace = 0,
         sAssociatedTextBoxPostPrefixVerticalOffset = 0;
      private static List<int> sFindIndexes = new List<int>();
      private static MatchCollection? sMatches;
      private static Point sOriginalLocation;
      private static readonly PrintDocument sPreviewDocument = new PrintDocument();
      private static ScrollBars sTextBoxScrollbars = ScrollBars.None;
      private static Size sOriginalSize, sResolution;
      private static readonly string sAppFolder = AppDomain.CurrentDomain.BaseDirectory,
         sHelpFolderLocation = AppDomain.CurrentDomain.BaseDirectory + @"Help\",
         sAppName = "EasyPad",
         sMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\",
         sEasyPadData = @"Easy_Pad_Data\",
         sMyDocumentsEasyPadData = sMyDocuments + sEasyPadData,
         sRecentFileHistoryPath = sMyDocumentsEasyPadData + "EasyPad_Recent_File_History.txt",
         sThemesFile = "EasyPad_Themes.txt",
         sThemeNamePrefix = "Name: ",
         sTextBoxFontFamily = "TextBox Font Family: ",
         sTextBoxFontSize = "TextBox Font Size: ",
         sTextBoxFontStyle = "TextBox Font Style: ",
         sInterfaceFontFamily = "Interface Font Family: ",
         sInterfaceFontSize = "Interface Font Size: ",
         sInterfaceFontStyle = "Interface Font Style: ",
         sTextBoxFontColor = "TextBox Font Color: ",
         sTextBoxBackgroundColor = "TextBox Background Color: ",
         sInterfaceFontColor = "Interface Font Color: ",
         sInterfaceBackgroundColor = "Interface Background Color: ",
         sStatusBarBackgroundColor = "Status Bar Background Color: ",
         sPanelBackgroundColor = "Panel Background Color: ",
         sThemesHeader = "Easy Pad Themes",
         sThemesVersion = "Themes Version: ",
         sNumberOfThemes = "Number Of Themes: ",
         sDefaultThemeName = "♫EFM5_EASY_PAD_Current_Theme♪",
      //efm5 sExtensionsFilter, sAcceptableExtensions and sFilterExtensionList must remain in the same order
      //efm5 Currently they are in alphabetical order
      //efm5 The first "All Files" is not actually alphabetized – keep it first
      sExtensionsFilter = "All Files|*.*|Auto Hot Key Files|*.ahk|AutoIT Files|*.au3|Batch Files|*.bat|C# Source Files|*.cs|" +
            "C++ Source Files|*.c++|C Source Files|*.c|CSV Files|.csv|Commandline Scripts|*.cmd|DVC Files|*.dvc|" +
            "HTM Files|*.htm|HTML Files|*.html|INI Files|*.ini|LOG Files|*.log|Registry Files|*.reg|Text Files|*.txt|" +
            "VBS Files|*.vbs|XML Files|*.xml";
      private static readonly string[] sAcceptableExtensions = new string[] { ".ahk", ".au3", ".bat", ".cs", ".c++", ".c", ".csv", ".cmd",
         ".dvc", ".htm", ".html", ".ini", ".log", ".reg", ".txt", ".vbs", ".xml" };
      private static readonly List<string> sFilterExtensionList = new List<string>() { "All Files|*.*", "Auto Hot Key Files|*.ahk",
          "AutoIT Files|*.au3", "Batch Files|*.bat", "C# Source Files|*.cs", "C++ Source Files|*.c++", "C Source Files|*.c",
          "CSV Files|.csv", "Commandline Scripts|*.cmd", "DVC Files|*.dvc", "HTM Files|*.htm", "HTML Files|*.html", "INI Files|*.ini",
          "LOG Files|*.log", "Registry Files|*.reg", "Text Files|*.txt", "VBS Files|*.vbs", "XML Files|*.xml" };
      private static string sImportedText = string.Empty, sDefaultExtension = "txt", sTextToPrint = string.Empty;
      private static MyTextBox? textBox = null;
      private static TextEncoding sTextEncoding = TextEncoding.UTF8;
      private static List<Theme> sThemeList = new List<Theme>() {
      new Theme("Dark",
         new Font("Arial", 12f, FontStyle.Regular),
         new Font("Segoe UI", 10f, FontStyle.Regular), Color.White,
            Color.Black, Color.White, Color.Black,
            darkRed, darkGray),
      new Theme("Light",
         new Font("Arial", 12f, FontStyle.Regular),
         new Font("Segoe UI", 10f, FontStyle.Regular), Color.Black,
            Color.White, Color.Black, Color.White,
            lightRed, darkWhite)
      };
      private static Theme? sCurrentTheme = sThemeList[DARK_THEME], sDarkTheme = sThemeList[DARK_THEME],
         sLightTheme = sThemeList[LIGHT_THEME], sTemporaryTheme = new Theme();
      private static ThemePickerUsage sUsingThemePickerFor;
      private static ToolBarLocation sToolBarLocation = ToolBarLocation.Top;
      private const uint TIMED_MESSAGEBOX_FLAGS = /*MB_OK*/ 0x00000000 | /*MB_SETFOREGROUND*/  0x00010000 |
         /*MB_SYSTEMMODAL*/ 0x00001000 | /*MB_ICONEXCLAMATION*/ 0x00000030;
      private static readonly List<UnReDoData> sUndoList = new List<UnReDoData>();
      private static readonly List<UnReDoData> sRedoList = new List<UnReDoData>();
      #endregion

      #region public static variables
#pragma warning disable CA2211 // shoud be private
      public static string sCurrentFile = string.Empty, sWindowTitle = sAppName, sArgument = string.Empty;
#pragma warning restore CA2211 // 
      #endregion

      #region classes
      public class UnReDoData {
         public int sSelectionStart = 0;
         public string mTextString = string.Empty;

         public UnReDoData(int pSelectionStart, string pTextString) {
            sSelectionStart = pSelectionStart;
            mTextString = pTextString;
         }
      }

      public class MyTextBox : TextBox {
         protected override void WndProc(ref Message pM) {
            switch (pM.Msg) {
               case 0x302: //WM_PASTE
                  try {
                     if (Clipboard.ContainsText()) {
                        if (RepairedLineEndings(Clipboard.GetText(), out string oString)) {
                           Clipboard.SetText(oString);
                           Paste();
                           return;
                        }
                        else {
                           base.WndProc(ref pM);
                           return;
                        }
                     }
                  }
                  catch (Exception pException) {
                     TimedMessage("WndProc threw an exception;" + Environment.NewLine +
                           "Clipboard.SetText(RepairLineEndings(Clipboard.GetText()) failed" + Environment.NewLine +
                        pException.ToString(), "EXCEPTION", 0);
                     return;
                  }
                  break;
            }
            base.WndProc(ref pM);
         }

#pragma warning disable IDE1006
         private static bool RepairedLineEndings(string pInput, out string opString) {
#pragma warning restore IDE1006
            char bell = '\x06';//efm5 a non-printing character

            //efm5 None of these quoted strings may be Localized
            opString = pInput.Replace("\r\n", char.ToString(bell));
            opString = opString.Replace("\n\r", char.ToString(bell));
            opString = opString.Replace('\n', bell);
            opString = opString.Replace('\r', bell);
            opString = opString.Replace('\a', bell);
            opString = opString.Replace(char.ToString(bell), Environment.NewLine);
            //opString = opString.Replace(char.ToString(bell), "\r\n");
            return !string.Equals(pInput, opString, StringComparison.Ordinal);
         }
      }

      public class Theme {
         public string mName;
         public Font mTextBoxFont;
         public Font mInterfaceFont;
         public Color mTextBoxFontColor;
         public Color mTextBoxBackgroundColor;
         public Color mInterfaceFontColor;
         public Color mInterfaceBackgroundColor;
         public Color mStatusBarBackgroundColor;
         public Color mPanelBackgroundColor;

         public Theme() {
            mName = string.Empty;
            mTextBoxFont = new Font("Arial", 12f, FontStyle.Regular);
            mInterfaceFont = new Font("Segoe UI", 10f, FontStyle.Regular);
            mTextBoxFontColor = Color.White;
            mTextBoxBackgroundColor = Color.Black;
            mInterfaceFontColor = Color.White;
            mInterfaceBackgroundColor = Color.Black;
            mStatusBarBackgroundColor = darkRed;
            mPanelBackgroundColor = darkGray;
         }

         public Theme(string pName, Font pTextBoxFont, Font pInterfaceFont, Color pTextBoxFontColor, Color pTextBoxBackgroundColor,
            Color pInterfaceFontColor, Color pInterfaceBackgroundColor, Color pStatusBarBackground, Color pPanelBackgroundColor) {
            mName = pName;
            mTextBoxFont = CreateNewFont(pTextBoxFont);
            mInterfaceFont = CreateNewFont(pInterfaceFont);
            mTextBoxFontColor = pTextBoxFontColor;
            mTextBoxBackgroundColor = pTextBoxBackgroundColor;
            mInterfaceFontColor = pInterfaceFontColor;
            mInterfaceBackgroundColor = pInterfaceBackgroundColor;
            mStatusBarBackgroundColor = pStatusBarBackground;
            mPanelBackgroundColor = pPanelBackgroundColor;
         }

         public Theme(Theme pTheme) {
            mName = pTheme.mName;
            mTextBoxFont = CreateNewFont(pTheme.mTextBoxFont);
            mInterfaceFont = CreateNewFont(pTheme.mInterfaceFont);
            mTextBoxFontColor = pTheme.mTextBoxFontColor;
            mTextBoxBackgroundColor = pTheme.mTextBoxBackgroundColor;
            mInterfaceFontColor = pTheme.mInterfaceFontColor;
            mInterfaceBackgroundColor = pTheme.mInterfaceBackgroundColor;
            mStatusBarBackgroundColor = pTheme.mStatusBarBackgroundColor;
            mPanelBackgroundColor = pTheme.mPanelBackgroundColor;
         }

         public static void Assignment(Theme pReceiverTheme, Theme pDonorTheme) {
            if ((pDonorTheme == null) || (pReceiverTheme == null))
               return;
            pReceiverTheme.mName = pDonorTheme.mName;
            pReceiverTheme.mTextBoxFont = CreateNewFont(pDonorTheme.mTextBoxFont);
            pReceiverTheme.mInterfaceFont = CreateNewFont(pDonorTheme.mInterfaceFont);
            pReceiverTheme.mTextBoxFontColor = pDonorTheme.mTextBoxFontColor;
            pReceiverTheme.mTextBoxBackgroundColor = pDonorTheme.mTextBoxBackgroundColor;
            pReceiverTheme.mInterfaceFontColor = pDonorTheme.mInterfaceFontColor;
            pReceiverTheme.mInterfaceBackgroundColor = pDonorTheme.mInterfaceBackgroundColor;
            pReceiverTheme.mStatusBarBackgroundColor = pDonorTheme.mStatusBarBackgroundColor;
            pReceiverTheme.mPanelBackgroundColor = pDonorTheme.mPanelBackgroundColor;
         }

         public string ToToolTip() {
            string returnString = string.Format("{0}{1}", sThemeNamePrefix, mName) + Environment.NewLine +
               string.Format("{0}{1}", sTextBoxFontFamily, mTextBoxFont.Name) + Environment.NewLine +
               string.Format("{0}{1}", sTextBoxFontSize, mTextBoxFont.SizeInPoints) + Environment.NewLine +
               string.Format("{0}{1}", sTextBoxFontStyle, mTextBoxFont.Style) + Environment.NewLine +
               string.Format("{0}{1}", sInterfaceFontFamily, mInterfaceFont.Name) + Environment.NewLine +
               string.Format("{0}{1}", sInterfaceFontSize, mInterfaceFont.SizeInPoints) + Environment.NewLine +
               string.Format("{0}{1}", sInterfaceFontStyle, mInterfaceFont.Style) + Environment.NewLine +
               string.Format("{0}{1}", sTextBoxFontColor, MassageColorName(mTextBoxFontColor.Name)) +
               Environment.NewLine +
               string.Format("{0}{1}", sTextBoxBackgroundColor,
                  MassageColorName(mTextBoxBackgroundColor.Name)) +
               Environment.NewLine +
               string.Format("{0}{1}", sInterfaceFontColor,
                  MassageColorName(mInterfaceFontColor.Name)) +
               Environment.NewLine +
               string.Format("{0}{1}", sInterfaceBackgroundColor,
                  MassageColorName(mInterfaceBackgroundColor.Name)) +
               Environment.NewLine +
               string.Format("{0}{1}", sStatusBarBackgroundColor,
                  MassageColorName(mStatusBarBackgroundColor.Name)) +
               Environment.NewLine +
               string.Format("{0}{1}", sPanelBackgroundColor,
                  MassageColorName(mPanelBackgroundColor.Name));
            return returnString;
         }

         public void Write(StreamWriter pWrite) {
            try {
               FontConverter? converter = new FontConverter();
               pWrite.WriteLine(string.Format("{0}{1}", sThemeNamePrefix, mName));
               pWrite.WriteLine(string.Format("{0}{1}", sTextBoxFontFamily, converter.ConvertToInvariantString(mTextBoxFont)));
               pWrite.WriteLine(string.Format("{0}{1}", sInterfaceFontFamily, converter.ConvertToInvariantString(mInterfaceFont)));
               pWrite.WriteLine(string.Format("{0}{1}", sTextBoxFontColor, ColorTranslator.ToWin32(mTextBoxFontColor)));
               pWrite.WriteLine(string.Format("{0}{1}", sTextBoxBackgroundColor, ColorTranslator.ToWin32(mTextBoxBackgroundColor)));
               pWrite.WriteLine(string.Format("{0}{1}", sInterfaceFontColor, ColorTranslator.ToWin32(mInterfaceFontColor)));
               pWrite.WriteLine(string.Format("{0}{1}", sInterfaceBackgroundColor, ColorTranslator.ToWin32(mInterfaceBackgroundColor)));
               pWrite.WriteLine(string.Format("{0}{1}", sStatusBarBackgroundColor, ColorTranslator.ToWin32(mStatusBarBackgroundColor)));
               pWrite.WriteLine(string.Format("{0}{1}", sPanelBackgroundColor, ColorTranslator.ToWin32(mPanelBackgroundColor)));

               pWrite.WriteLine(string.Empty);
            }
            catch (Exception pException) {
               TimedMessage("Theme:Write failed." + Environment.NewLine + pException.ToString(), "EXCEPTION", 0);
            }
         }

         public void Dispose() {
            mTextBoxFont.Dispose();
            mInterfaceFont.Dispose();
         }
      }
      #endregion

      #region enumerations
      public enum TextEncoding : int {
         ASCII = 0,
         //UTF7 = 1,
         UTF8 = 2,
         UTF16 = 3,
         UTF16LE = 3,
         Unicode = 3,
         UnicodeLE = 3,
         LittleEndianUnicode = 3,
         UTF16BE = 4,
         UnicodeBE = 4,
         BigEndianUnicode = 4,
         UTF32 = 5,
         UTF64 = 6
      }

      public enum EscapeFrom : int {
         Main = 1,
         Find = 2,
         Replace = 3,
         Go = 4,
         CommentWidth = 5,
         RecentFilesHistory = 6,
         FontSize = 7,
         CreateTheme = 8,
         ThemePicker = 9,
         ThemeEditor = 10
      }

      public enum ColorUsage : int {
         TextBackground = 0,
         Text = 1,
         InterfaceBackground = 2,
         InterfaceText = 3,
         StatusBarBackground = 4,
         DialogBackground = 5
      }

      public enum FontUsage : int {
         TextBox = 0,
         Interface = 1,
         TextBoxMono = 2
      }

      public enum FontSizeUsage : int {
         TextBox = 0,
         Interface = 1,
         TemporaryTextBox = 2,
         TemporaryInterface = 3
      }

      public enum ThemePickerUsage : int {
         Edit = 0,
         Remove = 1
      }

      public enum ToolBarLocation : int {
         Top = 1,
         Bottom = 2,
         Left = 3,
         Right = 4,
         Nowhere = 5
      }

      public enum ToolBarSize : int {
         Sixteen = 16,
         Twenty_Four = 24,
         Thirty_Two = 32,
         Sixty_Four = 64,
         One_Hundred_Twenty_Eight = 128
      }

      public enum ResponseType {
         Yes,
         No,
         Cancel
      }

      #endregion
#pragma warning restore IDE0052 // unsued var - efm5 only during developement
#pragma warning restore IDE0051 // unsued var - efm5 only during developement
#pragma warning restore IDE0044 // unsued var - efm5 only during developement  
   }
}