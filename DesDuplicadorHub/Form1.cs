using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesDuplicadorHub.Connection;

namespace DesDuplicadorHub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loadProduct_Click(object sender, EventArgs e)
        {
            database objConnection = new database(textBox1.Text, dgData);
        }
    }
}
