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

        private Game _game = new Game();
        private Color[,] _cellColors2;


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


            // Game of Life
            var columns2 = tableLayoutPanel2.ColumnCount;
            var rows2 = tableLayoutPanel2.RowCount;
            _cellColors2 = new Color[columns2, rows2];
            SetDoubleBuffered(tableLayoutPanel2);
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            ColorTable();

        }

        // Automata 1D

        private void Button1_Click(object sender, EventArgs e)
        {
            var rule = textBox1.Text;
            BitArray[] result;
            result = _automata.run(rule);
            ShowRule(_automata.getRule());

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

        // Game of Life

        private void Btn_start_Click(object sender, EventArgs e)
        {
            if (btn_start.Text == "Start")
            {
                timer1.Start();
                tableLayoutPanel2.Refresh();
                btn_start.Text = "Pause";
            }
            else
            {
                timer1.Stop();
                btn_start.Text = "Start";
            }
        }

        private void Btn_stop_Click(object sender, EventArgs e)
        {
            GameStop();
        }

        private void GameStart()
        {

            if (_game != null)
            {
                _game.Run();
                ColorTable();
            }
        }

        private void GameStop() // Resets game
        {
            if (_game != null)
            {
                timer1.Stop();
                _game = new Game();
                ColorTable();
                btn_start.Text = "Start";
            }
        }

        private void ColorTable() // Color table Black or White based on Cell.IsAlive
        {
            for (int x = 0; x < _game.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < _game.Cells.GetLength(1); y++)
                {
                    _cellColors2[x, y] = _game.Cells[x, y].IsAlive ? Color.Black : Color.White;

                }
            }
            tableLayoutPanel2.Refresh();
        }

        private void Tb_speed_ValueChanged(object sender, EventArgs e)
        {
            if (tb_speed.Value == 10) timer1.Interval = 100;
            if (tb_speed.Value == 9) timer1.Interval = 200;
            if (tb_speed.Value == 8) timer1.Interval = 300;
            if (tb_speed.Value == 7) timer1.Interval = 400;
            if (tb_speed.Value == 6) timer1.Interval = 500;
            if (tb_speed.Value == 5) timer1.Interval = 600;
            if (tb_speed.Value == 4) timer1.Interval = 700;
            if (tb_speed.Value == 3) timer1.Interval = 800;
            if (tb_speed.Value == 2) timer1.Interval = 900;
            if (tb_speed.Value == 1) timer1.Interval = 1000;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            GameStart();
            tableLayoutPanel2.Refresh();
        }

        private void TableLayoutPanel2_CellPaint(object sender, TableLayoutCellPaintEventArgs e) // Paint table
        {
            if (_cellColors2 != null)
            {
                var color = _cellColors2[e.Column, e.Row];
                e.Graphics.FillRectangle(new SolidBrush(color), e.CellBounds);
            }
        }

        private void TableLayoutPanel2_MouseClick(object sender, MouseEventArgs e) // Change Cell.IsAlive on table mouseclick
        {
            int row = 0;
            int verticalOffset = 0;

            foreach (int h in tableLayoutPanel2.GetRowHeights())
            {
                int column = 0;
                int horizontalOffset = 0;
                foreach (int w in tableLayoutPanel2.GetColumnWidths())
                {
                    Rectangle rectangle = new Rectangle(horizontalOffset, verticalOffset, w, h);
                    if (rectangle.Contains(e.Location))
                    {
                        if (_game != null)
                        {
                            _game.Cells[column, row].IsAlive = !_game.Cells[column, row].IsAlive;
                        }
                        ColorTable();
                        return;
                    }
                    horizontalOffset += w;
                    column++;
                }
                verticalOffset += h;
                row++;
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


