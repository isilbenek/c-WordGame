using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp8ssssssssssss
{
    public partial class Form5 : Form
    {
        private Random random;
        private excel.Application excelApp;
        private excel.Workbook workbook;
        private excel.Worksheet worksheet;
        private List<string> words;
        private List<string> antonyms;
        private int currentQuestionIndex;
        private int score = 0;
        public Form5()
        {
            InitializeComponent();
            InitializeExcel();
            LoadWordData();
            ShuffleWords();
            ShowNextQuestion();
        }
        int saniye = 0;
        private void InitializeExcel()
        {
            excelApp = new excel.Application();
            workbook = excelApp.Workbooks.Open("C:\\Users\\ışıl\\source\\repos\\WindowsFormsApp8ssssssssssss\\WindowsFormsApp8ssssssssssss\\Properties\\ZIT ANLAMLI.xlsx");
            worksheet = workbook.Sheets[1];
        }
        private void LoadWordData()
        {
            // Excel dosyasındaki kelimeler ve zıt anlamlılarını yükle
            int rowCount = worksheet.UsedRange.Rows.Count;

            words = new List<string>();
            antonyms = new List<string>();

            for (int row = 2; row <= rowCount; row++)
            {
                string word = worksheet.Cells[row, 1].Value2.ToString();
                string antonym = worksheet.Cells[row, 2].Value2.ToString();

                words.Add(word);
                antonyms.Add(antonym);
            }
        }
        private void ShuffleWords()//kelimeleri karıştır.
        {
            random = new Random();
            int n = words.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Swap(words, k, n);
                Swap(antonyms, k, n);
            }
        }

        private void Swap<T>(List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        private void ShowNextQuestion()
        {


            if (currentQuestionIndex < words.Count)
            {

                string currentWord = words[currentQuestionIndex];


                lblQuestion.Text = $"Kelimenin zıt anlamını tahmin edin";
                label2.Text = currentWord;
                currentQuestionIndex++;
            }
            else
            {
                MessageBox.Show("Tüm sorular tamamlandı!");
                CloseExcel();
                Close();
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            progressBar1.Maximum = 59;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex <= words.Count)

            {
                string userAnswer = richTextBox1.Text.Trim().ToLower();
                string correctAnswer = antonyms[currentQuestionIndex - 1].ToLower();

                if (userAnswer == correctAnswer)
                {
                    MessageBox.Show("Doğru!");



                    richTextBox1.Clear();
                    ShowNextQuestion();


                    // Skoru artır
                    score += 10;
                    skorlabel1.Text = "Skor: " + score; // skoru göster

                }
                else
                {

                    MessageBox.Show($"Yanlış! lütfen tekrar deneyin ");
                    //eğer puanı 0 ise daha fazla azalıp eksiye düşmeyecek:
                    if (score == 0)
                    {
                        skorlabel1.Text = "Skor: " + score; // skoru göster

                    }
                    else
                    {
                        score -= 5;
                        skorlabel1.Text = "Skor: " + score; // skoru göster
                    }



                }


            }
        }
        private void CloseExcel()
        {
            workbook.Close(false);
            excelApp.Quit();

            ReleaseObject(worksheet);
            ReleaseObject(workbook);
            ReleaseObject(excelApp);
        }
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("ReleaseObject Hatası: " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void btnCheckAnswer_MouseDown(object sender, MouseEventArgs e)
        {

           
            btnCheckAnswer.BackColor = Color.White;

        }
        private void btnCheckAnswer_MouseUp(object sender, MouseEventArgs e)
        {
            btnCheckAnswer.BackColor = Color.LightBlue;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //klavye
            string harf1 = richTextBox1.Text;
            harf1 = "A";
            richTextBox1.Text = harf1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
          string harf2 = richTextBox1.Text;
            richTextBox1.Text = harf2;
        }
        private void btn_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Button b = (System.Windows.Forms.Button)sender;
            richTextBox1.Text = richTextBox1.Text + b.Text;
            timer1.Enabled = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.Length - 1);
            }
            timer1.Enabled = false;
        }

        private void button30_Click(object sender, EventArgs e)
        {

            timer1.Stop(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            saniye++;
            progressBar1.Value = saniye;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            saniye = 0;
            timer1.Start();
        }
    }
}
