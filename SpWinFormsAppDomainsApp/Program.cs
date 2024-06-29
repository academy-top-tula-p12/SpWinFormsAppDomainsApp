using System.Reflection;

namespace SpWinFormsAppDomainsApp
{
    static class Program
    {
        static AppDomain Writer;
        static AppDomain Viewer;

        static Assembly WriterAssambly;
        static Assembly ViewerAssambly;

        static Form? WriterForm;
        static Form? ViewerForm;

        [STAThread]
        [LoaderOptimization(LoaderOptimization.MultiDomain)]
        static void Main()
        {
            Application.EnableVisualStyles();

            Viewer = AppDomain.CreateDomain("Viewer");
            Writer = AppDomain.CreateDomain("Writer");

            WriterAssambly = Writer.Load("WinFormsTextWriterApp.exe");
            ViewerAssambly = Viewer.Load("WinFormsTextViewApp.exe");

            WriterForm = Activator.CreateInstance
                (WriterAssambly.GetType("WinFormsTextWriterApp.Form1"), new object[]
                {
                    ViewerAssambly.GetModule("WinFormsTextViewApp.exe"),
                    ViewerForm
                }) as Form;
                                                            

            ViewerForm = Activator.CreateInstance(ViewerAssambly.GetType("WinFormsTextViewApp.Form1")) as Form;

            (new Thread(new ThreadStart(WriterRun))).Start();
            (new Thread(new ThreadStart(ViewerRun))).Start();
        }

        static void ViewerRun()
        {
            ViewerForm.ShowDialog();
            AppDomain.Unload(Viewer);
        }

        static void WriterRun()
        {
            WriterForm.ShowDialog();
            Application.Exit();
        }
    }
}