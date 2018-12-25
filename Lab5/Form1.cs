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

namespace Lab5
{
    public partial class Form1 : Form
    {
        public class MinMax
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public MinMax(int pmin, int pmax)
            {
                this.Min = pmin;
                this.Max = pmax;
            }
        }

        public class ParallelSearchResult
        {
            /// <summary>
            /// Найденное слово
            /// </summary>
            public string word { get; set; }
            /// <summary>
            /// Расстояние
            /// </summary>
            public int dist { get; set; }
            /// <summary>
            /// Номер потока
            /// </summary>
            public int ThreadNum { get; set; }
        }

        class ParallelSearchThreadParam
        {
            /// <summary>
            /// Массив для поиска
            /// </summary>
            public List<string> tempList { get; set; }
            /// <summary>
            /// Слово для поиска
            /// </summary>
            public string wordPattern { get; set; }
            public int maxDist { get; set; }
            /// <summary>
            /// Номер потока
            /// </summary>
            public int ThreadNum { get; set; }
        }


        public static List<ParallelSearchResult> ArrayThreadTask(object paramObj)
        {
            ParallelSearchThreadParam param =
           (ParallelSearchThreadParam)paramObj;
            //Слово для поиска в верхнем регистре
            string wordUpper = param.wordPattern.Trim().ToUpper();
            //Результаты поиска в одном потоке
            List<ParallelSearchResult> Result = new
           List<ParallelSearchResult>();
            //Перебор всех слов во временном списке данного потока
            foreach (string str in param.tempList)
            {
                //Вычисление расстояния Дамерау-Левенштейна
                int dist = Damerau_Livenstein.Distance(str.ToUpper(), wordUpper);
                if (dist <= param.maxDist)
                {
                    ParallelSearchResult temp = new ParallelSearchResult()
                    {
                        word = str,
                        dist = dist,
                        ThreadNum = param.ThreadNum
                    };
                    Result.Add(temp);
                }
            }
            return Result;
        }


        public static class SubArrays
        {
            public static List<MinMax> DivideSubArrays(
               int beginIndex, int endIndex, int subArraysCount)
            {
                //Результирующий список пар с индексами подмассивов
                List<MinMax> result = new List<MinMax>();
                //Если число элементов в массиве слишком мало для деления
                //то возвращается массив целиком
                if ((endIndex - beginIndex) <= subArraysCount)
                {
                    result.Add(new MinMax(0, (endIndex - beginIndex)));
                }

                else

                {
                    //Размер подмассива
                    int delta = (endIndex - beginIndex) / subArraysCount;
                    //Начало отсчета
                    int currentBegin = beginIndex;
                    //Пока размер подмассива укладывается в оставшуюся
                    //последовательность
                    while ((endIndex - currentBegin) >= 2 * delta)
                    {
                        //Формируем подмассив на основе начала
                        //последовательности
                        result.Add(
                        new MinMax(currentBegin, currentBegin + delta));
                        //Сдвигаем начало последовательности
                        //вперед на размер подмассива
                        currentBegin += delta;
                    }
                    //Оставшийся фрагмент массива
                    result.Add(new MinMax(currentBegin, endIndex));
                }
                //Возврат списка результатов
                return result;
            }
        }

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
                string text = File.ReadAllText(fd.FileName, Encoding.GetEncoding(1251));
                char[] sep = { ' ', '.', ',', '!', '?', '/', '\t', '\n' };
                string[] textlist = text.Split(sep);
                foreach (string s in textlist)
                {
                    string str = s.Trim();
                    if (!list.Contains(str)) list.Add(str);
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
            //Слово для поиска
            string word = this.textBox2.Text.Trim();

            //Если слово для поиска не пусто
            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)
            {
                //Слово для поиска в верхнем регистре
                string wordUpper = word.ToUpper();
                //Временные результаты поиска
                List<string> tempList = new List<string>();
                Stopwatch t = new Stopwatch();
                t.Start();
                foreach (string str in list)
                {
                    if (str.ToUpper().Contains(wordUpper))
                    {
                        tempList.Add(str);
                    }
                }
                t.Stop();
                this.textBox3.Text = t.Elapsed.ToString();
                this.listBox1.BeginUpdate();
                //Очистка списка
                this.listBox1.Items.Clear();
                //Вывод результатов поиска
                foreach (string str in tempList)
                {
                    this.listBox1.Items.Add(str);
                }
                this.listBox1.EndUpdate();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать файл и ввести слово для поиска");
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {//Слово для поиска
            string word = this.textBox2.Text.Trim();
            //Если слово для поиска не пусто
            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)
            {
                int maxDist;
                if (!int.TryParse(this.textBox4.Text.Trim(), out maxDist))
                {
                    MessageBox.Show("Необходимо указать максимальное расстояние");
                    return;
                }
                if (maxDist < 1 || maxDist > 5)
                {
                    MessageBox.Show("Максимальное расстояние должно быть в диапазоне от 1 до 5");
                    return;
                }
                int ThreadCount;
                if (!int.TryParse(this.textBox6.Text.Trim(), out ThreadCount))
                {
                    MessageBox.Show("Необходимо указать количество потоков");
                    return;
                }
                Stopwatch timer = new Stopwatch();
                timer.Start();

                //-------------------------------------------------
                // Начало параллельного поиска
                //-------------------------------------------------
                //Результирующий список
                List<ParallelSearchResult> Result = new List<ParallelSearchResult>();

                //Деление списка на фрагменты для параллельного запуска в потоках
                List<MinMax> arrayDivList = SubArrays.DivideSubArrays(0, list.Count,
               ThreadCount);
                int count = arrayDivList.Count;

                //Количество потоков соответствует количеству фрагментов массива
                Task<List<ParallelSearchResult>>[] tasks = new
                Task<List<ParallelSearchResult>>[count];

                //Запуск потоков
                for (int i = 0; i < count; i++)
                {
                    //Создание временного списка, чтобы потоки не работали параллельно с одной коллекцией
                    List<string> tempTaskList = list.GetRange(arrayDivList[i].Min, arrayDivList[i].Max - arrayDivList[i].Min);
                    tasks[i] = new Task<List<ParallelSearchResult>>(
                     //Метод, который будет выполняться в потоке
                     ArrayThreadTask,
                     //Параметры потока
                     new ParallelSearchThreadParam()
                     {
                         tempList = tempTaskList,
                         maxDist = maxDist,
                         ThreadNum = i,
                         wordPattern = word
                     });
                    //Запуск потока
                    tasks[i].Start();
                }
                Task.WaitAll(tasks);
                timer.Stop();
                //Объединение результатов
                for (int i = 0; i < count; i++)
                {
                    Result.AddRange(tasks[i].Result);
                }
                //-------------------------------------------------
                // Завершение параллельного поиска
                //-------------------------------------------------
                timer.Stop();
                //Вывод результатов
                //Время поиска
                this.textBox5.Text = timer.Elapsed.ToString();
                //Вычисленное количество потоков
                this.textBox7.Text = count.ToString();
                //Начало обновления списка результатов
                this.listBox1.BeginUpdate();
                //Очистка списка
                this.listBox1.Items.Clear();
                //Вывод результатов поиска
                foreach (var x in Result)
                {
                    string temp = x.word + "(расстояние=" + x.dist.ToString() + " поток="
                   + x.ThreadNum.ToString() + ")";
                    this.listBox1.Items.Add(temp);
                }
                //Окончание обновления списка результатов
                this.listBox1.EndUpdate();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать файл и ввести слово для поиска");
            }
        }
    }
}
