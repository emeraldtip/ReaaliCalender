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
        private List<DailyTask> tasks = new List<DailyTask>();
        public string fname = "data.json";
        public string data = "[{\"ID\":0,\"Date\":\"\",\"Name\":\"\",\"ExtendedDesc\":\"\",\"Done\":false,\"NoDo\":false}]";
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
         * Id
         * DateTime
         * Task name
         * Task desc????
         * Task done
         * In progress
        */
        private class DailyTask
        {
            public int ID { get; set; }
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
            //parse datexx
            
            tasks = JsonSerializer.Deserialize<List<DailyTask>>(data);

            DailyTask currentTask;
            //this may be a bad Idea
            //too bad
            foreach (DailyTask task in tasks)
            {

                if (task.Date.ToShortDateString() == calendar.SelectedDate.Value.Date.ToShortDateString())
                {
                    //siia toppida mingi canvas koos mitme tekstivälja ja kahe checkboxiga
                    currentTask = task;
                    Label date = new Label();
                    date.Content = task.Date.ToShortDateString();
                    date.FontSize = 20;
                    Label time = new Label();
                    time.Content = task.Date.ToShortTimeString();
                    time.FontSize = 20;
                    TextBox name = new TextBox();
                    name.IsReadOnly = true;
                    name.Text = task.Name;
                    name.FontSize = 20;
                    name.Name = "taskdesc";
                    name.Tag = task.ID; 
                    //increase this if needed
                    name.Width = 200;
                    //trigger and setter cancer that is not even used
                    /*
                    Trigger t = new Trigger();
                    t.Property = UIElement.IsMouseOverProperty;
                    t.Value = false;
                    name.Triggers.Add(t);
                    */
                    //WHAT DO YOU MEAN MICROSOFT THAT THERE IS NO WORKAROUND BROOOOOOOOOOOOOOO. The issue was submitted in March. MARCH
                    name.ToolTip = task.ExtendedDesc;
                    
                    CheckBox done = new CheckBox();
                    done.Content = task.Done;
                    CheckBox inProgress = new CheckBox();
                    inProgress.Content = task.InProgress;
                    Button editButton = new Button();
                    editButton.Content = "Muuda";
                    editButton.Margin = new Thickness(5, 0, 0, 0);
                    editButton.Click += new RoutedEventHandler(Button_Click);

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.Children.Add(date);
                    stackPanel.Children.Add(time);
                    stackPanel.Children.Add(name);
                    stackPanel.Children.Add(done);
                    stackPanel.Children.Add(inProgress);
                    stackPanel.Children.Add(editButton);

                    datePanel.Children.Add(stackPanel);
                }
            }
            
            
        }
        
        //quicc test mesag
        private void ad (object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Un gran bruh momento");
        }

        //edit buttons thjings
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)((Button)sender).Parent;
            
            //cancelbutton
            Button cancel = new Button();
            cancel.Content = "Tühista";
            cancel.Click += new RoutedEventHandler(Button_Click);
            stackPanel.Children.Add(cancel);

            //see line on nagu uberimportant soo
            TextBox taskname = stackPanel.Children.OfType<TextBox>().Single(Child => Child.Name != null && Child.Name == "taskdesc");

            if ((String)((Button)sender).Content=="Muuda")
            {
                taskname.IsReadOnly = false;
                ((Button)sender).Content = "Salvesta";
                //tag of the button will store the previous value.
                ((Button)sender).Tag = taskname.Text;
            }
            else if ((String)((Button)sender).Content=="Salvesta")
            {
                stackPanel.Children.Remove((Button)stackPanel.Children.OfType<Button>().Single(Child => Child.Name != null && Child.Name == "Tühista"));
                taskname.IsReadOnly = true;
                ((Button)sender).Content = "Muuda";
                //del the prev value
                ((Button)sender).Tag = "";
                //insert saving routine here
                editTask("name",taskname.Text,(int)taskname.Tag);
            }
            else if ((String)((Button)sender).Content == "Tühista")
            {
                Button saveButton = (Button)stackPanel.Children.OfType<Button>().Single(Child => Child.Name != null && Child.Name == "Salvesta");
                saveButton.Content = "Muuda";
                taskname.Text = saveButton.Tag.ToString(); 
                saveButton.Tag = "";
                taskname.IsReadOnly = true;
                stackPanel.Children.Remove((Button)sender);
            }
        }

        
        private void editTask(string property, string updated, int id)
        {
            if (property == "name")
            {
                foreach (DailyTask task in tasks)
                {
                    if (task.ID == id)
                    {
                        task.Name = updated;
                    }
                }
            }
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
            dailyTask.ID = tasks.Count() + 1;
            //drop that into a list
            tasks.Add(dailyTask);
            //and write that into a file
            File.WriteAllText("data.json",JsonSerializer.Serialize<List<DailyTask>>(tasks));

        }
    }
}
