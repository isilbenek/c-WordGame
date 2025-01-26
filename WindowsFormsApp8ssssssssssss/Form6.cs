using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp8ssssssssssss
{
    public partial class Form6 : Form
    {
        private Random random;
        private excel.Application excelApp;
        private excel.Workbook workbook;
        private excel.Worksheet worksheet;
        private List<string> words;
        private List<string> synonyms;
        private int currentQuestionIndex;
        private int score = 0;
       
        public Form6()

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
            workbook = excelApp.Workbooks.Open("C:\\Users\\ışıl\\source\\repos\\WindowsFormsApp8ssssssssssss\\WindowsFormsApp8ssssssssssss\\Properties\\eş anlamlı.xlsx");
            worksheet = workbook.Sheets[1];
        }
        private void LoadWordData()
        {
            // Excel dosyasındaki kelimeler ve eş anlamlılarını yükle
            int rowCount = worksheet.UsedRange.Rows.Count;

            words = new List<string>();
            synonyms = new List<string>();

            for (int row = 2; row <= rowCount; row++)
            {
                string word = worksheet.Cells[row, 1].Value2.ToString();
                string synonym = worksheet.Cells[row, 2].Value2.ToString();

                words.Add(word);
                synonyms.Add(synonym);
            }

        }
        private void ShuffleWords()//keimeleri karıştır.
        {
            random = new Random();
            int n = words.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Swap(words, k, n);
                Swap(synonyms, k, n);
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

                lblQuestion.Text = $"Kelimenin eş anlamını tahmin edin";
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


        private void Form6_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            progressBar1.Maximum = 59;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex <= words.Count)
            {
                string userAnswer = txtAnswer.Text.Trim().ToLower();
                string correctAnswer = synonyms[currentQuestionIndex - 1].ToLower();

                if (userAnswer == correctAnswer)
                {
                    MessageBox.Show("Doğru!");
                    txtAnswer.Clear();
                    ShowNextQuestion();

                    // Skoru artır
                    score += 10;
                    skorlabel1.Text = "Skor: " + score; //  skoru göster
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
        private void button1_Click_1(object sender, EventArgs e)
        {
            //kalvye
            string harf1 = txtAnswer.Text;
            harf1 = "A";
            txtAnswer.Text = harf1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string harf2 = txtAnswer.Text;
            txtAnswer.Text = harf2;
        }
        private void btn_click1(object sender, EventArgs e)
        {

            System.Windows.Forms.Button b = (System.Windows.Forms.Button)sender;
            txtAnswer.Text = txtAnswer.Text + b.Text;
            timer1.Enabled = true;
        }


        private void btn19_clik(object sender, EventArgs e)
        { //silme
            if (txtAnswer.Text.Length > 0)
            {
                txtAnswer.Text = txtAnswer.Text.Remove(txtAnswer.Text.Length - 1);
            }
            timer1.Enabled = false;
        }

        private void lblQuestion_Click(object sender, EventArgs e)
        {
            
        }
        private void btnCheckAnswer_MouseDown(object sender, MouseEventArgs e)
        {
            btnCheckAnswer.BackColor = Color.White;
        }

        private void btnCheckAnswer_MouseUp(object sender, MouseEventArgs e)
        {
            btnCheckAnswer.BackColor = Color.LightBlue;
        }

        

        private void btn_click(object sender, EventArgs e)
        {
           
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            saniye++;
            progressBar1.Value = saniye;

        }

        private void button30_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button29_Click_1(object sender, EventArgs e)
        {
            saniye = 0;
            timer1.Start();
        }

        private void lblQuestion_Click_1(object sender, EventArgs e)
        {

        }
    }
   
}