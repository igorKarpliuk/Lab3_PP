namespace CalcGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text += ((Button)sender).Text;
        }

    }
}