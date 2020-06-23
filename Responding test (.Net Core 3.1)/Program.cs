namespace Responding_test
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Responding test (.Net Core 3.1)");
            System.Console.WriteLine("Press ESC to stop.");

            string dummyFilePath = $@"{System.IO.Path.GetTempPath()}dummy_file";

            var dummyFile = new System.IO.FileStream(dummyFilePath, System.IO.FileMode.Create);
            _ = dummyFile.Seek(2048L * 1024 * 1024, System.IO.SeekOrigin.Begin);
            dummyFile.WriteByte(0);
            dummyFile.Close();

            bool? oldResponding = null;
            var process = System.Diagnostics.Process.Start(@"C:\Program Files\Windows NT\Accessories\wordpad.exe", dummyFilePath);
            try
            {
                do
                {
                    process?.Refresh();
                    bool? responding = process?.Responding;
                    if (responding != oldResponding)
                    {
                        oldResponding = responding;
                        System.Console.WriteLine($"{responding}");
                    }
                    System.Threading.Thread.Sleep(1000);
                } while (!(System.Console.KeyAvailable && System.Console.ReadKey(true).Key == System.ConsoleKey.Escape));
            }
            finally
            {
                process?.Kill();
            }
        }
    }
}
