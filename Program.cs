using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace SimpleTips
{
    /// <summary>Extension methods for EventHandler-type delegates.</summary>
    public static class EventExtensions
    {
        /// <summary>Raises the event (on the UI thread if available).</summary>
        /// <param name="multicastDelegate">The event to raise.</param>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        /// <returns>The return value of the event invocation or null if none.</returns>
        public static object Raise(this MulticastDelegate multicastDelegate, object sender, EventArgs e)
        {
            object retVal = null;

            MulticastDelegate threadSafeMulticastDelegate = multicastDelegate;
            if (threadSafeMulticastDelegate != null)
            {
                foreach (Delegate d in threadSafeMulticastDelegate.GetInvocationList())
                {
                    var synchronizeInvoke = d.Target as ISynchronizeInvoke;
                    if ((synchronizeInvoke != null) && synchronizeInvoke.InvokeRequired)
                    {
                        retVal = synchronizeInvoke.EndInvoke(synchronizeInvoke.BeginInvoke(d, new[] { sender, e }));
                    }
                    else
                    {
                        retVal = d.DynamicInvoke(new[] { sender, e });
                    }
                }
            }

            return retVal;
        }
    }

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
        UIOperator uiOperator;
        public Controller()
        {
            uiOperator = new UIOperator();
            
            t = new Thread( startUIThread);
            SynchronizationContext uiContext = SynchronizationContext.Current;
            t.Start(uiContext);
            //startUIThread();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            
            
            
        }
        public void onUIScanned(object test)
        {
            Highlighter.BufferList(test as List<DrawItem>);
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            t.Abort();
        }

        private void startUIThread(object state)
        {
            SynchronizationContext uiContext = state as SynchronizationContext;
            while (1 < 2)
            {
                var s = new Stopwatch();
                s.Start();
                var output = uiOperator.execute();
                s.Stop();
                // Console.WriteLine("UI loop:" + s.ElapsedMilliseconds);
                uiContext.Post(onUIScanned, output);
                // break;
                Thread.Sleep(10);
            }
        }
    }
}
