using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace IS_naloga_1
{
    public partial class Form1 : Form
    {
        private Color[,] cellColors = null;
        Automata automata = new Automata();

        public Form1()
        {
            InitializeComponent();
            var columns = tableLayoutPanel1.ColumnCount;
            var rows = tableLayoutPanel1.RowCount;
            cellColors = new Color[columns, rows];

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rule = textBox1.Text;
            BitArray[] result;
            result = automata.run(rule);
            showRule(automata.getRule());

            Debug.WriteLine("result lenght: " + result.Length);
            Debug.WriteLine("result0 lenght: " + result[0].Length);

            int i = 0;
            foreach (var item in result) //iterate through BitArray result
            {
                int j = 0;
                foreach (bool b in item)
                {
                    if (b)
                    {
                        cellColors[j, i] = Color.Black; //if True color = Black
                    }
                    else
                    {
                        cellColors[j, i] = Color.White; //if False color = White
                    }
                    j++;
                }
                i++;
            }
            tableLayoutPanel1.Refresh();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (cellColors != null)
            {
                var color = cellColors[e.Column, e.Row];
                e.Graphics.FillRectangle(new SolidBrush(color), e.CellBounds);
            }
        }

        private void showRule(string s)
        {
            textBox_rule_1.Text = s[0].ToString();
            textBox_rule_2.Text = s[1].ToString();
            textBox_rule_3.Text = s[2].ToString();
            textBox_rule_4.Text = s[3].ToString();
            textBox_rule_5.Text = s[4].ToString();
            textBox_rule_6.Text = s[5].ToString();
            textBox_rule_7.Text = s[6].ToString();
            textBox_rule_8.Text = s[7].ToString();
        }
    }
}
