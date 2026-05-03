using EasyPad.Properties;

namespace EasyPad {
   public partial class EasyPadForm : Form {
      public EasyPadForm() {
         Opacity = 0;
         textBox = new MyTextBox {
            MaxLength = 9000000,
            Multiline = true,
            TabIndex = 0,
            AcceptsReturn = true,
            AcceptsTab = true,
            AllowDrop = true,
            Dock = DockStyle.Fill,
            Font = CreateNewFont(Settings.Default.TextFont),
            ContextMenuStrip = textBoxContextMenuStrip
         };
         textBox.TextChanged += TextBox_TextChanged;
         textBox.DragDrop += TextBox_DragDrop;
         textBox.DragEnter += TextBox_DragEnter;
         textBox.DragLeave += TextBox_DragLeave;
         textBox.KeyDown += TextBox_KeyDown;
         textBox.KeyUp += TextBox_KeyUp;
         textBox.Click += TextBox_Click;
         TopMost = true;//efm5 Temporarily just to force it to come out on top
         InitializeComponent();
         toolStripContainer.ContentPanel.Controls.Add(textBox);
         toolStrip.GripStyle = ToolStripGripStyle.Hidden;
         sForm = this;
         searchTextBox.Enter += (pSender, pE) => {
            this.BeginInvoke(new Action(() => {
               searchTextBox.SelectAll();
            }));
         };
         replaceTextBox.Enter += (pSender, pE) => {
            this.BeginInvoke(new Action(() => {
               replaceTextBox.SelectAll();
            }));
         };
      }
   }
}
