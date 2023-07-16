using System.Diagnostics;

public static class Bootstrap
{
    public static string RootPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.FullName;

    public static string ConfigPath = Path.Combine(RootPath, "config.txt");

    public static string TargetPath = string.Empty;

    public static void OnProcessExit(object? sender, EventArgs e)
    {
        string currentPath = Environment.GetCommandLineArgs()[0];
        //string flag = Directory.GetParent(currentPath).Name;

        string config = File.ReadAllText(ConfigPath);
        int flag = Convert.ToInt32(config);
       
        if (flag == 0)
        {
            return;
        }

        flag = (flag == 1 ? 2 : 1);
        File.WriteAllText(ConfigPath, flag.ToString());
        string runPath = Path.Combine(RootPath, flag.ToString(), Path.GetFileName(Environment.GetCommandLineArgs()[0]));

        ProcessStartInfo process = new ProcessStartInfo(runPath);
        process.WorkingDirectory = Directory.GetParent(runPath)!.FullName;
        process.UseShellExecute = true;

        Process.Start(process);
        File.WriteAllText("C:\\Users\\empty\\Downloads\\web\\1.txt", runPath);
    }
}
