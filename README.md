# 📁 FileMetadataViewer

A simple cross-platform CLI tool built with **.NET 9.0** to extract and optionally export file metadata to a CSV file.

---

## ✨ Features

-   View file metadata directly in the terminal
-   Export metadata to CSV using `--export`
-   Automatically opens the output folder and selects the generated file.
-   Supports Windows, Linux, and macOS

---

## ⚙️ Technologies Used

-   **.NET 9.0**
-   `System.Runtime.InteropServices`
-   `System.Text`
-   Cross-platform file explorer integration

---

## 🚀 Usage

### 🧪 Example Command

```bash
dotnet run "C:/Users/sujoy/Downloads/logo.png" --export
```

### 🖥️ Output:

```
C:\Users\sujoy\Downloads\logo.png metadata:
File Name      : logo.png
File Extension : .png
File Size      : 22274 bytes
Created On     : 30/05/2025 11:22:11 PM
Modified On    : 30/05/2025 11:22:12 PM
Read-only      : False
Directory      : C:\Users\sujoy\Downloads
Exists         : True
Last Accessed  : 2/06/2025 3:57:45 AM
Hidden         : False
System File    : False
Archive File   : True
Temporary File : False
Exported to: C:\Users\sujoy\Documents\MetadataCSV\logo.csv
```

File Explorer will open automatically and select the generated `.csv` file.

---

## 🧾 Arguments

-   `file_path` – Path to the file you want to inspect
-   `--export` – (Optional) Saves metadata to `Documents/MetadataCSV/<original_filename>.csv`

---

## 🏗️ Building and Publishing

### ✅ Windows (Standalone `.exe`):

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

📌 Output Path:

```
bin\Release\net9.0\win-x64\publish\FileMetadataViewer.exe
```

You can add this folder to your **system PATH** and use it globally:

```bash
FileMetadataViewer "C:\Path\To\File.txt" --export
```

---

### 🐧 Linux (Intel/AMD):

```bash
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

### 🍏 macOS (Apple M1/M2):

```bash
dotnet publish -c Release -r osx-arm64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

---

## 💡 What the `dotnet publish` flags mean:

-   `-c Release`: Use the Release configuration
-   `-r win-x64` / `linux-x64` / `osx-arm64`: Target runtime/architecture
-   `--self-contained true`: Package .NET runtime into the build
-   `-p:PublishSingleFile=true`: Bundle all files into a single executable
-   `-p:IncludeNativeLibrariesForSelfExtract=true`: Extract native dependencies at runtime (required for some OS APIs)

---

## 📝 Notes

-   The `openFileExplorer()` function was generated with the help of ChatGPT and currently works on **Windows**. macOS/Linux are untested but logic is there. You are welcome to test.
-   If you use `--export`, the `.csv` will be saved to your **"Documents/MetadataCSV"** folder.
-   The tool does not require .NET runtime after publishing. it's completely self-contained.
