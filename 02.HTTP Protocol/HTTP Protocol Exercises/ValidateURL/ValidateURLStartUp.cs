using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;

namespace ValidateURL
{
    class ValidateURLStartUp
    {
        private const string InvalidUrlMessage = "Invalid URL";
        private const string resultMesage = "Protocol: {0}\nHost: {1}\nPort: {2}";

        static void Main()
        {
            // Variant 1:
            //string input = Console.ReadLine();
            ////string regexString = @"^((http|https):\/(\/[0-9a-zA-Z\-\.]+)(\:\d+)?)(\/([0-9a-zA-Z\-\.\/]+)*)?(\?[0-9a-zA-Z\-\.\=\+\&_]+)?(\#[0-9a-zA-Z\-\.]+)?$";
            ////string regexString = @"^((http|https):\/(\/[0-9a-zA-Z\-\.]+)(\:\d+)?)(\/([0-9a-zA-Z\-\.\/]+)*)?(\?[0-9a-zA-Z\-\.\=\+\&_]+)?(\#[0-9a-zA-Z\-\.]+)?$";
            //var regex = new Regex(@"^(?<protocol>https|https?):\/\/(?<host>[a-zA-Z-.]+):?(?<port>\d+)?\/(?<path>[a-zA-Z0-9]+)?\??(?<query>[a-zA-Z0-9=&]+)?#?(?<fragment>[0-9a-zA-Z\-\.]+)?$");
            //var match = regex.Match(input);

            //string protocol = string.Empty;
            //string host = string.Empty;
            //string port = string.Empty;
            //string path = string.Empty;
            //bool valitURL = true;
            //if (match.Success)
            //{
            //    protocol = match.Groups["protocol"].ToString();
            //    host = match.Groups["host"].ToString();
            //    port = match.Groups["port"].ToString();
            //    path = match.Groups["path"].ToString();
            //    if (protocol == "http" & (port == "80" || string.IsNullOrEmpty(port)))
            //    {
            //        Console.WriteLine(string.Format(resultMesage, protocol, host, 80));                                     
            //    }
            //    else if (protocol == "https" & (port == "443" || string.IsNullOrEmpty(port)))
            //    {
            //        Console.WriteLine(string.Format(resultMesage, protocol, host, 443));
            //    }
            //    else
            //    {
            //        Console.WriteLine(InvalidUrlMessage);
            //        valitURL = false;
            //    }

            //    if (valitURL && string.IsNullOrEmpty(path))
            //    {
            //        Console.WriteLine("Path: /");
            //    }
            //    else if(valitURL)
            //    {
            //        Console.WriteLine($"Path: /{path}");
            //    }

            //    string query = match.Groups["query"].ToString();
            //    if (valitURL && !string.IsNullOrEmpty(query))
            //    {
            //        Console.WriteLine($"Query: {query}");  // Самото query може да се разбие с URLDecode!
            //    }

            //    string fragment = match.Groups["fragment"].ToString();
            //    if (valitURL && !string.IsNullOrEmpty(fragment))
            //    {
            //        Console.WriteLine($"Fragment: {fragment}");
            //    }
            //} 

            // Variant 2:  !!!
            string input = Console.ReadLine();
            var decodedURL = WebUtility.UrlDecode(input);

            Uri parsedURL = new Uri(decodedURL); // !!! - see in Debug for all details!

            if (string.IsNullOrEmpty(parsedURL.Scheme) || string.IsNullOrEmpty(parsedURL.Host)
                || string.IsNullOrEmpty(parsedURL.LocalPath) || !parsedURL.IsDefaultPort)
            {
                Console.WriteLine(InvalidUrlMessage);
            }
            else
            {
                var sbResult = new StringBuilder();

                sbResult.AppendLine($"Protocol: {parsedURL.Scheme}")
                    .AppendLine($"Host: {parsedURL.Host}")
                    .AppendLine($"Port: {parsedURL.Port}")
                    .AppendLine($"Path: {parsedURL.AbsolutePath}");

                if (!string.IsNullOrEmpty(parsedURL.Query))
                {
                    sbResult.AppendLine($"Query: {parsedURL.Query.Substring(1)}");
                }
                if (!string.IsNullOrEmpty(parsedURL.Fragment))
                {
                    sbResult.AppendLine($"Fragment: {parsedURL.Fragment.Substring(1)}");
                }

                Console.WriteLine(sbResult.ToString().Trim());
            }
        }
    }
}
