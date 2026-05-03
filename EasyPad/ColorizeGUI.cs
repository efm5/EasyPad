using EasyPad.Properties;

namespace EasyPad {
   public partial class EasyPadForm : Form {
      private void ColorizeGui() {
         SizeF opSizeF = new SizeF(200, 30);

         findTextBox.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
         searchTextBox.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
         replaceTextBox.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
         createThemeNameTextBox.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
         SizeTextBoxToFitString(out opSizeF, findTextBox);
         findTextBox.Size = new Size((int)opSizeF.Width, (int)opSizeF.Height);
         SizeTextBoxToFitString(out opSizeF, searchTextBox);
         searchTextBox.Size = new Size((int)opSizeF.Width, (int)opSizeF.Height);
         SizeTextBoxToFitString(out opSizeF, replaceTextBox, "The quick brownñÑ çÇ");
         replaceTextBox.Size = new Size((int)opSizeF.Width, (int)opSizeF.Height);
         SizeTextBoxToFitString(out opSizeF, createThemeNameTextBox, "The quick brown fox jumpedñÑ çÇ");
         createThemeNameTextBox.Size = new Size((int)opSizeF.Width, (int)opSizeF.Height);
         Font titleFont = new Font(Settings.Default.InterfaceFont.Name,
            (Settings.Default.InterfaceFont.SizeInPoints * 1.5f), FontStyle.Bold);
         findTitleLabel.Font = CreateNewFont(titleFont);
         replaceTitleLabel.Font = CreateNewFont(titleFont);
         goTitleLabel.Font = CreateNewFont(titleFont);
         recentFilesHistoryTitleLabel.Font = CreateNewFont(titleFont);
         fileHistoryMinMaxLabel.Font = CreateNewFont(titleFont);
         fontSizeTitleLabel.Font = CreateNewFont(titleFont);
         wheelingVelocityTitleLabel.Font = CreateNewFont(titleFont);
         questionResponseTitleLabel.Font = CreateNewFont(titleFont);
         informationTitleLabel.Font = CreateNewFont(titleFont);
         sizeFontCloseOnOkayCheckBox.Font = CreateNewFont(Settings.Default.InterfaceFont);
         statusStrip.Font = CreateNewFont(Settings.Default.InterfaceFont);
         menuStrip.Font = CreateNewFont(Settings.Default.InterfaceFont);
         textBox.Font = CreateNewFont(Settings.Default.TextFont);
         textBox.ForeColor = Settings.Default.TextColor;
         goUpDown.Font = CreateNewFont(Settings.Default.InterfaceFont);
         recentFilesHistoryUpDown.Font = CreateNewFont(Settings.Default.InterfaceFont);
         fontSizeUpDown.Font = CreateNewFont(Settings.Default.InterfaceFont);
         questionResponseDiscussionLabel.Font = CreateNewFont(Settings.Default.InterfaceFont);
         questionResponseQuestionLabel.Font = CreateNewFont(Settings.Default.InterfaceFont);
         informationLabel.Font = CreateNewFont(Settings.Default.InterfaceFont);
         ColorByTheme();
         FlatButton(findWhatPrefixButton);
         FlatButton(searchSearchPrefixButton);
         FlatButton(searchReplacePrefixButton);
         FlatButton(goToPrefixButton);
         FlatButton(fontSizePrefixButton);
         FlatButton(recentFilesHistoryPrefixButton);
         FlatButton(createThemeNamePrefixButton);
         SizeAllUpDowns(new List<NumericUpDown>() { goUpDown, recentFilesHistoryUpDown, fontSizeUpDown, wheelingVelocityUpDown });
         ColorPrefixesByTheme();
      }

      private void ColorByTheme() {
         toolStrip.BackColor = sCurrentTheme.mStatusBarBackgroundColor;
         toolStripContainer.BackColor = sCurrentTheme.mStatusBarBackgroundColor;
         toolStripContainer.TopToolStripPanel.BackColor = sCurrentTheme.mStatusBarBackgroundColor;
         toolStripContainer.BottomToolStripPanel.BackColor = sCurrentTheme.mStatusBarBackgroundColor;
         toolStripContainer.LeftToolStripPanel.BackColor = sCurrentTheme.mStatusBarBackgroundColor;
         toolStripContainer.RightToolStripPanel.BackColor = sCurrentTheme.mStatusBarBackgroundColor;
         findPanel.BackColor = sCurrentTheme.mPanelBackgroundColor;
         replacePanel.BackColor = sCurrentTheme.mPanelBackgroundColor;
         goPanel.BackColor = sCurrentTheme.mPanelBackgroundColor;
         fontSizePanel.BackColor = sCurrentTheme.mPanelBackgroundColor;
         recentFilesHistoryPanel.BackColor = sCurrentTheme.mPanelBackgroundColor;
         createThemePanel.BackColor = sCurrentTheme.mPanelBackgroundColor;
         pickThemePanel.BackColor = sCurrentTheme.mPanelBackgroundColor;
         wheelingVelocityPanel.BackColor = sCurrentTheme.mPanelBackgroundColor;
         questionResponsePanel.BackColor = sCurrentTheme.mPanelBackgroundColor;
         informationPanel.BackColor = sCurrentTheme.mPanelBackgroundColor;
         ForeColor = sCurrentTheme.mInterfaceFontColor;
         BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         menuStrip.ForeColor = sCurrentTheme.mInterfaceFontColor;
         menuStrip.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         textBox.ForeColor = sCurrentTheme.mTextBoxFontColor;
         textBox.BackColor = sCurrentTheme.mTextBoxBackgroundColor;
         statusStrip.ForeColor = sCurrentTheme.mInterfaceFontColor;
         statusStrip.BackColor = sCurrentTheme.mStatusBarBackgroundColor;
         toolBarLocationSectionTSMI.ForeColor = sCurrentTheme.mTextBoxFontColor;
         toolBarLocationSectionTSMI.BackColor = SystemColors.GradientInactiveCaption;
         toolBarSizeSectionTSMI.ForeColor = sCurrentTheme.mTextBoxFontColor;
         toolBarSizeSectionTSMI.BackColor = SystemColors.GradientInactiveCaption;

         findTextBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         findTextBox.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         searchTextBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         searchTextBox.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         replaceTextBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         replaceTextBox.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         createThemeNameTextBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         createThemeNameTextBox.BackColor = sCurrentTheme.mInterfaceBackgroundColor;

         goUpDown.ForeColor = sCurrentTheme.mInterfaceFontColor;
         goUpDown.BackColor = sCurrentTheme.mPanelBackgroundColor;
         recentFilesHistoryUpDown.ForeColor = sCurrentTheme.mInterfaceFontColor;
         recentFilesHistoryUpDown.BackColor = sCurrentTheme.mPanelBackgroundColor;
         fontSizeUpDown.ForeColor = sCurrentTheme.mInterfaceFontColor;
         fontSizeUpDown.BackColor = sCurrentTheme.mPanelBackgroundColor;

         sizeFontCloseOnOkayCheckBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         findTitleLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         replaceTitleLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         goTitleLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         recentFilesHistoryTitleLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         fontSizeTitleLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         createThemeTitleLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         questionResponseTitleLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         informationTitleLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         pickThemeLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         fileHistoryMinMaxLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         questionResponseDiscussionLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         questionResponseQuestionLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         informationLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;

         findCheckBoxGroupBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         findCheckBoxGroupBox.BackColor = sCurrentTheme.mTextBoxBackgroundColor;
         findScopeGroupBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         findScopeGroupBox.BackColor = sCurrentTheme.mTextBoxBackgroundColor;
         replaceCheckBoxGroupBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         replaceCheckBoxGroupBox.BackColor = sCurrentTheme.mTextBoxBackgroundColor;
         replaceScopeGroupBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         replaceScopeGroupBox.BackColor = sCurrentTheme.mTextBoxBackgroundColor;

         foreach (Button button in informationPanel.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         }

         foreach (Button button in questionResponsePanel.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         }
         foreach (Button button in createThemePanel.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         }
         foreach (Button button in fontSizePanel.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         }
         foreach (Button button in findPanel.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mPanelBackgroundColor;
         }
         foreach (GroupBox groupBox in findPanel.Controls.OfType<GroupBox>()) {
            groupBox.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            groupBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
            groupBox.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         }
         foreach (CheckBox checkBox in findCheckBoxGroupBox.Controls.OfType<CheckBox>()) {
            checkBox.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            checkBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         }
         foreach (RadioButton radioButton in findScopeGroupBox.Controls.OfType<RadioButton>()) {
            radioButton.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            radioButton.ForeColor = sCurrentTheme.mInterfaceFontColor;
         }
         foreach (RadioButton radioButton in findScopeGroupBox.Controls.OfType<RadioButton>()) {
            radioButton.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            radioButton.ForeColor = sCurrentTheme.mInterfaceFontColor;
         }
         foreach (Button button in replacePanel.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         }
         foreach (GroupBox groupBox in replacePanel.Controls.OfType<GroupBox>()) {
            groupBox.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            groupBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
            groupBox.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         }
         foreach (CheckBox checkBox in replaceCheckBoxGroupBox.Controls.OfType<CheckBox>()) {
            checkBox.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            checkBox.ForeColor = sCurrentTheme.mInterfaceFontColor;
         }
         foreach (RadioButton radioButton in replaceScopeGroupBox.Controls.OfType<RadioButton>()) {
            radioButton.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            radioButton.ForeColor = sCurrentTheme.mInterfaceFontColor;
         }
         foreach (Button button in goPanel.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mPanelBackgroundColor;
         }
         foreach (Button button in recentFilesHistoryPanel.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         }
         foreach (ToolStripItem tsi in menuStrip.Items)
            PaintMenuItems(tsi);
      }

      private void MakeTransparent() {
         sizeFontCloseOnOkayCheckBox.BackColor = Color.Transparent;
         findTitleLabel.BackColor = Color.Transparent;
         replaceTitleLabel.BackColor = Color.Transparent;
         goTitleLabel.BackColor = Color.Transparent;
         recentFilesHistoryTitleLabel.BackColor = Color.Transparent;
         fileHistoryMinMaxLabel.BackColor = Color.Transparent;

         foreach (CheckBox checkBox in findCheckBoxGroupBox.Controls.OfType<CheckBox>())
            checkBox.BackColor = Color.Transparent;

         foreach (RadioButton radioButton in findScopeGroupBox.Controls.OfType<RadioButton>())
            radioButton.BackColor = Color.Transparent;

         foreach (RadioButton radioButton in findScopeGroupBox.Controls.OfType<RadioButton>())
            radioButton.BackColor = Color.Transparent;

         foreach (CheckBox checkBox in replaceCheckBoxGroupBox.Controls.OfType<CheckBox>())
            checkBox.BackColor = Color.Transparent;

         foreach (RadioButton radioButton in replaceScopeGroupBox.Controls.OfType<RadioButton>())
            radioButton.BackColor = Color.Transparent;
      }

      private void ColorPrefixesByTheme() {
         recentFilesHistoryPrefixButton.ForeColor = sCurrentTheme.mInterfaceFontColor;
         recentFilesHistoryPrefixButton.BackColor = sCurrentTheme.mPanelBackgroundColor;
         createThemeNamePrefixButton.ForeColor = sCurrentTheme.mInterfaceFontColor;
         createThemeNamePrefixButton.BackColor = sCurrentTheme.mPanelBackgroundColor;
      }

      private static void FlatButton(Button pControl) {
         pControl.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
         pControl.BackColor = Color.Transparent;
         pControl.FlatAppearance.BorderSize = 0;
         pControl.FlatStyle = FlatStyle.Flat;
         if (string.Equals(Settings.Default.StoredThemeName, "Dark", StringComparison.OrdinalIgnoreCase))
            pControl.FlatAppearance.BorderColor = darkGray;
         else if (string.Equals(Settings.Default.StoredThemeName, "Light", StringComparison.OrdinalIgnoreCase))
            pControl.FlatAppearance.BorderColor = darkWhite;
         else
            pControl.FlatAppearance.BorderColor = Color.Gray;//DEBUG efm5 2024 04 5 do it right
      }
   }
}