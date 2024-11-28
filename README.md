# Awesome-Sanitizers 🔒

A collection of sanitizer implementations across multiple programming languages, designed to prevent injection attacks by cleaning and decoding user input. 🚫

## Features ✨

- **HTML Decoding**: Automatically decodes HTML entities (e.g., `&lt;` → `<`).
- **Disallowed Character Removal**: Removes dangerous characters 🛑.
- **Space Trimming**: Removes spaces from the start and end of input ✂️.
- **Multi-Language Support**: Includes sanitizer implementations in multiple languages (e.g., Go, C#) 🌍.

## Languages 🖥️

The repository contains sanitizers implemented in the following languages:

- **Go**: Available in `golang/`
- *(More languages coming soon!)*

## Usage 💻

Each language folder contains its own implementation. To use a sanitizer in your project, simply copy the implementation into your codebase and call the `Sanitize` function with the input you want to sanitize.

### Example 📋

```bash
Original: %27; DROP TABLE users;
Sanitized: DROP TABLE users
```

## Contributing 🤝

1. Fork the repository.
2. Create your feature branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Open a pull request to merge your changes.