namespace WinFormsTextViewApp
{
    public partial class Form1 : Form
    {
        string SourceText;
        Font ViewFont { get; set; }
        public Form1()
        {
            InitializeComponent();
            ViewFont = new("Arial", 30);
        }

        public void SetText(string text)
        {
            SourceText = text;
            panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
        }
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = ViewFont;
            if (fontDialog.ShowDialog() == DialogResult.OK)
                ViewFont = fontDialog.Font;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if(SourceText.Length > 0)
            {
                Image imgText = new Bitmap(panel1.ClientRectangle.Width, panel1.ClientRectangle.Height);
                
                Graphics graphics = Graphics.FromImage(imgText);
                graphics.Clear(BackColor);

                graphics.DrawString(SourceText, ViewFont, Brushes.Red, ClientRectangle);
                e.Graphics.DrawImage(imgText, 0, 0);
            }
        }
    }
}
