using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MarketCatalog
{
    public partial class Form1 : System.Windows.Forms.Form
    {

        public const string PATH = "articles.dat";
        public List<Earnings> list = new List<Earnings>();
        public BinaryFormatter bf = new BinaryFormatter();
        
        public Form1()
        {
            InitializeComponent();
            earningsBindingSource.DataSource = list;
        }

       private bool findID(Earnings id_item)
        {
            if (id_item.Id== earningsDataGridView.CurrentRow.Index)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Main form load
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(PATH))
            {
                FileStream fs = new FileStream(PATH, FileMode.Create);
                list.Add(new Earnings { Id = 1, Name = "Demo", Month = "April", Category = "Electronics", Adv_channel = "Demo", Income = 1000, Expenses = 500 });
                list.Add(new Earnings { Id = 2, Name = "Demo2", Month = "July", Category = "Electronics", Adv_channel = "Demo", Income = 1000, Expenses = 500 });
                list.Add(new Earnings { Id = 3, Name = "Demo3", Month = "April", Category = "Clothes", Adv_channel = "Demo", Income = 1000, Expenses = 500 });
                bf.Serialize(fs, list);

                fs.Close();
            }

            LoadFromFile(); // load data from file

            earningsDataGridView.DataSource = list; // bind the list to the data grid view
        }

        // Save to file
        private void SaveToFile() { 
        
           FileStream fs = new FileStream(PATH, FileMode.OpenOrCreate);
            bf.Serialize(fs, list);
            fs.Close();
        }

       private void LoadFromFile()
        {
            if (File.Exists(PATH))
            {
                FileStream fs = new FileStream(PATH, FileMode.Open);
                if (fs.Length > 0) { list = (List<Earnings>)bf.Deserialize(fs); } // read from file

                fs.Close();
            }
        }


        // T0D0
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (earningsDataGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = earningsDataGridView.SelectedRows[0].Index;
                Earnings selectedEarnings = list[selectedIndex];
                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    list.RemoveAt(selectedIndex);
                    earningsDataGridView.DataSource = null;
                    earningsDataGridView.DataSource = list;
                    earningsDataGridView.Refresh();

                    SaveToFile();
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Navigation buttons (Exit)
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        } // end of exit button

        // Navigation buttons (Add)
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add add = new Add();
            add.ShowDialog();
        }

        // Navigation buttons (Save)
        private void earningsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            SaveToFile();
            MessageBox.Show("Успешно добавяне!");
        }

        // Refresh button
        private void button1_Click(object sender, EventArgs e)
        {

            LoadFromFile();
            earningsDataGridView.DataSource = null;
            earningsDataGridView.DataSource = list;
        }

        // Add button (same as earningsBindingNavigatorSaveItem_Click)
        private void button2_Click(object sender, EventArgs e)
        {
            SaveToFile();
            MessageBox.Show("Успешно добавяне!");
        }

        // Add button (same as addToolStripMenuItem_Click)
        private void button3_Click(object sender, EventArgs e)
        {
            Add add = new Add();
            add.ShowDialog();
        }
    } // end of class
} // end of namespace
