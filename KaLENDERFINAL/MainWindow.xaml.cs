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
        }
        //data formatting:
        /*
         * DateTime
         * Task name
         * Task desc????
         * Task done
         * Task ain't gonna do it
        */
        private class DailyTask
        {
            public DateTime Date { get; set; }
            public string Name { get; set; }
            public string? ExtendedDesc { get; set; }
            public bool Done { get; set; }
            public bool NoDo { get; set; }
        }
        private void Calendar_SelectedDatesChanged(object sender,
            SelectionChangedEventArgs e)
        {
            List<DailyTask> tasks = new List<DailyTask>();
            //Get reference. (an anime one perhaps?) (no.)
            var calendar = sender as Calendar;
            tasks = JsonSerializer.Deserialize<List<DailyTask>>(data);
            //See if a date is selected. (what if no eyes doe)
            if (calendar.SelectedDate.HasValue)
            {
                DateTime date = calendar.SelectedDate.Value;
                this.Title= date.ToShortDateString();
            }
            //Add a check here if any of the dates in the list match somehow
            //vb vaja teha mingi date list et teha quick searchi aga äkki otsida et kas saab otsida class elementide kaupa
        }
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
            dailyTask.NoDo = false;
            //drop that into a list
            List<DailyTask> tasks = new List<DailyTask> {dailyTask};
            //and write that into a file
            File.WriteAllText("data.json",JsonSerializer.Serialize<List<DailyTask>>(tasks));

        }
    }
}
