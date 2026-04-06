# Simple Web Server Project

[![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20macOS-lightgrey.svg)](https://github.com/FranMustico/Simple-Web-Server-Project)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE.md)

A lightweight web server built with ASP.NET Core, designed to serve static files, handle dynamic file requests, and demonstrate fundamental web server operations.

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Installation](#installation)
- [Usage](#usage)
- [Documentation](#documentation)
- [Development](#development)
- [Contributing](#contributing)
- [License](#license)

---

## Overview

This project is a simple web server built using ASP.NET Core. It is designed to handle HTTP requests, serve static files, and provide dynamic file handling capabilities. The project was developed as part of the LTU Computer Networks Project 1 by Francisco Mustico.

---

## Features

- **Static File Serving**: Serves static files such as HTML, CSS, JavaScript, and assets from the `wwwroot` directory.
- **Dynamic File Handling**: Accepts dynamic file paths via API endpoints and returns files from the server's `wwwroot` directory.
- **Error Handling**: Returns appropriate HTTP status codes for success (200 OK) and errors (404 Not Found).
- **Modern UI**: The landing page features a clean, iOS-inspired design with a file download button and a textbox for entering custom file paths.

---

## Architecture

### Project Structure

```
Simple-Web-Server-Project/
├── src/
│   ├── Endpoints/
│   │   └── website_endpoints.cs          # API endpoint definitions
│   ├── wwwroot/
│   │   ├── Assets/                      # Static assets (images, PDFs)
│   │   └── Landing/                     # Landing page files (HTML, CSS, JS)
│   ├── Program.cs                       # Application entry point
│   └── Simple Web Server Project.csproj # Project file
├── LICENSE                              # License file
├── README.md                            # Project documentation
└── Simple-Web-Server-Project.sln        # Solution file
```

### Design Patterns

- **Middleware Pattern**: ASP.NET Core middleware for static file serving and routing.
- **Separation of Concerns**: Endpoints, static files, and application configuration are modularized.

---

## Installation

### Prerequisites

- .NET 10.0 Runtime
- Windows 10+ or macOS
- Browser (e.g., Chrome, Firefox, Safari)

### Quick Start

#### Clone the Repository

```bash
git clone https://github.com/FranMustico/Simple-Web-Server-Project.git
cd Simple-Web-Server-Project
```

#### Build and Run

```bash
# Build the project
dotnet build

# Run the server
dotnet run --project src/Simple-Web-Server-Project.csproj
```

#### Access the Web Server

Open a browser and navigate to `http://localhost:5000`.

---

## Usage

### API Endpoints

#### 1. **GET `/`**
- **Description**: Serves the landing page (`Landing.html`).
- **Response**: HTML content.

#### 2. **GET `/file`**
- **Description**: Serves the `Francisco_Mustico_.pdf` file.
- **Response**: PDF file.

#### 3. **GET `/{filePath}`**
- **Description**: Dynamically serves files based on the provided path.
- **Example**: `GET /Assets/file-icon.png`
- **Response**: The requested file or a 404 error if the file does not exist.

### Example Workflow

1. Open the landing page.
2. Enter the file path (e.g., `Assets/file-icon.png`) in the textbox.
3. Press `Enter` or click the download button.

---

## Documentation

### Supported File Types

- **HTML**: Landing page (`Landing.html`)
- **CSS**: Stylesheets (`Landing.css`)
- **JavaScript**: Client-side logic (`Landing.js`)
- **Assets**: Images and PDFs (`file-icon.png`, `Francisco_Mustico_.pdf`)

### Error Handling

- **404 Not Found**: Returned when a requested file does not exist.
- **500 Internal Server Error**: Returned for unexpected server errors.

---

## Development

### Building for Different Platforms

```bash
# Build for the current platform
dotnet publish -c Release

# Build for Windows x64
dotnet publish -c Release -r win-x64 --self-contained

# Build for macOS ARM64
dotnet publish -c Release -r osx-arm64 --self-contained
```

### Development Mode

```bash
# Hot reload during development
dotnet watch run
```

### Key Dependencies

- **.NET 10.0** - Core runtime
- **System.IO** - File handling for dynamic endpoints
- **Microsoft.AspNetCore.StaticFiles** - Middleware for static file serving

### Testing

#### Manual Testing Scenarios

1. **Static File Serving**
   - Verify that `Landing.html` is served correctly.
   - Test CSS and JavaScript loading.

2. **Dynamic File Handling**
   - Test valid file paths (e.g., `Assets/file-icon.png`).
   - Test invalid file paths (e.g., `Assets/nonexistent.png`).

3. **Error Handling**
   - Verify 404 responses for missing files.
   - Test server stability with invalid requests.

---

## Contributing

This project is part of an educational exercise demonstrating web server design principles.


### Development Setup

1. Fork the repository.
2. Create a feature branch.
3. Make your changes.
4. Test thoroughly.
5. Submit a pull request.

---

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
