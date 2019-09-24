using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Files
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> list = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Текстовые файлы|*.txt";
            fd.Title = "Выберите текстовый файл";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Stopwatch t = new Stopwatch();
                t.Start();
                string text = File.ReadAllText(fd.FileName);
                char[] sep = { ' ', '.', ',', '!', '?', '/', '\t', '\n' };
                string[] textlist = text.Split(sep);
                foreach (string s in textlist)
                {
                    string str = s.Trim();
                    if (!list.Contains(str))  list.Add(str);
                }
                t.Stop();
                textBox1.Text = t.Elapsed.ToString();
            }
            else
            {
                MessageBox.Show("Файл не выбран");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string word = textBox2.Text.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)
            {
                List<string> search = new List<string>();
                Stopwatch t = new Stopwatch();
                t.Start();
                foreach (string s in list)
                {
                    if (s.ToLower().Contains(word)) search.Add(s);
                }
                t.Stop();
                textBox3.Text = t.Elapsed.ToString();

                listBox1.BeginUpdate();
                listBox1.Items.Clear();
                foreach (string str in search)
                {
                    listBox1.Items.Add(str);
                }
                listBox1.EndUpdate();
            }
            else
            {
                MessageBox.Show("Слово для поиска не введено");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

