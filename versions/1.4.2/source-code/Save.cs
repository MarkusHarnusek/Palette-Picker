using PalettePicker.Resources.OptionsWindowResources;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace PalettePicker
{
    internal class Save
    {
        private static string filePath = string.Empty;

        private static void SetLanguage(int languageID)
        {
            string[] cultures = { "en-UK", "de-DE", "es-ES", "fr-FR", "zh-CN", "pt-PT", "ru-RU" };

            if (languageID < 0 || languageID >= cultures.Length)
            {
                languageID = 0;
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultures[languageID]);
        }

        public Save()
        {
            SetLanguage(MainWindow.currentLanguage);
        }

        public static void SelectFile()
        {
            string title = Resources.SaveClassResources.SaveClass.FileErrMsg;

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = title,
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
                System.Windows.MessageBox.Show(Resources.SaveClassResources.SaveClass.SelectErrMsg,
                    Resources.SaveClassResources.SaveClass.SelectErrTitle,
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
                return;
            }

            if (!ValidatePallleteJsonFile(filePath, out string errMsg))
            {
                System.Windows.MessageBox.Show(Resources.SaveClassResources.SaveClass.FileErrMsg,
                    Resources.SaveClassResources.SaveClass.FileErrTitle,
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
                return;
            }

            MainWindow.editingFilePath = filePath;
        }

        public static string GetSavePath()
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = Resources.SaveClassResources.SaveClass.SaveDialog,
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                DefaultExt = ".json",
                AddExtension = true,
                OverwritePrompt = true
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                System.Windows.MessageBox.Show(
                    Resources.SaveClassResources.SaveClass.FolderSelectErrMsg,
                    Resources.SaveClassResources.SaveClass.FolderSelectErrTitle,
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
                return string.Empty;
            }
        }

        public static string SaveFile(string primary1, string primary2, string seconadary1, string secondary2, string text, bool readOnly, bool homeVisible, bool pinned)
        {
            string path = GetSavePath();

            if (!string.IsNullOrEmpty(path) && Directory.Exists(Path.GetDirectoryName(path)))
            {
                readOnly = false;
                homeVisible = true;
                pinned = true;

                Pallete palette = new Pallete
                {
                    valid = true,
                    filePath = Path.GetDirectoryName(path),
                    paletteName = Path.GetFileNameWithoutExtension(path),
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
                File.WriteAllText(path, jsonString);

                return Path.GetFileNameWithoutExtension(path);
            }
            else
            {
                System.Windows.MessageBox.Show(Resources.SaveClassResources.SaveClass.SaveErrMsg,
                    Resources.SaveClassResources.SaveClass.SaveErrTitle,
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
                return string.Empty;
            }
        }

        public static (string name, string primary1, string primary2, string secondary1, string secondary2, string text, bool readOnly, bool homeVisible, bool pinned) GetSaveInfo(string path)
        {
            if (!ValidatePallleteJsonFile(path, out string errMsg))
            {
                System.Windows.MessageBox.Show(Resources.SaveClassResources.SaveClass.FileReadErrMsg + errMsg,
                    Resources.SaveClassResources.SaveClass.FileReadErrTitle,
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
                return (string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, false, false);
            }

            Dictionary<string, string>? jsonData = GetJsonValues(path);
            if (jsonData == null)
            {
                System.Windows.MessageBox.Show(Resources.SaveClassResources.SaveClass.FileReadErrMsg + errMsg, 
                    Resources.SaveClassResources.SaveClass.FileReadErrTitle,
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
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
                System.Windows.MessageBox.Show(Resources.SaveClassResources.SaveClass.FileReadErrMsg + errMsg,
                    Resources.SaveClassResources.SaveClass.FileReadErrTitle,
                    System.Windows.MessageBoxButton.OK, 
                    System.Windows.MessageBoxImage.Warning);
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
                errorMessage = Resources.SaveClassResources.SaveClass.FileNotExistErr;
                return false;
            }

            string fileExtension = Path.GetExtension(jsonFile);
            if (fileExtension != ".json")
            {
                errorMessage = Resources.SaveClassResources.SaveClass.FileNotJSONErr;
                return false;
            }

            try
            {
                string? content = File.ReadAllText(jsonFile);

                if (string.IsNullOrWhiteSpace(content))
                {
                    errorMessage = Resources.SaveClassResources.SaveClass.FileEmptyErr;
                    return false;
                }

                var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

                if (jsonData == null)
                {
                    errorMessage = Resources.SaveClassResources.SaveClass.JSONNullErr;
                    return false;
                }

                if (jsonData.Count != 11)
                {
                    errorMessage = Resources.SaveClassResources.SaveClass.WrongPairCountErr;
                    return false;
                }

                string[] requiredKeys = { "valid", "filePath", "paletteName", "primary1", "primary2", "secondary1", "secondary2", "text", "readOnly", "homeVisible", "pinned" };
                foreach (string key in requiredKeys)
                {
                    if (!jsonData.ContainsKey(key))
                    {
                        errorMessage = Resources.SaveClassResources.SaveClass.MissingKeyErr + key;
                        return false;
                    }
                }

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = Resources.SaveClassResources.SaveClass.ReadingErr + ex.Message;
                return false;
            }
        }
    }
}