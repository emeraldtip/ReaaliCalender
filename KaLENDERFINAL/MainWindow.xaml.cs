using System;
using System.IO;
using System.Collections;
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
using System.Text.Json;
using System.Collections.Generic;
using System.Data;

namespace KaLENDERFINAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string fname = "data.json";
        public string data = "[{\"Date\":\"\",\"Name\":\"\",\"ExtendedDesc\":\"\",\"Done\":false,\"NoDo\":false}]";
        public MainWindow()
        {
         InitializeComponent();
            
            if (File.Exists(fname))
            {
                data = File.ReadAllText(fname);
            }
            
            //dateGrid.
        }
        //data formatting:
        /*
         * DateTime
         * Task name
         * Task desc????
         * Task done
         * In progress
        */
        private class DailyTask
        {
            public DateTime Date { get; set; }
            public string Name { get; set; }
            public string? ExtendedDesc { get; set; }
            public bool Done { get; set; }
            public bool InProgress { get; set; }
        }
        private void Calendar_SelectedDatesChanged(object sender,
            SelectionChangedEventArgs e)
        {
            StackPanel datePanel = (StackPanel)this.FindName("dateStack");
            datePanel.Children.Clear();
            //Get reference. (an anime one perhaps?) (no.)
            var calendar = sender as Calendar;
            //See if a date is selected. (what if no eyes doe)
            if (calendar.SelectedDate.HasValue)
            {
                DateTime date = calendar.SelectedDate.Value;
               this.Title= date.ToShortDateString();
            }
            //parse date
            List<DailyTask> tasks = new List<DailyTask>();
            tasks = JsonSerializer.Deserialize<List<DailyTask>>(data);

            //this may be a bad Idea
            //too bad
            foreach (DailyTask task in tasks)
            {

                if (task.Date.ToShortDateString() == calendar.SelectedDate.Value.Date.ToShortDateString())
                {
                    //siia toppida mingi canvas koos mitme tekstivälja ja kahe checkboxiga
                    Label date = new Label();
                    date.Content = task.Date.ToShortDateString();
                    date.FontSize = 20;
                    Label time = new Label();
                    time.Content = task.Date.ToShortTimeString();
                    time.FontSize = 20;
                    Label name = new Label();
                    name.Content = task.Name;
                    name.FontSize = 20;
                    CheckBox done = new CheckBox();
                    done.Content = task.Done;
                    CheckBox inProgress = new CheckBox();
                    inProgress.Content = task.InProgress;

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.Children.Add(date);
                    stackPanel.Children.Add(time);
                    stackPanel.Children.Add(done);
                    stackPanel.Children.Add(inProgress);

                    datePanel.Children.Add(stackPanel);
                }
            }
            
            
        }
        
        //quicc test mesag
        private void ad (object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Un gran bruh momento");
        }


        //this also works as a serializiation example for later
        private void tested(object sender, RoutedEventArgs e)
        {
            //input the things
            DailyTask dailyTask = new DailyTask();  
            dailyTask.Date = DateTime.Now;
            dailyTask.Name = "Magama";
            dailyTask.ExtendedDesc = "Go to slep lol";
            dailyTask.Done = false;
            dailyTask.InProgress = false;
            //drop that into a list
            List<DailyTask> tasks = new List<DailyTask> {dailyTask};
            //and write that into a file
            File.WriteAllText("data.json",JsonSerializer.Serialize<List<DailyTask>>(tasks));

        }
    }
}
