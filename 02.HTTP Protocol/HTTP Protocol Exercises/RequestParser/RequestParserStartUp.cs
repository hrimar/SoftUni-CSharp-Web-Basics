using System;
using System.Collections.Generic;
using System.Linq;

namespace RequestParser
{
    class RequestParserStartUp
    {
        private const string HttpTemplateHeader =
            "HTTP/1.1 {0}\nContent-Length: {1}\nContent-Type: text/plain\n\n{2}";

        private static Dictionary<int, string> StatusTextByResponseCode =
            new Dictionary<int, string>()
            {
                { 200, "OK" },
                 {404, "Not Found" }
            };


        static void Main()
        {
            var pathByMethods = new Dictionary<string, HashSet<string>>(); // method, paths

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var args = input.Substring(1).Split('/');
                var path = args[0];
                var method = args[1];

                if (!pathByMethods.ContainsKey(method))
                {
                    pathByMethods[method] = new HashSet<string>();
                }
                pathByMethods[method].Add(path);
            }

            var requestSplit = Console.ReadLine().ToLower().Split(' ');
            var requestMethod = requestSplit[0];
            var requestPath = requestSplit[1].Substring(1);

            var responsePathExists = pathByMethods.ContainsKey(requestMethod)
                ? pathByMethods[requestMethod].FirstOrDefault(p => p == requestPath)
                : string.Empty;

            string response = string.Empty;
            if (string.IsNullOrEmpty(responsePathExists))
            {
                var responseStatusCode =
                  $"{StatusTextByResponseCode.Keys.FirstOrDefault(k => k == 404)} {StatusTextByResponseCode[404]}";
                response = string.Format(HttpTemplateHeader,
                    responseStatusCode,
                    StatusTextByResponseCode[404].Length,
                    StatusTextByResponseCode[404]);
            }
            else
            {
                var responseStatusCode = 
                    $"{StatusTextByResponseCode.Keys.FirstOrDefault(k => k == 200)} {StatusTextByResponseCode[200]}";
                response = string.Format(HttpTemplateHeader, 
                    responseStatusCode, 
                    StatusTextByResponseCode[200].Length,
                    StatusTextByResponseCode[200]);
            }

            Console.WriteLine(response);
			
			// Variant 2:
			 var requests = new Dictionary<string, HashSet<string>>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var parts = input.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                var path = $"/{parts[0]}";
                var method = parts[1];

                if (!requests.ContainsKey(path))
                {
                    requests[path] = new HashSet<string>();
                }

                requests[path].Add(method);
            }

            var request = Console.ReadLine();
            var requestParts = request.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var requestMethod = requestParts[0].ToLower();
            var requestPath = requestParts[1];
            var requestProtocol = requestParts[2];

            var statusCode = "200 OK";

            if (!requests.ContainsKey(requestPath) || !requests[requestPath].Contains(requestMethod))
            {
                statusCode = "404 Not Found";
            }

            var statusMessage = statusCode.Substring(4);
            var contentLength = statusMessage.Length;

            Console.WriteLine($"HTTP/1.1 {statusCode}");
            Console.WriteLine($"Content-Length: {contentLength}");
            Console.WriteLine("Content-Type: text/plain");
            Console.WriteLine();
            Console.WriteLine(statusMessage);
        }
    }
}
