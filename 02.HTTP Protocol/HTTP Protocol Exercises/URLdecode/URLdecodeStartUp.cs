using System;
using System.Net;

namespace URLdecode
{
    class URLdecodeStartUp
    {
        static void Main()
        {
            string inputURL = Console.ReadLine();
            string outputURL = WebUtility.UrlDecode(inputURL);
            Console.WriteLine(outputURL);
        }
    }
}
