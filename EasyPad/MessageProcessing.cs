using EasyPad.Properties;
using static EasyPad.Program.NativeMethods;

namespace EasyPad {
   public partial class EasyPadForm : Form {
      public static readonly int
         SP_SHOW_AUTO_BOX = RegisterWindowMessage("SP_SHOW_AUTO_BOX"),
         SP_X_ECP_COLOR_PICKED = RegisterWindowMessage("SP_X_ECP_COLOR_PICKED"),
         SP_X_ECP_COLOR_NOT_PICKED = RegisterWindowMessage("SP_X_ECP_COLOR_NOT_PICKED"),
         SP_X_EFP_CODE_FONT_PICKED = RegisterWindowMessage("SP_X_EFP_CODE_FONT_PICKED"),
         SP_X_EFP_FONT_PICKED = RegisterWindowMessage("SP_X_EFP_FONT_PICKED"),
         SP_X_EFP_FONT_NOT_PICKED = RegisterWindowMessage("SP_X_EFP_FONT_NOT_PICKED");

      protected override void WndProc(ref Message pM) {
         Color color = Color.White;
         IntPtr wParameter = pM.WParam, lParameter = pM.LParam;

         if ((pM.Msg == SP_X_EFP_FONT_PICKED) || (pM.Msg == SP_X_EFP_CODE_FONT_PICKED)) {
            try {
               if (Clipboard.ContainsText()) {
                  string fontName = Clipboard.GetText();

                  Thread.Sleep(CLIPBOARD_DELAY);
                  if (!string.IsNullOrEmpty(fontName)) {
                     try {
                        switch (sFontUsage) {
                           case FontUsage.TextBoxMono:
                           case FontUsage.TextBox:
                              Settings.Default.TextFont =
                                 new Font(fontName, textBox.Font.SizeInPoints, textBox.Font.Style);
                              textBox.Font = CreateNewFont(Settings.Default.TextFont);
                              Settings.Default.Zoom = textBox.Font.SizeInPoints;
                              break;
                           case FontUsage.Interface:
                              Settings.Default.InterfaceFont =
                                 new Font(fontName, textBox.Font.SizeInPoints, textBox.Font.Style);
                              ColorizeGui();
                              break;
                        }
                     }
                     catch (Exception pException) {
                        TimedMessage("WndProc (m.Msg == SP_X_EFP_FONT_PICKED) " +
                              "threw an exception trying to create a new font." + Environment.NewLine + pException.ToString(),
                           "EXCEPTION CAUGHT", 0);
                     }
                  }
               }
               else {
                  TimedMessage("There was no text data on the clipboard!", "Empty Clipboard", 2500);
               }
               HardFocus();
            }
            catch (Exception pException) {
               TimedMessage("WndProc (m.Msg == SP_X_EFP_FONT_PICKED) " +
                     "threw an exception trying to get text from the Clipboard." + Environment.NewLine + pException.ToString(),
                     "EXCEPTION CAUGHT", 0);
            }
         }
         else if (pM.Msg == SP_X_EFP_FONT_NOT_PICKED)
            Show();
         else if (pM.Msg == SP_X_ECP_COLOR_PICKED) {
            try {
               if (Clipboard.ContainsText()) {
                  string colorName = Clipboard.GetText();

                  Thread.Sleep(CLIPBOARD_DELAY);
                  if (!string.IsNullOrEmpty(colorName) && !string.Equals("Unknown", colorName,
                     StringComparison.OrdinalIgnoreCase)) {
                     switch (sColorUsage) {
                        case ColorUsage.Text:
                           Settings.Default.TextColor = Color.FromName(colorName);
                           textBox.ForeColor = Color.FromName(colorName);
                           sCurrentTheme.mTextBoxFontColor = Color.FromName(colorName);
                           break;
                        case ColorUsage.TextBackground:
                           Settings.Default.TextBackgroundColor = Color.FromName(colorName);
                           textBox.BackColor = Color.FromName(colorName);
                           sCurrentTheme.mTextBoxBackgroundColor = Color.FromName(colorName);
                           break;
                        case ColorUsage.InterfaceText:
                           Settings.Default.InterfaceFontColor = Color.FromName(colorName);
                           sCurrentTheme.mInterfaceFontColor = Color.FromName(colorName);
                           ColorizeGui();
                           break;
                        case ColorUsage.DialogBackground:
                           Settings.Default.PanelBackgroundColor = Color.FromName(colorName);
                           sCurrentTheme.mPanelBackgroundColor = Color.FromName(colorName);
                           ColorizeGui();
                           break;
                        case ColorUsage.StatusBarBackground:
                           Settings.Default.StatusBarBackgroundColor = Color.FromName(colorName);
                           sCurrentTheme.mStatusBarBackgroundColor = Color.FromName(colorName);
                           ColorizeGui();
                           break;
                        default://ColorUsage.InterfaceBackground
                           Settings.Default.InterfaceBackgroundColor = Color.FromName(colorName);
                           sCurrentTheme.mInterfaceBackgroundColor = Color.FromName(colorName);
                           ColorizeGui();
                           break;
                     }
                  }
               }
               else {
                  TimedMessage("There was no text data on the clipboard!", "Empty Clipboard", 2500);
               }
               HardFocus();
            }
            catch (Exception pException) {
               TimedMessage("WndProc (m.Msg == SP_X_ECP_COLOR_PICKED) " +
                     "threw an exception trying to get text from the Clipboard." + Environment.NewLine + pException.ToString(),
                     "EXCEPTION CAUGHT", 0);
            }
         }
         else if (pM.Msg == SP_X_ECP_COLOR_NOT_PICKED)
            Show();

         base.WndProc(ref pM);
      }

      protected override bool ProcessCmdKey(ref Message pMsg, Keys pKeyData) {
         if ((pKeyData == (Keys.Control | Keys.Add)) || ((pKeyData == (Keys.Control | Keys.Oemplus)))) {
            SetZoom("+");
            return true;
         }
         else if ((pKeyData == (Keys.Control | Keys.Subtract)) || ((pKeyData == (Keys.Control | Keys.OemMinus)))) {
            SetZoom("-");
            return true;
         }
         else if (pKeyData == (Keys.Control | Keys.D0)) {
            SetZoom(11f);//efm5 reset to default
            return true;
         }
         else if (pKeyData == Keys.F1) {
            Help();
            return true;
         }
         else if (pKeyData == (Keys.Alt | Keys.F4)) {
            Close();
            return true;
         }
         else if (pKeyData == (Keys.Alt | Keys.Left)) {
            Console.Beep(300, 500);//DEBUG Beep
            //return true;
         }
         else if (pKeyData == (Keys.Alt | Keys.Right)) {
            Console.Beep(2300, 500);//DEBUG Beep
            //return true;
         }
         else if (pKeyData == (Keys.Alt | Keys.Up)) {
            Console.Beep(300, 500);//DEBUG Beep
            //return true;
         }
         else if (pKeyData == (Keys.Alt | Keys.Down)) {
            Console.Beep(2300, 500);//DEBUG Beep
            //return true;
         }
         else if (pKeyData == (Keys.Alt | Keys.PageUp)) {
            Console.Beep(300, 500);//DEBUG Beep
            //return true;
         }
         else if (pKeyData == (Keys.Alt | Keys.PageDown)) {
            Console.Beep(1300, 500);//DEBUG Beep
            //return true;
         }
         else if (pKeyData == Keys.Delete) {
            if (textBox.SelectedText.Length > 0)
               return false;
            textBox.Select(textBox.SelectionStart, 1);
            return false;
         }
         else if (pKeyData == Keys.Escape) {
            switch (sEscapeFrom) {
               case EscapeFrom.Find:
                  CloseFind();
                  return true;
               case EscapeFrom.Replace:
                  CloseReplace();
                  return true;
               case EscapeFrom.Go:
                  CloseGo();
                  return true;
               case EscapeFrom.RecentFilesHistory:
                  CloseRecentFilesHistory();
                  return true;
               case EscapeFrom.FontSize:
                  CloseFontSize();
                  return true;
               case EscapeFrom.CreateTheme:
                  CloseCreateTheme();
                  return true;
               case EscapeFrom.ThemePicker:
                  CloseThemePicker();
                  return true;
               case EscapeFrom.ThemeEditor:
                  CloseEditTheme();
                  return true;
               //case EscapeFrom.Main:
               default:
                  //efm5 we could allow escape from Main here, but… 
                  TimedMessage("Pressing the escape key only works for dialogs," + Environment.NewLine + "not the main window.",
                     "Sorry", 2000);
                  break;
            }
         }
         return base.ProcessCmdKey(ref pMsg, pKeyData);
      }
   }
}