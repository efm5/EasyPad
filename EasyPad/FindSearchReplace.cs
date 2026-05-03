using System.Diagnostics;
using System.Text.RegularExpressions;
using EasyPad.Properties;

namespace EasyPad {
    public partial class EasyPadForm : Form {
        #region handlers
        private void SearchWithTSMI_Click(object pSender, EventArgs pE) {
            try {
                Process process = new Process {
                    StartInfo = new ProcessStartInfo(@"https://www.google.com/advanced_search") {
                        UseShellExecute = true
                    }
                };
                process.Start();
                Thread.Sleep(100);
            }
            catch (Exception pException) {
                TimedMessage("Search With threw an exception" + Environment.NewLine + pException.ToString(),
                   "ERROR", 0);
            }
        }

        #region Find dialog
        private void FindWhatPrefixButton_Click(object pSender, EventArgs pE) {
            findTextBox.Focus();
            findTextBox.SelectAll();
        }

        private void FindCancelButton_Click(object pSender, EventArgs pE) {
            CloseFind();
            RestoreMain();
            sSelectionStart = textBox.SelectionStart;
            sSelectionLength = textBox.SelectionLength;
        }

        private void FindButton_Click(object pSender, EventArgs pE) {
            if (FindAndSelect(findTextBox.Text)) {
                CloseFind();
                textBox.ScrollToCaret();
                if (findVerboseCheckBox.Checked)
                    TimedMessage(string.Format("“{0}” was found {1} time(s).", findTextBox.Text, sMatches.Count));
            }
            else {
                TimedMessage(string.Format("The text “{0}” could not be found.", findTextBox.Text), "WARNING", 3000);
                findTextBox.Focus();
                findTextBox.SelectAll();
            }
        }

        private void FindTSMI_Click(object pSender, EventArgs pE) {
            if (string.IsNullOrEmpty(textBox.Text))
                return;
            if (textBox.SelectionLength > 0)
                findTextBox.Text = textBox.SelectedText;
            else if (string.IsNullOrEmpty(findTextBox.Text))
                findTextBox.Text = Settings.Default.FindString;
            RememberWindow();
            sSelectionStart = textBox.SelectionStart;
            sSelectionLength = textBox.SelectionLength;
            if (!Controls.Contains(findPanel))
                Controls.Add(findPanel);
            DisableMain();
            sEscapeFrom = EscapeFrom.Find;
            LayoutFindPanel();
            FitToDialog();
            findPanel.Enabled = true;
            findPanel.Show();
            findPanel.BringToFront();
            CenterControl(this, findPanel);
            findTextBox.Focus();
            findTextBox.SelectAll();
        }

        private void FindNextTSMI_Click(object pSender, EventArgs pE) {
            sFindPosition++;
            if (sFindPosition == sMatches.Count) {
                sFindPosition--;
                TimedMessage("Sorry, there is nothing (going forward) left to find.", "Nothing To Find");
                return;
            }
            if (!string.IsNullOrEmpty(sMatches[sFindPosition].ToString())) {
                textBox.SelectionStart = sMatches[sFindPosition].Index;
                textBox.SelectionLength = sMatches[sFindPosition].ToString().Length;
                textBox.ScrollToCaret();
            }
        }

        private void FindPreviousTSMI_Click(object pSender, EventArgs pE) {
            sFindPosition--;
            if (sFindPosition == -1) {
                sFindPosition++;
                TimedMessage("Sorry, there is nothing (going backward) left to find.", "Nothing To Find");
                return;
            }
            if (!string.IsNullOrEmpty(sMatches[sFindPosition].ToString())) {
                textBox.SelectionStart = sMatches[sFindPosition].Index;
                textBox.SelectionLength = sMatches[sFindPosition].ToString().Length;
                textBox.ScrollToCaret();
            }
        }
        #endregion

        #region Search Replace dialog
        private void SearchSearchPrefixButton_Click(object pSender, EventArgs pE) {
            searchTextBox.Focus();
            searchTextBox.SelectAll();
        }

        private void SearchReplacePrefixButton_Click(object pSender, EventArgs pE) {
            replaceTextBox.Focus();
            replaceTextBox.SelectAll();
        }

        private void SearchCancelButton_Click(object pSender, EventArgs pE) {
            CloseReplace();
            RestoreMain();
            sSelectionStart = textBox.SelectionStart;
            sSelectionLength = textBox.SelectionLength;
        }

        private void SearchReplaceButton_Click(object pSender, EventArgs pE) {
            CloseReplace();
            ReplaceFirst();
        }

        private void SearchReplaceAllButton_Click(object pSender, EventArgs pE) {
            CloseReplace();
            ReplaceAll();
            if (replaceVerboseCheckBox.Checked)
                TimedMessage(string.Format("“{0}” was replaced {1} time(s) with: “{2}”.", searchTextBox.Text, sMatches.Count,
                   replaceTextBox.Text));
        }

        private void ReplaceTSMI_Click(object pSender, EventArgs pE) {
            if (string.IsNullOrEmpty(textBox.Text))
                return;
            if (textBox.SelectionLength > 0)
                searchTextBox.Text = textBox.SelectedText;
            else if (string.IsNullOrEmpty(searchTextBox.Text))
                searchTextBox.Text = Settings.Default.ReplaceSearchString;
            if (Clipboard.ContainsText())
                replaceTextBox.Text = Clipboard.GetText();
            else if (string.IsNullOrEmpty(replaceTextBox.Text))
                replaceTextBox.Text = Settings.Default.ReplaceReplaceString;
            RememberWindow();
            sSelectionStart = textBox.SelectionStart;
            sSelectionLength = textBox.SelectionLength;
            if (!Controls.Contains(replacePanel))
                Controls.Add(replacePanel);
            DisableMain();
            sEscapeFrom = EscapeFrom.Replace;
            LayoutReplacePanel();
            FitToDialog();
            replacePanel.Enabled = true;
            replacePanel.Show();
            replacePanel.BringToFront();
            CenterControl(this, replacePanel);
            searchTextBox.Focus();
            searchTextBox.SelectAll();
        }
        #endregion
        #endregion

        #region find procedures
        private void LayoutFindPanel() {
            findVerboseCheckBox.Checked = Settings.Default.VerboseFind;
            controlList.Clear();
            findWhatPrefixButton.Top = findTitleLabel.Bottom + (sWidgetBigVerticalOffset * 2);
            SizeTextBoxToFitString(out SizeF sizeF, findTextBox);
            findTextBox.Size = new Size(FIND_WIDTH, (int)sizeF.Height);
            findTextBox.Location = new Point(findWhatPrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
              findWhatPrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
            findButton.Location = new Point(findTextBox.Right + sWidgetBigHorizontalSpace,
               findTextBox.Top + sAssociatedButtonPostTextBoxVerticalOffset);
            controlList.Add(findWhatPrefixButton);
            controlList.Add(findTextBox);
            findCheckBoxGroupBox.Location = new Point(sIndent, Bottommost(controlList) + (sWidgetBigVerticalOffset * 2));
            findMatchCaseCheckBox.Location = new Point(sGroupLeftPad, GetGroupBoxFirstLineOffset(findCheckBoxGroupBox));
            findWholeWordCheckBox.Location = new Point(sGroupLeftPad, findMatchCaseCheckBox.Bottom + sWidgetVerticalOffset);
            findRegularExpressionsCheckBox.Location = new Point(sGroupLeftPad, findWholeWordCheckBox.Bottom + sWidgetBigVerticalOffset);
            findVerboseCheckBox.Location = new Point(sGroupLeftPad, findRegularExpressionsCheckBox.Bottom + sWidgetBigVerticalOffset);
            SizeGroupBox(findCheckBoxGroupBox);
            findScopeGroupBox.Location = new Point(findCheckBoxGroupBox.Right + (sWidgetBigVerticalOffset * 2),
               findCheckBoxGroupBox.Top);
            findSelectionRadioButton.Location = new Point(sGroupLeftPad, GetGroupBoxFirstLineOffset(findScopeGroupBox));
            findGlobalRadioButton.Location = new Point(sGroupLeftPad, findSelectionRadioButton.Bottom + sWidgetVerticalOffset);
            SizeGroupBox(findScopeGroupBox);
            int cancelLeft = Math.Max(findScopeGroupBox.Right, findButton.Right);
            findCancelButton.Location = new Point(cancelLeft - findCancelButton.Width, findScopeGroupBox.Bottom + sCancelOffset);
            SizePanel(findPanel);
            CenterControlHorizontally(findPanel, findTitleLabel);
        }

        private void CloseFind() {
            Settings.Default.VerboseFind = findVerboseCheckBox.Checked;
            sEscapeFrom = EscapeFrom.Main;
            findPanel.Enabled = false;
            findPanel.Hide();
            if (Controls.Contains(findPanel))
                Controls.Remove(findPanel);
            RestoreMain();
            RestoreWindow();
            textBox.SelectionStart = sSelectionStart;
            textBox.SelectionLength = sSelectionLength;
        }

        private bool FindAndSelect(string pTextToFind) {
            string pattern = pTextToFind;
            RegexOptions regexOptions = RegexOptions.None;

            if (!findMatchCaseCheckBox.Checked)
                regexOptions |= RegexOptions.IgnoreCase;
            if (findRegularExpressionsCheckBox.Checked) {
                if (findWholeWordCheckBox.Checked)
                    pattern = @"\b" + pattern + @"\b";
            }
            else {
                if (findWholeWordCheckBox.Checked)
                    pattern = @"\b(" + Regex.Escape(pattern) + @")\b";
            }
            sMatches = Regex.Matches(textBox.Text, pattern, regexOptions);
            if (findSelectionRadioButton.Checked) {
                foreach (Match match in sMatches) {
                    if (match.Index >= textBox.SelectionStart) {
                        sSelectionStart = match.Index;
                        sSelectionLength = match.ToString().Length;
                        sFindPosition = match.Index;
                        return true;
                    }
                }
                return false;
            }
            else {//Global
                if (sMatches.Count > 0) {
                    sSelectionStart = sMatches[0].Index;
                    sSelectionLength = sMatches[0].ToString().Length;
                    sFindPosition = 0;
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region replace procedures
        private void LayoutReplacePanel() {
            replaceVerboseCheckBox.Checked = Settings.Default.VerboseReplace;
            controlList.Clear();
            searchSearchPrefixButton.Top = replaceTitleLabel.Bottom + (sWidgetBigVerticalOffset * 2);
            SizeTextBoxToFitString(out SizeF sizeFS, searchTextBox);
            searchTextBox.Size = new Size(FIND_WIDTH, (int)sizeFS.Height);
            searchTextBox.Location = new Point(searchSearchPrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
              searchSearchPrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
            replaceButton.Location = new Point(searchTextBox.Right + sWidgetBigHorizontalSpace,
               searchTextBox.Top + sAssociatedButtonPostTextBoxVerticalOffset);
            controlList.Add(searchSearchPrefixButton);
            controlList.Add(searchTextBox);
            searchReplacePrefixButton.Top = Bottommost(controlList) + sWidgetBigVerticalOffset;
            SizeTextBoxToFitString(out SizeF sizeFR, replaceTextBox);
            replaceTextBox.Size = new Size(FIND_WIDTH, (int)sizeFR.Height);
            replaceTextBox.Location = new Point(searchReplacePrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
              searchReplacePrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
            replaceAllButton.Location = new Point(replaceTextBox.Right + sWidgetBigHorizontalSpace,
               replaceTextBox.Top + sAssociatedButtonPostTextBoxVerticalOffset);
            controlList.Add(searchReplacePrefixButton);
            controlList.Add(replaceTextBox);
            replaceCheckBoxGroupBox.Location = new Point(sIndent, Bottommost(controlList) + (sWidgetBigVerticalOffset * 2));
            replaceMatchCaseCheckBox.Location = new Point(sGroupLeftPad, GetGroupBoxFirstLineOffset(replaceCheckBoxGroupBox));
            replaceWholeWordCheckBox.Location = new Point(sGroupLeftPad, replaceMatchCaseCheckBox.Bottom + sWidgetVerticalOffset);
            replaceRegularExpressionsCheckBox.Location = new Point(sGroupLeftPad, replaceWholeWordCheckBox.Bottom + sWidgetBigVerticalOffset);
            replaceVerboseCheckBox.Location = new Point(sGroupLeftPad, replaceRegularExpressionsCheckBox.Bottom + sWidgetBigVerticalOffset);
            SizeGroupBox(replaceCheckBoxGroupBox);
            replaceScopeGroupBox.Location = new Point(replaceCheckBoxGroupBox.Right + (sWidgetBigVerticalOffset * 2), replaceCheckBoxGroupBox.Top);
            replaceSelectionRadioButton.Location = new Point(sGroupLeftPad, GetGroupBoxFirstLineOffset(replaceScopeGroupBox));
            replaceGlobalRadioButton.Location = new Point(sGroupLeftPad, replaceSelectionRadioButton.Bottom + sWidgetVerticalOffset);
            SizeGroupBox(replaceScopeGroupBox);
            int cancelLeft = Math.Max(replaceScopeGroupBox.Right, Math.Max(replaceButton.Right, replaceAllButton.Right));
            replaceCancelButton.Location = new Point(cancelLeft - replaceCancelButton.Width, replaceScopeGroupBox.Bottom + sCancelOffset);
            SizePanel(replacePanel);
            CenterControlHorizontally(replacePanel, replaceTitleLabel);
        }

        private void CloseReplace() {
            Settings.Default.VerboseReplace = replaceVerboseCheckBox.Checked;
            sEscapeFrom = EscapeFrom.Main;
            replacePanel.Enabled = false;
            replacePanel.Hide();
            if (Controls.Contains(replacePanel))
                Controls.Remove(replacePanel);
            RestoreMain();
            RestoreWindow();
            textBox.SelectionStart = sSelectionStart;
            textBox.SelectionLength = sSelectionLength;
        }

        private void ReplaceFirst() {
            string pattern = searchTextBox.Text, replacement = replaceTextBox.Text;
            RegexOptions regexOptions = RegexOptions.None;

            if (!replaceMatchCaseCheckBox.Checked)
                regexOptions |= RegexOptions.IgnoreCase;
            if (replaceRegularExpressionsCheckBox.Checked) {
                if (replaceWholeWordCheckBox.Checked)
                    pattern = @"\b" + pattern + @"\b";
            }
            else {
                if (replaceWholeWordCheckBox.Checked)
                    pattern = @"\b(" + Regex.Escape(pattern) + @")\b";
            }
            if (replaceGlobalRadioButton.Checked) {
                Regex regex = new Regex(textBox.Text, regexOptions);
                textBox.Text = regex.Replace(pattern, replacement, 1);
            }
            else {//efm5 Selected
                Regex regex = new Regex(textBox.SelectedText, regexOptions);
                textBox.SelectedText = regex.Replace(pattern, replacement, 1);
            }
        }

        private void ReplaceAll() {
            string pattern = searchTextBox.Text, replacement = replaceTextBox.Text;
            RegexOptions regexOptions = RegexOptions.None;

            if (!replaceMatchCaseCheckBox.Checked)
                regexOptions |= RegexOptions.IgnoreCase;
            if (replaceRegularExpressionsCheckBox.Checked) {
                if (replaceWholeWordCheckBox.Checked)
                    pattern = @"\b" + pattern + @"\b";
            }
            else {
                if (replaceWholeWordCheckBox.Checked)
                    pattern = @"\b(" + Regex.Escape(pattern) + @")\b";
            }
            if (replaceGlobalRadioButton.Checked)
                textBox.Text = Regex.Replace(textBox.Text, pattern, replacement, regexOptions);
            else
                textBox.SelectedText = Regex.Replace(textBox.SelectedText, pattern, replacement, regexOptions);
        }
        #endregion
    }
}