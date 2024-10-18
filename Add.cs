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


namespace MarketCatalog
{
    public partial class Add : System.Windows.Forms.Form
    {
        public const string PATH = "articles.dat";
        public List<Earnings> list = new List<Earnings>();
        public BinaryFormatter bf = new BinaryFormatter();

        public Add()
        {
            InitializeComponent();
        }

        // Add button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(PATH))
                {
                    File.Create(PATH).Close();
                }

                FileStream fs = new FileStream(PATH, FileMode.OpenOrCreate);
                if (fs.Length > 0)
                {
                    list = (List<Earnings>)bf.Deserialize(fs);
                }
                fs.Close();

                Earnings earnings = new Earnings();
                earnings.Id = list.Count + 1; // auto increment id
                earnings.Name = name_textBox1.Text;
                earnings.Month = month_dateTimePicker1.Text;
                earnings.Category = cat_listBox1.Text;
                earnings.Adv_channel = adv_channelTextBox.Text;
                earnings.Income = Convert.ToDouble(income_numericUpDown1.Value);
                earnings.Expenses = Convert.ToDouble(expenses_numericUpDown2.Value);

                if (earnings.Name == string.Empty || earnings.Month == string.Empty || earnings.Adv_channel == string.Empty)
                {
                    MessageBox.Show("Моля попълнете всички полета!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
                if (earnings.Income < 0 || earnings.Expenses < 0)
                {
                    MessageBox.Show("Полетата 'Приходи' и 'Разходи' не могат да бъдат отрицателни!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (earnings.Income < earnings.Expenses)
                {
                    MessageBox.Show("Полето 'Приходи' не може да бъде по-малко от полето 'Разходи'!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (earnings.Income == 0 && earnings.Expenses == 0)
                {
                    MessageBox.Show("Полетата 'Приходи' и 'Разходи' не могат да бъдат равни на 0!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (earnings.Income == 0)
                {
                    MessageBox.Show("Полето 'Приходи' не може да бъде равно на 0!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (earnings.Expenses == 0)
                {
                    MessageBox.Show("Полето 'Разходи' не може да бъде равно на 0!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                list.Add(earnings);

               SaveToFile();

                MessageBox.Show("Успешно добавяне!", "СЪобщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearInputs();

                
            }
            catch (IOException ex)
            {
                MessageBox.Show("Възникна грешка при отварянето на файла!: " + ex.Message, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // end of add button
         
        private void ClearInputs()
        {
            name_textBox1.Text = string.Empty;
            month_dateTimePicker1.Value = DateTime.Now;
            cat_listBox1.SelectedIndex = -1;
            adv_channelTextBox.Text = string.Empty;
            income_numericUpDown1.Value = 0;
            expenses_numericUpDown2.Value = 0;
        }

        private void SaveToFile()
        {
            FileStream fs = new FileStream(PATH, FileMode.Create);
            bf.Serialize(fs, list);
            fs.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Add_Load(object sender, EventArgs e)
        { 

            month_dateTimePicker1.Format = DateTimePickerFormat.Custom;
            month_dateTimePicker1.CustomFormat = "MM/yyyy";
        }
    }
}
