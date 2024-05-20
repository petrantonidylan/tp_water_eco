using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
using System.IO;

namespace application_wpf_water_eco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("https://localhost:7082/api/Kc/GetKcs").Result;
                HttpResponseMessage response2 = client.GetAsync("https://localhost:7082/api/Surfaceculture/GetSurfacecultures").Result;
                HttpResponseMessage response3 = client.GetAsync("https://localhost:7082/api/Watervolume/GetWatervolumes").Result;



                string responseBody = response.Content.ReadAsStringAsync().Result;
                string responseBody2 = response2.Content.ReadAsStringAsync().Result;
                string responseBody3 = response3.Content.ReadAsStringAsync().Result;

                List<Kc> kcList = JsonSerializer.Deserialize<List<Kc>>(responseBody);
                List<Surfaceculture> surfaceList = JsonSerializer.Deserialize<List<Surfaceculture>>(responseBody2);
                List<Watervolume> watList = JsonSerializer.Deserialize<List<Watervolume>>(responseBody3);

                foreach (Kc kc in kcList)
                {
                    ListKc.Items.Add($"{kc.kcName} - {kc.kcNeed} L/m^2");
                    Combo.Items.Add($"{kc.kcId} - {kc.kcName}");
                    comboAuto1.Items.Add($"{kc.kcId} - {kc.kcName}");
                }

                foreach (Surfaceculture Surface in surfaceList)
                {
                    Combo2.Items.Add($"{Surface.surId} - {Surface.surValue} m^2");
                    comboAuto2.Items.Add($"{Surface.surId} - {Surface.surValue} m^2");
                }

                foreach(Watervolume Wat in watList)
                {
                    comboAuto3.Items.Add($"{Wat.watId} - {Wat.watName} m^2");
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string value = inseee.Text;



                HttpResponseMessage response = client.GetAsync("https://localhost:7082/api/Watervolume/GetWatervolumeByInsee?Insee="+value).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    Watervolume leVolume = JsonSerializer.Deserialize<Watervolume>(responseBody);

                    water.Text = $"NOM : {leVolume.watName}\nVOLUME MAX : {leVolume.watMaxVolume} L\nVOLUME ACTUEL : {leVolume.watCurrentVolume} L\n";
                }
                else
                {
                    water.Text = "insee inconnu";
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string value1 = insert1.Text;
            string value2 = insert2.Text;
            string value3 = insert3.Text;
            string value5 = insert5.Text;
            // Données JSON à envoyer
            var data = new
            {
                watId = 0,
                watName = value1,
                watMaxVolume = value2,
                watCurrentVolume = value3,
                watUnit = "Litre(s)",
                watInsee = value5
            };

            string jsonData = JsonSerializer.Serialize(data);

            string apiUrl = "https://localhost:7082/api/Watervolume/InsertWatervolume";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Données insérées avec succès !");
                        insert1.Text = "";
                        insert2.Text = "";
                        insert3.Text = "";
                        insert5.Text = "";
                    }
                    else
                    {
                        MessageBox.Show($"Échec de l'insertion des données. Code de statut : {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur s'est produite : {ex.Message}");
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                char kcId = Combo.Text[0];
                char surId = Combo2.Text[0];

                HttpResponseMessage response = client.GetAsync($"https://localhost:7082/api/Kc/GetConsoByKcAndSurface?KcId={kcId}&SurId={surId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    Kc laRep = JsonSerializer.Deserialize<Kc>(responseBody);

                    repConso.Text = laRep.kcName;
                }
                else
                {
                    water.Text = "insee inconnu";
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                char kcId = comboAuto1.Text[0];
                char surId = comboAuto2.Text[0];
                char watId = comboAuto3.Text[0];

                HttpResponseMessage response = client.GetAsync($"https://localhost:7082/api/Watervolume/GetAutonomie?idKc={kcId}&idWat={watId}&idSur={surId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    resultAuto.Text = responseBody;
                }
                else
                {
                    water.Text = "Erreur.";
                }
            }
        }
    }

    public class Kc
    {
        public int kcId { get; set; }

        public string kcName { get; set; } = null!;

        public decimal kcNeed { get; set; }
    }

    public partial class Watervolume
    {
        public int watId { get; set; }

        public string watName { get; set; } = null!;

        public decimal watMaxVolume { get; set; }

        public decimal watCurrentVolume { get; set; }

        public string watUnit { get; set; } = null!;

        public int watInsee { get; set; }

        //public virtual ICollection<Surfaceculture> Surfacecultures { get; } = new List<Surfaceculture>();
    }

    public class Surfaceculture
    {
        public int surId { get; set; }

        public decimal surValue { get; set; }

        public string surUnit { get; set; } = null!;

        public int watId { get; set; }

        public virtual Watervolume wat { get; set; } = null!;
    }

}




