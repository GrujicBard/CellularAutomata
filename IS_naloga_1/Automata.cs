using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Diagnostics;

namespace IS_naloga_1
{
    class Automata
    {
        BitArray cells, ncells;
        const int MAX_CELLS = 31;
        const int height = 16;
        BitArray[] result;
        int resultCounter;
        string rl;

        public BitArray[] run(string rule)
        {
            cells = new BitArray(MAX_CELLS);
            ncells = new BitArray(MAX_CELLS);
            result = new BitArray[height];
            resultCounter = 1;

            doRule(int.Parse(rule));

            return result;
        }

        //returns chars of row index in sets of 3
        private string getCells(int index)
        {
            byte b;
            string s;
            int i1 = index - 1,
                i2 = index,
                i3 = index + 1;

            if (i1 < 0) i1 = MAX_CELLS - 1;
            if (i3 >= MAX_CELLS) i3 -= MAX_CELLS;

            s = (cells.Get(i1) ? "1" : "0") + (cells.Get(i2) ? "1" : "0") + (cells.Get(i3) ? "1" : "0");
            //Debug.WriteLine(s);
            return s;
        }

        //Converts integer to string in binary and adds 0s in front while string length < 8
        public BitArray toBinary(int i)
        {
            BitArray rule = new BitArray(8);
            string s = Convert.ToString(i, 2); //Converts int to binary string
            while (s.Length < 8){ //adds missing 0s to front of string if length < 8 
                s = "0" + s;
            }
            rl = s;
            for (int st = 0; st < s.Length; st++)
            { //fill BitArray from string
                rule[st] = s[st] == '1' ? true : false;
            }
            
            return rule;
        }

        private void doRule(int rule)
        {
            BitArray rl = toBinary(rule);
            cells.SetAll(false);
            ncells.SetAll(false);
            cells.Set(MAX_CELLS / 2, true); //generate first row with true in middle
            result[0] = cells;

            for (int gen = 0; gen < height-1; gen++)
            {
                int i = 0;
                while (true)
                {
                    string cells = getCells(i);

                    switch (cells)
                    {
                        case "111":
                            ncells[i] = rl[0] ? true : false;
                            break;
                        case "110":
                            ncells[i] = rl[1] ? true : false;
                            break;
                        case "101":
                            ncells[i] = rl[2] ? true : false;
                            break;
                        case "100":
                            ncells[i] = rl[3] ? true : false;
                            break;
                        case "011":
                            ncells[i] = rl[4] ? true : false;
                            break;
                        case "010":
                            ncells[i] = rl[5] ? true : false;
                            break;
                        case "001":
                            ncells[i] = rl[6] ? true : false;
                            break;
                        case "000":
                            ncells[i] = rl[7] ? true : false;
                            break;
                    }
                    ++i;
                    if (i == MAX_CELLS) break;
                }

                i = 0;
                cells = new BitArray(MAX_CELLS);
                foreach (bool b in ncells)
                {
                    cells[i++] = b;                 
                }

                result[resultCounter] = cells;
                resultCounter++;
            }           
        }

        public string getRule()
        {
            return rl;
        }
    };
}
