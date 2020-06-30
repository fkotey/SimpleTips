using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.ContextMenuStrip = GetContent();
            notifyIcon.Icon = Properties.Resources.simple;
            notifyIcon.Visible = true;
                   
            Application.Run(new Controller()); 
        }
        
        private static ContextMenuStrip GetContent()
        {
            ContextMenuStrip cms = new ContextMenuStrip();
                cms.Items.Add("Exit", null, new EventHandler(Exit_Click));
            return cms;
        }

        private static void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    public class Controller : ApplicationContext
    {
        Thread t;
        public Controller()
        {
            t = new Thread(new ThreadStart(startUIThread));
            t.Start();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            t.Abort();
        }

        private static void startUIThread()
        {
            UIOperator uiOperator = new UIOperator();
            while (1 < 2)
            {
                var s = new Stopwatch();
                s.Start();
                uiOperator.execute();
                s.Stop();
                Console.WriteLine("UI loop:" + s.ElapsedMilliseconds);
                Thread.Sleep(10);
            }
        }
    }
}
