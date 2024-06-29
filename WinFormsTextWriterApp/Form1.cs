using System.Reflection;


namespace WinFormsTextWriterApp
{
    public partial class Form1 : Form
    {
        Module ViewModule { get; set; }

        object ViewWindow { get; set; }

        public Form1(Module viewModule, object viewWindow)
        {
            InitializeComponent();
            ViewModule = viewModule;
            ViewWindow = viewWindow;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ViewModule.GetType("WinFormsTextViewApp.Form1")?
                      .GetMethod("SetText")?
                      .Invoke(ViewWindow, new object[] { textBox1.Text });
        }
    }
}
