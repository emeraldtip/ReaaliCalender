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
using HtmlAgilityPack;

namespace KaLENDERFINAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DragDropEffects notAllowed;
        private StackPanel draggable;
        private int obengound = 0;
        private object Sneder;
        public static List<DailyTask> tasks = new List<DailyTask>();
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists("data.json"))
            {
                tasks = JsonSerializer.Deserialize<List<DailyTask>>(File.ReadAllText("data.json"));
                //sort the things by date and stuff
                tasks.Sort(delegate (DailyTask i, DailyTask u) { return i.Date.CompareTo(u.Date); });

            }
            if (!File.Exists("data2.json"))
            {
                InitialWizard initialWizard = new InitialWizard();
                initialWizard.ShowDialog();
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
        public class DailyTask
        {
            public int ID { get; set; }
            public DateTime Date { get; set; }
            public string Name { get; set; }
            //ma vihkan warninguid soooo
            #pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            //see on nyyd unused sest selle muutmise implementeerimine oleks olnud väga janky
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
            if (calendar.SelectedDate != null)
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
            if (calendar.SelectedDate != null)
            {
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
                        //name.ToolTip = task.ExtendedDesc;

                        ScaleTransform scale = new ScaleTransform(2.5, 2.5);

                        CheckBox done = new CheckBox();
                        done.IsChecked = task.Done;
                        done.Name = "done";
                        done.Tag = task.ID;
                        done.Width = 40;
                        done.Height = 40;
                        done.Margin = new Thickness(0, 0, 0, 0);
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

                        Button removeButton = new Button();
                        removeButton.Content = "Eemalda";
                        removeButton.Margin = new Thickness(0, 0, 0, 0);
                        removeButton.Tag= task.ID;
                        removeButton.Click += new RoutedEventHandler(removeClick);

                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;
                        stackPanel.Children.Add(date);
                        stackPanel.Children.Add(time);
                        stackPanel.Children.Add(name);
                        stackPanel.Children.Add(done);
                        stackPanel.Children.Add(inProgress);
                        stackPanel.Children.Add(editButton);
                        stackPanel.Children.Add(removeButton);

                        datePanel.Children.Add(stackPanel);
                    }
                }
                Button a = new Button();
                a.Visibility = Visibility.Hidden;
                StackPanel e = new StackPanel();
                e.Children.Add(a);
                datePanel.Children.Add(e);
            }
            
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
        private void removeClick(object sender, RoutedEventArgs e)
        {
            foreach (DailyTask task in tasks)
            {
                if (task.ID == (int)((Button)sender).Tag)
                {
                    tasks.Remove(task);
                    File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                    //same thing wiht this thing I did this in a really weird way before
                    UpdateTasks(this.FindName("cal"));
                    ((Button)sender).Background = Brushes.Red;
                    if ((string)((TabItem)((TabControl)this.FindName("DabGondrol")).SelectedItem).Header == "Kõik ülesanded")
                    {
                        if (((DatePicker)this.FindName("StartDate")).SelectedDate != null && ((DatePicker)this.FindName("EndDate")).SelectedDate != null)
                        {
                            refreshTrello();
                        }
                    }
                    return;
                }
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)((Button)sender).Parent;
            
            

            //see line on nagu uberimportant bn
            TextBox taskname = stackPanel.Children.OfType<TextBox>().Single(Child => Child.Name != null && Child.Name == "taskdesc");
            TextBox time = stackPanel.Children.OfType<TextBox>().Single(Child => Child.Name != null && Child.Name == "time");
            DatePicker date = stackPanel.Children.OfType<DatePicker>().Single(Child => Child.Name != null);
            CheckBox done = new CheckBox();
            CheckBox inprogress = new CheckBox();
            Button rembutton = new Button();
            try
            {
                rembutton = stackPanel.Children.OfType<Button>().Single(Child => Child.Content != null && (string)Child.Content == "Eemalda");
            }
            catch (Exception) {}
            
            if ((string)((TabItem)((TabControl)this.FindName("DabGondrol")).SelectedItem).Header != "Kõik ülesanded")
            {
                done = stackPanel.Children.OfType<CheckBox>().Single(Child => Child.Name != null && Child.Name == "done");
                inprogress = stackPanel.Children.OfType<CheckBox>().Single(Child => Child.Name != null && Child.Name == "inprogress");
            }
            

            if ((String)((Button)sender).Content=="Muuda")
            {
                try
                {
                    stackPanel.Children.Remove(rembutton);
                }
                catch (Exception) { }
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
                    if ((string)((TabItem)((TabControl)this.FindName("DabGondrol")).SelectedItem).Header == "Kõik ülesanded" && ((DatePicker)this.FindName("StartDate")).SelectedDate == null && ((DatePicker)this.FindName("EndDate")).SelectedDate == null)
                    {
                        ((DatePicker)this.FindName("StartDate")).SelectedDate = date.SelectedDate;
                        ((DatePicker)this.FindName("EndDate")).SelectedDate = date.SelectedDate;
                    }

                    Button removeButton = new Button();
                    removeButton.Content = "Eemalda";
                    removeButton.Margin = new Thickness(0, 0, 0, 0);
                    removeButton.Tag = taskname.Tag;
                    removeButton.Click += new RoutedEventHandler(removeClick);
                    stackPanel.Children.Add(removeButton);

                }
                catch(Exception)
                {
                    MessageBox.Show("Incorrect time format. Correct format: (hour):(minute)", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //MessageBox.Show(ex.Message + ex.Source); 
                }
                
                
            }
            else if ((String)((Button)sender).Content == "Tühista")
            {
                Button saveButton = (Button)stackPanel.Children.OfType<Button>().Single(Child => Child.Content != null && (string)Child.Content == "Salvesta");
                if (saveButton.Tag == null)
                {
                    if ((string)((TabItem)((TabControl)this.FindName("DabGondrol")).SelectedItem).Header != "Kõik ülesanded")
                    {
                        ((StackPanel)this.FindName("dateStack")).Children.Remove(stackPanel);
                    }
                    else
                    {
                        ((StackPanel)this.FindName("unDoneStack")).Children.Remove(stackPanel);
                    }
                        
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

                    Button removeButton = new Button();
                    removeButton.Content = "Eemalda";
                    removeButton.Margin = new Thickness(0, 0, 0, 0);
                    removeButton.Tag = taskname.Tag;
                    removeButton.Click += new RoutedEventHandler(removeClick);
                    stackPanel.Children.Add(removeButton);
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
            //this certainly looks ugly
            StackPanel datePanel = (StackPanel)this.FindName("dateStack");
            DatePicker date = new DatePicker();

            if ((string)((TabItem)((TabControl)this.FindName("DabGondrol")).SelectedItem).Header == "Kõik ülesanded")
            {
                datePanel = (StackPanel)this.FindName("unDoneStack");
                date.SelectedDate = DateTime.Today;
            }
            else
            {
                Calendar cal = (Calendar)this.FindName("cal");
                date.SelectedDate = cal.SelectedDate.Value;
            }
            

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
            name.Tag = tasks.Count + 2 + obengound;

            name.Width = 130;

            //WHAT DO YOU MEAN MICROSOFT THAT THERE IS NO WORKAROUND BROOOOOOOOOOOOOOO. The issue was submitted in March. MARCH (tooltip appears for like 2 ms and then disappears is the issue)
            name.ToolTip = "";

            ScaleTransform scale = new ScaleTransform(2.5, 2.5);

            CheckBox done = new CheckBox();
            CheckBox inProgress = new CheckBox();
            if (datePanel.Name != "unDoneStack")
            {
                //increase this if needed
                name.Width = 200;

                done.IsChecked = false;
                done.Name = "done";
                done.IsEnabled = false;
                done.Tag = tasks.Count + 2 + obengound;
                done.Width = 40;
                done.Height = 40;
                done.Margin = new Thickness(0, 0, 0, 0);
                done.RenderTransform = scale;
                done.Checked += new RoutedEventHandler(checcer);
                done.Unchecked += new RoutedEventHandler(checcer);

                inProgress.IsChecked = false;
                inProgress.Name = "inprogress";
                inProgress.IsEnabled = false;
                inProgress.Tag = tasks.Count + 2 + obengound;
                inProgress.Width = 40;
                inProgress.Height = 40;
                inProgress.Margin = new Thickness(0, 0, 0, 0);
                inProgress.RenderTransform = scale;
                inProgress.Checked += new RoutedEventHandler(checcer);
                inProgress.Unchecked += new RoutedEventHandler(checcer);
            }

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

            //I could have put both of them in the same if statement but I want to keep the structure

            if (datePanel.Name != "unDoneStack")
            {
                stackPanel.Children.Add(done);
                stackPanel.Children.Add(inProgress);
            }
                
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
                        task.InProgress = false;
                        File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                    }
                }
                if (neww)
                {
                    DailyTask task = new DailyTask();
                    task.ID = id;
                    task.Done = (bool)updated;
                    task.InProgress = false;
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
                        task.Done = false;
                        File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                    }
                }
                if (neww)
                {
                    DailyTask task = new DailyTask();
                    task.ID = id;
                    task.InProgress = (bool)updated;
                    task.Done = false;
                    tasks.Add(task);
                    File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                }
            }

            if (obengound < 2)
            {
                if ((string)((TabItem)((TabControl)this.FindName("DabGondrol")).SelectedItem).Header != "Kõik ülesanded")
                {
                    UpdateTasks(Sneder);
                }
                else
                {
                    if (((DatePicker)this.FindName("StartDate")).SelectedDate != null && ((DatePicker)this.FindName("EndDate")).SelectedDate != null)
                    {
                        refreshTrello();
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
            //dailyTask.ExtendedDesc = "Go to slep lol";
            dailyTask.Done = false;
            dailyTask.InProgress = false;
            dailyTask.ID = tasks.Count + 1;
            //drop tt into a list
            tasks.Add(dailyTask);
            //and write that into a file
            File.WriteAllText("data.json",JsonSerializer.Serialize<List<DailyTask>>(tasks));
            UpdateTasks(Sneder);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)((TabItem)((TabControl)sender).SelectedItem).Header == "Kõik ülesanded")
            {
                if (((DatePicker)this.FindName("StartDate")).SelectedDate != null && ((DatePicker)this.FindName("EndDate")).SelectedDate != null)
                {
                    refreshTrello();
                }
            }
            else
            {
                UpdateTasks((Calendar)this.FindName("cal"));
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
            ((StackPanel)this.FindName("doneStack")).Children.Clear();
            ((StackPanel)this.FindName("unDoneStack")).Children.Clear();
            ((StackPanel)this.FindName("progressStack")).Children.Clear();

            for (DateTime dt = (DateTime)((DatePicker)this.FindName("StartDate")).SelectedDate.Value; dt <= (DateTime)((DatePicker)this.FindName("EndDate")).SelectedDate.Value; dt = dt.AddDays(1))
            {
                foreach (DailyTask task in tasks)
                {
                    if (task.Date.ToShortDateString() == dt.Date.ToShortDateString())
                    {
                        Label mover = new Label();
                        mover.Content = "≡";
                        mover.MouseDown += new MouseButtonEventHandler(dragMouseDown);

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
                        name.Width = 130;
                        //trigger and setter cancer that is not even used
                        /*
                        Trigger t = new Trigger();
                        t.Property = UIElement.IsMouseOverProperty;
                        t.Value = false;
                        name.Triggers.Add(t);
                        */
                        //WHAT DO YOU MEAN MICROSOFT THAT THERE IS NO WORKAROUND BROOOOOOOOOOOOOOO. The issue was submitted in March. MARCH
                        ///name.ToolTip = task.ExtendedDesc;

                        ScaleTransform scale = new ScaleTransform(2.5, 2.5);

                        Button editButton = new Button();
                        editButton.Content = "Muuda";
                        editButton.Margin = new Thickness(0, 0, 0, 0);
                        editButton.Click += new RoutedEventHandler(Button_Click);

                        Button removeButton = new Button();
                        removeButton.Content = "Eemalda";
                        removeButton.Margin = new Thickness(0, 0, 0, 0);
                        removeButton.Tag = task.ID;
                        removeButton.Click += new RoutedEventHandler(removeClick);

                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;
                        stackPanel.Children.Add(mover);
                        stackPanel.Children.Add(date);
                        stackPanel.Children.Add(time);
                        stackPanel.Children.Add(name);
                        stackPanel.Children.Add(editButton);
                        stackPanel.Children.Add(removeButton);


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

        void dragMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (draggable == null)
            {
                StackPanel dragParent = (StackPanel)((StackPanel)((Label)sender).Parent).Parent;
                draggable = (StackPanel)((Label)sender).Parent;
                dragParent.Children.Remove(draggable);

                ((Canvas)this.FindName("TrelloCanv")).Children.Add(draggable);
                
                notAllowed = DragDrop.DoDragDrop(draggable, draggable, DragDropEffects.Move);

                if (notAllowed == DragDropEffects.None)
                {
                    ((Canvas)this.FindName("TrelloCanv")).Children.Remove(draggable);
                    dragParent.Children.Add(draggable);
                    draggable = null;
                    ((StackPanel)this.FindName("doneStack")).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffe1");
                    ((StackPanel)this.FindName("progressStack")).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffe1");
                    ((StackPanel)this.FindName("unDoneStack")).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffe1");
                }
            }
        }
        void dragMouseDrop(object sender, DragEventArgs e)
        {
            if (!((StackPanel)sender).Children.Contains(draggable))
            {
                ((Canvas)this.FindName("TrelloCanv")).Children.Remove(draggable);
                ((StackPanel)sender).Children.Add(draggable);
            }
            
            TextBox namebox = (TextBox)draggable.Children.OfType<TextBox>().Single(Child => Child.Name != null && (string)Child.Name == "taskdesc");
            draggable = null;
            if (((StackPanel)sender).Name == "doneStack")
            {
                foreach (DailyTask task in tasks)
                {
                    if (task.ID == (int)namebox.Tag)
                    {
                        task.Done = true;
                        task.InProgress = false;
                        File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                    }
                }
            }
            else if (((StackPanel)sender).Name == "progressStack")
            {
                foreach (DailyTask task in tasks)
                {
                    if (task.ID == (int)namebox.Tag)
                    {
                        task.InProgress = true;
                        task.Done = false;
                        File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                    }
                }
            }
            else if (((StackPanel)sender).Name == "unDoneStack")
            {
                foreach (DailyTask task in tasks)
                {
                    if (task.ID == (int)namebox.Tag)
                    {
                        task.Done = false;
                        task.InProgress = false;
                        File.WriteAllText("data.json", JsonSerializer.Serialize<List<DailyTask>>(tasks));
                    }
                }
            }

            ((StackPanel)this.FindName("doneStack")).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffe1");
            ((StackPanel)this.FindName("progressStack")).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffe1");
            ((StackPanel)this.FindName("unDoneStack")).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffe1");

        }
        void dragOverThing(object sender, DragEventArgs e)
        {
            Point droPoint = e.GetPosition(this);

            Canvas.SetLeft(draggable, droPoint.X);
            Canvas.SetTop(draggable, droPoint.Y);
            Panel.SetZIndex(draggable, 69420);
        }
        void dragHighlight(object sender, DragEventArgs e)
        {
            //#ffffe1 is the default background color
            //#d6f6e6 is highlight color

            //may be just a tiny tiny tiny little bit inefficient but it's ez so
            ((StackPanel)this.FindName("doneStack")).Background = (SolidColorBrush) new BrushConverter().ConvertFrom("#ffffe1");
            ((StackPanel)this.FindName("progressStack")).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffe1");
            ((StackPanel)this.FindName("unDoneStack")).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffffe1");

            ((StackPanel)sender).Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#d6f6e6");

        }

        private void SettingClick(object sender, RoutedEventArgs e)
        {
            InitialWizard initialWizard = new InitialWizard();
            initialWizard.ShowDialog();
        }

        
    }
}
