namespace EasyPadCleaner {
   partial class EasyPadCleanerForm {
      /// <summary>
      ///  Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      ///  Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      ///  Required method for Designer support - do not modify
      ///  the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasyPadCleanerForm));
         titleLabel = new Label();
         itemsPanel = new Panel();
         silentlyCheckBox = new CheckBox();
         cacheCheckBox = new CheckBox();
         themesCheckBox = new CheckBox();
         recentFileHistoryCheckBox = new CheckBox();
         allCheckBox = new CheckBox();
         itemsTitleLabel = new Label();
         instructions1Label = new Label();
         bottomPanel = new Panel();
         cancelButton = new Button();
         okButton = new Button();
         instructions2Label = new Label();
         instructions2aLabel = new Label();
         instructions3Label = new Label();
         instructions2bLabel = new Label();
         instructions4Label = new Label();
         instructions3aLabel = new Label();
         instructions4aLabel = new Label();
         instructions5aLabel = new Label();
         instructions5Label = new Label();
         itemsPanel.SuspendLayout();
         bottomPanel.SuspendLayout();
         SuspendLayout();
         // 
         // titleLabel
         // 
         titleLabel.AutoSize = true;
         titleLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
         titleLabel.Location = new Point(11, 10);
         titleLabel.Margin = new Padding(6, 0, 6, 0);
         titleLabel.Name = "titleLabel";
         titleLabel.Size = new Size(161, 25);
         titleLabel.TabIndex = 0;
         titleLabel.Text = "Easy Pad Cleaner";
         // 
         // itemsPanel
         // 
         itemsPanel.AutoSize = true;
         itemsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
         itemsPanel.BackColor = Color.LightCyan;
         itemsPanel.Controls.Add(silentlyCheckBox);
         itemsPanel.Controls.Add(cacheCheckBox);
         itemsPanel.Controls.Add(themesCheckBox);
         itemsPanel.Controls.Add(recentFileHistoryCheckBox);
         itemsPanel.Controls.Add(allCheckBox);
         itemsPanel.Controls.Add(itemsTitleLabel);
         itemsPanel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         itemsPanel.Location = new Point(8, 48);
         itemsPanel.Margin = new Padding(6, 7, 6, 7);
         itemsPanel.Name = "itemsPanel";
         itemsPanel.Size = new Size(202, 228);
         itemsPanel.TabIndex = 1;
         // 
         // silentlyCheckBox
         // 
         silentlyCheckBox.AutoSize = true;
         silentlyCheckBox.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
         silentlyCheckBox.Location = new Point(19, 192);
         silentlyCheckBox.Margin = new Padding(6, 7, 6, 7);
         silentlyCheckBox.Name = "silentlyCheckBox";
         silentlyCheckBox.Size = new Size(96, 29);
         silentlyCheckBox.TabIndex = 7;
         silentlyCheckBox.Text = "Silently";
         silentlyCheckBox.UseVisualStyleBackColor = true;
         // 
         // cacheCheckBox
         // 
         cacheCheckBox.AutoSize = true;
         cacheCheckBox.Checked = true;
         cacheCheckBox.CheckState = CheckState.Checked;
         cacheCheckBox.Location = new Point(19, 75);
         cacheCheckBox.Margin = new Padding(6, 7, 6, 7);
         cacheCheckBox.Name = "cacheCheckBox";
         cacheCheckBox.Size = new Size(83, 29);
         cacheCheckBox.TabIndex = 6;
         cacheCheckBox.Text = "Cac&he";
         cacheCheckBox.UseVisualStyleBackColor = true;
         cacheCheckBox.CheckedChanged += CheckBox_CheckedChanged;
         // 
         // themesCheckBox
         // 
         themesCheckBox.AutoSize = true;
         themesCheckBox.Checked = true;
         themesCheckBox.CheckState = CheckState.Checked;
         themesCheckBox.Location = new Point(19, 118);
         themesCheckBox.Margin = new Padding(6, 7, 6, 7);
         themesCheckBox.Name = "themesCheckBox";
         themesCheckBox.Size = new Size(96, 29);
         themesCheckBox.TabIndex = 4;
         themesCheckBox.Text = "&Themes";
         themesCheckBox.UseVisualStyleBackColor = true;
         themesCheckBox.CheckedChanged += CheckBox_CheckedChanged;
         // 
         // recentFileHistoryCheckBox
         // 
         recentFileHistoryCheckBox.AutoSize = true;
         recentFileHistoryCheckBox.Checked = true;
         recentFileHistoryCheckBox.CheckState = CheckState.Checked;
         recentFileHistoryCheckBox.Location = new Point(11, 149);
         recentFileHistoryCheckBox.Margin = new Padding(6, 7, 6, 7);
         recentFileHistoryCheckBox.Name = "recentFileHistoryCheckBox";
         recentFileHistoryCheckBox.Size = new Size(185, 29);
         recentFileHistoryCheckBox.TabIndex = 3;
         recentFileHistoryCheckBox.Text = "Recent File History";
         recentFileHistoryCheckBox.UseVisualStyleBackColor = true;
         recentFileHistoryCheckBox.CheckedChanged += CheckBox_CheckedChanged;
         // 
         // allCheckBox
         // 
         allCheckBox.AutoSize = true;
         allCheckBox.Checked = true;
         allCheckBox.CheckState = CheckState.Checked;
         allCheckBox.Location = new Point(19, 42);
         allCheckBox.Margin = new Padding(6, 7, 6, 7);
         allCheckBox.Name = "allCheckBox";
         allCheckBox.Size = new Size(53, 29);
         allCheckBox.TabIndex = 1;
         allCheckBox.Text = "All";
         allCheckBox.UseVisualStyleBackColor = true;
         allCheckBox.CheckedChanged += AllCheckBox_CheckedChanged;
         // 
         // itemsTitleLabel
         // 
         itemsTitleLabel.AutoSize = true;
         itemsTitleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
         itemsTitleLabel.Location = new Point(11, 10);
         itemsTitleLabel.Margin = new Padding(6, 0, 6, 0);
         itemsTitleLabel.Name = "itemsTitleLabel";
         itemsTitleLabel.Size = new Size(171, 25);
         itemsTitleLabel.TabIndex = 0;
         itemsTitleLabel.Text = "Items To Clean Up";
         // 
         // instructions1Label
         // 
         instructions1Label.AutoSize = true;
         instructions1Label.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         instructions1Label.Location = new Point(239, 48);
         instructions1Label.Margin = new Padding(6, 0, 6, 0);
         instructions1Label.Name = "instructions1Label";
         instructions1Label.Size = new Size(722, 25);
         instructions1Label.TabIndex = 2;
         instructions1Label.Text = "This utility is designed to remove the Easy Pad-related items from the user’computer.";
         // 
         // bottomPanel
         // 
         bottomPanel.AutoSize = true;
         bottomPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
         bottomPanel.BackColor = Color.Cyan;
         bottomPanel.Controls.Add(cancelButton);
         bottomPanel.Controls.Add(okButton);
         bottomPanel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         bottomPanel.Location = new Point(283, 510);
         bottomPanel.Margin = new Padding(6, 7, 6, 7);
         bottomPanel.Name = "bottomPanel";
         bottomPanel.Size = new Size(241, 44);
         bottomPanel.TabIndex = 3;
         // 
         // cancelButton
         // 
         cancelButton.AutoSize = true;
         cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
         cancelButton.Location = new Point(156, 2);
         cancelButton.Margin = new Padding(6, 7, 6, 7);
         cancelButton.Name = "cancelButton";
         cancelButton.Size = new Size(79, 35);
         cancelButton.TabIndex = 1;
         cancelButton.Text = "&Cancel";
         cancelButton.UseVisualStyleBackColor = true;
         cancelButton.Click += CancelButton_Click;
         // 
         // okButton
         // 
         okButton.AutoSize = true;
         okButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
         okButton.Location = new Point(73, 2);
         okButton.Margin = new Padding(6, 7, 6, 7);
         okButton.Name = "okButton";
         okButton.Size = new Size(47, 35);
         okButton.TabIndex = 0;
         okButton.Text = "&OK";
         okButton.UseVisualStyleBackColor = true;
         okButton.Click += OkButton_Click;
         // 
         // instructions2Label
         // 
         instructions2Label.AutoSize = true;
         instructions2Label.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         instructions2Label.Location = new Point(262, 89);
         instructions2Label.Margin = new Padding(6, 0, 6, 0);
         instructions2Label.Name = "instructions2Label";
         instructions2Label.Size = new Size(730, 25);
         instructions2Label.TabIndex = 4;
         instructions2Label.Text = "Checking “All” results in the removal of the Easy Pad-related settings files and folders ";
         // 
         // instructions2aLabel
         // 
         instructions2aLabel.AutoSize = true;
         instructions2aLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         instructions2aLabel.Location = new Point(303, 122);
         instructions2aLabel.Margin = new Padding(6, 0, 6, 0);
         instructions2aLabel.Name = "instructions2aLabel";
         instructions2aLabel.Size = new Size(648, 25);
         instructions2aLabel.TabIndex = 5;
         instructions2aLabel.Text = "from the user's local AppData directory. It also removes all Easy_Pad-related\r\n";
         // 
         // instructions3Label
         // 
         instructions3Label.AutoSize = true;
         instructions3Label.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         instructions3Label.Location = new Point(262, 217);
         instructions3Label.Margin = new Padding(6, 0, 6, 0);
         instructions3Label.Name = "instructions3Label";
         instructions3Label.Size = new Size(581, 25);
         instructions3Label.TabIndex = 6;
         instructions3Label.Text = "Cleaning up the Cache results in the removal of all Easy Pad-related\r\n";
         // 
         // instructions2bLabel
         // 
         instructions2bLabel.AutoSize = true;
         instructions2bLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         instructions2bLabel.Location = new Point(303, 161);
         instructions2bLabel.Margin = new Padding(6, 0, 6, 0);
         instructions2bLabel.Name = "instructions2bLabel";
         instructions2bLabel.Size = new Size(725, 25);
         instructions2bLabel.TabIndex = 7;
         instructions2bLabel.Text = "files and folders from the user's \"My Documents\" directory and the recent history file.\r\n";
         // 
         // instructions4Label
         // 
         instructions4Label.AutoSize = true;
         instructions4Label.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         instructions4Label.Location = new Point(262, 313);
         instructions4Label.Margin = new Padding(6, 0, 6, 0);
         instructions4Label.Name = "instructions4Label";
         instructions4Label.Size = new Size(759, 25);
         instructions4Label.TabIndex = 8;
         instructions4Label.Text = "Cleaning up Themes results in the removal both the Easy Pad-related “Easy_Pad_Themes”";
         // 
         // instructions3aLabel
         // 
         instructions3aLabel.AutoSize = true;
         instructions3aLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         instructions3aLabel.Location = new Point(303, 267);
         instructions3aLabel.Margin = new Padding(6, 0, 6, 0);
         instructions3aLabel.Name = "instructions3aLabel";
         instructions3aLabel.Size = new Size(553, 25);
         instructions3aLabel.TabIndex = 9;
         instructions3aLabel.Text = "settings files and folders from the user's local AppData directory.\r\n";
         // 
         // instructions4aLabel
         // 
         instructions4aLabel.AutoSize = true;
         instructions4aLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         instructions4aLabel.Location = new Point(303, 362);
         instructions4aLabel.Margin = new Padding(6, 0, 6, 0);
         instructions4aLabel.Name = "instructions4aLabel";
         instructions4aLabel.Size = new Size(433, 25);
         instructions4aLabel.TabIndex = 10;
         instructions4aLabel.Text = "text file from the user's \"My Documents\" directory.";
         // 
         // instructions5aLabel
         // 
         instructions5aLabel.AutoSize = true;
         instructions5aLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         instructions5aLabel.Location = new Point(303, 478);
         instructions5aLabel.Margin = new Padding(6, 0, 6, 0);
         instructions5aLabel.Name = "instructions5aLabel";
         instructions5aLabel.Size = new Size(612, 25);
         instructions5aLabel.TabIndex = 12;
         instructions5aLabel.Text = "“Recent_File_History” text file from the user's \"My Documents\" directory.";
         // 
         // instructions5Label
         // 
         instructions5Label.AutoSize = true;
         instructions5Label.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
         instructions5Label.Location = new Point(262, 426);
         instructions5Label.Margin = new Padding(6, 0, 6, 0);
         instructions5Label.Name = "instructions5Label";
         instructions5Label.Size = new Size(658, 25);
         instructions5Label.TabIndex = 11;
         instructions5Label.Text = "Cleaning up Recent File History results in the removal of the Easy Pad-related\r\n";
         // 
         // EasyPadCleanerForm
         // 
         AutoScaleDimensions = new SizeF(15F, 37F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(1080, 596);
         Controls.Add(instructions5aLabel);
         Controls.Add(instructions5Label);
         Controls.Add(instructions4aLabel);
         Controls.Add(instructions3aLabel);
         Controls.Add(instructions4Label);
         Controls.Add(instructions2bLabel);
         Controls.Add(instructions3Label);
         Controls.Add(instructions2aLabel);
         Controls.Add(instructions2Label);
         Controls.Add(bottomPanel);
         Controls.Add(instructions1Label);
         Controls.Add(itemsPanel);
         Controls.Add(titleLabel);
         Icon = (Icon)resources.GetObject("$this.Icon");
         Margin = new Padding(6, 7, 6, 7);
         Name = "EasyPadCleanerForm";
         StartPosition = FormStartPosition.CenterScreen;
         Text = "Easy Pad Cleaner";
         Shown += Form_Shown;
         itemsPanel.ResumeLayout(false);
         itemsPanel.PerformLayout();
         bottomPanel.ResumeLayout(false);
         bottomPanel.PerformLayout();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      private Label titleLabel;
      private Panel itemsPanel;
      private CheckBox allCheckBox;
      private Label itemsTitleLabel;
      private CheckBox cacheCheckBox;
      private CheckBox documentsCheckBox;
      private CheckBox themesCheckBox;
      private CheckBox recentFileHistoryCheckBox;
      private CheckBox silentlyCheckBox;
      private Label instructions1Label;
      private Panel bottomPanel;
      private Button cancelButton;
      private Button okButton;
      private Label instructions2Label;
      private Label instructions2aLabel;
      private Label instructions3Label;
      private Label instructions2bLabel;
      private Label instructions4Label;
      private Label instructions3aLabel;
      private Label instructions4aLabel;
      private Label instructions5aLabel;
      private Label instructions5Label;
      private CheckBox checkBox1;
   }
}
