using System;
using System.IO;
using System.Text;

namespace VernamCipher
{
    static internal class CipherRe
    {
        private static string StringToBits(string text)
        {
            //create arrays of bytes
            byte[] textbyte = Encoding.ASCII.GetBytes(text);
            string textbitSt = "";

            //create string of bits
            for (int i = 0; i < textbyte.Length; i++)
            {
                string ch = Convert.ToString(textbyte[i],2);

                //insert "0" while char representation length < 8 symbols
                while (ch.Length < 8)
                {
                    ch = ch.Insert(0, "0");
                }
                textbitSt = textbitSt.Insert(textbitSt.Length, ch);
            }

            return textbitSt;
        }

        public static string Encrypt(string FileName, out string key)
        {
            string text = File.ReadAllText(FileName);

            //генерируем ключ нужной длинны
            key = GenerateKey(text.Length);

            //преобразование текста и ключа в двоичную форму представления
            text = StringToBits(text);
            string keybits = StringToBits(key);

            string result="";
            for (int i = 0; i < text.Length; i++)
            {
                bool temp = ToBoolWord(text.Substring(i, 1)) ^ ToBoolWord(keybits.Substring(i, 1));
                result = result.Insert(result.Length,ToBoolNum(temp));
            }
            return result;
        }

        public static string Decrypt(string datePath,string keyPath)
        {
            string encryptedText = File.ReadAllText(datePath);
            string key = File.ReadAllText(keyPath);

            //преобразование ключа в двоичную систему
            key = StringToBits(key);

            string result = "";
            for (int i = 0; i < encryptedText.Length; i++)
            {
                bool temp = ToBoolWord(encryptedText.Substring(i, 1)) ^ ToBoolWord(key.Substring(i, 1));
                result = result.Insert(result.Length, ToBoolNum(temp));
            }
            //объявляем byte[]
            byte[] bytes = new byte[result.Length / 8];
            int count = 0;
            for (int i = 0; i < result.Length; i+=8)
            {
                //считываем 8 бит в строку
                string temp = result.Substring(i, 8);
                //переделываем строку в byte[]
                bytes[count] = Convert.ToByte(temp, 2);
                count++;
            }
            //переводим в символ
            result=Encoding.ASCII.GetString(bytes);
            return result;
        }

        private static string GenerateKey(int size)
        {
            Random random = new Random();
            string result = "";
            byte[] temp=new byte[size];
            for (int i = 0; i < size; i++)
            {
                temp[i]=(byte)random.Next(65, 91);
            }
            result = Encoding.ASCII.GetString(temp);
;
            return result;
        }

        private static bool ToBoolWord(string s)
        {
            if (s == "0") return false;
            else if (s == "1") return true;
            else throw new Exception("error ToBoolWord func");
        }
        private static string ToBoolNum(bool b)
        {
            if (b == false) return "0";
            else if (b == true) return "1";
            else throw new Exception("error ToBoolNum func");
        }
    }
}
