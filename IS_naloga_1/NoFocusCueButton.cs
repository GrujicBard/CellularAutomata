using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IS_naloga_1
{
    public class NoFocusCueButton : Button
   {
        public NoFocusCueButton() : base()
        {

            SetStyle(ControlStyles.Selectable, false);
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
    }
}
