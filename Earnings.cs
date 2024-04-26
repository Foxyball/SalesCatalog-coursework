using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MarketCatalog
{
    [Serializable]
    public class Earnings
    {

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string month;
        public string Month
        {
            get { return month; }
            set { month = value; }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private string adv_channel;
        public string Adv_channel
        {
            get { return adv_channel; }
            set { adv_channel = value; }
        }

        private double income;
        public double Income
        {
            get { return income; }
            set { income = value; }
        }

        private double expenses;
        public double Expenses
        {
            get { return expenses; }
            set { expenses = value; }
        }

        // calculate profit
        public double Profit
        {
            get { return income - expenses; }
        }
    }
}
