using System.IO;
using System.Text.Json;

namespace PalettePicker
{
    internal class Save
    {
        private static string filePath = string.Empty;

        public static void SelectFile()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Select the color palette",
                CheckFileExists = true,
                Multiselect = false,
                CheckPathExists = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
            }

            if (string.IsNullOrEmpty(filePath))
            {
                System.Windows.MessageBox.Show("No file selected.", "Select Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            if (!ValidatePallleteJsonFile(filePath, out string errMsg))
            {
                System.Windows.MessageBox.Show($"The selected file is not a valid palette JSON file. {errMsg}", "Select Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            MainWindow.editingFilePath = filePath;
        }

        public static string GetSavePath()
        {
            Microsoft.Win32.OpenFolderDialog dialog = new();
            dialog.Title = "Select the folder to save the palette";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Multiselect = false;

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                return dialog.FolderName;
            }
            else
            {
                System.Windows.MessageBox.Show("No folder selected.", "Select Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return string.Empty;
            }
        }

        public static void SaveFile(string paletteName, string primary1, string primary2, string seconadary1, string secondary2, string text, bool readOnly, bool homeVisible, bool pinned)
        {
            // Fixed values > Implemt logic with later update

            string path = GetSavePath();

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                readOnly = false;
                homeVisible = true;
                pinned = true;

                Pallete palette = new Pallete
                {
                    valid = true,
                    filePath = path,
                    paletteName = paletteName,
                    primary1 = primary1,
                    primary2 = primary2,
                    secondary1 = seconadary1,
                    secondary2 = secondary2,
                    text = text,
                    readOnly = readOnly,
                    homeVisible = homeVisible,
                    pinned = pinned
                };

                string jsonString = JsonSerializer.Serialize(palette);
                File.WriteAllText(Path.Combine(palette.filePath, $"{palette.paletteName}.json"), jsonString);
            }
            else
            {
                System.Windows.MessageBox.Show("Error occured while trying to save file. Path is either empty or does not exist.", "Save Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            }
        }

        public static (string name, string primary1, string primary2, string secondary1, string secondary2, string text, bool readOnly, bool homeVisible, bool pinned) GetSaveInfo(string path)
        {
            if (!ValidatePallleteJsonFile(path, out string errMsg))
            {
                System.Windows.MessageBox.Show($"Error occured while extracting json save file with following error message: {errMsg}", "File Read Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return (string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, false, false);
            }

            Dictionary<string, string>? jsonData = GetJsonValues(path);
            if (jsonData == null)
            {
                System.Windows.MessageBox.Show("Error occured while extracting json save file.", "File Read Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return (string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, false, false);
            }

            string name = jsonData["paletteName"];
            string primary1 = jsonData["primary1"];
            string primary2 = jsonData["primary2"];
            string secondary1 = jsonData["secondary1"];
            string secondary2 = jsonData["secondary2"];
            string text = jsonData["text"];
            bool readOnly = jsonData["readOnly"] == "true";
            bool homeVisible = jsonData["homeVisible"] == "true";
            bool pinned = jsonData["pinned"] == "true";

            return (name, primary1, primary2, secondary1, secondary2, text, readOnly, homeVisible, pinned);
        }

        private static Dictionary<string, string>? GetJsonValues(string path)
        {
            if (!ValidatePallleteJsonFile(path, out string errMsg))
            {
                System.Windows.MessageBox.Show($"Error occured while extracting json save file with following error message: {errMsg}", "File Read Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return null;
            }

            string jsonContent = File.ReadAllText(path);
            Dictionary<string, string> jsonData = new Dictionary<string, string>();

            string[] lines = jsonContent.Split(new[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] keyValue = line.Split(':');
                if (keyValue.Length == 2)
                {
                    string key = keyValue[0].Trim().Trim('"');
                    string value = keyValue[1].Trim().Trim('"');
                    jsonData.Add(key, value);
                }
            }

            return jsonData;
        }

        private static bool ValidatePallleteJsonFile(string jsonFile, out string errorMessage)
        {
            if (!File.Exists(jsonFile))
            {
                errorMessage = "File does not exist.";
                return false;
            }

            string fileExtension = Path.GetExtension(jsonFile);
            if (fileExtension != ".json")
            {
                errorMessage = "File is not a JSON file.";
                return false;
            }

            try
            {
                string content = File.ReadAllText(jsonFile);

                if (string.IsNullOrWhiteSpace(content))
                {
                    errorMessage = "File is empty.";
                    return false;
                }

                var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(content) ?? null;

                if (jsonData == null)
                {
                    errorMessage = "JSON data was null.";
                }

                if (jsonData.Count != 11)
                {
                    errorMessage = "File does not contain the correct number of value pairs.";
                    return false;
                }

                string[] requiredKeys = { "valid", "filePath", "paletteName", "primary1", "primary2", "secondary1", "secondary2", "text", "readOnly", "homeVisible", "pinned" };
                foreach (string key in requiredKeys)
                {
                    if (!jsonData.ContainsKey(key))
                    {
                        errorMessage = $"Missing required key: {key}";
                        return false;
                    }
                }

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Error reading file: {ex.Message}";
                return false;
            }
        }
    }
}
