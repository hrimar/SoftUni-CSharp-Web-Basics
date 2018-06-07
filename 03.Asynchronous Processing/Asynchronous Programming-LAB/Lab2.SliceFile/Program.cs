using System;
using System.IO;
using System.Threading.Tasks;

namespace Lab2.SliceFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var videoPath = Console.ReadLine();
            var destinationPath = Console.ReadLine();
            int parts = int.Parse(Console.ReadLine());

            SliceAsync(videoPath, destinationPath, parts);

            Console.WriteLine("Anything else?");
            while (true)
            {
                Console.ReadLine();
            }

        }

        static void Slice(string sourceFile, string destinationPath, int parts)
        {

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var source = new FileStream(sourceFile, FileMode.Open))
            {
                FileInfo fileInfo = new FileInfo(sourceFile);

                if (parts == 0)
                {
                    parts = 1;
                }

                long partLength = (source.Length / parts) + 1;
                long currentByte = 0;
                long bufferLength = (source.Length / parts) + 1;

                for (int currentPart = 1; currentPart <= parts; currentPart++)
                {
                    string filePath = string.Format("{0}/Part-{1}{2}",
                        destinationPath, currentPart, fileInfo.Extension);

                    using (var destination = new FileStream(filePath, FileMode.Create))
                    {
                        byte[] buffer = new byte[bufferLength];
                        while (currentByte <= partLength * currentPart)
                        {
                            int readBytesCount = source.Read(buffer, 0, buffer.Length);
                            if(readBytesCount==0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, readBytesCount);
                            currentByte += readBytesCount;
                        }
                    }

                    Console.WriteLine($"Part {currentPart}/{parts} ready.");
                }

                Console.WriteLine("Slice complete.");
            }
           
        }

        static void SliceAsync(string sourceFile, string destinationPath, int parts)
        {
            Task.Run(() =>
            {
                Slice(sourceFile, destinationPath, parts);
            });
        }
    }
}
