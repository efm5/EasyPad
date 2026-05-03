using EasyPad.Properties;

namespace EasyPad {
   public partial class EasyPadForm : Form {
      private void Theme_Click(object? pSender, EventArgs pE) {
         ToolStripMenuItem toolStripMenuItem = pSender as ToolStripMenuItem;

         foreach (ToolStripMenuItem tsmi in themesTSMI.DropDownItems.OfType<ToolStripMenuItem>()) {
            tsmi.Click -= new EventHandler(Theme_Click);
            tsmi.Checked = false;
         }
         toolStripMenuItem.Checked = true;
         try {
            foreach (Theme theme in sThemeList) {
               if (string.Equals(theme.mName, ((ToolStripMenuItem)pSender).Text, StringComparison.OrdinalIgnoreCase)) {
                  sCurrentTheme.Dispose();
                  sCurrentTheme = null;
                  sCurrentTheme = new Theme(theme);
                  break;
               }
            }
            if (sCurrentTheme != null) {
               Settings.Default.TextFont = CreateNewFont(sCurrentTheme.mTextBoxFont);
               Settings.Default.InterfaceFont = CreateNewFont(sCurrentTheme.mInterfaceFont);
               Settings.Default.TextColor = sCurrentTheme.mTextBoxFontColor;
               Settings.Default.TextBackgroundColor = sCurrentTheme.mTextBoxBackgroundColor;
               Settings.Default.InterfaceBackgroundColor = sCurrentTheme.mInterfaceBackgroundColor;
               Settings.Default.StatusBarBackgroundColor = sCurrentTheme.mStatusBarBackgroundColor;
               ColorizeGui();
            }
         }
         catch (Exception pException) {
            TimedMessage("Theme_Click threw an exception" + Environment.NewLine + pException.ToString(),
               "EXCEPTION", 0);
         }
         foreach (ToolStripMenuItem tsmi in themesTSMI.DropDownItems.OfType<ToolStripMenuItem>())
            tsmi.Click += new EventHandler(Theme_Click);
      }

      private void UseTheme_Click(object? pSender, EventArgs pE) {
         Button button = (Button)pSender;
         string tag = button.Tag as string;

         switch (sUsingThemePickerFor) {
            case ThemePickerUsage.Edit:
               if (!string.IsNullOrEmpty(tag)) {
                  foreach (Theme theme in sThemeList) {
                     if (string.Equals(theme.mName, tag, StringComparison.OrdinalIgnoreCase)) {
                        EditTheme(theme);
                        break;
                     }
                  }
               }
               break;
            case ThemePickerUsage.Remove:
               if (!string.IsNullOrEmpty(tag)) {
                  foreach (Theme theme in sThemeList) {
                     if (string.Equals(theme.mName, tag, StringComparison.OrdinalIgnoreCase)) {
                        sThemeList.Remove(theme);
                        theme?.Dispose();
                        foreach (Button item in pickThemePanel.Controls.OfType<Button>()) {
                           if (string.Equals(item.Tag as string, tag, StringComparison.OrdinalIgnoreCase)) {
                              pickThemePanel.Controls.Remove(item);
                              item?.Dispose();
                              break;
                           }
                        }
                        break;
                     }
                  }
               }
               break;
         }
         if (!doMoreCheckBox.Checked)
            CloseThemePicker();
      }

      #region create theme
      private void CreateThemeNamePrefixButton_Click(object pSender, EventArgs pE) {
         createThemeNameTextBox.Focus();
      }

      private void CreateThemeOkayButton_Click(object pSender, EventArgs pE) {
         CreateNewTheme();
         CloseCreateTheme();
      }

      private void CreateThemeCancelButton_Click(object pSender, EventArgs pE) {
         CloseCreateTheme();
      }

      private void CreateThemeTSMI_Click(object pSender, EventArgs pE) {
         RememberWindow();
         if (!Controls.Contains(createThemePanel))
            Controls.Add(createThemePanel);
         DisableMain();
         sEscapeFrom = EscapeFrom.CreateTheme;
         LayoutCreateThemePanel();
         FitToDialog();
         createThemePanel.Enabled = true;
         createThemePanel.Show();
         createThemePanel.BringToFront();
         CenterControl(this, createThemePanel);
         createThemeNameTextBox.Focus();
      }

      private void LayoutCreateThemePanel() {
         createThemeNameTextBox.Clear();
         controlList.Clear();
         createThemeNamePrefixButton.Top = createThemeTitleLabel.Bottom + (sWidgetBigVerticalOffset * 2);
         createThemeNameTextBox.Location = new Point(createThemeNamePrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
            createThemeNamePrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
         controlList.Add(createThemeNamePrefixButton);
         controlList.Add(createThemeNameTextBox);
         createThemeOkayButton.Location = new Point(sIndent, Bottommost(controlList) + (sWidgetBigVerticalOffset * 2));
         createThemeCancelButton.Top = createThemeOkayButton.Top;
         SizePanel(createThemePanel);
         createThemeCancelButton.Left = createThemePanel.Width - sCancelOffset - createThemeCancelButton.Width;
         CenterControlHorizontally(createThemePanel, createThemeTitleLabel);
      }

      private void CloseCreateTheme() {
         sEscapeFrom = EscapeFrom.Main;
         createThemePanel.Enabled = false;
         createThemePanel.Hide();
         if (Controls.Contains(createThemePanel))
            Controls.Remove(createThemePanel);
         RestoreMain();
      }

      private void CreateNewTheme() {
         if (string.IsNullOrEmpty(createThemeNameTextBox.Text)) {
            TimedMessage("The theme must have a name!", "Invalid Name");
            return;
         }
         foreach (Theme theme in sThemeList) {
            if (string.Equals(theme.mName, createThemeNameTextBox.Text, StringComparison.OrdinalIgnoreCase)) {
               TimedMessage("The theme must have a unique name!", "Invalid Name");
               return;
            }
         }
         foreach (ToolStripMenuItem toolStripMenuItem in menuStrip.Items.OfType<ToolStripMenuItem>()) {
            if (string.Equals(toolStripMenuItem.Text, createThemeNameTextBox.Text, StringComparison.OrdinalIgnoreCase)) {
               TimedMessage("No theme may have the same name as the main menu items!", "Invalid Name");
               return;
            }
         }
         foreach (ToolStripMenuItem toolStripMenuItem in viewTSMI.DropDownItems.OfType<ToolStripMenuItem>()) {
            if (string.Equals(toolStripMenuItem.Text, createThemeNameTextBox.Text, StringComparison.OrdinalIgnoreCase)) {
               TimedMessage("No theme may have the same name as a View menu subitem!", "Invalid Name");
               return;
            }
         }
         foreach (ToolStripMenuItem toolStripMenuItem in themesTSMI.DropDownItems.OfType<ToolStripMenuItem>()) {
            if (string.Equals(toolStripMenuItem.Text, createThemeNameTextBox.Text, StringComparison.OrdinalIgnoreCase)) {
               TimedMessage("No theme may have the same name as a Themes menu subitem!", "Invalid Name");
               return;
            }
         }
         sCurrentTheme = new Theme(createThemeNameTextBox.Text, textBox.Font,
            menuStrip.Font, textBox.ForeColor,
            textBox.BackColor, menuStrip.ForeColor,
            menuStrip.BackColor, statusStrip.BackColor, goPanel.BackColor);
         sThemeList.Add(sCurrentTheme);
         foreach (ToolStripMenuItem toolStripMenuItem in themesTSMI.DropDownItems.OfType<ToolStripMenuItem>())
            toolStripMenuItem.Checked = false;
         ToolStripMenuItem tsmi = new ToolStripMenuItem() {
            Text = createThemeNameTextBox.Text,
            Name = createThemeNameTextBox.Text.Replace(" ", "") + "TSMI",
            Font = CreateNewFont(menuStrip.Font),
            ForeColor = menuStrip.ForeColor,
            BackColor = menuStrip.BackColor,
            Checked = true
         };
         tsmi.Click += new EventHandler(Theme_Click);
         themesTSMI.DropDownItems.Add(tsmi);
         CloseCreateTheme();
      }
      #endregion

      #region pick theme
      private void ThemePickerTSMI_Click(object pSender, EventArgs pE) {
         ToolStripMenuItem tsmi = pSender as ToolStripMenuItem;
         string tag = tsmi.Tag as string;

         if (!string.IsNullOrEmpty(tag)) {
            switch (tag) {
               case "Edit":
                  PickTheme(ThemePickerUsage.Edit);
                  break;
               case "Remove":
                  PickTheme(ThemePickerUsage.Remove);
                  break;
            }
         }
      }

      private void PickThemeCancelButton_Click(object pSender, EventArgs pE) {
         CloseThemePicker();
      }

      private void PickTheme(ThemePickerUsage pUsage) {
         int top = ClientRectangle.Top + menuStrip.Height + 10,
            right = ClientRectangle.Left + 10;//efm5 For both of these 10 = 8 pixels offset

         sUsingThemePickerFor = pUsage;
         RememberWindow();
         if (!Controls.Contains(pickThemePanel))
            Controls.Add(pickThemePanel);
         DisableMain();
         sEscapeFrom = EscapeFrom.ThemePicker;
         pickThemeLabel.Text = "Pick Theme To Edit";
         if (pUsage == ThemePickerUsage.Remove)
            pickThemeLabel.Text = "Pick Theme To Remove";
         //else
         //   pickThemePanel.Controls.Remove(doMoreCheckBox);
         LayoutThemePickerPanel();
         FitToDialog();
         pickThemePanel.Enabled = true;
         pickThemePanel.Show();
         pickThemePanel.BringToFront();
         CenterControl(this, pickThemePanel);
         pickThemeCancelButton.Left = pickThemePanel.Width - pickThemeCancelButton.Width - sCancelOffset;
         //if (pUsage == ThemePickerUsage.Remove)
         doMoreCheckBox.Top = pickThemeCancelButton.Top;
         CenterControlHorizontally(pickThemePanel, pickThemeLabel);
         pickThemeCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
         pickThemePanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
         pickThemeCancelButton.Focus();
      }

      private void LayoutThemePickerPanel() {
         int index = 12700, top = pickThemeLabel.Bottom + sWidgetBigVerticalOffset, left = 10, bottom = pickThemeLabel.Bottom, tooWide;
         const int horizontalSpacingBetweenButtons = 4, verticalSpaceBetweenButtons = 16, horizontalPadding = 22;
         List<Theme> themesList = sThemeList.ToList();

         themesList.RemoveAt(0);//Dark; These may be neither edited nor removed
         themesList.RemoveAt(0);//Light
         if (themesList.Count == 0)
            pickThemeLabel.Text = "There are no themes to pick";
         else {
            tooWide = ClientSize.Width - SystemInformation.VerticalScrollBarWidth + horizontalPadding;

            foreach (Theme theme in themesList) {
               Button button = new Button() {
                  Location = new Point(left, top),
                  AutoSize = true,
                  AutoSizeMode = AutoSizeMode.GrowAndShrink,
                  Text = theme.mName,
                  Font = CreateNewFont(sCurrentTheme.mInterfaceFont),
                  ForeColor = sCurrentTheme.mInterfaceFontColor,
                  BackColor = sCurrentTheme.mInterfaceBackgroundColor,
                  Tag = theme.mName,
                  TabIndex = index++
               };
               button.Click += UseTheme_Click;
               pickThemePanel.Controls.Add(button);
               left = button.Right + horizontalSpacingBetweenButtons;
               if (left > tooWide) {
                  top = button.Bottom + verticalSpaceBetweenButtons;
                  button.Location = new Point(10, top);
                  left = button.Right + horizontalSpacingBetweenButtons;
               }
               bottom = button.Bottom;
            }
         }
         left = 0;
         pickThemePanel.Controls.Remove(pickThemeCancelButton);
         foreach (Button button in pickThemePanel.Controls.OfType<Button>())
            if (left < button.Right)
               left = button.Right;
         pickThemePanel.Controls.Add(pickThemeCancelButton);
         pickThemeCancelButton.Top = bottom + sCancelOffset;
         pickThemePanel.Size = new Size(left - SystemInformation.VerticalScrollBarWidth + horizontalPadding,
            pickThemeCancelButton.Bottom + sCancelOffset + SystemInformation.HorizontalScrollBarHeight);
         if (pickThemePanel.Width > (ClientSize.Width - 10)) {
            pickThemePanel.Width = (ClientSize.Width - 10);
         }
         if (pickThemePanel.Height > (ClientSize.Height - 10)) {
            pickThemePanel.Height = (ClientSize.Height - 10);
         }
      }

      private void CloseThemePicker() {
         sEscapeFrom = EscapeFrom.Main;
         pickThemePanel.Enabled = false;
         pickThemePanel.Hide();
         if (Controls.Contains(pickThemePanel))
            Controls.Remove(pickThemePanel);
         RestoreMain();
         RestoreWindow();
      }
      #endregion

      #region edit theme
      private void EditTheme(Theme pTheme) {
         Theme.Assignment(sTemporaryTheme, pTheme);
         RememberWindow();
         if (!Controls.Contains(editThemePanel))
            Controls.Add(editThemePanel);
         DisableMain();
         sEscapeFrom = EscapeFrom.ThemeEditor;
         LayoutEditThemePanel(pTheme);
         FitToDialog();
         editThemePanel.Enabled = true;
         editThemePanel.Show();
         editThemePanel.BringToFront();
         CenterControl(this, editThemePanel);
         editThemeCancelButton.Left = editThemePanel.Width - editThemeCancelButton.Width - sCancelOffset;
         CenterControlHorizontally(editThemePanel, editThemeTitleLabel);
         editThemeCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
         editThemePanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
         editThemeNameTextBox.Focus();
         editThemeNameTextBox.SelectAll();
      }

      private void LayoutEditThemePanel(Theme pTheme) {
         editThemeTextBoxGroupBox.Font = new Font(sCurrentTheme.mInterfaceFont.Name,
            sCurrentTheme.mInterfaceFont.SizeInPoints * 1.2f, FontStyle.Bold);
         editThemeTextBoxGroupBox.BackColor = Color.Gray;
         editThemeTextBoxGroupBox.ForeColor = Color.White;
         interfaceGroupBox.Font = new Font(sCurrentTheme.mInterfaceFont.Name,
            sCurrentTheme.mInterfaceFont.SizeInPoints * 1.2f, FontStyle.Bold);
         interfaceGroupBox.BackColor = Color.Gray;
         interfaceGroupBox.ForeColor = Color.White;
         editThemePanel.BackColor = sCurrentTheme.mPanelBackgroundColor;

         textBoxFontLabel.Text = string.Format("Font: {0}; size: {1}; style: {2}", pTheme.mTextBoxFont.Name,
            (int)pTheme.mTextBoxFont.SizeInPoints, pTheme.mTextBoxFont.Style);
         interfaceFontLabel.Text = string.Format("Font: {0}; size: {1}; style: {2}", pTheme.mInterfaceFont.Name,
            (int)pTheme.mInterfaceFont.SizeInPoints, pTheme.mInterfaceFont.Style);
         editThemeNameTextBox.Text = pTheme.mName;
         editThemeNameTextBox.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
         SizeTextBoxToFitString(out SizeF oSizeF, editThemeNameTextBox, pTheme.mName);
         editThemeNameTextBox.Size = new Size(Math.Max((int)oSizeF.Width, 200), (int)oSizeF.Height);

         getTextBoxFontColorPanel.ForeColor = pTheme.mTextBoxFontColor;
         getTextBoxBackgroundColorPanel.ForeColor = pTheme.mTextBoxBackgroundColor;
         getInterfaceFontColorPanel.ForeColor = pTheme.mInterfaceFontColor;
         getInterfaceBackgroundColorPanel.ForeColor = pTheme.mInterfaceBackgroundColor;
         getStatusBarBackgroundColorPanel.ForeColor = (pTheme.mStatusBarBackgroundColor);
         getPanelBackgroundColorPanel.ForeColor = (pTheme.mPanelBackgroundColor);

         editThemeTitleLabel.Font = new Font(pTheme.mInterfaceFont.Name, (pTheme.mInterfaceFont.SizeInPoints * 1.3f),
            pTheme.mInterfaceFont.Style);
         editThemeTitleLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         editThemeTitleLabel.BackColor = Color.Transparent;

         int buttonHeight = 0;
         foreach (Button button in editThemePanel.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
            buttonHeight = button.Height;
         }
         buttonHeight = Math.Max(buttonHeight, sToolbarScale);
         Size panelSize = new Size(buttonHeight, buttonHeight);
         getTextBoxFontColorPanel.Size = panelSize;
         getTextBoxBackgroundColorPanel.Size = panelSize;
         getInterfaceFontColorPanel.Size = panelSize;
         getInterfaceBackgroundColorPanel.Size = panelSize;
         getStatusBarBackgroundColorPanel.Size = panelSize;
         getPanelBackgroundColorPanel.Size = panelSize;

         foreach (Button button in editThemeTextBoxGroupBox.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         }
         foreach (Button button in interfaceGroupBox.Controls.OfType<Button>()) {
            button.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
            button.ForeColor = sCurrentTheme.mInterfaceFontColor;
            button.BackColor = sCurrentTheme.mInterfaceBackgroundColor;
         }
         textBoxFontLabel.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
         textBoxFontLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         textBoxFontLabel.BackColor = Color.Transparent;

         interfaceDetailsLabel.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
         interfaceDetailsLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         interfaceDetailsLabel.BackColor = Color.Transparent;

         interfaceFontLabel.Font = CreateNewFont(sCurrentTheme.mInterfaceFont);
         interfaceFontLabel.ForeColor = sCurrentTheme.mInterfaceFontColor;
         interfaceFontLabel.BackColor = Color.Transparent;

         FlatButton(editThemeNamePrefixButton);

         editThemeNamePrefixButton.Top = editThemeTitleLabel.Bottom + sWidgetBigVerticalOffset;
         editThemeNameTextBox.Location = new Point(editThemeNamePrefixButton.Right + sAssociatedTextBoxPostPrefixHorizontalSpace,
            editThemeNamePrefixButton.Top + sAssociatedTextBoxPostPrefixVerticalOffset);

         editThemeTextBoxGroupBox.Top = Math.Max(editThemeNamePrefixButton.Bottom, textBoxFontLabel.Bottom);
         textBoxFontLabel.Top = GetGroupBoxFirstLineOffset(editThemeTextBoxGroupBox);
         getTextBoxFontButton.Top = textBoxFontLabel.Bottom + sWidgetVerticalOffset;
         getTextBoxFontSizeButton.Top = Math.Max(textBoxFontLabel.Bottom, getTextBoxFontButton.Bottom) + sWidgetVerticalOffset;
         getTextBoxFontColorButton.Location = new Point(getTextBoxFontSizeButton.Right + sWidgetHorizontalSpace,
            getTextBoxFontSizeButton.Top);
         getTextBoxFontColorPanel.Location = new Point(getTextBoxFontColorButton.Right + sWidgetHorizontalSpace,
            getTextBoxFontColorButton.Top);
         getTextBoxBackgroundColorButton.Top = Math.Max(getTextBoxFontSizeButton.Bottom,
            Math.Max(getTextBoxFontColorButton.Bottom, getTextBoxFontColorPanel.Bottom)) + sWidgetVerticalOffset;
         getTextBoxBackgroundColorPanel.Location = new Point(getTextBoxBackgroundColorButton.Right + sWidgetHorizontalSpace,
            getTextBoxBackgroundColorButton.Top);
         SizeGroupBox(editThemeTextBoxGroupBox);

         interfaceGroupBox.Top = editThemeTextBoxGroupBox.Bottom + sWidgetBigVerticalOffset;
         interfaceDetailsLabel.Top = GetGroupBoxFirstLineOffset(interfaceGroupBox);
         interfaceFontLabel.Top = interfaceDetailsLabel.Bottom + sWidgetVerticalOffset;
         getInterfaceFontButton.Top = interfaceFontLabel.Bottom + sWidgetVerticalOffset;
         getInterfaceFontSizeButton.Top = Math.Max(interfaceFontLabel.Bottom, getInterfaceFontButton.Bottom) + sWidgetVerticalOffset;
         getInterfaceFontColorButton.Location = new Point(getInterfaceFontSizeButton.Right + sWidgetHorizontalSpace,
            getInterfaceFontSizeButton.Top);
         getInterfaceFontColorPanel.Location = new Point(getInterfaceFontColorButton.Right + sWidgetHorizontalSpace,
            getInterfaceFontColorButton.Top);
         getInterfaceBackgroundColorButton.Top = Math.Max(getInterfaceFontSizeButton.Bottom,
            Math.Max(getInterfaceFontColorButton.Bottom, getInterfaceFontColorPanel.Bottom)) + sWidgetVerticalOffset;
         getInterfaceBackgroundColorPanel.Location = new Point(getInterfaceBackgroundColorButton.Right + sWidgetHorizontalSpace,
            getInterfaceBackgroundColorButton.Top);
         SizeGroupBox(interfaceGroupBox);

         getStatusBarBackgroundColorButton.Top = interfaceGroupBox.Bottom + sWidgetBigVerticalOffset;
         getStatusBarBackgroundColorPanel.Location = new Point(getStatusBarBackgroundColorButton.Right + sWidgetHorizontalSpace,
            getStatusBarBackgroundColorButton.Top);
         getPanelBackgroundColorButton.Top = Math.Max(getStatusBarBackgroundColorButton.Bottom, getStatusBarBackgroundColorPanel.Bottom)
            + sWidgetBigVerticalOffset;
         getPanelBackgroundColorPanel.Location = new Point(getPanelBackgroundColorButton.Right + sWidgetHorizontalSpace,
            getPanelBackgroundColorButton.Top);
         editThemeOkayButton.Top = Math.Max(getPanelBackgroundColorButton.Bottom, getPanelBackgroundColorPanel.Bottom) +
            sWidgetBigVerticalOffset;
         editThemeCancelButton.Top = editThemeOkayButton.Top;
         SizePanel(editThemePanel);
      }

      private void CloseEditTheme() {
         sEscapeFrom = EscapeFrom.Main;
         editThemePanel.Enabled = false;
         editThemePanel.Hide();
         if (Controls.Contains(editThemePanel))
            Controls.Remove(editThemePanel);
         RestoreMain();
         RestoreWindow();
      }

      private void EditThemeCancelButton_Click(object pSender, EventArgs pE) {
         CloseEditTheme();
      }

      private void EditThemeOkayButton_Click(object pSender, EventArgs pE) {
         Theme.Assignment(sCurrentTheme, sTemporaryTheme);
         CloseEditTheme();
         ColorizeGui();
      }

      private void EditThemeNamePrefixButton_Click(object pSender, EventArgs pE) {
         editThemeNameTextBox.Focus();
         editThemeNameTextBox.SelectAll();
      }

      private void GetTextBoxFontButton_Click(object pSender, EventArgs pE) {
         fontDialog.ShowColor = true;
         fontDialog.Font = textBox.Font;
         fontDialog.Color = textBox.ForeColor;
         if (fontDialog.ShowDialog() != DialogResult.Cancel) {
            sTemporaryTheme.mTextBoxFont = CreateNewFont(fontDialog.Font);
            sTemporaryTheme.mTextBoxFontColor = fontDialog.Color;
            textBoxFontLabel.Text = string.Format("Font: {0}; size: {1}; style: {2}", sTemporaryTheme.mTextBoxFont.Name,
            (int)sTemporaryTheme.mTextBoxFont.SizeInPoints, sTemporaryTheme.mTextBoxFont.Style);
         }
      }

      private void GetTextBoxFontSizeButton_Click(object pSender, EventArgs pE) {
         sFontSizeUsage = FontSizeUsage.TemporaryTextBox;
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

      private void GetTextBoxFontColor() {
         colorDialog.Color = textBox.ForeColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            sTemporaryTheme.mTextBoxFontColor = colorDialog.Color;
            getTextBoxFontColorPanel.BackColor = colorDialog.Color;
         }
      }

      private void GetTextBoxFontColorButton_Click(object pSender, EventArgs pE) {
         GetTextBoxFontColor();
      }

      private void GetTextBoxFontColorPanel_Click(object pSender, EventArgs pE) {
         GetTextBoxFontColor();
      }

      private void GetTextBoxBackgroundColor() {
         colorDialog.Color = textBox.BackColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            sTemporaryTheme.mTextBoxBackgroundColor = colorDialog.Color;
            getTextBoxBackgroundColorPanel.BackColor = colorDialog.Color;
         }
      }

      private void GetTextBoxBackgroundColorButton_Click(object pSender, EventArgs pE) {
         GetTextBoxBackgroundColor();
      }

      private void GetTextBoxBackgroundColorPanel_Click(object pSender, EventArgs pE) {
         GetTextBoxBackgroundColor();
      }

      private void GetInterfaceFontButton_Click(object pSender, EventArgs pE) {
         fontDialog.ShowColor = true;
         fontDialog.Font = textBox.Font;
         fontDialog.Color = textBox.ForeColor;
         if (fontDialog.ShowDialog() != DialogResult.Cancel) {
            sTemporaryTheme.mTextBoxFont = CreateNewFont(fontDialog.Font);
            sTemporaryTheme.mTextBoxFontColor = fontDialog.Color;
            interfaceFontLabel.Text = string.Format("Font: {0}; size: {1}; style: {2}", sTemporaryTheme.mInterfaceFont.Name,
             (int)sTemporaryTheme.mInterfaceFont.SizeInPoints, sTemporaryTheme.mInterfaceFont.Style);
         }
      }

      private void GetInterfaceFontSizeButton_Click(object pSender, EventArgs pE) {
         sFontSizeUsage = FontSizeUsage.TemporaryInterface;
         if (!Controls.Contains(fontSizePanel))
            Controls.Add(fontSizePanel);
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

      private void GetInterfaceFontColor() {
         colorDialog.Color = menuStrip.ForeColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            sTemporaryTheme.mInterfaceFontColor = colorDialog.Color;
            getInterfaceFontColorPanel.BackColor = colorDialog.Color;
         }
      }

      private void GetInterfaceFontColorButton_Click(object pSender, EventArgs pE) {
         GetInterfaceFontColor();
      }

      private void GetInterfaceFontColorPanel_Click(object pSender, EventArgs pE) {
         GetInterfaceFontColor();
      }

      private void GetInterfaceBackgroundColor() {
         colorDialog.Color = menuStrip.BackColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            sTemporaryTheme.mInterfaceBackgroundColor = colorDialog.Color;
            getInterfaceBackgroundColorPanel.BackColor = colorDialog.Color;
         }
      }

      private void GetInterfaceBackgroundColorButton_Click(object pSender, EventArgs pE) {
         GetInterfaceBackgroundColor();
      }

      private void GetInterfaceBackgroundColorPanel_Click(object pSender, EventArgs pE) {
         GetInterfaceBackgroundColor();
      }

      private void GetStatusBarBackgroundColor() {
         colorDialog.Color = statusStrip.BackColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            sTemporaryTheme.mStatusBarBackgroundColor = colorDialog.Color;
            getStatusBarBackgroundColorPanel.BackColor = colorDialog.Color;
         }
      }

      private void GetStatusBarBackgroundColorButton_Click(object pSender, EventArgs pE) {
         GetStatusBarBackgroundColor();
      }

      private void GetStatusBarBackgroundColorPanel_Click(object pSender, EventArgs pE) {
         GetStatusBarBackgroundColor();
      }

      private void GetPanelBackgroundColor() {
         colorDialog.Color = goPanel.BackColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            sTemporaryTheme.mPanelBackgroundColor = colorDialog.Color;
            getPanelBackgroundColorPanel.BackColor = colorDialog.Color;
         }
      }

      private void GetPanelBackgroundColorButton_Click(object pSender, EventArgs pE) {
         GetPanelBackgroundColor();
      }

      private void GetPanelBackgroundColorPanel_Click(object pSender, EventArgs pE) {
         GetPanelBackgroundColor();
      }
      #endregion

      #region theme file procedures
      private static void SaveThemes() {
         try {
            sThemeList.RemoveAll(pX => pX.mName == sDefaultThemeName);
            if (sThemeList.Count >= 2) {
               sThemeList.RemoveAt(0);//Dark            
               sThemeList.RemoveAt(0);//Light
            }
            sCurrentTheme.mName = sDefaultThemeName;
            sThemeList.Insert(0, new Theme(sCurrentTheme));

            Directory.CreateDirectory(sMyDocumentsEasyPadData);
            if (File.Exists(sMyDocumentsEasyPadData + sThemesFile))
               File.Delete(sMyDocumentsEasyPadData + sThemesFile);
            using (StreamWriter writer = new StreamWriter(sMyDocumentsEasyPadData + sThemesFile)) {
               writer.WriteLine(sThemesHeader);
               writer.WriteLine(string.Format("{0}{1}", sThemesVersion, THEMES_VERSION));
               writer.WriteLine(string.Format("{0}{1}", sNumberOfThemes, sThemeList.Count));
               writer.WriteLine(string.Empty);

               foreach (Theme theme in sThemeList)
                  theme.Write(writer);
               writer.WriteLine(string.Empty);
            }
         }
         catch (Exception pException) {
            TimedMessage("SaveThemes threw an exception" + Environment.NewLine + pException.ToString(),
               "EXCEPTION", 0);
         }
      }

      private void LoadThemes() {
         string errorMessage = string.Empty;
         try {
            if (File.Exists(sMyDocumentsEasyPadData + sThemesFile)) {
               using (StreamReader reader = new StreamReader(sMyDocumentsEasyPadData + sThemesFile)) {
                  string line = reader.ReadLine();
                  if (!string.Equals(line, sThemesHeader, StringComparison.OrdinalIgnoreCase)) {
                     errorMessage = string.Format("The Themes header was invalid.{0}{1}",
                        Environment.NewLine, line);
                     goto ERROR;
                  }
                  line = reader.ReadLine();
                  if (!line.StartsWith(sThemesVersion)) {
                     errorMessage = string.Format("The Themes version header was invalid.{0}{1}",
                        Environment.NewLine, line);
                     goto ERROR;
                  }
                  line = line.Replace(sThemesVersion, string.Empty);
                  if (!int.TryParse(line, out int oInteger)) {
                     errorMessage = string.Format("The Themes version number could not be parsed.{0}{1}",
                        Environment.NewLine, line);
                     goto ERROR;
                  }
                  if (oInteger != THEMES_VERSION) {
                     errorMessage = string.Format("The Themes version number{0}{1}{0} was not: {2}.",
                        Environment.NewLine, line, THEMES_VERSION);
                     goto ERROR;
                  }
                  line = reader.ReadLine();
                  if (!line.StartsWith(sNumberOfThemes)) {
                     errorMessage = string.Format("The Themes count header was invalid.{0}{1}",
                        Environment.NewLine, line);
                     goto ERROR;
                  }
                  line = line.Replace(sNumberOfThemes, string.Empty);
                  if (!int.TryParse(line, out oInteger)) {
                     errorMessage = string.Format("The Themes count could not be parsed.{0}{1}",
                        Environment.NewLine, line);
                     goto ERROR;
                  }
                  line = reader.ReadLine();
                  if (oInteger > 0) {
                     for (int i = 0; i < oInteger; i++) {
                        if (!ReadTheme(reader)) {
                           errorMessage = string.Format("A Theme could not be read from file.{0}Failed at count: {1}",
                              Environment.NewLine, i);
                           goto ERROR;
                        }
                     }
                     sCurrentTheme = sThemeList[CURRENT_THEME];
                  }
               }
            }
         }
         catch (Exception pException) {
            TimedMessage("LoadThemes threw an exception" + Environment.NewLine + pException.ToString(),
               "EXCEPTION", 0);
         }
         return;
ERROR:
         TimedMessage(errorMessage, "ERROR", 0);
      }

      private bool ReadTheme(StreamReader pReader) {
         string errorMessage = string.Empty, name = string.Empty;
         Color textColor, textBackgroundColor, interfaceTextColor, interfaceBackgroundColor, statusBarBackgroundGround,
            panelBackgroundColor;
         FontConverter? converter = new FontConverter();
         Font textFont, interfaceFont;

         try {
            string line = pReader.ReadLine();
            if (!line.StartsWith(sThemeNamePrefix))
               return false;
            line = line.Replace(sThemeNamePrefix, string.Empty);
            name = line;

            line = pReader.ReadLine();
            line = line.Replace(sTextBoxFontFamily, string.Empty);
            textFont = CreateNewFont(converter.ConvertFromInvariantString(line) as Font);
            line = pReader.ReadLine();
            line = line.Replace(sInterfaceFontFamily, string.Empty);
            interfaceFont = CreateNewFont(converter.ConvertFromInvariantString(line) as Font);

            line = pReader.ReadLine();
            if (!line.StartsWith(sTextBoxFontColor))
               return false;
            line = line.Replace(sTextBoxFontColor, string.Empty);
            //DEBUG efm5 2024 04 6 use try parse
            textColor = ColorTranslator.FromWin32(int.Parse(line));
            line = pReader.ReadLine();
            if (!line.StartsWith(sTextBoxBackgroundColor))
               return false;
            line = line.Replace(sTextBoxBackgroundColor, string.Empty);
            textBackgroundColor = ColorTranslator.FromWin32(int.Parse(line));
            line = pReader.ReadLine();
            if (!line.StartsWith(sInterfaceFontColor))
               return false;
            line = line.Replace(sInterfaceFontColor, string.Empty);
            interfaceTextColor = ColorTranslator.FromWin32(int.Parse(line));
            line = pReader.ReadLine();
            if (!line.StartsWith(sInterfaceBackgroundColor))
               return false;
            line = line.Replace(sInterfaceBackgroundColor, string.Empty);
            interfaceBackgroundColor = ColorTranslator.FromWin32(int.Parse(line));
            line = pReader.ReadLine();
            if (!line.StartsWith(sStatusBarBackgroundColor))
               return false;
            line = line.Replace(sStatusBarBackgroundColor, string.Empty);
            statusBarBackgroundGround = ColorTranslator.FromWin32(int.Parse(line));
            line = pReader.ReadLine();
            if (!line.StartsWith(sPanelBackgroundColor))
               return false;
            line = line.Replace(sPanelBackgroundColor, string.Empty);
            panelBackgroundColor = ColorTranslator.FromWin32(int.Parse(line));
            line = pReader.ReadLine();

            sThemeList.Add(new Theme(name, textFont, interfaceFont,
                textColor, textBackgroundColor, interfaceTextColor,
                interfaceBackgroundColor, statusBarBackgroundGround,
                panelBackgroundColor));
            ToolStripMenuItem tsmi = new ToolStripMenuItem() {
               Text = name,
               Name = name.Replace(" ", "") + "TSMI",
               Font = CreateNewFont(menuStrip.Font),
               ForeColor = menuStrip.ForeColor,
               BackColor = menuStrip.BackColor,
               Checked = false
            };
            tsmi.Click += new EventHandler(Theme_Click);
            themesTSMI.DropDownItems.Add(tsmi);
            return true;
         }
         catch (Exception pException) {
            TimedMessage("CreateTheme threw an exception" + Environment.NewLine + pException.ToString(),
               "EXCEPTION", 0);
            return false;
         }
      }

      private void CheckCurrentTheme() {
         foreach (ToolStripMenuItem tsmi in themesTSMI.DropDownItems.OfType<ToolStripMenuItem>())
            tsmi.Checked = false;
         foreach (ToolStripMenuItem tsmi in themesTSMI.DropDownItems.OfType<ToolStripMenuItem>()) {
            if (string.Equals(tsmi.Text, sCurrentTheme.mName, StringComparison.OrdinalIgnoreCase)) {
               tsmi.Checked = true;
               return;
            }
         }
      }
      #endregion
   }
}