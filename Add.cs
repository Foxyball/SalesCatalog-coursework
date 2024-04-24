using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketCatalog
{
    public partial class Add : System.Windows.Forms.Form
    {
        public const string PATH = "articles.txt";
        public List<Earnings> list = new List<Earnings>();

        public Add(List<Earnings> earningsList)
        {
            InitializeComponent();

            earningsBindingSource.DataSource = earningsList; // bind the list to the binding source

            month_dateTimePicker1.Format = DateTimePickerFormat.Custom;
            month_dateTimePicker1.CustomFormat = "MM/yyyy";
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
