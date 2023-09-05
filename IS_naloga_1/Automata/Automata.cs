using System;
using System.Collections;

namespace IS_naloga_1
{
    internal class Automata
    {
        BitArray cells, ncells; // First row, second Row
        const int MAX_CELLS = 31;
        const int height = 16;
        BitArray[] result;
        int resultCounter;
        string rl; // Display rule

        public BitArray[] Run(string rule)
        {
            cells = new BitArray(MAX_CELLS);
            ncells = new BitArray(MAX_CELLS);
            result = new BitArray[height];
            resultCounter = 1;

            DoRule(int.Parse(rule));

            return result;
        }

        // Returns chars of row index in sets of 3
        private string GetCells(int index)
        {
            string s;
            int i1 = index - 1,
                i2 = index,
                i3 = index + 1;

            if (i1 < 0) i1 = MAX_CELLS - 1; // Left edge, go to right side
            if (i3 >= MAX_CELLS) i3 -= MAX_CELLS; // Right edge, go to left side

            s = (cells.Get(i1) ? "1" : "0") + (cells.Get(i2) ? "1" : "0") + (cells.Get(i3) ? "1" : "0");
            //Debug.WriteLine(s);
            return s;
        }

        //Converts integer to string in binary and adds 0s in front while string length < 8
        public BitArray ToBinary(int i)
        {
            BitArray rule = new BitArray(8);
            string s = Convert.ToString(i, 2); //Converts int to binary string
            while (s.Length < 8)
            { //adds missing 0s to front of string if length < 8 
                s = "0" + s;
            }
            rl = s;
            for (int st = 0; st < s.Length; st++)
            { //fill BitArray from string
                rule[st] = s[st] == '1' ? true : false;
            }

            return rule;
        }

        private void DoRule(int rule)
        {
            BitArray rl = ToBinary(rule); // Transform rule input to binary value
            cells.SetAll(false);
            ncells.SetAll(false);
            cells.Set(MAX_CELLS / 2, true); // Generate first row with true in middle
            result[0] = cells; // First row middle cell is always true, insert first row into result

            for (int gen = 0; gen < height - 1; gen++) // Iterate through rows
            {
                int i = 0;
                while (true)
                {
                    string cells = GetCells(i); // Get cells in string of 3

                    switch (cells) // Go through rule of old row
                    {
                        case "111":
                            ncells[i] = rl[0] ? true : false; // Generate new row by looking at old row
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

                cells = ncells.Clone() as BitArray;

                result[resultCounter] = cells; // Fill result row by row
                resultCounter++;
            }
        }

        public string GetRule()
        {
            return rl;
        }
    };
}
