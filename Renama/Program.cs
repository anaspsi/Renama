// See https://aka.ms/new-console-template for more information

DirectoryInfo di = new DirectoryInfo(@".\");
FileSystemInfo[] fsi = di.GetFileSystemInfos();

int nomor = 1;
int invalidNomor = 0;
foreach (var i in fsi)
{
    if (i.Name.ToUpper().Contains("PDF"))
    {
        string nameWithoutExtension = Path.GetFileNameWithoutExtension(i.Name);
        if (int.TryParse(nameWithoutExtension, out _))
        {

        }
        else
        {
            Console.WriteLine("the following file name is invalid," + nameWithoutExtension);
            invalidNomor = nomor;
        }

    }
    nomor++;
}
if (invalidNomor == 0)
{
    Console.WriteLine("Please backup your data before proceed !");
    IEnumerable<FileSystemInfo> sortAscendingQuery = from ffile in fsi
                                                     where ffile.Name.ToUpper().Contains("PDF")
                                                     orderby int.Parse(Path.GetFileNameWithoutExtension(ffile.Name))
                                                     select ffile;
    nomor = 1;
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

    if (isTobeRenamedExist)
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
    }
    else
    {
        Console.Write("Nothing");
    }
}

Console.ReadKey();