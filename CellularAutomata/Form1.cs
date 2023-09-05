using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace IS_naloga_1
{
    public partial class Form1 : Form
    {

        readonly private Automata _automata = new Automata();
        readonly private Color[,] _cellColors1;

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);

            // Automata 1D
            var columns1 = tableLayoutPanel1.ColumnCount;
            var rows1 = tableLayoutPanel1.RowCount;
            _cellColors1 = new Color[columns1, rows1];
            SetDoubleBuffered(tableLayoutPanel1);
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

        }

        // Automata 1D

        private void Button1_Click(object sender, EventArgs e)
        {
            var rule = textBox1.Text;
            BitArray[] result;
            result = _automata.Run(rule);
            ShowRule(_automata.GetRule());

            //Debug.WriteLine("result lenght: " + result.Length);
            //Debug.WriteLine("result0 lenght: " + result[0].Length);

            int i = 0;
            foreach (var item in result) //iterate through BitArray result
            {
                int j = 0;
                foreach (bool b in item)
                {
                    if (b)
                    {
                        _cellColors1[j, i] = Color.Black; //if True color = Black
                    }
                    else
                    {
                        _cellColors1[j, i] = Color.White; //if False color = White
                    }
                    j++;
                }
                i++;
            }
            tableLayoutPanel1.Refresh();
        }

        private void ShowRule(string s)
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

        private void TableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (_cellColors1 != null)
            {
                var color = _cellColors1[e.Column, e.Row];
                e.Graphics.FillRectangle(new SolidBrush(color), e.CellBounds);
            }
        }

        // Hack to speed up table loading
        public static void SetDoubleBuffered(Control c)
        {
            if (SystemInformation.TerminalServerSession)
                return;
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }
    }
}


