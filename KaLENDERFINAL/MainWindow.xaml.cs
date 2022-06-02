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
        private int obengound = 0;
        private object Sneder;
        private List<DailyTask> tasks = new List<DailyTask>();
        public string fname = "data.json";
        public string data = "[{\"ID\":0,\"Date\":\"2069-06-09T15:21:50.1551516+03:00\",\"Name\":\"\",\"ExtendedDesc\":\"\",\"Done\":false,\"NoDo\":false}]";
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
            //ma vihkan warninguid soooo
            #pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public string? ExtendedDesc { get; set; }
            #pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public bool Done { get; set; }
            public bool InProgress { get; set; }
        }
        private void Calendar_SelectedDatesChanged(object sender,SelectionChangedEventArgs e)
        {
            Sneder = sender;
            UpdateTasks(sender);
        }
        private void focc(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, (StackPanel)sender);
            Keyboard.Focus((StackPanel)sender);
        }
        private void bruh(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("yeeee");
        }
        private void UpdateTasks(object sender)
        {
            StackPanel datePanel = (StackPanel)this.FindName("dateStack");
            datePanel.Children.Clear();
            //Get reference. (an anime one perhaps?) (no.)
            var calendar = sender as Calendar;
            //See if a date is selected. (what if no eyes doe)
            if (calendar.SelectedDate.HasValue)
            {
                ((Button)this.FindName("AddTaskButton")).IsEnabled = true;
                DateTime date = calendar.SelectedDate.Value;
                this.Title = date.ToShortDateString();
            }
            //updat
            if (File.Exists("data.json"))
            {
                tasks = JsonSerializer.Deserialize<List<DailyTask>>(File.ReadAllText("data.json"));
                //sort the things by date and stuff
                tasks.Sort(delegate (DailyTask i, DailyTask u) { return i.Date.CompareTo(u.Date); });
            }
            

            //this may be a bad Idea
            //too bad
            foreach (DailyTask task in tasks)
            {

                if (task.Date.ToShortDateString() == calendar.SelectedDate.Value.Date.ToShortDateString())
                {
                     //siia toppida mingi canvas koos mitme tekstivälja ja kahe checkboxiga
                    DatePicker date = new DatePicker();
                    date.SelectedDate = task.Date;
                    date.FontSize = 20;
                    date.Width = 150; 
                    date.Tag = task.Date;
                    date.Name = "no";
                    date.SelectedDateChanged += this.dateChanged;

                    TextBox time = new TextBox();
                    time.Text = task.Date.ToShortTimeString();
                    time.FontSize = 20;
                    time.Name = "time";
                    time.Tag = time.Text;
                    time.Width = 55;
                    time.IsReadOnly = true; 

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

                    ScaleTransform scale = new ScaleTransform(2.5, 2.5);

                    CheckBox done = new CheckBox();
                    done.IsChecked = task.Done;
                    done.Name = "done";
                    done.Tag = task.ID;
                    done.Width = 40;
                    done.Height = 40;
                    done.Margin = new Thickness(0,0,0,0);
                    done.RenderTransform = scale;
                    done.Checked += new RoutedEventHandler(checcer);
                    done.Unchecked += new RoutedEventHandler(checcer);

                    CheckBox inProgress = new CheckBox();
                    inProgress.IsChecked = task.InProgress;
                    inProgress.Name = "inprogress";
                    inProgress.Tag = task.ID;
                    inProgress.Width = 40;
                    inProgress.Height = 40;
                    inProgress.Margin = new Thickness(0, 0, 0, 0);
                    inProgress.RenderTransform = scale;
                    inProgress.Checked += new RoutedEventHandler(checcer);
                    inProgress.Unchecked += new RoutedEventHandler(checcer);

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
            Button a = new Button();
            a.Visibility = Visibility.Hidden;
            StackPanel e = new StackPanel();
            e.Children.Add(a);
            datePanel.Children.Add(e);
        }
        

        //quicc test mesag
        private void ad (object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Un gran bruh momento");
        }


        //edit buttons thjings
        
        private void dateChanged(object sender, RoutedEventArgs e)
        {
            DatePicker picker = (DatePicker)sender;
            if (picker.Name == "no")
            {
                picker.SelectedDate = (DateTime)picker.Tag;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)((Button)sender).Parent;
            
            

            //see line on nagu uberimportant bn
            TextBox taskname = stackPanel.Children.OfType<TextBox>().Single(Child => Child.Name != null && Child.Name == "taskdesc");
            TextBox time = stackPanel.Children.OfType<TextBox>().Single(Child => Child.Name != null && Child.Name == "time");
            DatePicker date = stackPanel.Children.OfType<DatePicker>().Single(Child => Child.Name != null);
            CheckBox done = stackPanel.Children.OfType<CheckBox>().Single(Child => Child.Name != null && Child.Name == "done");
            CheckBox inprogress = stackPanel.Children.OfType<CheckBox>().Single(Child => Child.Name != null && Child.Name == "inprogress");

            if ((String)((Button)sender).Content=="Muuda")
            {
                date.Name = "yes";
                taskname.IsReadOnly = false;
                time.IsReadOnly = false;
                ((Button)sender).Content = "Salvesta";
                //tag of the button will store the previous value.
                ((Button)sender).Tag = taskname.Text;
                //cancelbutton
                Button cancel = new Button();
                cancel.Content = "Tühista";
                cancel.Click += new RoutedEventHandler(Button_Click);
                stackPanel.Children.Add(cancel);
                obengound += 1;
            }
            else if ((String)((Button)sender).Content=="Salvesta")
            {
                string h, m;
                #pragma warning disable CS0168 // Variable is declared but never used
                try
                {    
                    if (!time.Text.Contains(':'))
                    {
                        throw new ArithmeticException(); //This is the way I've done this multiple tiems, idk if this is really the ideal solution but it works so
                    }
                    else
                    {
                        h = time.Text.Split(":")[0];
                        m = time.Text.Split(":")[1];
                        if (h.Length==2 && h[0]=='0') { h = h[1..]; }
                        if (m.Length==2 && m[0]=='0') { m = m[1..]; }
                        
                        if (int.Parse(h)> 24 || int.Parse(m) > 59)
                        {
                            throw new ArithmeticException(); //This is the way I've done this multiple tiems, idk if this is really the ideal solution but it works so
                        }
                    }

                    //insert saving routine here
                    date.Name = "no";
                    time.IsReadOnly = true;
                    taskname.IsReadOnly = true;
                    stackPanel.Children.Remove((Button)stackPanel.Children.OfType<Button>().Single(Child => Child.Content != null && (string)Child.Content == "Tühista"));
                    ((Button)sender).Content = "Muuda";
                    //del the prev value
                    ((Button)sender).Tag = "";
                    //time format checking
                    editTask("name", taskname.Text, (int)taskname.Tag);
                    editTask("date", (DateTime)date.SelectedDate + new TimeSpan(int.Parse(h), int.Parse(m), 0), (int)taskname.Tag);
                    obengound -= 1;
                    done.IsEnabled = true;
                    inprogress.IsEnabled = true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Incorrect time format. Correct format: (hour):(minute)", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //MessageBox.Show(ex.Message + ex.Source); 
                }
                #pragma warning restore CS0168 // Variable is declared but never used
                
            }
            else if ((String)((Button)sender).Content == "Tühista")
            {
                Button saveButton = (Button)stackPanel.Children.OfType<Button>().Single(Child => Child.Content != null && (string)Child.Content == "Salvesta");
                if (saveButton.Tag == null)
                {
                    ((StackPanel)this.FindName("dateStack")).Children.Remove(stackPanel);
                }
                else
                {
                    date.Name = "no";
                    time.IsReadOnly = true;
                    taskname.IsReadOnly = true;
                    saveButton.Content = "Muuda";
                    taskname.Text = saveButton.Tag.ToString();
                    saveButton.Tag = "";
                    stackPanel.Children.Remove((Button)sender);
                    obengound -= 1;
                }
                
            }
        }
        private void checcer(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            editTask(box.Name, box.IsChecked, (int)box.Tag);
            
        }
        private void addTaskButton(object sender, RoutedEventArgs e)
        {
            addTask();
        }
        private void addTask()
        {
            
            StackPanel datePanel = (StackPanel)this.FindName("dateStack");
            Calendar cal = (Calendar)this.FindName("cal");

            DatePicker date = new DatePicker();
            date.SelectedDate = cal.SelectedDate.Value;
            date.FontSize = 20;
            date.Width = 150;
            date.Tag = cal.SelectedDate;
            date.Name = "yes";
            date.SelectedDateChanged += this.dateChanged;

            TextBox time = new TextBox();
            time.Text = "";
            time.FontSize = 20;
            time.Name = "time";
            time.Tag = time.Text;
            time.IsReadOnly = false;

            TextBox name = new TextBox();
            name.IsReadOnly = false;
            name.Text = "";
            name.FontSize = 20;
            name.Name = "taskdesc";
            name.Tag = tasks.Count + 1 + obengound;
            //increase this if needed
            name.Width = 200;

            //WHAT DO YOU MEAN MICROSOFT THAT THERE IS NO WORKAROUND BROOOOOOOOOOOOOOO. The issue was submitted in March. MARCH (tooltip appears for like 2 ms and then disappears is the issue)
            name.ToolTip = "";

            ScaleTransform scale = new ScaleTransform(2.5, 2.5);

            CheckBox done = new CheckBox();
            done.IsChecked = false;
            done.Name = "done";
            done.IsEnabled = false;
            done.Tag = tasks.Count + 1 + obengound;
            done.Width = 40;
            done.Height = 40;
            done.Margin = new Thickness(0, 0, 0, 0);
            done.RenderTransform = scale;
            done.Checked += new RoutedEventHandler(checcer);
            done.Unchecked += new RoutedEventHandler(checcer);

            CheckBox inProgress = new CheckBox();
            inProgress.IsChecked = false;
            inProgress.Name = "inprogress";
            inProgress.IsEnabled = false;
            inProgress.Tag = tasks.Count + 1 + obengound;
            inProgress.Width = 40;
            inProgress.Height = 40;
            inProgress.Margin = new Thickness(0, 0, 0, 0);
            inProgress.RenderTransform = scale;
            inProgress.Checked += new RoutedEventHandler(checcer);
            inProgress.Unchecked += new RoutedEventHandler(checcer);

            Button editButton = new Button();
            editButton.Content = "Salvesta";
            editButton.Margin = new Thickness(5, 0, 0, 0);
            editButton.Click += new RoutedEventHandler(Button_Click);

            Button cancel = new Button();
            cancel.Content = "Tühista";
            cancel.Click += new RoutedEventHandler(Button_Click);

            time.Width = 55;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Children.Add(date);
            stackPanel.Children.Add(time);
            stackPanel.Children.Add(name);
            stackPanel.Children.Add(done);
            stackPanel.Children.Add(inProgress);
            stackPanel.Children.Add(editButton);
            stackPanel.Children.Add(cancel);

            datePanel.Children.Add(stackPanel);
            obengound += 1;
        }
        
        private void editTask(string property, object updated, int id)
        {
            bool neww = true;
            if (property == "name")
            {
                foreach (DailyTask task in tasks)
                {
                    if (task.ID == id)
                    {
                        neww = false;
                        task.Name = (string)updated;
                        File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                        //on probably parem viis selle tegemiseks, aga see on kõige lihtsam praegu
                    }
                }
                if (neww)
                {
                    DailyTask task = new DailyTask();
                    task.ID = id;
                    task.Name = (string)updated;
                    tasks.Add(task);
                    File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                }
            }
            else if (property == "date")
            {
                foreach (DailyTask task in tasks)
                {
                    if (task.ID == id)
                    {
                        neww = false;
                        task.Date = (DateTime)updated;
                        File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));

                    }
                }
                if (neww)
                {
                    DailyTask task = new DailyTask();
                    task.ID = id;
                    task.Date = (DateTime)updated;
                    tasks.Add(task);
                    File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                }
            }
            else if (property == "done")
            {
                foreach (DailyTask task in tasks)
                {
                    if (task.ID == id)
                    {
                        neww = false;
                        task.Done = (bool)updated;
                        File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                    }
                }
                if (neww)
                {
                    DailyTask task = new DailyTask();
                    task.ID = id;
                    task.Done = (bool)updated;
                    tasks.Add(task);
                    File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                }
            }
            else if (property == "inprogress")
            {
                foreach (DailyTask task in tasks)
                {
                    if (task.ID == id)
                    {
                        neww = false;
                        task.InProgress = (bool)updated;
                        File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                    }
                }
                if (neww)
                {
                    DailyTask task = new DailyTask();
                    task.ID = id;
                    task.InProgress = (bool)updated;
                    tasks.Add(task);
                    File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                }
            }

            if (obengound < 2)
            {
                UpdateTasks(Sneder);
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
            dailyTask.ID = tasks.Count + 1;
            //drop that into a list
            tasks.Add(dailyTask);
            //and write that into a file
            File.WriteAllText("data.json",JsonSerializer.Serialize<List<DailyTask>>(tasks));
            UpdateTasks(Sneder);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)((TabItem)((TabControl)sender).SelectedItem).Header == "Kõik ülesanded")
            {
                MessageBox.Show("bruh moetn");
            }
        }

        private void Chang(object sender, SelectionChangedEventArgs ee)
        {
            if (((DatePicker)this.FindName("StartDate")).SelectedDate != null && ((DatePicker)this.FindName("EndDate")).SelectedDate != null)
            {
                refreshTrello();
            }
        }

        //yes I'm using the name of another todo app
        private void refreshTrello()
        {
            for (DateTime dt = (DateTime)((DatePicker)this.FindName("StartDate")).SelectedDate.Value; dt <= (DateTime)((DatePicker)this.FindName("EndDate")).SelectedDate.Value; dt = dt.AddDays(1))
            {
                foreach (DailyTask task in tasks)
                {
                    if (task.Date.ToShortDateString() == dt.Date.ToShortDateString())
                    {
                        //siia toppida mingi canvas koos mitme tekstivälja ja kahe checkboxiga
                        DatePicker date = new DatePicker();
                        date.SelectedDate = task.Date;
                        date.FontSize = 20;
                        date.Width = 150;
                        date.Tag = task.Date;
                        date.Name = "no";
                        date.SelectedDateChanged += this.dateChanged;

                        TextBox time = new TextBox();
                        time.Text = task.Date.ToShortTimeString();
                        time.FontSize = 20;
                        time.Name = "time";
                        time.Tag = time.Text;
                        time.Width = 55;
                        time.IsReadOnly = true;

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

                        ScaleTransform scale = new ScaleTransform(2.5, 2.5);

                        Button editButton = new Button();
                        editButton.Content = "Muuda";
                        editButton.Margin = new Thickness(5, 0, 0, 0);
                        editButton.Click += new RoutedEventHandler(Button_Click);


                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;
                        stackPanel.Children.Add(date);
                        stackPanel.Children.Add(time);
                        stackPanel.Children.Add(name);
                        stackPanel.Children.Add(editButton);

                        if (task.Done)
                        {
                            ((StackPanel)this.FindName("doneStack")).Children.Add(stackPanel);
                        }
                        else if (task.InProgress)
                        {
                            ((StackPanel)this.FindName("progressStack")).Children.Add(stackPanel);
                        }
                        else
                        {
                            ((StackPanel)this.FindName("unDoneStack")).Children.Add(stackPanel);
                        }
                    }
                }
            
           
            }
            //this is for better visiblity in the stackpanel
            //Also I think there's a better way of doing this but my 5 min googling didn't turn up anything useful

            Button a = new Button();
            a.Visibility = Visibility.Hidden;
            StackPanel e = new StackPanel();
            e.Children.Add(a);
            ((StackPanel)this.FindName("doneStack")).Children.Add(e);

            Button b = new Button();
            b.Visibility = Visibility.Hidden;
            StackPanel f = new StackPanel();
            e.Children.Add(b);
            ((StackPanel)this.FindName("progressStack")).Children.Add(f);

            Button c = new Button();
            c.Visibility = Visibility.Hidden;
            StackPanel g = new StackPanel();
            g.Children.Add(c);
            ((StackPanel)this.FindName("unDoneStack")).Children.Add(g);
        }
    }
}
