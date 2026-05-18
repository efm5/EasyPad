using System.Diagnostics;
using System.Drawing.Printing;
using EasyPad.Properties;
using Microsoft.WindowsAPICodePack.Dialogs.Controls;
using static EasyPad.Program.NativeMethods;

namespace EasyPad {
   public partial class EasyPadForm : Form {
      #region Form
      private void EasyPadForm_Shown(object pSender, EventArgs pE) {
         Rectangle screenRectangle = RectangleToScreen(ClientRectangle);
         Screen fromscreen = Screen.FromPoint(Settings.Default.Location);

         if (File.Exists(sRecentFileHistoryPath))
            LoadFileHistory();
         LoadThemes();
         MakeTransparent();
         sTitleBarHeight = screenRectangle.Top - Top;
         WindowState = (FormWindowState)Settings.Default.WindowState;
         if (WindowState == FormWindowState.Maximized) {
            Location = Settings.Default.RestoreLocation;
            Size = Settings.Default.RestoreSize;
         }
         else {
            Location = Settings.Default.Location;
            Size = Settings.Default.Size;
         }
         textBox.MouseWheel += new MouseEventHandler(MouseWheeling);
         System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
         FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
         versionStatusLabel.Text = "  version: " + fvi.FileVersion;
         showToolBarTSMI.Checked = Settings.Default.ShowToolBar;
         toolBarShowToolsTSMI.Checked = Settings.Default.ShowTools;
         toolBarShowScrollersTSMI.Checked = Settings.Default.ShowScrollers;
         wordWrapTSMI.Checked = Settings.Default.WordWrap;
         fadeInTSMI.Checked = Settings.Default.Fade;
         wordWrapTSMI.Checked = Settings.Default.WordWrap;
         alwaysOnTopTSMI.Checked = Settings.Default.AlwaysOnTop;
         showStatusBarTSMI.Checked = Settings.Default.ShowStatusBar;
         findMatchCaseCheckBox.Checked = Settings.Default.FindMatchCaseChecked;
         findRegularExpressionsCheckBox.Checked = Settings.Default.FindRegExChecked;
         findWholeWordCheckBox.Checked = Settings.Default.FindWholeWordChecked;
         findTextBox.Text = Settings.Default.FindString;
         replaceMatchCaseCheckBox.Checked = Settings.Default.ReplaceMatchCaseChecked;
         replaceRegularExpressionsCheckBox.Checked = Settings.Default.ReplaceRegExChecked;
         replaceWholeWordCheckBox.Checked = Settings.Default.ReplaceWholeWordChecked;
         searchTextBox.Text = Settings.Default.ReplaceSearchString;
         replaceTextBox.Text = Settings.Default.ReplaceReplaceString;
         allIfNothingTSMI.Checked = Settings.Default.AllIfNothing;
         toolStripContainer.TopToolStripPanel.AutoSize = true;
         toolStripContainer.BottomToolStripPanel.AutoSize = true;
         toolStripContainer.LeftToolStripPanel.AutoSize = true;
         toolStripContainer.RightToolStripPanel.AutoSize = true;
         LayoutMain();
         textBox.Enter += (pSender, pE) => {
            this.BeginInvoke(new Action(() => {
               textBox.SelectAll();
            }));
         };
      }

      private async void EasyPad_FormClosing(object pSender, FormClosingEventArgs pE) {
         bool dirty = false;
         if ((textBox.Modified == true) && (textBox.Text.Length > 0) &&
            !string.Equals(textBox.Text, sImportedText, StringComparison.Ordinal))
            dirty = true;
         if (!string.IsNullOrEmpty(textBox.Text) && dirty) {
            //efm5 this one must remain a NET MessageBox because the questionResponsePanel is unavailable by now
            DialogResult dialogResult = MessageBox.Show("The text box is not empty." +
               Environment.NewLine +
               "You might want to save; otherwise there might be a loss of data." +
               Environment.NewLine + Environment.NewLine +
               "Do you want to save before exiting?",
                "Unsaved Text", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes) {
               Save();
            }
            else if (dialogResult == DialogResult.Cancel) {
               pE.Cancel = true;
               return;
            }
         }
         SaveThemes();
         Settings.Default.WindowState = (int)WindowState;
         if (WindowState == FormWindowState.Maximized) {
            Settings.Default.Location = RestoreBounds.Location;
            Settings.Default.Size = RestoreBounds.Size;
            Settings.Default.RestoreLocation = RestoreBounds.Location;
            Settings.Default.RestoreSize = RestoreBounds.Size;
         }
         else {
            Settings.Default.Location = Location;
            Settings.Default.Size = Size;
            Settings.Default.RestoreLocation = Location;
            Settings.Default.RestoreSize = Size;
         }
         Settings.Default.Zoom = textBox.Font.SizeInPoints;
         Settings.Default.Fade = fadeInTSMI.Checked;
         Settings.Default.WordWrap = wordWrapTSMI.Checked;
         Settings.Default.StoredThemeName = sCurrentTheme.mName;
         Settings.Default.AlwaysOnTop = alwaysOnTopTSMI.Checked;
         Settings.Default.ShowStatusBar = showStatusBarTSMI.Checked;
         Settings.Default.FindScopeGlobal = findGlobalRadioButton.Checked;
         Settings.Default.FindMatchCaseChecked = findMatchCaseCheckBox.Checked;
         Settings.Default.FindRegExChecked = findRegularExpressionsCheckBox.Checked;
         Settings.Default.FindWholeWordChecked = findWholeWordCheckBox.Checked;
         Settings.Default.FindString = findTextBox.Text;
         Settings.Default.ReplaceScopeGlobal = replaceGlobalRadioButton.Checked;
         Settings.Default.ReplaceMatchCaseChecked = replaceMatchCaseCheckBox.Checked;
         Settings.Default.ReplaceRegExChecked = replaceRegularExpressionsCheckBox.Checked;
         Settings.Default.ReplaceWholeWordChecked = replaceWholeWordCheckBox.Checked;
         Settings.Default.ReplaceSearchString = searchTextBox.Text;
         Settings.Default.ReplaceReplaceString = replaceTextBox.Text;
         Settings.Default.ShowToolBar = showToolBarTSMI.Checked;
         Settings.Default.ShowTools = toolBarShowToolsTSMI.Checked;
         Settings.Default.ShowScrollers = toolBarShowScrollersTSMI.Checked;
         Settings.Default.TextFont = CreateNewFont(textBox.Font);
         Settings.Default.TextColor = textBox.ForeColor;
         Settings.Default.TextBackgroundColor = textBox.BackColor;
         Settings.Default.InterfaceFont = CreateNewFont(menuStrip.Font);
         Settings.Default.InterfaceFontColor = menuStrip.ForeColor;
         Settings.Default.InterfaceBackgroundColor = menuStrip.BackColor;
         Settings.Default.TextEncoding = (int)sTextEncoding;
         Settings.Default.ToolBarLocation = (int)sToolBarLocation;
         if (toolStrip.ImageScalingSize.Width == (int)ToolBarSize.Twenty_Four)
            Settings.Default.ToolBarSize = (int)ToolBarSize.Twenty_Four;
         else if (toolStrip.ImageScalingSize.Width == (int)ToolBarSize.Thirty_Two)
            Settings.Default.ToolBarSize = (int)ToolBarSize.Thirty_Two;
         else if (toolStrip.ImageScalingSize.Width == (int)ToolBarSize.Sixty_Four)
            Settings.Default.ToolBarSize = (int)ToolBarSize.Sixty_Four;
         else if (toolStrip.ImageScalingSize.Width == (int)ToolBarSize.One_Hundred_Twenty_Eight)
            Settings.Default.ToolBarSize = (int)ToolBarSize.One_Hundred_Twenty_Eight;
         else
            Settings.Default.ToolBarSize = (int)ToolBarSize.Sixteen;
         Settings.Default.Save();

         #region disposing
         sTemporaryTheme?.Dispose();
         sDarkTheme?.Dispose();
         sLightTheme?.Dispose();
         editThemeOkayButton?.Dispose();
         editThemeCancelButton?.Dispose();
         editThemeTitleLabel?.Dispose();
         editThemeNamePrefixButton?.Dispose();
         editThemeNameTextBox?.Dispose();
         textBoxFontLabel?.Dispose();
         getTextBoxFontButton?.Dispose();
         getTextBoxFontSizeButton?.Dispose();
         getTextBoxFontColorButton?.Dispose();
         getTextBoxFontColorPanel?.Dispose();
         getTextBoxBackgroundColorButton?.Dispose();
         getTextBoxBackgroundColorPanel?.Dispose();
         editThemeTextBoxGroupBox?.Dispose();
         interfaceDetailsLabel?.Dispose();
         interfaceFontLabel?.Dispose();
         getInterfaceFontButton?.Dispose();
         getInterfaceFontSizeButton?.Dispose();
         getInterfaceFontColorButton?.Dispose();
         getInterfaceFontColorPanel?.Dispose();
         getInterfaceBackgroundColorButton?.Dispose();
         getInterfaceBackgroundColorPanel?.Dispose();
         interfaceGroupBox?.Dispose();
         getStatusBarBackgroundColorButton?.Dispose();
         getStatusBarBackgroundColorPanel?.Dispose();
         getPanelBackgroundColorButton?.Dispose();
         getPanelBackgroundColorPanel?.Dispose();
         editThemePanel?.Dispose();
         doMoreCheckBox?.Dispose();
         clearFileHistoryButton?.Dispose();
         sizeFontCloseOnOkayCheckBox?.Dispose();
         fontSizeTitleLabel?.Dispose();
         fontSizePrefixButton?.Dispose();
         fontSizeUpDown?.Dispose();
         fontSizePanel?.Dispose();
         wheelingVelocityTitleLabel?.Dispose();
         wheelingVelocityPrefixButton?.Dispose();
         wheelingVelocityUpDown?.Dispose();
         wheelingVelocityOKButton?.Dispose();
         wheelingVelocityCancelButton?.Dispose();
         wheelingVelocityPanel?.Dispose();
         statusStripBackgroundColorTSMI?.Dispose();
         dialogBackgroundColorTSMI?.Dispose();
         xInterfaceFontTSMI?.Dispose();
         xMonospacedTextBoxFontTSMI?.Dispose();
         xTextFontTSMI?.Dispose();
         xFontPickersTSMI?.Dispose();
         xInterfaceFontColorTSMI?.Dispose();
         xInterfaceBackgroundColorTSMI?.Dispose();
         xTextFontColorTSMI?.Dispose();
         xTextBackgroundColorTSMI?.Dispose();
         xColorPickersTSMI?.Dispose();
         xStatusStripBackgroundColorTSMI?.Dispose();
         xDialogBackgroundColorTSMI?.Dispose();
         for (int i = 0; i < openRecentTSMI.DropDownItems.Count; i++)
            openRecentTSMI.DropDownItems[i]?.Dispose();
         openRecentTSMI?.Dispose();
         recentFilesHistoryCancelButton?.Dispose();
         recentFilesHistoryOkayButton?.Dispose();
         recentFilesHistoryPrefixButton?.Dispose();
         recentFilesHistoryUpDown?.Dispose();
         recentFilesHistoryTitleLabel?.Dispose();
         recentFilesHistoryPanel?.Dispose();
         findButton?.Dispose();
         findCheckBoxGroupBox?.Dispose();
         findCancelButton?.Dispose();
         findMatchCaseCheckBox?.Dispose();
         findRegularExpressionsCheckBox?.Dispose();
         findTitleLabel?.Dispose();
         findWhatPrefixButton?.Dispose();
         findTextBox?.Dispose();
         findWholeWordCheckBox?.Dispose();
         findPanel?.Dispose();
         goCancelButton?.Dispose();
         goToGoButton?.Dispose();
         goToPrefixButton?.Dispose();
         goUpDown?.Dispose();
         goTitleLabel?.Dispose();
         goPanel?.Dispose();
         replaceAllButton?.Dispose();
         replaceButton?.Dispose();
         replaceCheckBoxGroupBox?.Dispose();
         replaceCancelButton?.Dispose();
         replaceMatchCaseCheckBox?.Dispose();
         replaceRegularExpressionsCheckBox?.Dispose();
         replaceTitleLabel?.Dispose();
         searchTextBox?.Dispose();
         replaceWholeWordCheckBox?.Dispose();
         searchReplacePrefixButton?.Dispose();
         replaceTextBox?.Dispose();
         searchSearchPrefixButton?.Dispose();
         replacePanel?.Dispose();
         alwaysOnTopTSMI?.Dispose();
         charactersStatusLabel?.Dispose();
         copyTSMI?.Dispose();
         cutTSMI?.Dispose();
         darkThemeTSMI?.Dispose();
         lightThemeTSMI?.Dispose();
         createThemeTSMI?.Dispose();
         removeThemeTSMI?.Dispose();
         editThemeTSMI?.Dispose();
         xStatusStripBackgroundColorTSMI?.Dispose();
         deleteTSMI?.Dispose();
         editTSMI?.Dispose();
         exitTSMI?.Dispose();
         fadeInTSMI?.Dispose();
         fileTSMI?.Dispose();
         findNextTSMI?.Dispose();
         findPreviousTSMI?.Dispose();
         findTSMI?.Dispose();
         interfaceFontPickerTSMI?.Dispose();
         optionsTSMI?.Dispose();
         goToTSMI?.Dispose();
         helpTSMI?.Dispose();
         linesStatusLabel?.Dispose();
         menuStrip?.Dispose();
         newTSMI?.Dispose();
         newWindowTSMI?.Dispose();
         openTSMI?.Dispose();
         pageSetUpTSMI?.Dispose();
         pasteTSMI?.Dispose();
         printTSMI?.Dispose();
         redoTSMI?.Dispose();
         replaceTSMI?.Dispose();
         saveAsTSMI?.Dispose();
         saveTSMI?.Dispose();
         searchWithTSMI?.Dispose();
         selectAllTSMI?.Dispose();
         selectNoneTSMI?.Dispose();
         showStatusBarTSMI?.Dispose();
         statusStrip?.Dispose();
         textBox?.Dispose();
         toolStripSeparator1?.Dispose();
         toolStripSeparator2?.Dispose();
         toolStripSeparator3?.Dispose();
         toolStripSeparator4?.Dispose();
         toolStripSeparator5?.Dispose();
         toolStripSeparator6?.Dispose();
         toolStripSeparator7?.Dispose();
         toolStripSeparator10?.Dispose();
         toolStripSeparator12?.Dispose();
         toolStripSeparator13?.Dispose();
         toolStripSeparator14?.Dispose();
         toolStripSeparator15?.Dispose();
         toolStripSeparator16?.Dispose();
         toolStripSeparator17?.Dispose();
         undoTSMI?.Dispose();
         utfStatusLabel?.Dispose();
         versionStatusLabel?.Dispose();
         viewTSMI?.Dispose();
         wordsStatusLabel?.Dispose();
         wordWrapTSMI?.Dispose();
         zoomDefaultTSMI?.Dispose();
         zoomInTSMI?.Dispose();
         zoomOutTSMI?.Dispose();
         toolStrip?.Dispose();
         // efm5 2024 03 21 if we add Text-based buttons They will need to be disposed
         openToolStripButton?.Dispose();
         saveToolStripButton?.Dispose();
         saveAsTSB?.Dispose();
         printToolStripButton?.Dispose();
         cutToolStripButton?.Dispose();
         copyToolStripButton?.Dispose();
         pasteToolStripButton?.Dispose();
         createThemeTitleLabel?.Dispose();
         createThemeNamePrefixButton?.Dispose();
         createThemeNameTextBox?.Dispose();
         createThemeOkayButton?.Dispose();
         createThemeCancelButton?.Dispose();
         createThemePanel?.Dispose();
         foreach (ToolStripMenuItem tsmi in themesTSMI.DropDownItems.OfType<ToolStripMenuItem>())
            tsmi?.Dispose();
         foreach (Theme theme in sThemeList)
            theme?.Dispose();//efm5 Just disposes the fonts
         foreach (Button button in pickThemePanel.Controls.OfType<Button>())
            button.Dispose();
         pickThemeLabel?.Dispose();
         pickThemeCancelButton?.Dispose();
         pickThemePanel?.Dispose();
         fileHistoryMinMaxLabel?.Dispose();
         #endregion
      }

      private void ToolStrip_Resize(object pSender, EventArgs pE) {
         SyncToolStripSizing();
      }
      #endregion

      #region File menu
      private void NewTSMI_Click(object pSender, EventArgs pE) {
         New();
      }

      private void NewWindowTSMI_Click(object pSender, EventArgs pE) {
         if (string.IsNullOrEmpty(textBox.Text))
            return;
         try {
            Process process = new Process {
               StartInfo = new ProcessStartInfo(sAppFolder + sAppName + ".exe") {
                  UseShellExecute = true
               }
            };
            process.Start();
            Thread.Sleep(100);
         }
         catch (Exception pException) {
            TimedMessage("NewWindowTSMI_Click threw an exception" + Environment.NewLine + pException.ToString(),
               "ERROR", 0);
         }
      }

      private void OpenTSMI_Click(object pSender, EventArgs pE) {
         OpenFile();
      }

      private void SaveTSMI_Click(object pSender, EventArgs pE) {
         Save();
      }

      private void SaveAsTSMI_Click(object pSender, EventArgs pE) {
         SaveAs();
      }

      private void SaveAndExitTSMI_Click(object pSender, EventArgs pE) {
         Save();
         Close();
      }

      private void SaveAsDialogEncoding_SelectedIndexChanged(object? pSender, EventArgs pE) {
         CommonFileDialogRadioButtonList commonFileDialogRadioButtonList = (CommonFileDialogRadioButtonList)pSender;
         CommonFileDialogRadioButtonListItem item = commonFileDialogRadioButtonList.Items[commonFileDialogRadioButtonList.SelectedIndex];

         if (string.Equals(item.Text, "ASCII", StringComparison.OrdinalIgnoreCase))
            sTextEncoding = TextEncoding.ASCII;
         else if (string.Equals(item.Text, "UTF8", StringComparison.OrdinalIgnoreCase))
            sTextEncoding = TextEncoding.UTF8;
         else if (string.Equals(item.Text, "Unicode", StringComparison.OrdinalIgnoreCase))
            sTextEncoding = TextEncoding.Unicode;
      }

      #region printing
      private void PrintTSMI_Click(object pSender, EventArgs pE) {
         Print();
      }

      private void PrintDocument_BeginPrint(object pSender, PrintEventArgs pE) {
         char[] param = { '\n' };
         string[] lines;

         if (printDialog.PrinterSettings.PrintRange == PrintRange.Selection) {
            lines = textBox.SelectedText.Split(param);
         }
         else {
            lines = textBox.Text.Split(param);
         }

         int i = 0;
         char[] trimParam = { '\r' };
         foreach (string s in lines) {
            lines[i++] = s.TrimEnd(trimParam);
         }
      }

      private void PrintPreviewDialog_Click(object pSender, EventArgs pE) {
         printPreviewDialog.ShowDialog();
      }

      //private void PrintPreviewTSMI_Click(object pSender, EventArgs pE) {
      //   try {
      //      // Create a new printPreviewDialog using constructor.
      //      PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog() {
      //         ClientSize = new Size(700, 600),
      //         MinimumSize = new Size(375, 250),
      //         UseAntiAlias = true,
      //         Document = sPreviewDocument,
      //         Text = "SP Zen Editor Print Preview"
      //      };
      //      sPreviewDocument.PrintPage += new PrintPageEventHandler(PreviewDocument_PrintPage);
      //      if (textBox.SelectionLength > 0)
      //         sTextToPrint = textBox.SelectedText;
      //      else
      //         sTextToPrint = textBox.Text;
      //      printPreviewDialog.ShowDialog();
      //   }
      //   catch (Exception pException) {
      //      TimedMessage("PrintPreviewTSMI_Click threw an exception." + Environment.NewLine + pException.ToString(), "ERROR", 0);
      //   }
      //}

      //private void PreviewDocument_PrintPage(object pSender, PrintPageEventArgs pE) {
      //   try {
      //      Font printFont = CreateNewFont(textBox.Font);

      //      // See PrintDocument_PrintPage above for comments on the following statements
      //      pE.Graphics.MeasureString(sTextToPrint, printFont,
      //         pE.MarginBounds.Size, StringFormat.GenericTypographic,
      //         out int charactersOnLine, out int linesPerPage);
      //      pE.Graphics.DrawString(sTextToPrint, printFont, Brushes.Black,
      //         pE.MarginBounds, StringFormat.GenericTypographic);
      //      sTextToPrint = sTextToPrint.Substring(charactersOnLine);
      //      pE.HasMorePages = (sTextToPrint.Length > 0);
      //   }
      //   catch (Exception pException) {
      //      TimedMessage("PreviewDocument_PrintPage threw an exception." + Environment.NewLine + pException.ToString(), "ERROR", 0);
      //   }
      //}

      private void PrintDocument_PrintPage(object pSender, PrintPageEventArgs pE) {
         try {
            Font printFont = CreateNewFont(textBox.Font);

            // Sets the value of charactersOn Line to the number of characters
            // of sTextToPrint that will fit within the bounds of the page.
            pE.Graphics.MeasureString(sTextToPrint, printFont,
                pE.MarginBounds.Size, StringFormat.GenericTypographic,
                out int charactersOnLine, out int linesPerPage);

            // Draws the string within the bounds of the page
            pE.Graphics.DrawString(sTextToPrint, printFont, new SolidBrush(sCurrentTheme.mTextBoxFontColor),
                pE.MarginBounds, StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            sTextToPrint = sTextToPrint.Substring(charactersOnLine);

            // Check to see if more pages are to be printed.
            pE.HasMorePages = (sTextToPrint.Length > 0);
         }
         catch (Exception pException) {
            TimedMessage("PrintDocument_PrintPage threw an exception." + Environment.NewLine + pException.ToString(), "ERROR", 0);
         }
      }

      private void PageSetUpTSMI_Click(object pSender, EventArgs pE) {
         try {
            pageSetupDialog.Document = printDocument;
            pageSetupDialog.ShowDialog();
         }
         catch (Exception pException) {
            TimedMessage("PageSetupTSMI_Click threw an exception." + Environment.NewLine + pException.ToString(), "ERROR", 0);
         }
      }

      #endregion

      private void ExitTSMI_Click(object pSender, EventArgs pE) {
         Close();
      }
      #endregion

      #region Edit menu
      private void UndoTSMI_Click(object pSender, EventArgs pE) {
         if (sUndoList.Count == 0)
            return;
         sDoing = true;
         UnReDoData currentData = sUndoList.Last();
         if (!string.IsNullOrEmpty(textBox.Text))
            sRedoList.Add(new UnReDoData(currentData.sSelectionStart, textBox.Text));
         sUndoList.RemoveAt(sUndoList.Count - 1);
         if (sUndoList.Count > 0) {
            UnReDoData unReDoData = sUndoList.Last();
            textBox.Text = unReDoData.mTextString;
            textBox.SelectionStart = unReDoData.sSelectionStart;
         }
         if (sUndoList.Count == 1) {
            if (!string.IsNullOrEmpty(sImportedText) && (textBox.Text == string.Empty))
               textBox.Text = sImportedText;
         }
      }

      private void RedoTSMI_Click(object pSender, EventArgs pE) {
         if (sRedoList.Count == 0)
            return;
         sDoing = true;
         UnReDoData unReDoData = sRedoList.Last();
         textBox.Text = unReDoData.mTextString;
         textBox.SelectionStart = unReDoData.sSelectionStart;
         sRedoList.RemoveAt(sRedoList.Count - 1);
         sUndoList.Add(new UnReDoData(unReDoData.sSelectionStart, textBox.Text));
      }

      private void CutTSMI_Click(object pSender, EventArgs pE) {
         textBox.Cut();
      }

      private void CopyTSMI_Click(object pSender, EventArgs pE) {
         textBox.Copy();
      }

      private void EditCopyAllTSMI_Click(object pSender, EventArgs pE) {
         textBox.SelectAll();
         textBox.Copy();
      }

      private void EditCopyToBeginningTSMI_Click(object pSender, EventArgs pE) {
         int currentPosition = textBox.SelectionStart;

         textBox.SelectionStart = 0;
         textBox.SelectionLength = currentPosition;
         textBox.Copy();
      }

      private void EditCopyToEndTSMI_Click(object pSender, EventArgs pE) {
         textBox.SelectionLength = textBox.Text.Length;
         textBox.Copy();
      }

      private void PasteTSMI_Click(object pSender, EventArgs pE) {
         textBox.Paste();
      }

      private void DeleteTSMI_Click(object pSender, EventArgs pE) {
         if (textBox.SelectionLength == 0)
            return;
         textBox.SelectedText = string.Empty;
      }

      private void TrimToBeginningTSMI_Click(object pSender, EventArgs pE) {
         TrimToBeginning();
      }

      private void TrimToEndTSMI_Click(object pSender, EventArgs pE) {
         TrimToEnd();
      }

      private void Nn2RNTSMI_Click(object pSender, EventArgs pE) {
         LineEndingNN2RN();
      }

      private void N2RNTSMI_Click(object pSender, EventArgs pE) {
         LineEndingN2RN();
      }

      private void NR2RNTSMI_Click(object pSender, EventArgs pE) {
         LineEndingNR2RN();
      }

      private void RN2NNTSMI_Click(object pSender, EventArgs pE) {
         LineEndingRN2NN();
      }

      private void RN2RNRNTSMI_Click(object pSender, EventArgs pE) {
         LineEndingRN2RNRN();
      }

      private void SelectAllTSMI_Click(object pSender, EventArgs pE) {
         textBox.SelectAll();
      }

      private void SelectNoneTSMI_Click(object pSender, EventArgs pE) {
         textBox.DeselectAll();
      }
      #endregion

      #region View menu
      private void WordWrapTSMI_Click(object pSender, EventArgs pE) {
         WrapTextBox();
      }

      private void SetOpacityTSMI_Click(object pSender, EventArgs pE) {
         ToolStripMenuItem tsmi = pSender as ToolStripMenuItem;
         string tag = tsmi.Tag as string;
         if (!string.IsNullOrEmpty(tag)) {
            if (float.TryParse(tag, out float value)) {
               Settings.Default.Opacity = value;
               Opacity = value;
            }
         }
      }

      private void AlwaysOnTopTSMI_Click(object pSender, EventArgs pE) {
         Settings.Default.AlwaysOnTop = alwaysOnTopTSMI.Checked;
         TopMost = alwaysOnTopTSMI.Checked;
      }

      private void ZoomInTSMI_Click(object pSender, EventArgs pE) {
         textBox.Font = new Font(textBox.Font.Name, textBox.Font.SizeInPoints + 1f, textBox.Font.Style);
      }

      private void ZoomOutTSMI_Click(object pSender, EventArgs pE) {
         textBox.Font = new Font(textBox.Font.Name, textBox.Font.SizeInPoints - 1f, textBox.Font.Style);
      }

      private void ZoomDefaultTSMI_Click(object pSender, EventArgs pE) {
         textBox.Font = new Font(textBox.Font.Name, Settings.Default.DefaultZoom, textBox.Font.Style);
      }

      private void ScrollUpTSMI_Click(object pSender, EventArgs pE) {
         TimedMessage("scrollUpTSMI_Click", "NOT YET IMPLEMENTED");
         if (scrollDownTSMI.Checked)
            scrollDownTSMI.Checked = false;
      }

      private void ScrollDownTSMI_Click(object pSender, EventArgs pE) {
         TimedMessage("scrollDownTSMI_Click", "NOT YET IMPLEMENTED");
         if (scrollUpTSMI.Checked)
            scrollUpTSMI.Checked = false;
      }

      private void ScrollFasterTSMI_Click(object pSender, EventArgs pE) {
         TimedMessage("scrollFasterTSMI_Click", "NOT YET IMPLEMENTED");
      }

      private void ScrollSlowerTSMI_Click(object pSender, EventArgs pE) {
         TimedMessage("scrollSlowerTSMI_Click", "NOT YET IMPLEMENTED");
      }

      private void ScrollVelocityTSMI_Click(object pSender, EventArgs pE) {
         TimedMessage("scrollVelocityTSMI_Click", "NOT YET IMPLEMENTED");
      }

      private void ScrollQuitTSMI_Click(object pSender, EventArgs pE) {
         TimedMessage("scrollQuitTSMI_Click", "NOT YET IMPLEMENTED");
      }

      private void ShowStatusBarTSMI_Click(object pSender, EventArgs pE) {
         if (Controls.Contains(statusStrip))
            Controls.Remove(statusStrip);
         else
            Controls.Add(statusStrip);
      }

      private void ShowToolBarTSMI_Click(object pSender, EventArgs pE) {
         if (showToolBarTSMI.Checked) {
            toolBarTopTSMI.Enabled = true;
            toolBarBottomTSMI.Enabled = true;
            toolBarLeftTSMI.Enabled = true;
            toolBarRightTSMI.Enabled = true;
            switch (sToolBarLocation) {
               case ToolBarLocation.Top:
                  toolBarTopTSMI.Checked = true;
                  toolBarTopTSMI.CheckState = CheckState.Checked;
                  if (!toolStripContainer.TopToolStripPanel.Controls.Contains(toolStrip))
                     toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
                  break;
               case ToolBarLocation.Bottom:
                  toolBarBottomTSMI.Checked = true;
                  toolBarBottomTSMI.CheckState = CheckState.Checked;
                  if (!toolStripContainer.BottomToolStripPanel.Controls.Contains(toolStrip))
                     toolStripContainer.BottomToolStripPanel.Controls.Add(toolStrip);
                  break;
               case ToolBarLocation.Left:
                  toolBarLeftTSMI.Checked = true;
                  toolBarLeftTSMI.CheckState = CheckState.Checked;
                  if (!toolStripContainer.LeftToolStripPanel.Controls.Contains(toolStrip))
                     toolStripContainer.LeftToolStripPanel.Controls.Add(toolStrip);
                  break;
               case ToolBarLocation.Right:
                  toolBarRightTSMI.Checked = true;
                  toolBarRightTSMI.CheckState = CheckState.Checked;
                  if (!toolStripContainer.RightToolStripPanel.Controls.Contains(toolStrip))
                     toolStripContainer.RightToolStripPanel.Controls.Add(toolStrip);
                  break;
            }
         }
         else {
            toolBarTopTSMI.Enabled = false;
            toolBarBottomTSMI.Enabled = false;
            toolBarLeftTSMI.Enabled = false;
            toolBarRightTSMI.Enabled = false;
            if (toolStripContainer.TopToolStripPanel.Controls.Contains(toolStrip)) {
               toolBarTopTSMI.Checked = false;
               toolBarTopTSMI.CheckState = CheckState.Unchecked;
               toolStripContainer.TopToolStripPanel.Controls.Remove(toolStrip);
            }
            else if (toolStripContainer.BottomToolStripPanel.Controls.Contains(toolStrip)) {
               toolBarBottomTSMI.Checked = false;
               toolBarBottomTSMI.CheckState = CheckState.Unchecked;
               toolStripContainer.BottomToolStripPanel.Controls.Remove(toolStrip);
            }
            else if (toolStripContainer.LeftToolStripPanel.Controls.Contains(toolStrip)) {
               toolBarLeftTSMI.Checked = false;
               toolBarLeftTSMI.CheckState = CheckState.Unchecked;
               toolStripContainer.LeftToolStripPanel.Controls.Remove(toolStrip);
            }
            else if (toolStripContainer.RightToolStripPanel.Controls.Contains(toolStrip)) {
               toolBarRightTSMI.Checked = false;
               toolBarRightTSMI.CheckState = CheckState.Unchecked;
               toolStripContainer.RightToolStripPanel.Controls.Remove(toolStrip);
            }
         }
      }

      private void ToolBarLocation_Click(object? pSender, EventArgs pE) {
         if (pSender == null)
            return;
         if (pSender is ToolStripMenuItem tsmi && tsmi.Tag is ToolBarLocation targetLocation) {
            if (tsmi.Checked)
               return; // Already in the desired location
            sToolBarLocation = targetLocation;
            LocateToolbar();
         }
      }

      private void ToolBarSize_Click(object? pSender, EventArgs pE) {
         RespondToIconSizing(pSender as ToolStripMenuItem);
      }
      #endregion

      #region options menu
      private void InterfaceFontPickerTSMI_Click(object pSender, EventArgs pE) {
         fontDialog.ShowColor = true;
         fontDialog.Font = menuStrip.Font;
         fontDialog.Color = menuStrip.ForeColor;
         if (fontDialog.ShowDialog() != DialogResult.Cancel) {
            Settings.Default.InterfaceFont = fontDialog.Font;
            Settings.Default.InterfaceFontColor = fontDialog.Color;
            sCurrentTheme.mInterfaceFont = CreateNewFont(fontDialog.Font);
            sCurrentTheme.mInterfaceFontColor = fontDialog.Color;
            ColorizeGui();
         }
      }

      private void TextFontPickerTSMI_Click(object pSender, EventArgs pE) {
         fontDialog.ShowColor = true;
         fontDialog.Font = textBox.Font;
         fontDialog.Color = textBox.ForeColor;
         if (fontDialog.ShowDialog() != DialogResult.Cancel) {
            Settings.Default.TextFont = fontDialog.Font;
            Settings.Default.TextColor = fontDialog.Color;
            Settings.Default.DefaultZoom = fontDialog.Font.SizeInPoints;
            Settings.Default.Zoom = fontDialog.Font.SizeInPoints;
            sCurrentTheme.mTextBoxFont = CreateNewFont(fontDialog.Font);
            sCurrentTheme.mTextBoxFontColor = fontDialog.Color;
            textBox.Font = CreateNewFont(fontDialog.Font);
            textBox.ForeColor = fontDialog.Color;
            ColorizeGui();
         }
      }

      private void InterfaceBackgroundColorTSMI_Click(object pSender, EventArgs pE) {
         colorDialog.Color = menuStrip.BackColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            Settings.Default.InterfaceBackgroundColor = colorDialog.Color;
            sCurrentTheme.mInterfaceBackgroundColor = colorDialog.Color;
            ColorizeGui();
         }
      }

      private void TextBackgroundColorTSMI_Click(object pSender, EventArgs pE) {
         colorDialog.Color = textBox.BackColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            Settings.Default.TextBackgroundColor = colorDialog.Color;
            sCurrentTheme.mTextBoxBackgroundColor = colorDialog.Color;
            ColorizeGui();
         }
      }

      private void InterfaceFontColorTSMI_Click(object pSender, EventArgs pE) {
         colorDialog.Color = menuStrip.ForeColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            Settings.Default.InterfaceFontColor = colorDialog.Color;
            sCurrentTheme.mInterfaceFontColor = colorDialog.Color;
            ColorizeGui();
         }
      }

      private void TextFontColorTSMI_Click(object pSender, EventArgs pE) {
         colorDialog.Color = textBox.ForeColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            Settings.Default.TextColor = colorDialog.Color;
            sCurrentTheme.mTextBoxFontColor = colorDialog.Color;
            ColorizeGui();
         }
      }

      private void StatusStripBackgroundColorTSMI_Click(object pSender, EventArgs pE) {
         colorDialog.Color = statusStrip.BackColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            Settings.Default.StatusBarBackgroundColor = colorDialog.Color;
            sCurrentTheme.mStatusBarBackgroundColor = colorDialog.Color;
            ColorizeGui();
         }
      }

      private void DialogBackgroundColorTSMI_Click(object pSender, EventArgs pE) {
         colorDialog.Color = pickThemePanel.BackColor;
         if (colorDialog.ShowDialog() == DialogResult.OK) {
            Settings.Default.PanelBackgroundColor = colorDialog.Color;
            sCurrentTheme.mPanelBackgroundColor = colorDialog.Color;
            ColorizeGui();
         }
      }

      private void XInterfaceFontTSMI_Click(object pSender, EventArgs pE) {
         GetExternalFont(FontUsage.Interface);
      }

      private void XTextFontTSMI_Click(object pSender, EventArgs pE) {
         GetExternalFont(FontUsage.TextBox);
      }

      private void XMonospacedTextBoxFontTSMI_Click(object pSender, EventArgs pE) {
         GetExternalFont(FontUsage.TextBoxMono);
      }

      private void XTextFontColorTSMI_Click(object pSender, EventArgs pE) {
         GetExternalColor(ColorUsage.Text);
      }

      private void XInterfaceFontColorTSMI_Click(object pSender, EventArgs pE) {
         GetExternalColor(ColorUsage.InterfaceText);
      }

      private void XInterfaceBackgroundColorTSMI_Click(object pSender, EventArgs pE) {
         GetExternalColor(ColorUsage.InterfaceBackground);
      }

      private void XTextBackgroundColorTSMI_Click(object pSender, EventArgs pE) {
         GetExternalColor(ColorUsage.TextBackground);
      }

      private void XStatusBarBackgroundColorTSMI_Click(object pSender, EventArgs pE) {
         GetExternalColor(ColorUsage.StatusBarBackground);
      }

      private void XDialogBackgroundColorTSMI_Click(object pSender, EventArgs pE) {
         GetExternalColor(ColorUsage.DialogBackground);
      }

      private void AllIfNothingTSMI_Click(object pSender, EventArgs pE) {
         Settings.Default.AllIfNothing = allIfNothingTSMI.Checked;
      }
      #endregion

      #region Help menu
      private void HelpTSMI_Click(object pSender, EventArgs pE) {
         Help();
      }
      #endregion

      #region toolbar
      private void NewToolStripButton_Click(object pSender, EventArgs pE) {
         New();
      }

      private void OpenToolStripButton_Click(object pSender, EventArgs pE) {
         OpenFile();
      }

      private void SaveToolStripButton_Click(object pSender, EventArgs pE) {
         Save();
      }

      private void SaveAsTSB_Click(object pSender, EventArgs pE) {
         SaveAs();
      }

      private void PrintToolStripButton_Click(object pSender, EventArgs pE) {
         Print();
      }

      private void CutToolStripButton_Click(object pSender, EventArgs pE) {
         textBox.Cut();
      }

      private void TrimBeginningToolStripButton_Click(object pSender, EventArgs pE) {
         TrimToBeginning();
      }

      private void TrimEndToolStripButton_Click(object pSender, EventArgs pE) {
         TrimToEnd();
      }

      private void CopyToolStripButton_Click(object pSender, EventArgs pE) {
         textBox.Copy();
      }

      private void PasteToolStripButton_Click(object pSender, EventArgs pE) {
         textBox.Paste();
      }

      private void HelpToolStripButton_Click(object pSender, EventArgs pE) {
         Help();
      }
      #endregion

      #region Context Menu
      private void ContextCutTSMI_Click(object pSender, EventArgs pE) {
         textBox.Cut();
      }

      private void ContextCopyTSMI_Click(object pSender, EventArgs pE) {
         textBox.Copy();
      }

      private void ContextPasteTSMI_Click(object pSender, EventArgs pE) {
         textBox.Paste();
      }

      private void ContextSaveTSMI_Click(object pSender, EventArgs pE) {
         Save();
      }

      private void ContextSaveAsTSMI_Click(object pSender, EventArgs pE) {
         SaveAs();
      }

      private void ContextSaveExitTSMI_Click(object pSender, EventArgs pE) {
         Save();
         Close();
      }

      private void ContextSelectAllTSMI_Click(object pSender, EventArgs pE) {
         textBox.SelectAll();
      }

      private void ContextDeleteTSMI_Click(object pSender, EventArgs pE) {
         if (textBox.SelectionLength == 0)
            return;
         textBox.SelectedText = string.Empty;
      }
      #endregion

      #region scrolling & wheeling
      private void WheelingVelocityPrefixButton_Click(object pSender, EventArgs pE) {
         wheelingVelocityUpDown.Focus();
         wheelingVelocityUpDown.Select(0, wheelingVelocityUpDown.Text.Length);
      }

      private void WheelingVelocityOKButton_Click(object pSender, EventArgs pE) {
         TimedMessage("wheelingVelocityOKButton_Click", "NOT YET IMPLEMENTED");
      }

      private void WheelingVelocityCancelButton_Click(object pSender, EventArgs pE) {
         TimedMessage("wheelingVelocityCancelButton_Click", "NOT YET IMPLEMENTED");
      }

      private void ToolTopButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_VSCROLL, SB_PAGETOP, 0);
      }

      private void ToolLeftEdgeButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_SCROLL, SB_LEFT, 0);
      }

      private void ToolRightEdgeButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_SCROLL, SB_RIGHT, 0);
      }

      private void ToolBottomButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_VSCROLL, SB_PAGEBOTTOM, 0);
      }

      private void ToolScrollLeftButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_SCROLL, SB_LINELEFT, 0);
      }

      private void ToolScrollUpButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_VSCROLL, SB_LINEUP, 0);
      }

      private void ToolScrollDownButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_VSCROLL, SB_LINEDOWN, 0);
      }

      private void ToolScrollRightButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_SCROLL, SB_LINERIGHT, 0);
      }

      private void ToolPageLeftButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_SCROLL, SB_PAGELEFT, 0);
      }

      private void ToolPageUpButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_VSCROLL, SB_PAGEUP, 0);
      }

      private void ToolPageDownButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_VSCROLL, SB_PAGEDOWN, 0);
      }

      private void ToolPageRightButton_Click(object pSender, EventArgs pE) {
         _ = SendMessage(textBox.Handle, WM_SCROLL, SB_PAGERIGHT, 0);
      }
      #endregion

      private void ToolStrip_ParentChanged(object pSender, EventArgs pE) {
         SyncToolStripSizing();
      }

      private void ToolStrip_Layout(object pSender, LayoutEventArgs pE) {
         SyncToolStripSizing();
      }

      private void ToolBarShowToolsTSMI_Click(object pSender, EventArgs pE) {
         ToolStripMenuItem? toolStripMenuItem = pSender as ToolStripMenuItem;
         if (toolStripMenuItem == null)
            return;
         LayoutMain();
      }

      private void ToolBarShowScrollersTSMI_Click(object pSender, EventArgs pE) {
         ToolStripMenuItem? toolStripMenuItem = pSender as ToolStripMenuItem;

         if (toolStripMenuItem == null)
            return;
         HandleScrollers(toolStripMenuItem.Checked);
         LayoutMain();
      }

      private void ClearFileHistoryButton_Click(object pSender, EventArgs pE) {
         try {
            for (int i = 0; i < openRecentTSMI.DropDownItems.Count; i++) {
               ToolStripMenuItem tsmi = (ToolStripMenuItem)openRecentTSMI.DropDownItems[0];
               tsmi?.Dispose();
            }
            openRecentTSMI.DropDownItems.Clear();
            if (File.Exists(sRecentFileHistoryPath))
               File.Delete(sRecentFileHistoryPath);
            done.Show();
            done.BringToFront();
         }
         catch (Exception pException) {
            TimedMessage("ClearFileHistoryButton_Click threw an exception:" + Environment.NewLine + pException.ToString());
         }
      }

      #region dialogs
      private void QuestionResponseButton_Click(object pSender, EventArgs pE) {
         // Extract the button and its Tag
         if (pSender is Button button && button.Tag is ResponseType result)
            // This "completes" the task and sends the result back to the 'await' line
            _qrTaskCompletionSource?.TrySetResult(result);
      }

      private void InformingOkayButton_Click(object pSender, EventArgs pE) {
         // Extract the button and its Tag
         if (pSender is Button button && button.Tag is ResponseType result)
            // This "completes" the task and sends the result back to the 'await' line
            _infoTaskCompletionSource?.TrySetResult(result);
      }
      #endregion
   }
}