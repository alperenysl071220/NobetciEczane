using RestSharp;
using System.Text.Json;

namespace NobetciEczane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            illeriGetir();
        }

        public void illeriGetir()
        {
            String URL = "https://api.collectapi.com/health/districtList";

            var client = new RestClient(URL);
            var request = new RestRequest(URL, Method.Get);
            request.AddHeader("authorization", "apikey 2xd4saQ6GBpkzcHrIM4wLm:7kEsFOSO2sRWx7qhBFf8u5");
            request.AddHeader("Content-Type", "application/json");

            RestResponse response = client.Execute(request);

            Root root = JsonSerializer.Deserialize<Root>(response.Content);

            if (root.success == true)
            {
                for (int i = 0; i < root.result.Count(); i++)
                {
                    comboBox1.Items.Add(root.result[i].text);
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                String URL = "https://api.collectapi.com/health/districtList?il=" + comboBox1.SelectedItem;

                var client = new RestClient(URL);
                var request = new RestRequest(URL, Method.Get);
                request.AddHeader("authorization", "apikey 2xd4saQ6GBpkzcHrIM4wLm:7kEsFOSO2sRWx7qhBFf8u5");
                request.AddHeader("Content-Type", "application/json");

                RestResponse response = client.Execute(request);

                Root root = JsonSerializer.Deserialize<Root>(response.Content);

                comboBox2.Items.Clear();

                comboBox2.Items.Add("TÜM ÝLÇELER");

                if (root.success == true)
                {
                    comboBox2.SelectedItem = "TÜM ÝLÇELER";
                    comboBox2.Text = "TÜM ÝLÇELER";
                    for (int i = 0; i < root.result.Count(); i++)
                    {
                        comboBox2.Items.Add(root.result[i].text);
                    }
                }
            }
            else
            {
                comboBox2.Items.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen Þehir Seçiniz", "Eksik Þehir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (comboBox2.SelectedItem.Equals("TÜM ÝLÇELER"))
            {
                String URL = "https://api.collectapi.com/health/dutyPharmacy?il=" + comboBox1.SelectedItem;

                var client = new RestClient(URL);
                var request = new RestRequest(URL, Method.Get);
                request.AddHeader("authorization", "apikey 2xd4saQ6GBpkzcHrIM4wLm:7kEsFOSO2sRWx7qhBFf8u5");
                request.AddHeader("Content-Type", "application/json");

                RestResponse response = client.Execute(request);

                Root root = JsonSerializer.Deserialize<Root>(response.Content);

                if (root.success == true)
                {
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < root.result.Count(); i++)
                    {
                        String[] row = new String[] { Convert.ToString(comboBox1.SelectedItem), root.result[i].dist, root.result[i].name, root.result[i].address.ToUpper(), root.result[i].phone };

                        dataGridView1.Rows.Add(row);
                    }
                }
            }

            else
            {
                String URL = "https://api.collectapi.com/health/dutyPharmacy?ilce=" + comboBox2.SelectedItem + "&il=" + comboBox1.SelectedItem;

                var client = new RestClient(URL);
                var request = new RestRequest(URL, Method.Get);
                request.AddHeader("authorization", "apikey 2xd4saQ6GBpkzcHrIM4wLm:7kEsFOSO2sRWx7qhBFf8u5");
                request.AddHeader("Content-Type", "application/json");

                RestResponse response = client.Execute(request);

                Root root = JsonSerializer.Deserialize<Root>(response.Content);

                if (root.success == true)
                {
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < root.result.Count(); i++)
                    {
                        String[] row = new String[] { Convert.ToString(comboBox1.SelectedItem), root.result[i].dist, root.result[i].name, root.result[i].address.ToUpper(), root.result[i].phone };

                        dataGridView1.Rows.Add(row);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (0 <= DateTime.Now.Hour && DateTime.Now.Hour < 6)
            {
                label4.Text = "Tarih: " + DateTime.Now.AddDays(-1).ToShortDateString();
            }
            if (DateTime.Now.Hour < 24)
            {
                label4.Text = "Tarih: " + DateTime.Now.ToShortDateString();
            }

            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[3].Width = 600;

        }
    }


}