using System.Runtime.InteropServices;
using System.Text;

namespace FileMetadataViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Mention a file name to proceed.");
                return;
            }

            string filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            FileInfo fileInfo = new FileInfo(filePath);
            Console.WriteLine($"{fileInfo.FullName} metadata:");
            Console.WriteLine($"File Name      : {fileInfo.Name}");
            Console.WriteLine($"File Extension : {fileInfo.Extension}");
            Console.WriteLine($"File Size      : {fileInfo.Length} bytes");
            Console.WriteLine($"Created On     : {fileInfo.CreationTime}");
            Console.WriteLine($"Modified On    : {fileInfo.LastWriteTime}");
            Console.WriteLine($"Read-only      : {fileInfo.IsReadOnly}");
            Console.WriteLine($"Directory      : {fileInfo.DirectoryName}");
            Console.WriteLine($"Exists         : {fileInfo.Exists}");
            Console.WriteLine($"Last Accessed  : {fileInfo.LastAccessTime}");
            Console.WriteLine($"Hidden         : {fileInfo.Attributes.HasFlag(FileAttributes.Hidden)}");
            Console.WriteLine($"System File    : {fileInfo.Attributes.HasFlag(FileAttributes.System)}");
            Console.WriteLine($"Archive File   : {fileInfo.Attributes.HasFlag(FileAttributes.Archive)}");
            Console.WriteLine($"Temporary File : {fileInfo.Attributes.HasFlag(FileAttributes.Temporary)}");

            bool export = false;
            if (args.Length > 1 && args[1] == "--export")
            {
                export = true;
            }

            if (export)
            {
                string exportDirectoryWithName = exportToCSV(fileInfo);
                Console.WriteLine($"Exported to: {exportDirectoryWithName}");
                openFileExplorer(exportDirectoryWithName);
            }
        }

        public static string exportToCSV(FileInfo fileInfo)
        {
            string documentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string exportDirectory = Path.Combine(documentDirectory, "MetadataCSV");
            Directory.CreateDirectory(exportDirectory);

            string exportFileName = Path.GetFileNameWithoutExtension(fileInfo.Name) + ".csv";
            string exportDirectoryWithName = Path.Combine(exportDirectory, exportFileName);

            var csv = new StringBuilder();
            csv.AppendLine("Field,Value");
            csv.AppendLine($"Full Path,{fileInfo.FullName}");
            csv.AppendLine($"File Name,{fileInfo.Name}");
            csv.AppendLine($"File Extension,{fileInfo.Extension}");
            csv.AppendLine($"File Size (bytes),{fileInfo.Length}");
            csv.AppendLine($"Created On,{fileInfo.CreationTime}");
            csv.AppendLine($"Modified On,{fileInfo.LastWriteTime}");
            csv.AppendLine($"Read-only,{fileInfo.IsReadOnly}");
            csv.AppendLine($"Directory,{fileInfo.DirectoryName}");
            csv.AppendLine($"Exists,{fileInfo.Exists}");
            csv.AppendLine($"Last Accessed,{fileInfo.LastAccessTime}");
            csv.AppendLine($"Hidden,{fileInfo.Attributes.HasFlag(FileAttributes.Hidden)}");
            csv.AppendLine($"System File,{fileInfo.Attributes.HasFlag(FileAttributes.System)}");
            csv.AppendLine($"Archive File,{fileInfo.Attributes.HasFlag(FileAttributes.Archive)}");
            csv.AppendLine($"Temporary File,{fileInfo.Attributes.HasFlag(FileAttributes.Temporary)}");

            File.WriteAllText(exportDirectoryWithName, csv.ToString());
            return exportDirectoryWithName;
        }

        static void openFileExplorer(string csvPath)
        {
            // This opens File Explorer with the file selected
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{csvPath}\"");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                System.Diagnostics.Process.Start("open", $"-R \"{csvPath}\"");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Try with xdg-open (won't select file but opens folder)
                System.Diagnostics.Process.Start("xdg-open", $"\"{Path.GetDirectoryName(csvPath)}\"");
            }
        }
    }
}