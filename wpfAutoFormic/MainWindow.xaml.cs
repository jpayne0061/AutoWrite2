using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using wpfAutoFormic;


namespace wpfAutoFormic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentGroup = "";

        public MainWindow()
        {
            InitializeComponent();
            

            //Process notepad = Process.GetProcessesByName("notepad").SingleOrDefault();

            //Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(notepad.MainModule.FileName);

            //System.Windows.Controls.Image img = new System.Windows.Controls.Image();

            //img.Source = ico.ToBitmap().ToBitmapImage();

            //gridMain.Children.Add(img);

        }


        private void NavToAdd(object sender, RoutedEventArgs e)//By Prince Jain 
        {
            Add window1 = new Add();
            this.Visibility = Visibility.Hidden;
            this.Close();
            // window1.Show(); // Win10 tablet in tablet mode, use this, when sub Window is closed, the main window will be covered by the Start menu.
            window1.ShowDialog();
            
        }

        private string[] GetAllLines(string p)
        {
            string path = p;
            string[] lines = File.ReadAllLines(path);

            return lines;
        }

        private void Show_Names(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Height = 360;


            string[] lines = GetAllLines(@"..\..\data\data.txt");

            Grid g = new Grid();
            //g.Height = 150;

            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Content = g;
            //scrollViewer.Height = 150;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            //g

            for (var i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split('|');

                System.Windows.Application.Current.MainWindow.Height += 20;

                System.Windows.Controls.Button MyControl1 = new System.Windows.Controls.Button();
                //use enum here instead of magic number
                MyControl1.Content = line[1];
                MyControl1.Name = line[2].Replace(" ", "");
                MyControl1.Click += button1_Click;
                //MyControl1.Height = 20;

                g.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(MyControl1, i);
                g.Children.Add(MyControl1);

                //gridMain.Height = 200;
                gridMain.Content = g;

            }

        }


        public void GetScriptsByGroup(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Height = 360;

            var groupName = ((System.Windows.Controls.Button)sender).Name;

            currentGroup = groupName;

            string[] lines = GetAllLines(@"..\..\data\data.txt").Where(l => l.Split('|')[3].Replace(" ", "") == groupName).ToArray();

            Grid g = new Grid();
            //g.Height = 150;

            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Content = g;
            //scrollViewer.Height = 150;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;



            for (var i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split('|');

                

                System.Windows.Application.Current.MainWindow.Height += 20;

                System.Windows.Controls.Button MyControl1 = new System.Windows.Controls.Button();
                //use enum here instead of magic number
                MyControl1.Content = line[1];
                MyControl1.Name = line[2].Replace(" ", "");
                MyControl1.Click += button1_Click;
                //MyControl1.Height = 20;

                g.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(MyControl1, i);
                g.Children.Add(MyControl1);

                //gridMain.Height = 200;

            }

            gridMain.Content = g;
        }


        public void Get_Groups(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Height = 360;

            System.Windows.Controls.Button MyControl1 = new System.Windows.Controls.Button();
            MyControl1.Content = "Add Group";
            MyControl1.Name = "Add_Group";
            MyControl1.Click += Add_Group;

            Grid g = new Grid();

            g.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(MyControl1, 0);
            g.Children.Add(MyControl1);

            gridMain.Content = g;

            //loop through and get groups
            string[] groupLines = GetAllLines(@"..\..\data\groups.txt");

            for (var i = 1; i < groupLines.Length; i++)
            {

                System.Windows.Application.Current.MainWindow.Height += 20;

                System.Windows.Controls.Button GroupControl = new System.Windows.Controls.Button();
                //use enum here instead of magic number
                GroupControl.Content = groupLines[i];
                GroupControl.Name = groupLines[i].Replace(" ", "");
                GroupControl.Click += GetScriptsByGroup;
                //MyControl1.Height = 20;

                g.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(GroupControl, i);
                g.Children.Add(GroupControl);

                //gridMain.Height = 200;
                gridMain.Content = g;

            }

        }

        public void Add_Group(object sender, RoutedEventArgs e)
        {


            System.Windows.Controls.Label groupLabel = new System.Windows.Controls.Label();
            groupLabel.Content = "Enter Group Name";

            System.Windows.Controls.TextBox groupName = new System.Windows.Controls.TextBox();
            groupName.Name = "Group_Name";

            System.Windows.Controls.Button groupButton = new System.Windows.Controls.Button();
            groupButton.Content = "Save Group";
            groupButton.Name = "Save_Group";
            groupButton.Click += Save_Group;



            Grid g = new Grid();
            g.RowDefinitions.Add(new RowDefinition());
            g.RowDefinitions.Add(new RowDefinition());
            g.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(groupLabel, 0);
            Grid.SetRow(groupName, 1);
            Grid.SetRow(groupButton, 2);

            g.Children.Add(groupLabel);
            g.Children.Add(groupName);
            g.Children.Add(groupButton);

            gridMain.Content = g;

        }

        public void Save_Group(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox ui = (System.Windows.Controls.TextBox)((System.Windows.Controls.Grid)gridMain.Content).Children
                                                          .Cast<UIElement>()
                                                          .First(c => Grid.GetRow(c) == 1);

            string groupName = ui.Text;


            string path = @"..\..\data\groups.txt";

            File.AppendAllText(path, groupName + "\r\n");

            Get_Groups(sender, e);
            //string text = gridMain.Content.
        }




        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);


        private void button1_Click(object sender, EventArgs e)
        {
            string name = ((System.Windows.Controls.Button)sender).Name;

            string[] lines = GetAllLines(@"..\..\data\data.txt");


            string line = lines.Where(l => l.Split('|')[2].Replace(" ", "") == name).SingleOrDefault();

            string text = line.Split('|')[1];


            Thread.Sleep(3000);

            SendKeys.SendWait(text);

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Controls.Label groupLabel = new System.Windows.Controls.Label();
            groupLabel.Content = currentGroup != "" ? "Enter Script Name(currently in group "+ currentGroup + ")" : "Enter Script Name(no group)";


            System.Windows.Controls.TextBox groupName = new System.Windows.Controls.TextBox();
            groupName.Name = "Script_Name";

            System.Windows.Controls.TextBox sciptText = new System.Windows.Controls.TextBox();
            groupName.Name = "Script_Text";

            System.Windows.Controls.Button groupButton = new System.Windows.Controls.Button();
            groupButton.Content = "Save Script";
            groupButton.Name = "Save_Group";
            groupButton.Click += Save_Script;

            Grid g = new Grid();
            g.RowDefinitions.Add(new RowDefinition());
            g.RowDefinitions.Add(new RowDefinition());
            g.RowDefinitions.Add(new RowDefinition());
            g.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(groupLabel, 0);
            Grid.SetRow(groupName, 1);
            Grid.SetRow(sciptText, 2);
            Grid.SetRow(groupButton, 3);

            g.Children.Add(groupLabel);
            g.Children.Add(groupName);
            g.Children.Add(sciptText);
            g.Children.Add(groupButton);

            gridMain.Content = g;

        }

        public void Save_Script(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox scriptName = (System.Windows.Controls.TextBox)((System.Windows.Controls.Grid)gridMain.Content).Children
                                              .Cast<UIElement>()
                                              .First(c => Grid.GetRow(c) == 1);

            System.Windows.Controls.TextBox scriptText = (System.Windows.Controls.TextBox)((System.Windows.Controls.Grid)gridMain.Content).Children
                                  .Cast<UIElement>()
                                  .First(c => Grid.GetRow(c) == 2);



            string text = scriptText.Text;

            string name = scriptName.Text;

            string path = @"..\..\data\data.txt";

            string[] lines = GetAllLines(path);

            string last = lines[lines.Length - 1];

            string[] split = last.Split('|');

            int num = Int32.Parse(split[0]);

            int newNum = num + 1;

            string newLine = newNum.ToString() + "|" + text + "|" + name + "|" + currentGroup + "\r\n";

            File.AppendAllText(path, newLine);


        }

        //private void pnlMainGrid_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    MessageBox.Show("You clicked me at " + e.GetPosition(this).ToString());
        //}

        //private void pnlMainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void btnClickMe_Click(object sender, RoutedEventArgs e)
        //{
        //    lbResult.Items.Add(pnlMain.FindResource("strPanel").ToString());
        //    lbResult.Items.Add(this.FindResource("strWindow").ToString());
        //    lbResult.Items.Add(Application.Current.FindResource("strApp").ToString());
        //}
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    string s = null;
        //    try
        //    {
        //        s.Trim();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //    s.Trim();
        //}
    }
}
