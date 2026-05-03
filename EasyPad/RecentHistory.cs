using EasyPad.Properties;

namespace EasyPad {
   public partial class EasyPadForm : Form {
      #region handlers
      private void RecentFilesHistoryLimitTSMI_Click(object pSender, EventArgs pE) {
         RememberWindow();
         sSelectionStart = textBox.SelectionStart;
         sSelectionLength = textBox.SelectionLength;
         if (!Controls.Contains(recentFilesHistoryPanel))
            Controls.Add(recentFilesHistoryPanel);
         DisableMain();
         sEscapeFrom = EscapeFrom.RecentFilesHistory;
         LayoutRecentFilesHistoryPanel();
         FitToDialog();
         recentFilesHistoryPanel.Enabled = true;
         recentFilesHistoryPanel.Show();
         recentFilesHistoryPanel.BringToFront();
         CenterControl(this, recentFilesHistoryPanel);
         recentFilesHistoryUpDown.Focus();
         recentFilesHistoryUpDown.Select(0, recentFilesHistoryUpDown.Text.Length);
      }

      private void RecentFileTSMI_Click(object? pSender, EventArgs pE) {
         ToolStripMenuItem tsmi = pSender as ToolStripMenuItem;

         LoadFile(tsmi.Text);
      }

      private void RecentFilesHistoryPrefixButton_Click(object pSender, EventArgs pE) {
         recentFilesHistoryUpDown.Focus();
         recentFilesHistoryUpDown.Select(0, recentFilesHistoryUpDown.Text.Length);
      }

      private void RecentFilesHistoryOkayButton_Click(object pSender, EventArgs pE) {
         CloseRecentFilesHistory();
         Settings.Default.RecentFileHistoryLimit = (int)recentFilesHistoryUpDown.Value;
      }

      private void RecentFilesHistoryCancelButton_Click(object pSender, EventArgs pE) {
         CloseRecentFilesHistory();
         RestoreMain();
         sSelectionStart = textBox.SelectionStart;
         sSelectionLength = textBox.SelectionLength;
      }
      #endregion

      private void LayoutRecentFilesHistoryPanel() {
         recentFilesHistoryUpDown.Value = Settings.Default.RecentFileHistoryLimit;
         controlList.Clear();
         recentFilesHistoryPrefixButton.Top = recentFilesHistoryTitleLabel.Bottom + (sWidgetBigVerticalOffset * 2);
         recentFilesHistoryUpDown.Location = new Point(recentFilesHistoryPrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
            recentFilesHistoryPrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
         fileHistoryMinMaxLabel.Location = new Point(recentFilesHistoryUpDown.Right + sAssociatedLabelPostUpDownHorizontalSpace,
            recentFilesHistoryUpDown.Top + sAssociatedLabelPostUpDownVerticalOffset);
         controlList.Add(recentFilesHistoryPrefixButton);
         controlList.Add(recentFilesHistoryUpDown);
         clearFileHistoryButton.Location = new Point(sIndent,
            Bottommost(controlList) + (sWidgetBigVerticalOffset * 2));
         done.Location = new Point(clearFileHistoryButton.Right + sWidgetHorizontalSpace, clearFileHistoryButton.Top);
         recentFilesHistoryOkayButton.Location = new Point(clearFileHistoryButton.Right + sWidgetBigHorizontalSpace,
            clearFileHistoryButton.Top);
         recentFilesHistoryCancelButton.Top = clearFileHistoryButton.Top;
         SizePanel(recentFilesHistoryPanel);
         recentFilesHistoryCancelButton.Left = recentFilesHistoryPanel.Width - sCancelOffset - recentFilesHistoryCancelButton.Width;
         recentFilesHistoryOkayButton.Left = recentFilesHistoryCancelButton.Left - recentFilesHistoryOkayButton.Width - sCancelOffset;
         CenterControlHorizontally(recentFilesHistoryPanel, recentFilesHistoryTitleLabel);
         done.Hide();
      }

      private void CloseRecentFilesHistory() {
         sEscapeFrom = EscapeFrom.Main;
         recentFilesHistoryPanel.Enabled = false;
         recentFilesHistoryPanel.Hide();
         if (Controls.Contains(recentFilesHistoryPanel))
            Controls.Remove(recentFilesHistoryPanel);
         RestoreMain();
      }
   }
}