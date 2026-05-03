using EasyPad.Properties;

namespace EasyPad {
   public partial class EasyPadForm : Form {
      #region event handlers
      private void FontSizePrefixButton_Click(object pSender, EventArgs pE) {
         fontSizeUpDown.Focus();
         fontSizeUpDown.Select(0, fontSizeUpDown.Text.Length);
      }

      private void FontSizeOkayButton_Click(object pSender, EventArgs pE) {
         bool alwaysClose = false;
         switch (sFontSizeUsage) {
            case FontSizeUsage.TextBox:
               textBox.Font = new Font(textBox.Font.Name, (float)fontSizeUpDown.Value, textBox.Font.Style);
               Settings.Default.TextFont = CreateNewFont(textBox.Font);
               break;
            case FontSizeUsage.Interface:
               Settings.Default.InterfaceFont =
                  new Font(menuStrip.Font.Name, (float)fontSizeUpDown.Value, textBox.Font.Style);
               ColorizeGui();
               break;
            case FontSizeUsage.TemporaryTextBox:
               alwaysClose = true;
               sTemporaryTheme.mTextBoxFont = new Font(sTemporaryTheme.mTextBoxFont.Name, (float)fontSizeUpDown.Value,
                  sTemporaryTheme.mTextBoxFont.Style);
               textBoxFontLabel.Text = string.Format("Font: {0}; size: {1}; style: {2}", sTemporaryTheme.mTextBoxFont.Name,
               (int)sTemporaryTheme.mTextBoxFont.SizeInPoints, sTemporaryTheme.mTextBoxFont.Style);
               break;
            case FontSizeUsage.TemporaryInterface:
               alwaysClose = true;
               sTemporaryTheme.mInterfaceFont = new Font(sTemporaryTheme.mInterfaceFont.Name, (float)fontSizeUpDown.Value,
                  sTemporaryTheme.mInterfaceFont.Style);
               interfaceFontLabel.Text = string.Format("Font: {0}; size: {1}; style: {2}", sTemporaryTheme.mInterfaceFont.Name,
                (int)sTemporaryTheme.mInterfaceFont.SizeInPoints, sTemporaryTheme.mInterfaceFont.Style);
               break;
         }
         if (sizeFontCloseOnOkayCheckBox.Checked || alwaysClose) {
            CloseFontSize();
            sSelectionStart = textBox.SelectionStart;
            sSelectionLength = textBox.SelectionLength;
         }
      }

      private void FontSizeCancelButton_Click(object pSender, EventArgs pE) {
         CloseFontSize();
         sSelectionStart = textBox.SelectionStart;
         sSelectionLength = textBox.SelectionLength;
      }

      private void InterfaceFontSizeTSMI_Click(object pSender, EventArgs pE) {
         sFontSizeUsage = FontSizeUsage.Interface;
         RememberWindow();
         sSelectionStart = textBox.SelectionStart;
         sSelectionLength = textBox.SelectionLength;
         if (!Controls.Contains(fontSizePanel))
            Controls.Add(fontSizePanel);
         DisableMain();
         sEscapeFrom = EscapeFrom.FontSize;
         LayoutFontSizePanel();
         FitToDialog();
         fontSizePanel.Enabled = true;
         fontSizePanel.Show();
         fontSizePanel.BringToFront();
         CenterControl(this, fontSizePanel);
         fontSizeUpDown.Focus();
         fontSizeUpDown.Select(0, fontSizeUpDown.Text.Length);
      }

      private void TextBoxFontSizeTSMI_Click(object pSender, EventArgs pE) {
         sFontSizeUsage = FontSizeUsage.TextBox;
         RememberWindow();
         sSelectionStart = textBox.SelectionStart;
         sSelectionLength = textBox.SelectionLength;
         if (!Controls.Contains(fontSizePanel))
            Controls.Add(fontSizePanel);
         DisableMain();
         sEscapeFrom = EscapeFrom.FontSize;
         LayoutFontSizePanel();
         FitToDialog();
         fontSizePanel.Enabled = true;
         fontSizePanel.Show();
         fontSizePanel.BringToFront();
         CenterControl(this, fontSizePanel);
         fontSizeUpDown.Focus();
         fontSizeUpDown.Select(0, fontSizeUpDown.Text.Length);
      }

      private void SizeFontCloseOnOkayCheckBox_CheckedChanged(object? pSender, EventArgs pE) {
         Settings.Default.FontSizeCloseOnOkay = sizeFontCloseOnOkayCheckBox.Checked;
      }
      #endregion

      private void LayoutFontSizePanel() {
         fontSizeTitleLabel.Text = "TextBox Font Size";
         fontSizeUpDown.Value = (int)textBox.Font.SizeInPoints;
         if (sFontSizeUsage == FontSizeUsage.Interface) {
            fontSizeTitleLabel.Text = "Interface Font Size";
            fontSizeUpDown.Value = (int)menuStrip.Font.SizeInPoints;
         }
         sizeFontCloseOnOkayCheckBox.CheckedChanged -= SizeFontCloseOnOkayCheckBox_CheckedChanged;
         sizeFontCloseOnOkayCheckBox.Checked = Settings.Default.FontSizeCloseOnOkay;
         sizeFontCloseOnOkayCheckBox.CheckedChanged += SizeFontCloseOnOkayCheckBox_CheckedChanged;
         fontSizeUpDown.Value = Settings.Default.RecentFileHistoryLimit;
         controlList.Clear();
         fontSizePrefixButton.Top = fontSizeTitleLabel.Bottom + (sWidgetBigVerticalOffset * 2);
         fontSizeUpDown.Location = new Point(fontSizePrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
            fontSizePrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
         controlList.Add(fontSizePrefixButton);
         controlList.Add(fontSizeUpDown);
         sizeFontCloseOnOkayCheckBox.Top = Bottommost(controlList) + (sWidgetBigVerticalOffset * 2);
         fontSizeOkayButton.Location = new Point(sIndent, sizeFontCloseOnOkayCheckBox.Bottom + (sWidgetBigVerticalOffset * 2));
         fontSizeCancelButton.Top = fontSizeOkayButton.Top;
         fontSizePanel.Size = new Size(
            fontSizeTitleLabel.Left + fontSizeTitleLabel.Width + fontSizeTitleLabel.Left,
            fontSizeOkayButton.Bottom + sWidgetBigVerticalOffset);
         fontSizeCancelButton.Left = fontSizePanel.Width - sCancelOffset - fontSizeCancelButton.Width;
         CenterControlHorizontally(fontSizePanel, fontSizeTitleLabel);
      }

      private void CloseFontSize() {
         sEscapeFrom = EscapeFrom.Main;
         fontSizePanel.Enabled = false;
         fontSizePanel.Hide();
         if (Controls.Contains(fontSizePanel))
            Controls.Remove(fontSizePanel);
         RestoreMain();
         RestoreWindow();
      }
   }
}