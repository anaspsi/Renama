// See https://aka.ms/new-console-template for more information



Console.WriteLine("Please backup your data before proceed !");
DirectoryInfo di = new DirectoryInfo(@".\");
FileSystemInfo[] fsi = di.GetFileSystemInfos();

IEnumerable<FileSystemInfo> sortAscendingQuery = from ffile in fsi
                                                 where ffile.Name.ToUpper().Contains("PDF")
                                                 orderby int.Parse(Path.GetFileNameWithoutExtension(ffile.Name))
                                                 select ffile;
int nomor = 1;
bool isTobeRenamedExist = false; 
foreach (var file in sortAscendingQuery)
{
    string fileName = Path.GetFileNameWithoutExtension(file.Name);
    if (fileName != nomor.ToString())
    {
        Console.WriteLine(file.Name + " will be rename to " + nomor.ToString() + ".PDF");
        isTobeRenamedExist = true;
    }
    else
    {
        Console.WriteLine(file.Name + " no need to be renamed");
    }
    nomor++;
}

nomor = 1;

if(isTobeRenamedExist)
{
    Console.Write("Do you agree with condition above ? [Y/N] :");
    string key = Console.ReadLine();
    if (key.ToUpper().Equals("Y"))
    {
        foreach (var file in sortAscendingQuery)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.Name);
            if (fileName != nomor.ToString())
            {
                File.Move(file.FullName, @".\" + nomor.ToString() + ".PDF");
            }
            nomor++;
        }
        Console.Write("Done");
    }
    else
    {
        Console.Write("Thanks");
    }
} else
{
    Console.Write("Nothing");
}

Console.ReadKey();