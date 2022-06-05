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
    /// Interaction logic for InitialWizard.xaml
    /// </summary>
    public partial class InitialWizard : Window
    {
        private List<String[]> dunnid = new List<String[]>();
        private List<String> valikained = new List<String> { "Müto", "GIS", "Kv", "Fv", "Koor", "Mv", "Väitlu" }; //ma arvestan ainult 10. klassiga sest fuck me kui ma peaksin kõiki valikaineid sisestama hakkama
        private List<String> algusajad = new List<String> { "08:10","09:00","09:50","10:45","11:35","12:20","13:00","13:50","14:45","15:40","16:25"};
        private DateTime StartDate;
        private DateTime EndDate;
        private string prevlink = "";
       
        public InitialWizard()
        {
            InitializeComponent();
            if(File.Exists("data2.json"))
            {
                List<String> texts = JsonSerializer.Deserialize<List<String>>(File.ReadAllText("data2.json"));
                //technically peaks tegema mingi sus checki ka et ei overwriteks kui ryhma vahetada v keeli/valikaineid lisada aga ma ei viitsi
                link.Text = texts[0];
                prevlink = link.Text;
                ryhm.Text = texts[1];
                keel.Text = texts[2];
                aine.Text = texts[3];
            }
            for (int i = 0; i < 5; i++)
            {
                dunnid.Add(new String[10]);
            }

        }


        //mis oleks lõbusam kui iidse koodi mida sa kunagi kirjutasid uuesti kasutamine :)))))))
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (prevlink!= link.Text)
            {
                string geel = keel.Text;
                string[] ained = aine.Text.Split();
                int poolryhm;
                if (link.Text != "")
                {
                    if (ryhm.Text == "")
                    {
                        MessageBox.Show("Palun sisesta rühma number");
                        return;
                    }

                    try
                    {
                        poolryhm = int.Parse(ryhm.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Palun sisesta rühma number");
                        return;
                    }

                    try
                    {
                        int previ, prevj;
                        previ = prevj = 0;

                        var url = link.Text;
                        var web = new HtmlWeb();
                        var doc = web.Load(url);

                        var table = doc.DocumentNode.SelectSingleNode("//html/body/center/table");
                        var date = doc.DocumentNode.SelectSingleNode("//html/body/center/font/table").SelectNodes("./tr")[1].SelectNodes("./td")[1].InnerText.Trim().Split();
                        string year = "";
                        string starter = "";
                        bool monthReq = false;
                        foreach (string item in date)
                        {
                           
                            Char[] arr = item.ToArray().Where(x => char.IsDigit(x)).ToArray();
                            if (arr.Length > 0 && item.Substring(1) != ".")
                            {
                                //Miks ei suuda reaalkool oma tunniplaani kuupäeva yhtlaselt vormistada ffs
                                if (item.Contains("-"))
                                {
                                    
                                    foreach (var part in item.Split("-"))
                                    {
                                        if (part.Length == 10)
                                        {
                                            if (year != "")
                                            {
                                                if (int.Parse(part.Substring(6, 4)) == int.Parse(year.Substring(1, 4)) + 1)
                                                {
                                                    StartDate = EndDate;
                                                }
                                            }
                                            if (monthReq == false)
                                            {
                                                year = part.Substring(5, 5);
                                            }
                                            else
                                            {
                                                year = part.Substring(3, 7);
                                            }
                                            EndDate = DateTime.Parse(part);
                                        }
                                        else if (part.Length == 3)
                                        {
                                            starter = part;
                                            monthReq = true;
                                        }
                                        else
                                        {
                                            starter = part;
                                        }
                                    }
                                }
                                else
                                {
                                    if (item.Length == 10)
                                    {
                                        if (year != "")
                                        {
                                            if (int.Parse(item.Substring(6, 4)) == int.Parse(year.Substring(1, 4)) + 1)
                                            {
                                                StartDate = EndDate;
                                            }
                                        }
                                        if (monthReq == false)
                                        {
                                            year = item.Substring(5, 5);
                                        }
                                        else
                                        {
                                            year = item.Substring(3, 7);
                                        }
                                        year = item.Substring(5, 5);
                                        EndDate = DateTime.Parse(item);
                                    }
                                    else if (item.Length == 3)
                                    {
                                        starter = item;
                                        monthReq = true;
                                    }
                                    else
                                    {
                                        starter = item;
                                    }
                                }
                            }
                        }
                        if (starter != "")
                        {
                            StartDate = DateTime.Parse(starter + year);
                        }

                        var rows = table.SelectNodes("./tr");
                        int i = -1;
                        int lesson = 1;
                        foreach (var row in rows)
                        {
                            i++;
                            if (i == 0 || i > 1 && i % 2 == 0)
                            {
                                continue;
                            }

                            Console.WriteLine("Lesson " + lesson.ToString());

                            int j = -1;
                            var days = row.SelectNodes("./td/table");

                            foreach (var day in days)
                            {
                                j++;
                                if (j == 0)
                                {
                                    continue;
                                }
                                Console.WriteLine("Day " + j.ToString());
                                // taustavarvi saab naiteks day.ParentNode.Attributes["bgcolor"].Value

                                var classes = day.SelectNodes("./tr");

                                foreach (var cl in classes)
                                {
                                    string ctext = "";
                                    var details = cl.SelectNodes("./td");

                                    foreach (var detail in details) // esimene on tund, teine klass ja kolmas õpetaja, saaks eraldi indexiga lugeda
                                    {

                                        ctext += detail.InnerText.Trim() + " ";
                                    }

                                    // ctext here is basically lesson i of day j, if there isn't a lesson, then ctext is ""
                                    // there also might be parallel lessons, in which case this is run multiple times with same i and j
                                    // if necessary, the strings might be combined and output outside of this foreach, then there is always unique i and j while outputting

                                    if (ctext.Trim() != "" && ((j == prevj && i == previ) || valikained.Contains(ctext.Trim().Split()[0]) || char.IsDigit(char.Parse(ctext.Trim().Split()[0].Substring(ctext.Trim().Split()[0].Length - 1)))))
                                    {

                                        if (ctext.Trim().Split()[0] == geel || ained.Contains(ctext.Trim().Split()[0]) || ctext.Trim().Split()[0].Contains(char.Parse(poolryhm.ToString())))
                                        {

                                            dunnid[j - 1][(i + 1) / 2 - 1] = ctext.Trim();
                                            prevj = j;
                                            previ = i;
                                        }
                                        else
                                        {
                                            prevj = j;
                                            previ = i;
                                        }
                                    }
                                    else if (ctext.Trim() != "")
                                    {
                                        dunnid[j - 1][(i + 1) / 2 - 1] = ctext.Trim();
                                        prevj = j;
                                        previ = i;
                                    }


                                    //MessageBox.Show(j + " "+(i+1)/2+" "+ctext.Trim());
                                }

                            }
                            lesson++;
                        }
                        /*
                        foreach (var dund in dunnid)
                        {
                            foreach (var dunn in dund)
                            {
                                MessageBox.Show(dunn);
                            }
                        }
                        */
                        loadTunniplaan();
                        //ma just sain aur et ma olen kõige teinud this.findnamega ja see on veits braindead mõnedes situatsioonides
                        File.WriteAllText("data2.json", JsonSerializer.Serialize(new List<string> { link.Text, ryhm.Text, keel.Text, aine.Text}));
                    }
                    catch (Exception E)
                    {

                        MessageBox.Show("Midagi läks valesti. Kas URL on õigesti sisestatud?");
                        MessageBox.Show(E.Message);
                        return;
                    }
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Palun sisesta tunniplaani link.");
                }
            }
        }

        void loadTunniplaan()
        {
            List<MainWindow.DailyTask> tasks = new List<MainWindow.DailyTask>();
            if (File.Exists("data.json"))
            {
                MainWindow.tasks = JsonSerializer.Deserialize<List<MainWindow.DailyTask>>(File.ReadAllText("data.json"));
                //sort the things by date and stuff
                MainWindow.tasks.Sort(delegate (MainWindow.DailyTask i, MainWindow.DailyTask u) { return i.Date.CompareTo(u.Date); });
            }
            int e = 0;
            for (DateTime dt = StartDate; dt <= EndDate; dt = dt.AddDays(1))
            {
                if (e<5)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var tund = dunnid[e][i];
                        if (tund != null)
                        {
                            MainWindow.DailyTask task = new MainWindow.DailyTask();
                            task.ID = MainWindow.tasks.Count + 2;
                            task.Name = tund;
                            task.Date = (DateTime)dt + new TimeSpan(int.Parse(algusajad[i].Split(':')[0]), int.Parse(algusajad[i].Split(':')[1]), 0);
                            MainWindow.tasks.Add(task);
                            File.WriteAllText("data.json", JsonSerializer.Serialize<List<MainWindow.DailyTask>>(MainWindow.tasks));
                        }
                    }
                }
                
                e++;

                if (e > 6)
                {
                    e = 0;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("data2.json", JsonSerializer.Serialize(new List<string> { "", "", "", ""}));
            this.Close();
        }

    }
}
