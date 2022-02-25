using System;
using System.Windows.Forms;

namespace RealBox
{
    public partial class FrmRealBox : Form
    {
        public FrmRealBox() => InitializeComponent();
        private void Form1_Load(object sender, EventArgs e)
        {
            //  Supply initial values. Use format to avoid Culture issues such as comma instead of decimal point
            txtRealBox1.Text = string.Format("{0:#,##0.00}", 12.34);
            txtRealBox2.Text = string.Format("{0:#,##0.00}", 0.0);
            txtRealBox3.Text = 0.ToString("N2"); // Another way to do the same thing
        }
        private void txtRealBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 45)
            {
                TextBox t = (TextBox)sender;
                int cursorPosition = t.Text.Length - t.SelectionStart;      // Text in the box and Cursor position

                if (e.KeyChar == 45)                    
                    t.Text = t.Text[0] == 45 ? t.Text = t.Text[1..] : "-" + t.Text;                    
                else                    
                    if ( t.Text.Length < 20)
                        t.Text = ( decimal.Parse( t.Text.Insert( t.SelectionStart, e.KeyChar.ToString())
                                                .Replace(",", "").Replace(".", "")) / 100).ToString("N2");

                t.SelectionStart = (t.Text.Length - cursorPosition < 0 ? 0 : t.Text.Length - cursorPosition);
            }
            e.Handled = true;
        }
        private void txtRealBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)     // Deals with BackSpace e Delete keys
            {
                TextBox t = (TextBox)sender;
                int cursorPosition = t.Text.Length - t.SelectionStart;

                string Left  = t.Text.Substring( 0, t.Text.Length - cursorPosition).Replace(".", "").Replace(",", "");
                string Right = t.Text.Substring( t.Text.Length - cursorPosition   ).Replace(".", "").Replace(",", "");

                if (Left.Length > 0)
                {
                    Left = Left.Remove( Left.Length - 1);                            // Take out the rightmost digit
                    t.Text = ( decimal.Parse( Left + Right) / 100).ToString("N2");
                    t.SelectionStart = (t.Text.Length - cursorPosition < 0 ? 0 : t.Text.Length - cursorPosition);
                }                
                e.Handled = true;
            }

            if (e.KeyCode == Keys.End)                                  // Treats End key
            {
                TextBox t = (TextBox)sender;
                t.SelectionStart = t.Text.Length;                       // Moves the cursor o the rightmost position
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Home)                                 // Trata tecla Home
            {
                TextBox t = (TextBox)sender;
                t.Text = 0.ToString("N2");                              // Set field value to zero 
                t.SelectionStart = t.Text.Length;                       // Moves the cursor o the rightmost position
                e.Handled = true;
            }                
        }
        private void txtRealBox_Enter(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;                                // Desliga seleção de texto
            t.SelectionStart = t.Text.Length;
        }
        private void lblMsg_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
        }
    }
}