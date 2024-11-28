/*
   Language: C# Net Core
   Version: 9.0.100
   Author: github.com/9dl
   Date: 2024-11-28

   Usage: To run the program, use the command:
      dotnet run Program.cs
*/

using System.Net;

namespace ConsoleApp1;

internal class Program
{
    static void Main()
    {
        List<string> inputs = new List<string>
        {
            // SQL Injections
            "' OR 1=1 --",
            "' UNION SELECT null, username, password FROM users --",
            "admin' --",
            "1' AND 1=1 --",
            "1' OR 'x' = 'x' --",
            "1' HAVING 1=1 --",

            // XSS (Cross-site Scripting)
            "<script>alert('XSS')</script>",
            "<img src='x' onerror='alert(1)' />",
            "<script>console.log('XSS')</script>",
            "<iframe src='malicious-site.com'></iframe>",
            "<div onclick='alert(2)'>click me</div>",
            "<a href='javascript:alert(1)'>link</a>",
            "<script src='http://evil.com/malicious.js'></script>",

            // Command Injection
            "1; DROP TABLE users;",
            "echo 'Hacked' > /tmp/test.txt",
            "ping -c 1 127.0.0.1",

            // Path Traversal
            "../../../../etc/passwd",
            "../admin/../../etc/passwd",
            "/etc/passwd",

            // Server Side Request Forgery (SSRF)
            "http://localhost:8080/admin",
            "http://127.0.0.1/admin",
            "http://example.com/malicious",

            // Cross-Site Request Forgery (CSRF)
            "<img src='http://victim.com/delete?id=1' />",
            "<form action='http://victim.com/delete' method='POST'><input type='hidden' name='id' value='1'></form>",
        };

        try
        {
            using (StreamWriter file = new StreamWriter("README.md"))
            {
                file.WriteLine("# Test Results: Input Sanitization\n");
                file.WriteLine("This README contains test results for various input sanitization cases.\n");

                foreach (var input in inputs)
                {
                    var (sanitizedInput, exception) = Sanitize(input);

                    if (exception != null)
                    {
                        file.WriteLine("**Error Sanitizing Input:**");
                        file.WriteLine($"`{exception.Message}`");
                    }
                    else
                    {
                        file.WriteLine("**Original Input:**");
                        file.WriteLine($"`{input}`");
                        file.WriteLine("**Sanitized Input:**");
                        file.WriteLine($"`{sanitizedInput}`");
                    }

                    file.WriteLine("\n---\n");
                }
            }

            Console.WriteLine("Test results saved to README.md");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to README.md: {ex.Message}");
        }
    }

    private static (string, Exception) Sanitize(string input)
    {
        var sanitizedInput = input;

        if (string.IsNullOrEmpty(sanitizedInput)) return (sanitizedInput, new Exception("input is null or empty"));

        // Decode HTML entities
        sanitizedInput = WebUtility.HtmlDecode(sanitizedInput);

        // Check for disallowed characters
        var disallowedChars = "!@#$%^&*()_+.,<>?/\\|{}[]'\"`~;:=-_<>`";
        if (sanitizedInput.IndexOfAny(disallowedChars.ToCharArray()) != -1)
            sanitizedInput = new string(sanitizedInput.Where(c => !disallowedChars.Contains(c)).ToArray());
            //return (sanitizedInput, new Exception("input contains disallowed characters"));

        // Remove Spaces from Prefix
        sanitizedInput = sanitizedInput.TrimStart(" \t".ToCharArray());

        // Remove Spaces from Suffix
        sanitizedInput = sanitizedInput.TrimEnd(" \t".ToCharArray());

        return (sanitizedInput, null)!;
    }
}