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
        }

        // Exit button
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
           DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        } // end of exit button

        // Go to Add form
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add add = new Add(list);
            add.ShowDialog();
        }

        // Main form load
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(PATH))
            {
                FileStream fs = new FileStream(PATH, FileMode.Create);
                list.Add(new Earnings { Id = 1, Name = "Demo", Month = "April", Category = "Demo", Adv_channel = "Demo", Income = 1000, Expenses = 500 });
                list.Add(new Earnings { Id = 2, Name = "Demo2", Month = "July", Category = "Demo", Adv_channel = "Demo", Income = 1000, Expenses = 500 });
                list.Add(new Earnings { Id = 3, Name = "Demo3", Month = "April", Category = "Demo", Adv_channel = "Demo", Income = 1000, Expenses = 500 });
                bf.Serialize(fs, list);

                fs.Close();
            }

            if (File.Exists(PATH))
            {
                FileStream fs = new FileStream(PATH, FileMode.Open);
                if (fs.Length > 0) { list = (List<Earnings>)bf.Deserialize(fs); }

                fs.Close();
            }

            earningsDataGridView.DataSource = list;
        }

        //private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        //{

        //}

        private void earningsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(PATH, FileMode.OpenOrCreate);
            bf.Serialize(fs, list);
            fs.Close();
        }
    } // end of class
} // end of namespace
