using System;
using System.IO;
using System.Windows.Forms;

namespace VernamCipher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выберете файл для шифрования", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string fileName = openFileDialog1.FileName;
                if (Path.GetExtension(fileName) == ".txt")
                {
                    string key;
                    string encryptedText=CipherRe.Encrypt(fileName, out key);

                    //сохраним результат
                    MessageBox.Show("Шифрование выполнено. Сохраните файл с зашифрованными данными...", "Save",MessageBoxButtons.OK ,MessageBoxIcon.Asterisk);
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text files(*.txt)|*.txt";
                    DialogResult dialogResult = saveFileDialog.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        string EnT_fileName = saveFileDialog.FileName;
                        File.WriteAllText(EnT_fileName, encryptedText);
                    }
                    else { return; }
                    MessageBox.Show("Сохраните сохраните ключ...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    dialogResult = saveFileDialog.ShowDialog();
                    if (dialogResult==DialogResult.OK)
                    {
                        string key_fileName = saveFileDialog.FileName;
                        File.WriteAllText(key_fileName, key);
                    }
                    else { return; }
                    MessageBox.Show("Данные успешно зашифрованы", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dataFileName;
            string keyFileName;

            MessageBox.Show("Выберете файл для дешифрования", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                dataFileName = openFileDialog1.FileName;
            } else { return; }

            MessageBox.Show("Выберете файл ключа", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                keyFileName = openFileDialog1.FileName;
            } else { return; }

            //дешифрование
            string decrypted = CipherRe.Decrypt(dataFileName, keyFileName);

            MessageBox.Show("Дешифрование выполнено. Сохраните файл с расшифрованными данными", "Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files(*.txt)|*.txt";
            DialogResult result1 = saveFileDialog.ShowDialog();
            if (result1 == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;
                File.WriteAllText(filename, decrypted);
            } else { return; }
            MessageBox.Show("Данные успешно расшифрованы", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
