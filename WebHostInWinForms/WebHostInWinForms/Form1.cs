namespace WebHostInWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //在你的網頁中指定script src
            webBrowser1.DocumentText = $"<html><script src='http://localhost:8899'></script></html>";
        }
    }
}