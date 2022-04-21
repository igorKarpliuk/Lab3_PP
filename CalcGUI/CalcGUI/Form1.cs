using System.Runtime.InteropServices;

namespace CalcGUI
{
    public partial class Form1 : Form
    {
        Int64 buffer = 0;

        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();


        public Form1(string[] args)
        {
            InitializeComponent();

            AllocConsole();
            string expression = "";
            string[] argum = Environment.GetCommandLineArgs();
            for (int i = 1; i < argum.Length; i++)
            {
                expression += argum[i];
            }
            Console.WriteLine(expression);

            AnalaizerClass.AnalaizerClass analyzer = new AnalaizerClass.AnalaizerClass(expression);
            try
            {
                string res = analyzer.Estimate();
                Console.WriteLine(res);
            }
            catch (Exception exception)
            {
                string errorMessage = analyzer.lastError == "" ? exception.Message : analyzer.lastError;
                Console.WriteLine(errorMessage);
            }
        }



        private void button_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text += ((Button)sender).Text;
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (textBoxExpression.Text.Length > 3)
            {
                if (textBoxExpression.Text.Substring(textBoxExpression.Text.Length - 3, 3) == "mod")
                {
                    textBoxExpression.Text = textBoxExpression.Text.Remove(textBoxExpression.Text.Length - 3);
                    return;
                }
            }
            if(textBoxExpression.Text.Length > 0)
                textBoxExpression.Text = textBoxExpression.Text.Remove(textBoxExpression.Text.Length - 1);
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = "";
            textBoxResult.Text = "";
        }


        private void Evaluate()
        {
            try
            {
                AnalaizerClass.AnalaizerClass AC130 = new AnalaizerClass.AnalaizerClass(textBoxExpression.Text);

                textBoxResult.Text = AC130.Estimate();

            }
            catch (Exception ex)
            {
                textBoxResult.Text = ex.Message;
            }

        }


        private void buttonPlusMinus_Click(object sender, EventArgs e)
        {
            if (textBoxExpression.Text.Length == 0)
            {
                textBoxExpression.Text += 'm';
                return;
            }

            switch (textBoxExpression.Text.Last()) {
                case 'p':
                    textBoxExpression.Text = textBoxExpression.Text.Remove(textBoxExpression.Text.Length - 1) + 'm';
                    break;
                case 'm':
                    textBoxExpression.Text = textBoxExpression.Text.Remove(textBoxExpression.Text.Length - 1) + 'p';
                    break;
                default:
                    textBoxExpression.Text += 'm';
                    break;
            }
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            Evaluate();
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((int)e.KeyChar)
            {
                case 0x1B:
                    Close();
                    break;
                case 0x0D:
                    Evaluate();
                    break;
            }
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text += buffer.ToString();
        }

        private void buttonMPlus_Click(object sender, EventArgs e)
        {
            Int64 tmp = 0;
            if (Int64.TryParse(textBoxResult.Text, out tmp))
                buffer += tmp;
            else
                textBoxResult.Text = "Cannot do this :(";
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            buffer = 0;
        }
    }
}