using static EasyPadCleaner.Program;

namespace EasyPadCleaner {
   public partial class EasyPadCleanerForm : Form {
      public EasyPadCleanerForm() {
         InitializeComponent();
      }

      private void Form_Shown(object sender, EventArgs e) {
         itemsPanel.Top = titleLabel.Bottom + 10;
         itemsTitleLabel.Location = new Point(5, 5);
         allCheckBox.Location = new Point(50, itemsTitleLabel.Bottom + 20);
         cacheCheckBox.Location = new Point(10, allCheckBox.Bottom + 30);
         themesCheckBox.Location = new Point(cacheCheckBox.Left, cacheCheckBox.Bottom + 10);
         recentFileHistoryCheckBox.Location = new Point(cacheCheckBox.Left, themesCheckBox.Bottom + 20);
         silentlyCheckBox.Location = new Point(allCheckBox.Left, recentFileHistoryCheckBox.Bottom + 10);
         instructions1Label.Location = new Point(itemsPanel.Right + 20, itemsPanel.Top);
         instructions2Label.Location = new Point(instructions1Label.Left + 20, instructions1Label.Bottom + 20);
         instructions2aLabel.Location = new Point(instructions2Label.Left + 20, instructions2Label.Bottom + 5);
         instructions2bLabel.Location = new Point(instructions2aLabel.Left, instructions2aLabel.Bottom + 5);
         instructions3Label.Location = new Point(instructions2Label.Left, instructions2bLabel.Bottom + 20);
         instructions3aLabel.Location = new Point(instructions2aLabel.Left, instructions3Label.Bottom + 5);
         instructions4Label.Location = new Point(instructions2Label.Left, instructions3aLabel.Bottom + 20);
         instructions4aLabel.Location = new Point(instructions2aLabel.Left, instructions4Label.Bottom + 5);
         instructions5Label.Location = new Point(instructions2Label.Left, instructions4aLabel.Bottom + 20);
         instructions5aLabel.Location = new Point(instructions2aLabel.Left, instructions5Label.Bottom + 5);
         bottomPanel.Location = new Point(1, Math.Max(itemsPanel.Bottom, instructions5Label.Bottom) + 15);
         int rightmost = 0;
         foreach (Label label in Controls.OfType<Label>()) {
            if (label.Right > rightmost)
               rightmost = label.Right;
         }
         ClientSize = new Size(rightmost + 20 + SystemInformation.VerticalScrollBarWidth,
            bottomPanel.Bottom + SystemInformation.HorizontalScrollBarHeight);
         titleLabel.Left = (ClientSize.Width - titleLabel.Width) / 2;
         itemsTitleLabel.Left = (itemsPanel.Width - itemsTitleLabel.Width) / 2;
         bottomPanel.Dock = DockStyle.Bottom;
         cancelButton.Left = ClientSize.Width - cancelButton.Width - 20;
         okButton.Left = cancelButton.Left - okButton.Width - 20;
      }

      private void CancelButton_Click(object sender, EventArgs e) {
         Close();
      }

      private void OkButton_Click(object sender, EventArgs e) {
         silent = silentlyCheckBox.Checked;
         if (cacheCheckBox.Checked == true)
            CleanCache();
         if (themesCheckBox.Checked == true)
            CleanThemes();
         if (recentFileHistoryCheckBox.Checked == true)
            CleanRecent();
         Close();
      }

      private void AllCheckBox_CheckedChanged(object? sender, EventArgs e) {
         if (allCheckBox.Checked) {
            cacheCheckBox.CheckedChanged -= CheckBox_CheckedChanged;
            themesCheckBox.CheckedChanged -= CheckBox_CheckedChanged;
            recentFileHistoryCheckBox.CheckedChanged -= CheckBox_CheckedChanged;
            cacheCheckBox.Checked = true;
            themesCheckBox.Checked = true;
            recentFileHistoryCheckBox.Checked = true;
            cacheCheckBox.CheckedChanged += CheckBox_CheckedChanged;
            themesCheckBox.CheckedChanged += CheckBox_CheckedChanged;
            recentFileHistoryCheckBox.CheckedChanged += CheckBox_CheckedChanged;
         }
      }

      private void CheckBox_CheckedChanged(object? sender, EventArgs e) {
         allCheckBox.CheckedChanged -= AllCheckBox_CheckedChanged;
         if (!cacheCheckBox.Checked)
            allCheckBox.Checked = false;
         else if (cacheCheckBox.Checked && themesCheckBox.Checked && recentFileHistoryCheckBox.Checked)
            allCheckBox.Checked = true;
         allCheckBox.CheckedChanged += AllCheckBox_CheckedChanged;
      }
   }
}
