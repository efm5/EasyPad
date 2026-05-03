using EasyPad.Properties;

namespace EasyPad {
   public partial class EasyPadForm : Form {
      private void LayoutMain() {
         if (Controls.Contains(fontSizePanel))
            Controls.Remove(fontSizePanel);
         fontSizePanel.Hide();
         fontSizePanel.SendToBack();
         if (Controls.Contains(informationPanel))
            Controls.Remove(informationPanel);
         informationPanel.Hide();
         informationPanel.SendToBack();
         if (Controls.Contains(questionResponsePanel))
            Controls.Remove(questionResponsePanel);
         questionResponsePanel.Hide();
         questionResponsePanel.SendToBack();
         if (Controls.Contains(wheelingVelocityPanel))
            Controls.Remove(wheelingVelocityPanel);
         wheelingVelocityPanel.Hide();
         wheelingVelocityPanel.SendToBack();
         if (Controls.Contains(findPanel))
            Controls.Remove(findPanel);
         findPanel.Hide();
         findPanel.SendToBack();
         if (Controls.Contains(recentFilesHistoryPanel))
            Controls.Remove(recentFilesHistoryPanel);
         recentFilesHistoryPanel.Hide();
         recentFilesHistoryPanel.SendToBack();
         if (Controls.Contains(replacePanel))
            Controls.Remove(replacePanel);
         replacePanel.Hide();
         replacePanel.SendToBack();
         if (Controls.Contains(goPanel))
            Controls.Remove(goPanel);
         goPanel.Hide();
         goPanel.SendToBack();
         if (Controls.Contains(createThemePanel))
            Controls.Remove(createThemePanel);
         createThemePanel.Hide();
         createThemePanel.SendToBack();
         if (Controls.Contains(pickThemePanel))
            Controls.Remove(pickThemePanel);
         pickThemePanel.Hide();
         pickThemePanel.SendToBack();
         if (Controls.Contains(editThemePanel))
            Controls.Remove(editThemePanel);
         editThemePanel.Hide();
         editThemePanel.SendToBack();
         WrapTextBox();
         toolStrip.SendToBack();
         menuStrip.BringToFront();
         statusStrip.BringToFront();
         if (showToolBarTSMI.Checked) {
            HandleTools(toolBarShowToolsTSMI.Checked);
            HandleScrollers(toolBarShowScrollersTSMI.Checked);
         }
         if (Settings.Default.ShowStatusBar) {
            if (!toolStripContainer.BottomToolStripPanel.Controls.Contains(statusStrip))
               toolStripContainer.BottomToolStripPanel.Controls.Add(statusStrip);
         }
         else {
            if (toolStripContainer.BottomToolStripPanel.Controls.Contains(statusStrip))
               toolStripContainer.BottomToolStripPanel.Controls.Remove(statusStrip);
         }
         if (Settings.Default.FindScopeGlobal)
            findGlobalRadioButton.Checked = true;
         else
            findSelectionRadioButton.Checked = true;
         if (Settings.Default.ReplaceScopeGlobal)
            replaceGlobalRadioButton.Checked = true;
         else
            replaceSelectionRadioButton.Checked = true;
         ColorizeGui();
         MakePrefixes();
         UpdateStatusBar();
         CheckCurrentTheme();
         TopMost = alwaysOnTopTSMI.Checked;
         if (!string.IsNullOrEmpty(sArgument))
            LoadFile(sArgument);
         HardFocus();
         if (Settings.Default.Fade)
            FadeIn();
         else
            Opacity = 1;
      }
   }
}