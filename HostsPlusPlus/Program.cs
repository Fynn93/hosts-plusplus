using MiscUtil.IO;
using System.Runtime.InteropServices;

namespace HostsPlusPlus
{
    class HostsPlusPlus
    {
        [DllImport("msvcrt.dll")]
        public static extern int system(string format);

        static void Main(string[] args)
        {
            StreamReader sr = new(args[0]);
            string text = sr.ReadToEnd();
            sr.Close();

            foreach (string line in new LineReader(() => new StringReader(text)))
            {
                try
                {
                    if (line.StartsWith("##include"))
                    {
                        string[] l2 = line.Split(' ');
                        string file = l2[1];
                        StreamReader sr2;
                        Console.WriteLine($"Downloading \"{file}\"...");
                        system($"curl -s --output download.txt \"{file}\"");
                        sr2 = new("download.txt");
                        string text2 = sr2.ReadToEnd();
                        sr2.Close();
                        File.AppendAllText(args[0], $"\n# HostsPlusPlus v.{typeof(HostsPlusPlus).Assembly.GetName().Version}\n");
                        File.AppendAllText(args[0], $"# Included from {l2[1]}\n");
                        File.AppendAllText(args[0], $"{text2}\n");
                        if (File.Exists("download.txt")) { File.Delete("download.txt"); }
                    }
                    else if (line.StartsWith("##foreach(##include"))
                    {
                        string[] l2 = line.Split(' ');
                        string[] l3 = null!;
                        try { l3 = l2[1].Split(')'); } catch { Console.WriteLine("No ending paranthese found!"); }
                        string file = l3[0];
                        StreamReader sr2;
                        system($"curl -s --output download.txt \"{file}\"");
                        sr2 = new("download.txt");
                        string text2 = sr2.ReadToEnd();
                        sr2.Close();
                        StreamReader sr3;
                        foreach (string l in new LineReader(() => new StringReader(text2)))
                        {
                            Console.WriteLine($"Downloading \"{l}\"...");
                            system($"curl -s --output download.txt \"{l}\"");
                            sr3 = new("download.txt");
                            File.AppendAllText(args[0], $"\n# HostsPlusPlus v.{typeof(HostsPlusPlus).Assembly.GetName().Version}\n");
                            File.AppendAllText(args[0], $"# Included from {l}\n");
                            File.AppendAllText(args[0], $"{sr3.ReadToEnd()}\n");
                            sr3.Close();
                            File.Delete("download.txt");
                        }
                    }
                    else if (line.StartsWith("##config replace_0s")) {
                        Console.WriteLine("Removing 0.0.0.0s!");
                        string newtext = File.ReadAllText(args[0]);
                        newtext = newtext.Replace("0.0.0.0 ", "");
                        File.WriteAllText(args[0], newtext);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"No H++ Instruction found!; \n\n{e}");
                }
            }
        }
    }
}