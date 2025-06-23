# RestoreCleaner

## Overview
RestoreCleaner is a simple utility designed to automate the cleanup of build artifacts in the Palette Picker project. It scans all versioned `source-code` directories and removes the `bin` and `obj` folders, helping to keep your repository clean and reducing unnecessary clutter.

## How It Works
- The program searches for all version directories under `versions/` in the Palette Picker repository.
- For each version, it looks for a `source-code` subdirectory.
- If `bin` or `obj` folders are found within `source-code`, they are deleted recursively.
- The program provides console output for each action, including errors and a summary upon completion.

## Usage
1. Build the project using your preferred .NET build tool (e.g., Visual Studio or `dotnet build`).
2. Run the executable. No arguments are required.

```
restore-cleaner.exe
```

> **Note:** The script is hardcoded to work with the path `C:\Users\MarkusHTL\source\repos\PalettePickerRelease\versions`. Adjust the path in `Program.cs` if your directory structure is different.

## Output
- The console will display the number of version directories found.
- For each directory, it will indicate whether `bin` and `obj` folders were deleted or if none were found.
- Errors are shown in red for easy identification.
- A green message confirms successful cleanup at the end.

## License
This utility is provided as part of the Palette Picker project. See the main repository for license details.
