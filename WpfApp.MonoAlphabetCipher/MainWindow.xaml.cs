using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace WpfApp.MonoAlphabetCipher
{
  public partial class MainWindow : Window
  {
    const string RussianAlphabetDefault = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    const string RussianAlphabetCoded   = "ЧШЖЦЁЮЛХБВФАЙДКЯИЭЕРСГУЗТЫЩМОЬПЪН";
    const string EnglishAlphabetDefault = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string EnglishAlphabetCoded   = "BSXZAYKIVGONQMDPLECHRFWJTU";
    List<string> InputText = new List<string>();
    List<string> OutputText = new List<string>();
    public MainWindow()
    {
      InitializeComponent();
    }
    // Browse button click event to open file
    private void buttonBrowse_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog()
      {
        CheckFileExists = false,
        CheckPathExists = true,
        Multiselect = false,
        Title = "Choose file",
        Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
      };

      if (dialog.ShowDialog() == true)
      {
        tbInput.Text = File.ReadAllText(dialog.FileName, Encoding.UTF8);
      }
    }
    // Encrypt button click event
    private void buttonEncrypt_Click(object sender, RoutedEventArgs e)
    {
      InputText.Clear();
      OutputText.Clear();
      for (int i = 0; i < tbInput.LineCount; i++)
      {
        InputText.Add(tbInput.GetLineText(i).ToUpper());
      }


      if (radioEnglish.IsChecked == true)
      {
        Encryption(EnglishAlphabetDefault, EnglishAlphabetCoded);
      }
      if (radioRussian.IsChecked == true)
      {
        Encryption(RussianAlphabetDefault, RussianAlphabetCoded);
      }
    }
    // Encrypt function
    private void Encryption(string alphabetDefault, string alphabetCoded)
    {
      string newLine = "";
      foreach (var line in InputText)
      {
        for (int i = 0; i < line.Length; i++)
        {
          if (alphabetDefault.Contains(line[i].ToString()))
          {
            newLine += alphabetCoded[alphabetDefault.IndexOf(line[i])];
          }
          else
          {
            newLine += line[i];
          }
        }
        OutputText.Add(newLine);
        newLine = "";
      }
      tbOutput.Clear();
      for (int i = 0; i < OutputText.Count; i++)
      {
        tbOutput.Text += OutputText[i];
      }
    }
    // Decrypt button click event
    private void buttonDecrypt_Click(object sender, RoutedEventArgs e)
    {
      InputText.Clear();
      OutputText.Clear();
      for (int i = 0; i < tbInput.LineCount; i++)
      {
        InputText.Add(tbInput.GetLineText(i).ToUpper());
      }


      if (radioEnglish.IsChecked == true)
      {
        Decryption(EnglishAlphabetDefault, EnglishAlphabetCoded);
      }
      if (radioRussian.IsChecked == true)
      {
        Decryption(RussianAlphabetDefault, RussianAlphabetCoded);
      }
    }
    // Decrypt function
    private void Decryption(string alphabetDefault, string alphabetCoded)
    {
      string newLine = "";
      foreach (var line in InputText)
      {
        for (int i = 0; i < line.Length; i++)
        {
          if (alphabetDefault.Contains(line[i].ToString()))
          {
            newLine += alphabetDefault[alphabetCoded.IndexOf(line[i])];
          }
          else
          {
            newLine += line[i];
          }
        }
        OutputText.Add(newLine);
        newLine = "";
      }
      tbOutput.Clear();
      for (int i = 0; i < OutputText.Count; i++)
      {
        tbOutput.Text += OutputText[i];
      }
    }
    // Browse button click event to save file
    private void buttonSave_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog dialog = new SaveFileDialog()
      {
        Title = "Save file",
        Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
      };
      if (dialog.ShowDialog() == true)
      {
        File.WriteAllText(dialog.FileName, tbOutput.Text, Encoding.UTF8);
      }
    }
  }
}
