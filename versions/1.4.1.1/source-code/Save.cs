using System.IO;
using System.Text.Json;

namespace PalettePicker
{
    internal class Save
    {
        private static string filePath = string.Empty;

        #region Translation

        private static (string msg, string title) GetTranslatedInvalidErrMsg(int msgId, int languageId)
        {
            switch (msgId)
            {
                case 0:
                    switch (languageId)
                    {
                        case 0: return ("No file selected.", "Select Error");
                        case 1: return ("Keine Datei ausgewählt.", "Fehler beim Auswählen");
                        case 2: return ("Ningún archivo seleccionado.", "Seleccionar error");
                        case 3: return ("Aucun fichier sélectionné.", "Erreur de sélection");
                        case 4: return ("未选择文件。", "选择错误");
                        case 5: return ("Nenhum arquivo selecionado.", "Erro de seleção");
                        case 6: return ("Не выбрана ни одна файл.", "Ошибка выбора");
                    }

                    break;

                case 1:
                    switch (languageId)
                    {
                        case 0: return ("The selected file is not a valid palette JSON file.", "File Error");
                        case 1: return ("Die ausgewählte Datei ist keine gültige Palette-JSON-Datei.", "Dateifehler");
                        case 2: return ("El archivo seleccionado no es un archivo JSON de paleta válido.", "Error de archivo");
                        case 3: return ("Le fichier sélectionné n'est pas un fichier JSON de palette valide.", "Erreur de fichier");
                        case 4: return ("所选文件不是有效的调色板 JSON 文件。", "文件错误");
                        case 5: return ("O arquivo selecionado não é um arquivo JSON de paleta válido.", "Erro de arquivo");
                        case 6: return ("Выбранный файл не является допустимым файлом JSON палитры.", "Ошибка файла");
                    }

                    break;

                case 2:
                    switch (languageId)
                    {
                        case 0: return ("No folder select.", "Select Error");
                        case 1: return ("Kein Ordner ausgewählt.", "Fehler bei der Auswahl");
                        case 2: return ("Ninguna carpeta seleccionada.", "Error de selección");
                        case 3: return ("Aucun dossier sélectionné.", "Erreur de sélection");
                        case 4: return ("未选择文件夹。", "选择错误");
                        case 5: return ("Nenhuma pasta selecionada.", "Erro de seleção");
                        case 6: return ("Не выбрана ни одна папка.", "Ошибка выбора");
                    }

                    break;

                case 3:
                    switch (languageId)
                    {
                        case 0: return ("Error occured while trying to save file. Path is either empty or does not exist.", "Save Error");
                        case 1: return ("Fehler beim Speichern der Datei. Der Pfad ist entweder leer oder existiert nicht.", "Speicher Fehler");
                        case 2: return ("Se produjo un error al intentar guardar el archivo. La ruta está vacía o no existe.", "Error de guardado");
                        case 3: return ("Une erreur s'est produite lors de la tentative d'enregistrement du fichier. Le chemin est vide ou n'existe pas.", "Erreur d'enregistrement");
                        case 4: return ("保存文件时发生错误。路径为空或不存在。", "保存错误");
                        case 5: return ("Ocorreu um erro ao tentar salvar o arquivo. O caminho está vazio ou não existe.", "Erro de salvamento");
                        case 6: return ("Произошла ошибка при попытке сохранить файл. Путь либо пуст, либо не существует.", "Ошибка сохранения");
                    }

                    break;

                case 4:
                    switch (languageId)
                    {
                        case 0: return ("Error occured while extracting json save file with following error message:", "File Read Error");
                        case 1: return ("Fehler beim Extrahieren der JSON-Speicherdatei mit folgender Fehlermeldung:", "Dateilesefehler");
                        case 2: return ("Se produjo un error al extraer el archivo de guardado JSON con el siguiente mensaje de error:", "Error de lectura de archivo");
                        case 3: return ("Une erreur s'est produite lors de l'extraction du fichier de sauvegarde JSON avec le message d'erreur suivant:", "Erreur de lecture de fichier");
                        case 4: return ("提取 JSON 保存文件时发生错误，错误消息如下：", "文件读取错误");
                        case 5: return ("Ocorreu um erro ao extrair o arquivo de salvamento JSON com a seguinte mensagem de erro:", "Erro de leitura de arquivo");
                        case 6: return ("Произошла ошибка при извлечении файла сохранения JSON со следующим сообщением об ошибке:", "Ошибка чтения файла");
                    }

                    break;

                case 5:
                    switch (languageId)
                    {
                        case 0: return ("Error occured while extracting json save file.", "File Read Error");
                        case 1: return ("Fehler beim Extrahieren der JSON-Speicherdatei.", "Dateilesefehler");
                        case 2: return ("Se produjo un error al extraer el archivo de guardado JSON.", "Error de lectura de archivo");
                        case 3: return ("Une erreur s'est produite lors de l'extraction du fichier de sauvegarde JSON.", "Erreur de lecture de fichier");
                        case 4: return ("提取 JSON 保存文件时发生错误。", "文件读取错误");
                        case 5: return ("Ocorreu um erro ao extrair o arquivo de salvamento JSON.", "Erro de leitura de arquivo");
                        case 6: return ("Произошла ошибка при извлечении файла сохранения JSON.", "Ошибка чтения файла");
                    }

                    break;
            }

            return (string.Empty, string.Empty);
        }

        private static string GetShortTranslatedErrMsg(int errorId, int languageId)
        {
            switch (errorId)
            {
                case 0:
                    switch (languageId)
                    {
                        case 0: return "File does not exist.";
                        case 1: return "Datei existiert nicht.";
                        case 2: return "El archivo no existe.";
                        case 3: return "Le fichier n'existe pas.";
                        case 4: return "文件不存在。";
                        case 5: return "O arquivo não existe.";
                        case 6: return "Файл не существует.";
                    }

                    break;

                case 1:
                    switch (languageId)
                    {
                        case 0: return "File is not a JSON file.";
                        case 1: return "Datei ist keine JSON-Datei.";
                        case 2: return "El archivo no es un archivo JSON.";
                        case 3: return "Le fichier n'est pas un fichier JSON.";
                        case 4: return "文件不是 JSON 文件。";
                        case 5: return "O arquivo não é um arquivo JSON.";
                        case 6: return "Файл не является файлом JSON.";
                    }

                    break;

                case 2:
                    switch (languageId)
                    {
                        case 0: return "File is empty.";
                        case 1: return "Datei ist leer.";
                        case 2: return "El archivo está vacío.";
                        case 3: return "Le fichier est vide.";
                        case 4: return "文件是空的。";
                        case 5: return "O arquivo está vazio.";
                        case 6: return "Файл пуст.";
                    }

                    break;

                case 3:
                    switch (languageId)
                    {
                        case 0: return "JSON data was null.";
                        case 1: return "JSON-Daten waren null.";
                        case 2: return "Los datos JSON eran nulos.";
                        case 3: return "Les données JSON étaient nulles.";
                        case 4: return "JSON 数据为 null。";
                        case 5: return "Os dados JSON eram nulos.";
                        case 6: return "Данные JSON были нулевыми.";
                    }

                    break;

                case 4:
                    switch (languageId)
                    {
                        case 0: return "File does not contain the correct number of value pairs.";
                        case 1: return "Datei enthält nicht die richtige Anzahl von Wertpaaren.";
                        case 2: return "El archivo no contiene el número correcto de pares de valores.";
                        case 3: return "Le fichier ne contient pas le bon nombre de paires de valeurs.";
                        case 4: return "文件不包含正确数量的值对。";
                        case 5: return "O arquivo não contém o número correto de pares de valores.";
                        case 6: return "Файл не содержит правильного количества пар значений.";
                    }

                    break;

                case 5:
                    switch (languageId)
                    {
                        case 0: return "Missing required key";
                        case 1: return "Fehlender erforderlicher Schlüssel";
                        case 2: return "Falta la clave requerida";
                        case 3: return "Clé requise manquante";
                        case 4: return "缺少必需的密钥";
                        case 5: return "Falta a chave obrigatória";
                        case 6: return "Отсутствует обязательный ключ";
                    }

                    break;

                case 6:
                    switch (languageId)
                    {
                        case 0: return "Error reading file";
                        case 1: return "Fehler beim Lesen der Datei";
                        case 2: return "Error al leer el archivo";
                        case 3: return "Erreur de lecture du fichier";
                        case 4: return "读取文件时出错";
                        case 5: return "Erro ao ler o arquivo";
                        case 6: return "Ошибка чтения файла";
                    }

                    break;
            }

            return string.Empty;
        }

        #endregion

        public static void SelectFile()
        {
            string title = string.Empty;

            switch (MainWindow.currentLanguage)
            {
                case 0: title = "Select the color palette"; break;
                case 1: title = "Wählen Sie die Farbpalette aus"; break;
                case 2: title = "Seleccione la paleta de colores"; break;
                case 3: title = "Sélectionnez la palette de couleurs"; break;
                case 4: title = "选择调色板"; break;
                case 5: title = "Selecione a paleta de cores"; break;
                case 6: title = "Выберите цветовую палитру"; break;
            }

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
                System.Windows.MessageBox.Show(GetTranslatedInvalidErrMsg(0, MainWindow.currentLanguage).msg, GetTranslatedInvalidErrMsg(0, MainWindow.currentLanguage).title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            if (!ValidatePallleteJsonFile(filePath, out string errMsg))
            {
                System.Windows.MessageBox.Show($"{GetTranslatedInvalidErrMsg(1, MainWindow.currentLanguage).msg} {errMsg}", GetTranslatedInvalidErrMsg(1, MainWindow.currentLanguage).title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            MainWindow.editingFilePath = filePath;
        }

        public static string GetSavePath()
        {
            string title = "Select the folder to save the palette";

            switch (MainWindow.currentLanguage)
            {
                case 0: title = "Select where to save the palette"; break;

                case 1: title = "Wählen Sie aus, wo Sie die Palette speichern möchten"; break;

                case 2: title = "Seleziona dove salvare la palette"; break;

                case 3: title = "Sélectionnez où enregistrer la palette  "; break;

                case 4: title = "选择保存调色板的位置"; break;

                case 5: title = "Selecione onde salvar a paleta"; break;

                case 6: title = "Выберите место для сохранения палитры"; break;

            }

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = title,
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
                    GetTranslatedInvalidErrMsg(2, MainWindow.currentLanguage).msg,
                    GetTranslatedInvalidErrMsg(2, MainWindow.currentLanguage).title,
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
                System.Windows.MessageBox.Show(GetTranslatedInvalidErrMsg(3, MainWindow.currentLanguage).msg, GetTranslatedInvalidErrMsg(3, MainWindow.currentLanguage).title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return string.Empty;
            }
        }

        public static (string name, string primary1, string primary2, string secondary1, string secondary2, string text, bool readOnly, bool homeVisible, bool pinned) GetSaveInfo(string path)
        {
            if (!ValidatePallleteJsonFile(path, out string errMsg))
            {
                System.Windows.MessageBox.Show($"{GetTranslatedInvalidErrMsg(4, MainWindow.currentLanguage).msg} {errMsg}", GetTranslatedInvalidErrMsg(4, MainWindow.currentLanguage).title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return (string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, false, false);
            }

            Dictionary<string, string>? jsonData = GetJsonValues(path);
            if (jsonData == null)
            {
                System.Windows.MessageBox.Show(GetTranslatedInvalidErrMsg(5, MainWindow.currentLanguage).msg, GetTranslatedInvalidErrMsg(5, MainWindow.currentLanguage).title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
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
                System.Windows.MessageBox.Show($"{GetTranslatedInvalidErrMsg(4, MainWindow.currentLanguage).msg} {errMsg}", GetTranslatedInvalidErrMsg(4, MainWindow.currentLanguage).title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
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
                errorMessage = GetShortTranslatedErrMsg(0, MainWindow.currentLanguage);
                return false;
            }

            string fileExtension = Path.GetExtension(jsonFile);
            if (fileExtension != ".json")
            {
                errorMessage = GetShortTranslatedErrMsg(1, MainWindow.currentLanguage);
                return false;
            }

            try
            {
                string? content = File.ReadAllText(jsonFile);

                if (string.IsNullOrWhiteSpace(content))
                {
                    errorMessage = GetShortTranslatedErrMsg(2, MainWindow.currentLanguage);
                    return false;
                }

                var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

                if (jsonData == null)
                {
                    errorMessage = GetShortTranslatedErrMsg(3, MainWindow.currentLanguage);
                    return false;
                }

                if (jsonData.Count != 11)
                {
                    errorMessage = GetShortTranslatedErrMsg(4, MainWindow.currentLanguage);
                    return false;
                }

                string[] requiredKeys = { "valid", "filePath", "paletteName", "primary1", "primary2", "secondary1", "secondary2", "text", "readOnly", "homeVisible", "pinned" };
                foreach (string key in requiredKeys)
                {
                    if (!jsonData.ContainsKey(key))
                    {
                        errorMessage = $"{GetShortTranslatedErrMsg(5, MainWindow.currentLanguage)}: {key}";
                        return false;
                    }
                }

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"{GetShortTranslatedErrMsg(6, MainWindow.currentLanguage)}: {ex.Message}";
                return false;
            }
        }
    }
}
