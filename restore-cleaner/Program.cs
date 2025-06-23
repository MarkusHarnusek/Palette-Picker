using System.IO;
using System;
using System.Linq;

namespace RestoreCleaner;

internal class Program
{
    /// <summary>
    /// Entry point for the RestoreCleaner utility. Cleans up 'bin' and 'obj' folders from all versioned source-code directories.
    /// </summary>
    private static void Main(string[] args)
    {
        // Determine the base directory by traversing up from the executable location
        bool showDebugMessages = args.Contains("--debug");

        var baseDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
            .Parent?.Parent?.Parent?.Parent;

        if (baseDir == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Could not determine the base directory.");
            Console.ResetColor();
            return;
        }

        string versionsDir = Path.Combine(baseDir.FullName, "versions");

        if (showDebugMessages)
        {
            Console.WriteLine($"Base directory: {baseDir.FullName}");
            Console.WriteLine($"Scanning: {versionsDir}");
        }

        if (!Directory.Exists(versionsDir))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The 'versions' directory does not exist.");
            Console.ResetColor();
            return;
        }

        string[] versionDirectories = Directory.GetDirectories(versionsDir);
        if (versionDirectories.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("No version directories found.");
            Console.ResetColor();
            return;
        }

        if (showDebugMessages)
        {
            Console.WriteLine($"Found {versionDirectories.Length} version directories.\n");
        }

        foreach (string versionDir in versionDirectories)
        {
            string sourceCodeDir = Path.Combine(versionDir, "source-code");
            if (!Directory.Exists(sourceCodeDir))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"No 'source-code' directory in {versionDir}.");
                Console.ResetColor();
                continue;
            }

            // List of build artifact folders to clean
            string[] artifactDirs = { "bin", "obj" };
            foreach (var artifact in artifactDirs)
            {
                string artifactPath = Path.Combine(sourceCodeDir, artifact);
                if (Directory.Exists(artifactPath))
                {
                    try
                    {
                        Directory.Delete(artifactPath, true);

                        if (showDebugMessages)
                        {
                            Console.WriteLine($"Deleted '{artifact}' in {sourceCodeDir}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error deleting '{artifact}' in {sourceCodeDir}: {ex.Message}");
                        Console.ResetColor();
                    }
                }
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nCleanup completed successfully.");
        Console.ResetColor();
    }
}
