using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using RealEstate;

namespace RealEstateGUI
{
    public partial class Form1 : Form
    {
        string connectionString = "server=localhost;port=3307;database=ingatlan;user=root;password=;";
        List<Seller> sellers = new List<Seller>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSellers();
        }

        private void LoadSellers()
        {
            sellers.Clear();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, name, phone FROM sellers";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string name = reader.GetString("name");
                    string phone = reader.GetString("phone");
                    sellers.Add(new Seller(id, name, phone));
                }
            }

            listBox1.DataSource = sellers;
            listBox1.DisplayMember = "Name";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            Seller selectedSeller = listBox1.SelectedItem as Seller;
            if (selectedSeller != null)
            {
                label2.Text = selectedSeller.Name;
                label1.Text = selectedSeller.Phone;
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems == null) return;

            Seller selectedSeller = listBox1.SelectedItem as Seller;
            int adCount = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM realestates WHERE sellerId = @sellerId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@sellerId", selectedSeller.Id);
                adCount = Convert.ToInt32(command.ExecuteScalar());
            }

            label4.Text = $"{adCount}";
        }

       
    }
}