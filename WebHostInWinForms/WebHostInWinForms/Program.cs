using System.Net;
using System.Text;

namespace WebHostInWinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Task.Run(() =>
            {
                //模擬一個WebServer，應該有更好作法，例如弄成完整的WebAPI，這裡只是示意
                var listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:8899/");
                listener.Start();

                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        var ctx = o as HttpListenerContext;
                        HandleRequest(ctx);
                    }, context);
                }
            });

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

        }

        static void HandleRequest(HttpListenerContext context)
        {
            // 處理請求並回傳虛擬的內容
            string response = LoadFile();
            byte[] buffer = Encoding.UTF8.GetBytes(response);

            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
        }

        //讀取一個來自實體檔案，或者暫存
        static string? _File;
        static string LoadFile()
        {
            if (string.IsNullOrWhiteSpace(_File))
                _File = "alert('script is loaded.');"; //TODO load from file
            return _File;
        }
    }

}