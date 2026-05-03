using static EasyPad.Program.NativeMethods;

#pragma warning disable IDE0051
namespace EasyPad {
   public partial class EasyPadForm : Form {
      private const int OFFSET = 5, DOUBLE_OFFSET = (OFFSET * 2);

      public static int Largest(List<int> pNumbers) {
         int largest = 0;

         foreach (int number in pNumbers)
            if (number > largest)
               largest = number;
         return largest;
      }

      public static float Largest(List<float> pNumbers) {
         float largest = 0;

         foreach (float number in pNumbers)
            if (number > largest)
               largest = number;
         return largest;
      }

      public static double Largest(List<double> pNumbers) {
         double largest = 0;

         foreach (double number in pNumbers)
            if (number > largest)
               largest = number;
         return largest;
      }

      public static int Smallest(List<int> pNumbers) {
         int smallest = 0;

         foreach (int number in pNumbers)
            if (number < smallest)
               smallest = number;
         return smallest;
      }

      public static float Smallest(List<float> pNumbers) {
         float smallest = 0;

         foreach (float number in pNumbers)
            if (number < smallest)
               smallest = number;
         return smallest;
      }

      public static double Smallest(List<double> pNumbers) {
         double smallest = 0;

         foreach (double number in pNumbers)
            if (number < smallest)
               smallest = number;
         return smallest;
      }

      public static void RightAlign(List<Control> pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return;
         int rightmost = Rightmost(pControls);

         foreach (Control control in pControls)
            if (control.Right < rightmost)
               control.Left += (rightmost - control.Right);
      }

      public static int Tallest(List<Control> pControls) {
         int tallest = 0;

         foreach (Control control in pControls)
            if (control.Height > tallest)
               tallest = control.Height;
         return tallest;
      }

      public static int Widest(List<Control> pControls) {
         int widest = 0;

         foreach (Control control in pControls)
            if (control.Width > widest)
               widest = control.Width;
         return widest;
      }

      public static int Rightmost(List<Control> pControls) {
         int rightmost = 0;

         foreach (Control control in pControls)
            if (control.Right > rightmost)
               rightmost = control.Right;
         return rightmost;
      }

      public static int Leftmost(List<Control> pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;
         int leftmost = pControls[0].Left;

         foreach (Control control in pControls)
            if (control.Left < leftmost)
               leftmost = control.Left;
         return leftmost;
      }

      public static int Topmost(List<Control> pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;
         int topmost = pControls[0].Top;

         foreach (Control control in pControls)
            if (control.Top < topmost)
               topmost = control.Top;
         return topmost;
      }

      public static int Bottommost(List<Control> pControls) {
         int bottommost = 0;

         foreach (Control control in pControls)
            if (control.Bottom > bottommost)
               bottommost = control.Bottom;
         return bottommost;
      }

      public static void CenterControlHorizontally(Control pContainer, Control pChildControl) {
         if (pContainer.Width < pChildControl.Width) {
            pContainer.Width = pChildControl.Width + 2;
            pChildControl.Left = 1;
            //efm5 this must remain a timed message
            TimedMessage(string.Format("CenterControlHorizontally() error:{0}" +
                   "container: {1} is narrower than{0}" +
                   "the control to be centered: {2}",
                   Environment.NewLine, pContainer.Name, pChildControl.Name),
                "Sizing VIOLATION", 2000);
         }
         else
            pChildControl.Left = (pContainer.Width - pChildControl.Width) / 2;
      }

      public static void CenterControlVertically(Control pContainer, Control pChildControl) {
         if (pContainer.Height < pChildControl.Height) {
            pChildControl.Width = pContainer.Height + 2;
            pChildControl.Top = 1;
            //efm5 this must remain a timed message
            TimedMessage(string.Format("CenterControlVertically() error:{0}" +
                   "container: {1} is shorter than{0}" +
                   "the control to be centered: {2}",
                   Environment.NewLine, pContainer.Name, pChildControl.Name),
                "Sizing VIOLATION", 2000);
         }
         else
            pChildControl.Top = (pContainer.Height - pChildControl.Height) / 2;
      }

      public static void CenterControl(Control pContainer, Control pChildControl) {
         CenterControlHorizontally(pContainer, pChildControl);
         CenterControlVertically(pContainer, pChildControl);
      }

      //public void CenterDialogPanel(Panel pPanel, Panel pBorderPanel) {
      //   Rectangle workingArea = Screen.GetWorkingArea(this);
      //   int maxWidth = (int)(workingArea.Width * 0.99f),
      //      maxHeight = (int)(workingArea.Height * 0.99f), proposedWidth, proposedHeight;
      //   bool resize = false;

      //   pBorderPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
      //   pPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
      //   if (pBorderPanel.Controls.Contains(pPanel))
      //      pBorderPanel.Controls.Remove(pPanel);
      //   if (Controls.Contains(pBorderPanel))
      //      Controls.Remove(pBorderPanel);

      //   pBorderPanel.Size = new Size(pPanel.Width + PANEL_BORDER + (BORDER * 2),
      //      pPanel.Height + PANEL_BORDER);
      //   Controls.Add(pBorderPanel);
      //   pBorderPanel.Controls.Add(pPanel);
      //   pPanel.Location = new Point(BORDER, BORDER);
      //   pBorderPanel.Location = new Point(BORDER, BORDER);
      //   Size = new Size(pBorderPanel.Width + DOUBLE_BORDER,
      //      pBorderPanel.Height + sTitleBarHeight + DOUBLE_BORDER);
      //   if (Size.Width > maxWidth) {
      //      proposedWidth = maxWidth;
      //      resize = true;
      //   }
      //   else
      //      proposedWidth = Size.Width;
      //   if (Size.Height > maxHeight) {
      //      proposedHeight = maxHeight;
      //      resize = true;
      //   }
      //   else
      //      proposedHeight = Size.Height;
      //   if (resize) {
      //      Size = new Size(proposedWidth, proposedHeight);
      //      _ = EnsureWindowFitsMonitor(sForm);
      //      pBorderPanel.Size = new Size(ClientSize.Width - HALF_BORDER - pBorderPanel.Left,
      //         ClientSize.Height - HALF_BORDER - pBorderPanel.Top);
      //      pPanel.Size = new Size(pBorderPanel.Width - PANEL_BORDER, pBorderPanel.Height - PANEL_BORDER);
      //   }
      //   else {
      //      if (EnsureWindowFitsMonitor(sForm)) {//efm5 if this returns true the title was wider than the border panel
      //         pBorderPanel.Size = new Size(ClientSize.Width - HALF_BORDER - pBorderPanel.Left,
      //            ClientSize.Height - HALF_BORDER - pBorderPanel.Top);
      //         pPanel.Size = new Size(pBorderPanel.Width - PANEL_BORDER, pBorderPanel.Height - PANEL_BORDER);
      //      }
      //   }
      //   pBorderPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
      //   pPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
      //   CenterToScreen();
      //   pBorderPanel.BringToFront();
      //   pBorderPanel.Show();
      //   pBorderPanel.Refresh();
      //}

      public static bool EnsureWindowFitsMonitor(Form pForm, bool pControlBox = true) {
         Screen formScreen = Screen.FromControl(pForm), primaryScreen = Screen.PrimaryScreen;
         Rectangle workingArea = formScreen.WorkingArea;
         Size size = pForm.Size;
         SizeF titleSize;
         Size workingSize = new Size(formScreen.WorkingArea.Width, formScreen.WorkingArea.Height);
         int x = pForm.Location.X, y = pForm.Location.Y, wantedTitleBarWidth = 0, controlBoxSpace = 1;
         bool changeSize = false;
         //Make sure the window was wide enough to display the title if possible

         if (pControlBox)
            controlBoxSpace = 4;

         using (Graphics graphics = pForm.CreateGraphics()) {
            titleSize = graphics.MeasureString(pForm.Text, SystemFonts.CaptionFont);
         }
         wantedTitleBarWidth = (int)((titleSize.Width * 0.86f) + (SystemInformation.CaptionButtonSize.Width * controlBoxSpace));
         //efm5 four small icons = leading gap + 3 ControlBox buttons
         if (size.Width < wantedTitleBarWidth) {
            size.Width = wantedTitleBarWidth;
            changeSize = true;
         }
         if (size.Width > workingSize.Width) {
            size.Width = workingSize.Width - WINDOW_REDUCER;
            changeSize = true;
         }
         if (size.Height > workingSize.Height) {
            size.Height = workingSize.Height - WINDOW_REDUCER;
            changeSize = true;
         }
         pForm.Size = size;

         if (pForm.Right > workingArea.Right)
            x = workingArea.Right - size.Width - WINDOW_REDUCER;
         if (pForm.Bottom > workingArea.Bottom)
            y = workingArea.Bottom - size.Height - WINDOW_REDUCER;
         pForm.Location = new Point(x, y);
         if (IsOffScreen(pForm))
            pForm.Location = new Point(formScreen.Bounds.Left + OFFSET, formScreen.Bounds.Top + OFFSET);
         if (IsPartiallyHidden(pForm)) {
            pForm.Location = new Point(formScreen.Bounds.Left + OFFSET, formScreen.Bounds.Top + OFFSET);
            if (pForm.Size.Width > formScreen.WorkingArea.Width - DOUBLE_OFFSET)
               pForm.Width = formScreen.WorkingArea.Width - DOUBLE_OFFSET;
            if (pForm.Size.Height > formScreen.WorkingArea.Height - DOUBLE_OFFSET)
               pForm.Height = formScreen.WorkingArea.Height - DOUBLE_OFFSET;
            changeSize = true;
         }
         return changeSize;
      }

      private static void GetDpi(Screen pScreen, DpiType pDpiType, out uint pDpiX, out uint pDpiY) {
         var pnt = new System.Drawing.Point(pScreen.Bounds.Left + 1, pScreen.Bounds.Top + 1);
         var mon = MonitorFromPoint(pnt, 2/*MONITOR_DEFAULTTONEAREST*/);
         GetDpiForMonitor(mon, pDpiType, out pDpiX, out pDpiY);
      }

      //private void AdjustForResolution(Form pForm) {
      //   Rectangle screenRectangle = RectangleToScreen(ClientRectangle);
      //   Screen fromscreen = Screen.FromPoint(Location);
      //   GetDpi(fromscreen, DpiType.Effective, out uint x, out uint y);
      //   sScaling = (int)Math.Ceiling((float)x / 96f * 100f);
      //   sResolution = new Size(fromscreen.Bounds.Width, fromscreen.Bounds.Height);

      //ReportedAt 100%
      //scaling: 16; height: 25
      //scaling: 24; height: 31
      //scaling: 32; height: 39
      //scaling: 64; height: 71
      //scaling: 128; height: 135
      /*   Reported at 225%
scaling: 16; height: 26
scaling: 24; height: 34
scaling: 32; height: 42
scaling: 64; height: 74
scaling: 128; height: 138
       * */
      //These are actual measurements at the various Windows scalings = sScalings
      //Note that they are very close to the same as what is reported
      //and that they only very slightly, but continue to trend upward
      //switch (sToolbarScale) {
      //   case 16:
      //   default:
      //      sToolbarHeight = 25;
      //      if ((sScaling >= 125) && (sScaling < 150))
      //         sToolbarHeight = 26;
      //      else if ((sScaling >= 150) && (sScaling < 175))
      //         sToolbarHeight = 25;
      //      else if ((sScaling >= 175) && (sScaling < 200))
      //         sToolbarHeight = 26;
      //      else if ((sScaling >= 200) && (sScaling < 225))
      //         sToolbarHeight = 26;
      //      else if ((sScaling >= 225) && (sScaling < 250))
      //         sToolbarHeight = 26;
      //      else if (sScaling == 350)//efm5 The maximum my video card will support
      //         sToolbarHeight = 31;
      //      break;
      //   case 24:
      //      sToolbarHeight = 31;
      //      if ((sScaling >= 125) && (sScaling < 150))
      //         sToolbarHeight = 31;
      //      else if ((sScaling >= 150) && (sScaling < 175))
      //         sToolbarHeight = 33;
      //      else if ((sScaling >= 175) && (sScaling < 200))
      //         sToolbarHeight = 34;
      //      else if ((sScaling >= 200) && (sScaling < 225))
      //         sToolbarHeight = 34;
      //      else if ((sScaling >= 225) && (sScaling < 250))
      //         sToolbarHeight = 34;
      //      else if ((sScaling > 450) && (sScaling <= 500))
      //         sToolbarHeight = 39;
      //      break;
      //   case 32:
      //      sToolbarHeight = 39;
      //      if ((sScaling >= 125) && (sScaling < 150))
      //         sToolbarHeight = 39;
      //      else if ((sScaling >= 150) && (sScaling < 175))
      //         sToolbarHeight = 41;
      //      else if ((sScaling >= 175) && (sScaling < 200))
      //         sToolbarHeight = 42;
      //      else if ((sScaling >= 200) && (sScaling < 225))
      //         sToolbarHeight = 42;
      //      else if ((sScaling >= 225) && (sScaling < 250))
      //         sToolbarHeight = 42;
      //      else if ((sScaling > 450) && (sScaling <= 500))
      //         sToolbarHeight = 47;
      //      break;
      //   case 64:
      //      sToolbarHeight = 71;
      //      if ((sScaling >= 125) && (sScaling < 150))
      //         sToolbarHeight = 71;
      //      else if ((sScaling >= 150) && (sScaling < 175))
      //         sToolbarHeight = 73;
      //      else if ((sScaling >= 175) && (sScaling < 200))
      //         sToolbarHeight = 74;
      //      else if ((sScaling >= 200) && (sScaling < 225))
      //         sToolbarHeight = 74;
      //      else if ((sScaling >= 225) && (sScaling < 250))
      //         sToolbarHeight = 74;
      //      else if ((sScaling > 450) && (sScaling <= 500))
      //         sToolbarHeight = 79;
      //      break;
      //   case 128:
      //      sToolbarHeight = 134;
      //      if ((sScaling >= 125) && (sScaling < 150))
      //         sToolbarHeight = 134;
      //      else if ((sScaling >= 150) && (sScaling < 175))
      //         sToolbarHeight = 137;
      //      else if ((sScaling >= 175) && (sScaling < 200))
      //         sToolbarHeight = 137;
      //      else if ((sScaling >= 200) && (sScaling < 225))
      //         sToolbarHeight = 138;
      //      else if ((sScaling >= 225) && (sScaling < 250))
      //         sToolbarHeight = 138;
      //      else if ((sScaling > 450) && (sScaling <= 500))
      //         sToolbarHeight = 143;
      //      break;
      //}
      //}

      public static bool IsOffScreen(Form pForm) {
         Screen[] screens = Screen.AllScreens;
         foreach (Screen screen in screens) {
            Point formTopLeft = new Point(pForm.Left, pForm.Top);

            if (screen.WorkingArea.Contains(formTopLeft))
               return false;
         }
         return true;
      }

      public static bool IsPartiallyHidden(Form pForm) {
         Screen[] screens = Screen.AllScreens;
         foreach (Screen screen in screens) {
            if (screen.WorkingArea.Contains(new Point(pForm.Right, pForm.Bottom)))
               return false;
         }
         return true;
      }

      public static void CenterFormOnMonitor(Form pForm) {
         Screen screen = Screen.FromControl(pForm);
         Rectangle workingArea = screen.WorkingArea;
         pForm.Left = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - pForm.Width) / 2);
         pForm.Top = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - pForm.Height) / 2);
      }

      //public static void RecalculateAssociatedOffsets(Font pFont) {
      //   float fontSize = pFont.SizeInPoints;

      //   sAssociatedTextBoxPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);// 0
      //   sAssociatedTextBoxPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.2f);//  2
      //   sAssociatedUpDownPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);//  0
      //   sAssociatedUpDownPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.4f);//  4
      //   sAssociatedLabelPostUpDownHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);//  0
      //   sAssociatedLabelPostUpDownVerticalOffset = (int)Math.Ceiling(fontSize * 0.1f);//  1
      //   sAssociatedTextPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.5f);//  5
      //   sAssociatedCheckBoxPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.4f);// 4 
      //   sAssociatedLabelPostPanelVerticalOffset = (int)Math.Ceiling(fontSize * 0.3f);//  3 
      //   sAssociatedButtonPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.3f);//  3
      //   sAssociatedButtonPostLabelHorizontalSpace = (int)Math.Ceiling(fontSize * 0.3f);//  3
      //   sAssociatedButtonPostLabelVerticalOffset = (int)Math.Ceiling(fontSize * -0.4f);//  -4
      //   sAssociatedPostVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);//  0
      //   sAssociatedLabelPostCheckBoxHorizontalSpace = (int)Math.Ceiling(fontSize * 0.1f);//  1
      //   sAssociatedLabelPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.1f);//  1
      //   sAssociatedTextBoxPostCheckBoxVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);//  0
      //   sAssociatedTextBoxPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.5f);//  5
      //   sAssociatedLabelPostButtonHorizontalSpace = (int)Math.Ceiling(fontSize * 0.0f);//  0
      //   sAssociatedLabelPostButtonVerticalOffset = (int)Math.Ceiling(fontSize * 0.5f);//  5
      //   sIndent = (int)Math.Ceiling(fontSize * 0.5f);//  5 
      //   sCancelOffset = (int)Math.Ceiling(fontSize * 1.5f);//  15
      //   sOkOffset = (int)Math.Ceiling(fontSize * 1.5f);//  15 
      //   sWidgetHorizontalSpace = (int)Math.Ceiling(fontSize * 0.3f);//  3 
      //   sWidgetBigHorizontalSpace = (int)Math.Ceiling(fontSize * 1.0f);//  10
      //   sWidgetVerticalOffset = (int)Math.Ceiling(fontSize * 0.2f);//  2 
      //   sWidgetBigVerticalOffset = (int)Math.Ceiling(fontSize * 0.7f);//  7 
      //   sAssociatedPostVerticalOffset = (int)Math.Ceiling(fontSize * 0.0f);//  0 
      //   sEM = (int)Math.Ceiling(fontSize/* * 1.0f*/);//  10
      //   sMenuLeftOffset = (int)Math.Ceiling(fontSize * 2.5f);//  30
      //   sGroupTopPad = (int)Math.Ceiling(fontSize * 0.2f);//  2
      //   GroupBox groupBox = new GroupBox() {
      //      Font = CreateNewFont(pFont),
      //      Text = "the quick brown fox",//efm5 do not localize
      //      AutoSize = true
      //   };
      //   Panel panel = new Panel() {
      //      Font = CreateNewFont(pFont),
      //      Dock = DockStyle.Fill
      //   };
      //   Label label = new Label() {
      //      Font = CreateNewFont(pFont),
      //      Text = "the quick brown fox"//efm5 do not localize
      //   };
      //   panel.Controls.Add(label);
      //   groupBox.Controls.Add(panel);
      //   sGroupRightPad = groupBox.Width - panel.Width + (panel.Left * 2) + sIndent;
      //   sGroupBottomPad = groupBox.Height - panel.Height - panel.Top + sIndent + 2;
      //   label.Dispose();
      //   panel.Dispose();
      //   groupBox.Dispose();
      //}

      public static void SizeGroupBox(GroupBox pGroupBox, bool pGroupPad = true) {
         int right = 0, bottom = 0, rightPad = 0, bottomPad = 0, wide = sMenuLeftOffset,
            menuCount = 0;

         if (pGroupPad) {
            rightPad = sGroupRightPad;
            bottomPad = sGroupBottomPad;
         }
         foreach (Control control in pGroupBox.Controls) {
            if (control is MenuStrip strip) {
               int menuWidth = 0;
               menuCount++;
               foreach (ToolStripMenuItem tsmi in strip.Items)
                  menuWidth += (tsmi.Width + sCancelOffset);
               if (menuWidth > wide)
                  wide = menuWidth;
            }
         }
         if (menuCount > 0)
            right = wide;
         foreach (Control control in pGroupBox.Controls) {
            if (control is MenuStrip)
               continue;
            if (control.Right > right)
               right = control.Right;
            if (control.Bottom > bottom)
               bottom = control.Bottom;
         }
         pGroupBox.Size = new Size(right + rightPad, bottom + bottomPad);
      }

      //public static void SizeGroupBox(GroupBox pGroupBox, Panel pPanel, bool pGroupPad = true) {
      //   int rightPad = 0, bottomPad = 0;

      //   if (pGroupPad) {
      //      rightPad = sGroupRightPad;
      //      bottomPad = sGroupBottomPad;
      //   }
      //   pGroupBox.Size = new Size(
      //      pPanel.Width + pPanel.Left + rightPad + SystemInformation.VerticalScrollBarWidth + 4,
      //      pPanel.Height + pPanel.Top + bottomPad + SystemInformation.HorizontalScrollBarHeight);
      //}

      public static void SizePanel(Panel pPanel, int pLeftPad = 10, bool pScrollbarPad = true) {
         int right = 0, bottom = 0, scrollbarWidth = 0, scrollbarHeight = 0;

         if (pScrollbarPad) {
            scrollbarWidth = SystemInformation.VerticalScrollBarWidth;
            scrollbarHeight = SystemInformation.HorizontalScrollBarHeight;
         }
         foreach (Control control in pPanel.Controls) {
            string name = control.Name;
            if (control.Right > right)
               right = control.Right;
            if (control.Bottom > bottom)
               bottom = control.Bottom;
         }
         pPanel.Size = new Size(pLeftPad + right + scrollbarWidth, bottom + scrollbarHeight);
      }

      //public static void SizeTextBoxToFitString(out SizeF pSize, RichTextBox pTextBox,
      //   string pExample = "", bool pDoWidth = true, bool pDoHeight = true, bool pPadWidth = true) {
      //   Font font = pTextBox.Font;
      //   SizeF stringSize = new SizeF(0, 0);
      //   pSize = stringSize;

      //   using (Graphics graphics = pTextBox.CreateGraphics()) {
      //      if (!string.IsNullOrEmpty(pExample)) //Prefer example
      //         stringSize = graphics.MeasureString(pExample, font);
      //      else if (!string.IsNullOrEmpty(pTextBox.Text))
      //         stringSize = graphics.MeasureString(pTextBox.Text, font);
      //      else//Worst-case
      //         stringSize = graphics.MeasureString("The quick brown fox", font);
      //   }
      //   if (pDoWidth) {
      //      if (pPadWidth)
      //         pSize.Width = stringSize.Width + sEM;
      //      else
      //         pSize.Width = stringSize.Width;
      //   }
      //   if (pDoHeight)
      //      pSize.Height = (int)Math.Ceiling(stringSize.Height * 1.3f);
      //}

      public static void SizeTextBoxToFitString(out SizeF pSize, TextBox pTextBox, string pExample = "",
         bool pDoWidth = true, bool pDoHeight = true, bool pPadWidth = true) {
         Font font = pTextBox.Font;
         SizeF stringSize = new SizeF(0, 0);
         pSize = stringSize;

         using (Graphics graphics = pTextBox.CreateGraphics()) {
            if (!string.IsNullOrEmpty(pExample)) //Prefer example
               stringSize = graphics.MeasureString(pExample, font);
            else if (!string.IsNullOrEmpty(pTextBox.Text))
               stringSize = graphics.MeasureString(pTextBox.Text, font);
            else//Worst-case
               stringSize = graphics.MeasureString("The quick brown foxñÑ çÇ", font);
         }
         if (pDoWidth) {
            if (pPadWidth)
               pSize.Width = stringSize.Width + sEM;
            else
               pSize.Width = stringSize.Width;
         }
         if (pDoHeight)
            pSize.Height = stringSize.Height;
      }

      public static void SetComboBoxSize(out SizeF pSize, ComboBox pComboBox, string pExample = "") {
         SizeF stringSize = new SizeF(0, 0),
            paddingSize = new SizeF(0, 0);
         pSize = stringSize;
         Font font = pComboBox.Font;

         using (Graphics graphics = pComboBox.CreateGraphics()) {
            paddingSize = graphics.MeasureString("0yÑ", font);
            paddingSize.Height += (font.SizeInPoints / 2.7f);
            if (!string.IsNullOrEmpty(pExample)) //Prefer example
               stringSize = graphics.MeasureString(pExample, font);
            else if (pComboBox.Items.Count > 0) {
               foreach (string phrase in pComboBox.Items) {
                  if (!string.IsNullOrEmpty(phrase)) {
                     SizeF temporaryStringSize = new SizeF(0, 0);
                     temporaryStringSize = graphics.MeasureString(phrase, font);
                     if (temporaryStringSize.Width > stringSize.Width)
                        stringSize.Width = temporaryStringSize.Width;
                  }
               }
            }
            else//Worst-case
               stringSize = graphics.MeasureString("The quick brown fox", font);
         }
         pSize.Width = stringSize.Width + paddingSize.Width + SystemInformation.VerticalScrollBarWidth;
         pSize.Height = stringSize.Height + paddingSize.Height;
      }

      //public void SetComboBoxDropDownWidth(ComboBox pComboBox, int pMinimumWidth = 50) {
      //   if (pComboBox.Items.Count == 0)
      //      return;//don't change the width
      //   Font font = pComboBox.Font;
      //   float boxWidth = 0;
      //   SizeF stringSize = new SizeF();

      //   try {
      //      using (Graphics graphics = pComboBox.CreateGraphics()) {
      //         foreach (string phrase in pComboBox.Items) {
      //            if (!string.IsNullOrEmpty(phrase)) {
      //               stringSize = graphics.MeasureString(phrase, font);
      //               if (stringSize.Width > boxWidth)
      //                  boxWidth = stringSize.Width;
      //            }
      //         }
      //      }
      //      if (boxWidth < pMinimumWidth)
      //         boxWidth = pMinimumWidth;
      //      if (boxWidth > COMBOBOX_MAXIMUM_DROPDOWN_WIDTH)
      //         boxWidth = COMBOBOX_MAXIMUM_DROPDOWN_WIDTH;
      //      pComboBox.DropDownWidth = (int)boxWidth;
      //   }
      //   catch (Exception pException) {
      //      _ = AskingAsync(new TM("SetComboBoxDropDownWidth; exception caught and handled", pException));
      //      pComboBox.DropDownWidth = 200;
      //   }
      //}

      public static void SetUpDownBoxWidth(NumericUpDown pNumericUpDown) {
         float boxWidth = 0f, boxHeight = 0f;
         SizeF stringSize = new SizeF();
         string minimumValue = string.Format("{0}", pNumericUpDown.Minimum),
            maximumValue = string.Format("{0}", pNumericUpDown.Maximum);

         using (Graphics graphics = pNumericUpDown.CreateGraphics()) {
            if (maximumValue.Length > minimumValue.Length)
               stringSize = graphics.MeasureString(maximumValue + "0", pNumericUpDown.Font);
            else
               stringSize = graphics.MeasureString(minimumValue + "0", pNumericUpDown.Font);
            if (stringSize.Width > boxWidth)
               boxWidth = stringSize.Width;
            if (stringSize.Height > boxHeight)
               boxHeight = stringSize.Height;
         }
         //The Up/Down arrows is about the same width as the scrollbar width
         pNumericUpDown.Width = (int)(boxWidth + SystemInformation.VerticalScrollBarWidth);
         pNumericUpDown.Height = (int)(boxHeight + sIndent);
      }

      public static void SizeAllUpDowns(List<NumericUpDown> pUpDownList) {
         foreach (NumericUpDown upDown in pUpDownList)
            SetUpDownBoxWidth(upDown);
      }


      public static bool IsSizeBigger(Size pOriginal, Size pProposed) {
         if ((pProposed.Height > pOriginal.Height) || (pProposed.Width > pOriginal.Width))
            return true;
         return false;
      }

      public static bool RectangleContainsPoint(Rectangle pRectangle, Point pPoint) {
         if ((pRectangle.Left <= pPoint.X) || (pRectangle.Right >= pPoint.X) &&
            (pRectangle.Top <= pPoint.Y) || (pRectangle.Bottom >= pPoint.Y))
            return false;
         return true;
      }

      public static string MassageColorName(string pCompressedName) {
         string expandedName = string.Empty;

         if (!IsKnownColor(pCompressedName, out Color color))
            return "Unknown";
         foreach (char c in pCompressedName) {
            if (char.IsUpper(c))
               expandedName += " " + string.Format("{0}", c);
            else
               expandedName += string.Format("{0}", c);
         }
         expandedName = expandedName.Trim(' ');
         return expandedName;
      }

      private static Color ContrastingColor(Color pColor) {
         int saturation = pColor.R + pColor.G + pColor.B;
         if ((pColor.R + pColor.G + pColor.B) < 382)
            return Color.White;
         else
            return Color.Black;
      }

      private static bool ColorsAreSimilar(Color pColor1, Color pColor2) {
         int rDist = Math.Abs(pColor1.R - pColor2.R),
        gDist = Math.Abs(pColor1.G - pColor2.G),
        bDist = Math.Abs(pColor1.B - pColor2.B);

         if ((rDist + gDist + bDist) > 260)
            return false;
         return true;
      }

      private static Color SubtlyDifferent(Color pColor) {
         int r = 128, g = 128, b = 128;

         if ((pColor.R + pColor.G + pColor.B) < 382) {//Dark
            r = (int)Math.Floor(pColor.R * 1.1f);
            if (r > 255)
               r = 255;
            g = (int)Math.Floor(pColor.G * 1.1f);
            if (g > 255)
               g = 255;
            b = (int)Math.Floor(pColor.B * 1.1f);
            if (b > 255)
               b = 255;
         }
         else {
            r = (int)Math.Floor(pColor.R * 0.9f);
            if (r < 0)
               r = 0;
            g = (int)Math.Floor(pColor.G * 0.9f);
            if (g > 0)
               g = 0;
            b = (int)Math.Floor(pColor.B * 0.9f);
            if (b > 0)
               b = 0;
         }
         return Color.FromArgb(r, g, b);
      }

      public static bool IsKnownColor(Color pColor) {
         Color color;

         foreach (string colorName in Enum.GetNames<KnownColor>()) {
            //cast the colorName into a KnownColor
            KnownColor knownColor = Enum.Parse<KnownColor>(colorName);
            //check if the knownColor variable is a System color - 
            if (knownColor > KnownColor.Transparent) {//  Transparent -27- is the highest numbered system color
               color = Color.FromName(colorName);
               if (color == pColor) {
                  return true;
               }
            }
         }
         return false;
      }

#pragma warning disable IDE1006
      public static bool IsKnownColor(string pColorName, out Color opColor) {
#pragma warning restore IDE1006
         opColor = Color.Transparent;
         List<string> colors = new List<string>();

         foreach (string colorName in Enum.GetNames<KnownColor>()) {
            //cast the colorName into a KnownColor
            KnownColor knownColor = Enum.Parse<KnownColor>(colorName);
            //check if the knownColor variable is a System color - 
            if (knownColor > KnownColor.Transparent) {//  Transparent -27- is the highest numbered system color
               colors.Add(colorName);
            }
         }
         if (colors.Contains(pColorName, StringComparer.OrdinalIgnoreCase)) {
            opColor = Color.FromName(pColorName);
            return true;
         }
         return false;
      }

      public static bool IsKnownColor(string pColorName) {
         List<string> colors = new List<string>();

         foreach (string colorName in Enum.GetNames<KnownColor>()) {
            //cast the colorName into a KnownColor
            KnownColor knownColor = Enum.Parse<KnownColor>(colorName);
            //check if the knownColor variable is a System color
            if (knownColor > KnownColor.Transparent) {
               //add it to our list
               colors.Add(colorName);
            }
         }
         if (colors.Contains(pColorName, StringComparer.OrdinalIgnoreCase))
            return true;
         return false;
      }

      public static Color GroupBoxBackgroundColor(Color pColor) {
         int red = 127, green = 127, blue = 127;

         if (pColor.R <= 127)
            red = pColor.R + 50;
         else
            red = pColor.R - 50;
         if (pColor.G <= 127)
            green = pColor.G + 50;
         else
            green = pColor.G - 50;
         if (pColor.B <= 127)
            blue = pColor.B + 50;
         else
            blue = pColor.B - 50;

         return Color.FromArgb(red, green, blue);
      }

      //public static Color GroupBoxTextColor(Color pBackgroundColor) {
      //   if (ColorsAreSimilar(sProposedInterfaceTextColor, pBackgroundColor)) {
      //      int red = 0, green = 0, blue = 0;

      //      if (pBackgroundColor.R <= 127)
      //         red = 255;
      //      if (pBackgroundColor.G <= 127)
      //         green = 255;
      //      if (pBackgroundColor.B <= 127)
      //         blue = 255;
      //      return Color.FromArgb(red, green, blue);
      //   }
      //   return sProposedInterfaceTextColor;
      //}

      public static int GetGroupBoxFirstLineOffset(GroupBox pGroupBox) {
         SizeF stringSize = new SizeF();

         using (Graphics graphics = pGroupBox.CreateGraphics())
            stringSize = graphics.MeasureString(pGroupBox.Text + "Ñçg", pGroupBox.Font);
         return (int)stringSize.Height + sGroupTopPad + sScalingGroupBoxTopLinePad;
      }

      public void HidePanel(Panel pPanel) {
         if (pPanel == null)
            return;
         pPanel.SendToBack();
         pPanel.Hide();
         if (Controls.Contains(pPanel))
            Controls.Remove(pPanel);
      }

      public void ShowPanel(Panel pPanel) {
         if (pPanel == null)
            return;
         BringToFront();
         Activate();
         if (!Controls.Contains(pPanel))
            Controls.Add(pPanel);
         pPanel.Show();
         pPanel.BringToFront();
         //CenterFormOnMonitor(sForm);//DEBUG efm5 2024 01 8 unnecessary
      }

      public static void SetBottomPanelHeight(Panel pPanel) {
         int top = 1, height = 1;

         foreach (Button button in pPanel.Controls.OfType<Button>()) {
            if (button.Top > top)
               top = button.Top;
            if (button.Height > height)
               height = button.Height;
         }
         foreach (MenuStrip menu in pPanel.Controls.OfType<MenuStrip>()) {
            if (menu.Top > top)
               top = menu.Top;
            if (menu.Height > height)
               height = menu.Height;
         }
         pPanel.Height = height + (top * 2);
      }

      private static bool ColorsAreIdentical(Color pColor1, Color pColor2) {
         if ((pColor1.R == pColor2.R) && (pColor1.G == pColor2.G) && (pColor1.B == pColor2.B))
            return true;
         return false;
      }

      public static Font CreateNewFont(Font pFont) {
         return new Font(pFont.Name, pFont.SizeInPoints, pFont.Style);
      }

      internal static RECT RECTFromRectangle(Rectangle pRectangle) {
         return new RECT() {
            Left = pRectangle.Left,
            Top = pRectangle.Top,
            Right = pRectangle.Right,
            Bottom = pRectangle.Bottom
         };
      }

      internal static Rectangle RectangleFromRECT(RECT pRECT) {
         return new Rectangle() {
            X = pRECT.Left,
            Y = pRECT.Top,
            Size = new Size(pRECT.Right - pRECT.Left, pRECT.Bottom - pRECT.Top)
         };
      }

      private static void SelectPartOfText(RichTextBox pRichTextBox, float pPart = 0.5f) {
         pRichTextBox.Select(0, (int)(pRichTextBox.Text.Length * pPart));
         HomeTextBoxInsertionPoint(pRichTextBox);
         pRichTextBox.Refresh();
      }

      const int EM_LINESCROLL = 0x00B6;
      private static void HomeTextBoxInsertionPoint(RichTextBox pRichTextBox) {
         _ = SendMessage(pRichTextBox.Handle, EM_LINESCROLL, 0, 0);
      }

      private static void HomeTextBoxInsertionPoint(TextBox pTextBox) {
         _ = SendMessage(pTextBox.Handle, EM_LINESCROLL, 0, 0);
      }

      private static void SelectPartOfText(TextBox pTextBox, float pPart = 0.5f) {
         pTextBox.Select(0, (int)(pTextBox.Text.Length * pPart));
         HomeTextBoxInsertionPoint(pTextBox);
         pTextBox.Refresh();
      }

      public void FadeIn(double pTranslucency = 1d) {
         int sTransitionSteps = 8, sTransitionInterval = 10;

         if (Opacity >= pTranslucency)
            return;
         double translucency = pTranslucency;
         double translucencySteps = translucency / sTransitionSteps;

         Opacity = translucencySteps;
         for (int i = 0; i < (sTransitionSteps - 1); i++) {
            Opacity += translucencySteps;
            Thread.Sleep(sTransitionInterval);
         }
         Opacity = translucency;
      }

      public void FadeOut(double pTranslucency = 0d) {
         int sTransitionSteps = 8, sTransitionInterval = 10;

         if (Opacity <= pTranslucency)
            return;
         double translucencySteps = Opacity / sTransitionSteps;

         Opacity -= translucencySteps;
         for (int i = 0; i < (sTransitionSteps - 1); i++) {
            Opacity -= translucencySteps;
            Thread.Sleep(sTransitionInterval);
         }
         Opacity = pTranslucency;
      }

      //private static int LocateUpDownLine(Button pPrefixButton, NumericUpDown pUpDown, Label pSuffixLabel, int pTop,
      //   int pLeft = 0) {
      //   int returnValue = 0;
      //   if ((pPrefixButton == null) || (pUpDown == null)) {
      //      //efm5 this must remain a timed message
      //      TimedMessage("LocateUpDownLine() " + "some variable was null" + ".",
      //         "Code VIOLATION", 0);
      //      return -1;
      //   }
      //   pPrefixButton.Location = new Point(pLeft, pTop);
      //   pUpDown.Location = new Point(pPrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
      //      pPrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
      //   returnValue = Math.Max(pPrefixButton.Bottom, pUpDown.Bottom);
      //   if (pSuffixLabel != null) {
      //      pSuffixLabel.Location = new Point(pUpDown.Right + sAssociatedLabelPostUpDownHorizontalSpace,
      //         pUpDown.Top + sAssociatedLabelPostUpDownVerticalOffset);
      //      returnValue = Math.Max(returnValue, pSuffixLabel.Bottom);
      //   }
      //   return returnValue;
      //}

      //private static int LocateCheckBoxLine(CheckBox pCheckBox, Label pSuffixLabel, int pTop,
      //   int pLeft = 20) {
      //   if ((pCheckBox == null) || (pSuffixLabel == null)) {
      //      //efm5 this must remain a timed message
      //      TimedMessage("LocateCheckBoxLine() " + "some variable was null" + ".",
      //         "Code VIOLATION", 0);
      //      return -1;
      //   }
      //   pCheckBox.Location = new Point(pLeft, pTop);
      //   pSuffixLabel.Location = new Point(pCheckBox.Right + sAssociatedLabelPostCheckBoxHorizontalSpace,
      //      pCheckBox.Top + sAssociatedLabelPostCheckBoxVerticalOffset);
      //   return (int)Math.Max(pCheckBox.Bottom, pSuffixLabel.Bottom);
      //}

      //private static Point LocatePrefixedTextBox(Button pPrefixButton, TextBox pTextBox, int pTop, int pLeft) {
      //   if ((pPrefixButton == null) || (pTextBox == null)) {
      //      //efm5 this must remain a timed message
      //      TimedMessage("LocatePrefixedTextBoxLine() " + "something was null" + ".",
      //         "Code VIOLATION", 0);
      //      return new Point(pLeft, pTop);
      //   }
      //   pPrefixButton.Location = new Point(pLeft, pTop);
      //   pTextBox.Location = new Point(pPrefixButton.Right + sAssociatedTextBoxPostButtonHorizontalSpace,
      //      pPrefixButton.Top + sAssociatedTextBoxPostButtonVerticalOffset);
      //   return new Point(pTextBox.Right, Math.Max(pPrefixButton.Bottom, pTextBox.Bottom));
      //}

      public void ToCenterOrNot(Form pForm, bool pControlBox = true) {
         bool centerHorizontal = true, centerVertical = true;
         Screen currentMonitor = Screen.FromControl(pForm),
            primaryscreen = Screen.PrimaryScreen;
         int ninetyHorizontal = (int)Math.Floor(currentMonitor.WorkingArea.Width * 0.95f),
           ninetyVertical = (int)Math.Floor(currentMonitor.WorkingArea.Height * 0.95f);
         EnsureWindowFitsMonitor(pForm, pControlBox);
         if (pForm.Width > ninetyHorizontal)
            centerHorizontal = false;
         if (pForm.Height > ninetyVertical)
            centerVertical = false;
         if (centerHorizontal && centerVertical) {
            pForm.Location = new Point(
               (int)(currentMonitor.Bounds.X + (currentMonitor.Bounds.Width - Size.Width) / 2),
               (int)(currentMonitor.Bounds.Y + (currentMonitor.Bounds.Height - Size.Height) / 2));
         }
         else if (centerHorizontal) {
            pForm.Left =
               (int)(currentMonitor.Bounds.X + (currentMonitor.Bounds.Width - Size.Width) / 2);

         }
         else if (centerVertical) {
            pForm.Top =
               (int)(currentMonitor.Bounds.Y + (currentMonitor.Bounds.Height - Size.Height) / 2);

         }
      }
   }
}
#pragma warning restore IDE0051