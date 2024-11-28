/*
   Language: Golang
   Version: 1.23.3
   Author: github.com/9dl
   Date: 2024-11-28

   Usage: To run the program, use the command:
      go run main.go
*/

package main

import (
	"fmt"
	"html"
	"os"
	"strings"
)

func main() {
	inputs := []string{
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
	}

	file, err := os.Create("README.md")
	if err != nil {
		fmt.Println("Error creating README.md:", err)
		return
	}
	defer file.Close()

	_, _ = file.WriteString("# Test Results: Input Sanitization\n\n")
	_, _ = file.WriteString("This README contains test results for various input sanitization cases.\n\n")

	for _, input := range inputs {
		sanitizedInput, err := Sanitize(input)
		if err != nil {
			fmt.Println("Error sanitizing input:", err)
			continue
		}

		_, _ = file.WriteString("**Original Input:**\n")
		_, _ = file.WriteString(fmt.Sprintf("`%s`\n", input))
		_, _ = file.WriteString("**Sanitized Input:**\n")
		_, _ = file.WriteString(fmt.Sprintf("`%s`\n", sanitizedInput))
		_, _ = file.WriteString("\n")
		_, _ = file.WriteString("---\n")
	}

	fmt.Println("Test results saved to README.md")
}

func Sanitize(input string) (string, error) {
	var sanitizedInput = input

	// Check if input is empty
	if len(input) == 0 {
		return "", fmt.Errorf("input cannot be empty")
	}

	// Decode HTML entities
	sanitizedInput = html.UnescapeString(sanitizedInput)

	// Check for disallowed characters
	disallowedChars := "!@#$%^&*()_+.,<>?/\\|{}[]'\"`~;:=-_<>`"
	for _, char := range disallowedChars {
		/*if strings.Contains(sanitizedInput, string(char)) {
			return "", fmt.Errorf("input contains disallowed character: %s", string(char))
		}*/
		sanitizedInput = strings.ReplaceAll(sanitizedInput, string(char), "")
	}

	// Remove Spaces from Prefix
	sanitizedInput = strings.TrimLeft(sanitizedInput, " \t")

	// Remove Spaces from Suffix
	sanitizedInput = strings.TrimRight(sanitizedInput, " \t")

	return sanitizedInput, nil
}
