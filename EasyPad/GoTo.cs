namespace EasyPad {
   public partial class EasyPadForm : Form {
      #region handlers
      private void GoToTSMI_Click(object pSender, EventArgs pE) {
         if (string.IsNullOrEmpty(textBox.Text))
            return;
         RememberWindow();
         sSelectionStart = textBox.SelectionStart;
         sSelectionLength = textBox.SelectionLength;
         if (!Controls.Contains(goPanel))
            Controls.Add(goPanel);
         DisableMain();
         sEscapeFrom = EscapeFrom.Go;
         LayoutGoToPanel();
         FitToDialog();
         goPanel.Enabled = true;
         goPanel.Show();
         goPanel.BringToFront();
         goUpDown.Maximum = textBox.Lines.Length;
         CenterControl(this, goPanel);
         goUpDown.Focus();
         goUpDown.Select(0, goUpDown.Text.Length);
      }

      private void GoToPrefixbutton_Click(object pSender, EventArgs pE) {
         goUpDown.Focus();
         goUpDown.Select(0, goUpDown.Text.Length);
      }

      private void GoCancelButton_Click(object pSender, EventArgs pE) {
         CloseGo();
         RestoreMain();
         sSelectionStart = textBox.SelectionStart;
         sSelectionLength = textBox.SelectionLength;
      }

      private void GoToGoButton_Click(object pSender, EventArgs pE) {
         CloseGo();
         GotoLine((int)goUpDown.Value);
      }
      #endregion

      private void LayoutGoToPanel() {
         controlList.Clear();
         goToPrefixButton.Top = goTitleLabel.Bottom + (sWidgetBigVerticalOffset * 2);
         goUpDown.Location = new Point(goToPrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
            goToPrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
         controlList.Add(goToPrefixButton);
         controlList.Add(goUpDown);
         goToGoButton.Location = new Point(sIndent, Bottommost(controlList) + (sWidgetBigVerticalOffset * 2));
         goCancelButton.Top = goToGoButton.Top;
         goPanel.Size = new Size(goTitleLabel.Left + goTitleLabel.Width + goTitleLabel.Left,
            goToGoButton.Bottom + sWidgetBigVerticalOffset);
         goCancelButton.Left = goPanel.Width - sCancelOffset - goCancelButton.Width;
         CenterControlHorizontally(goPanel, goTitleLabel);
      }

      private static void GotoLine(int pLine) {// 1st line = 0
         textBox.Select(textBox.GetFirstCharIndexFromLine(pLine) - 1, 0);
         textBox.ScrollToCaret();
      }

      private void CloseGo() {
         sEscapeFrom = EscapeFrom.Main;
         goPanel.Enabled = false;
         goPanel.Hide();
         if (Controls.Contains(goPanel))
            Controls.Remove(goPanel);
         RestoreMain();
         RestoreWindow();
      }
   }
}