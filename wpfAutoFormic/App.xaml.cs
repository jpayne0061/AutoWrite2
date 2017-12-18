using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace wpfAutoFormic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private void AppStart(object ender, StartupEventArgs e)
        //{
        // Create the startup window
        //MainWindow wnd = new MainWindow();
        //wnd.Title = "Toodle McToodle";
        //if (e.Args.Length == 1)
        //{
        //    Process p = new Process();

        //    p.StartInfo.FileName = "notepad.exe";

        //    p.StartInfo.Arguments = "C:\\Windows\\" + e.Args[0];

        //    p.Start();
        //    //MessageBox.Show("Now opening file: \n\n" + e.Args[0]);

        //}




        //wnd.Show();
        //}
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }
    }
}
