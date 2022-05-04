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
        public string data = "{}";
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
            public string Title { get; set; }
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
        }
        private void tested (object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Un gran bruh momento");
        }


        //this also works as a serializiation example for later
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
