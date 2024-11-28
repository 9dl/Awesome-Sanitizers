import html

# Function to sanitize the input
def sanitize(input: str) -> str:
    sanitized_input = input

    # Check if input is empty
    if len(input) == 0:
        raise ValueError("input cannot be empty")

    # Decode HTML entities
    sanitized_input = html.unescape(sanitized_input)

    # Disallowed characters to remove
    disallowed_chars = "!@#$%^&*()_+.,<>?/\\|{}[]'\"`~;:=-_<>`"
    for char in disallowed_chars:
        sanitized_input = sanitized_input.replace(char, "")

    # Remove Spaces from Prefix
    sanitized_input = sanitized_input.lstrip(" \t")

    # Remove Spaces from Suffix
    sanitized_input = sanitized_input.rstrip(" \t")

    return sanitized_input


# Test cases similar to Go version
inputs = [
    # SQL Injections
    "' OR 1=1 --",
    "' UNION SELECT null, username, password FROM users --",
    "admin' --",
    "1' AND 1=1 --",
    "1' OR 'x' = 'x' --",
    "1' HAVING 1=1 --",

    # XSS (Cross-site Scripting)
    "<script>alert('XSS')</script>",
    "<img src='x' onerror='alert(1)' />",
    "<script>console.log('XSS')</script>",
    "<iframe src='malicious-site.com'></iframe>",
    "<div onclick='alert(2)'>click me</div>",
    "<a href='javascript:alert(1)'>link</a>",
    "<script src='http://evil.com/malicious.js'></script>",

    # Command Injection
    "1; DROP TABLE users;",
    "echo 'Hacked' > /tmp/test.txt",
    "ping -c 1 127.0.0.1",

    # Path Traversal
    "../../../../etc/passwd",
    "../admin/../../etc/passwd",
    "/etc/passwd",

    # Server Side Request Forgery (SSRF)
    "http://localhost:8080/admin",
    "http://127.0.0.1/admin",
    "http://example.com/malicious",

    # Cross-Site Request Forgery (CSRF)
    "<img src='http://victim.com/delete?id=1' />",
    "<form action='http://victim.com/delete' method='POST'><input type='hidden' name='id' value='1'></form>",
]

# Open the file for writing
with open("README.md", "w") as file:
    file.write("# Test Results: Input Sanitization\n\n")
    file.write("This README contains test results for various input sanitization cases.\n\n")

    # Process each input
    for input_str in inputs:
        try:
            sanitized_input = sanitize(input_str)
            file.write("**Original Input:**\n")
            file.write(f"`{input_str}`\n")
            file.write("**Sanitized Input:**\n")
            file.write(f"`{sanitized_input}`\n")
            file.write("\n")
            file.write("---\n")
        except ValueError as e:
            print(f"Error sanitizing input: {e}")
            continue

print("Test results saved to README.md")
