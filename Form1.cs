using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrencyExchange
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
        //обработчик нажатий на клавиши в текстовом поле
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
                return;
            if (e.KeyChar == '.') e.KeyChar = ',';
            if (e.KeyChar == ',') //если введена запятая
            {
                //в тексте не может быть больше одной запятой  
                //или текст начинаться с запятой
                if ((textBox1.Text.IndexOf(',') != -1 || (textBox1.Text.Length == 0)))
                {
                    e.Handled = true; //то остальные символы недоступны
                }
                return;
            }

            if (Char.IsControl(e.KeyChar))
                if (e.KeyChar == (char)Keys.Enter) //если нажат enter
                    button1.Focus(); //фокус переходит на кнопку
            return;

            e.Handled = true; ////
        }

        //если изменилось текстовое поле
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) //если символы не ввыдены
                button1.Enabled = false; //кнопка недоступна
            else
                button1.Enabled = true; //иначе доступна 
        }

        //обработчик нажатия на кнопку
        private void button1_Click(object sender, EventArgs e)
        {
            double kurs;
            DateTime date;
            date = dateTimePicker1.Value; //возвращаем время
            kurs = System.Convert.ToDouble(textBox1.Text); //из текста в текстбоксе конвертируем в дабл
        //получаем информацию о файле
            System.IO.FileInfo fi = new System.IO.FileInfo(Application.StartupPath + "\\usd.txt");
            //открываем поток для записи
            System.IO.StreamWriter sw;

            //если файл данных существует, открываем поток для добавления
            if (fi.Exists)
                sw = fi.AppendText();
            else sw = fi.CreateText(); //если нет то создать файл и открыть поток для записи

            sw.WriteLine(date.ToShortDateString()); //записать в файл дату в коротком формате
            sw.WriteLine(kurs.ToString("N"));
            sw.Close(); //закрываем поток

            //чтобы не записать данные дважды сделаем кнопку и поле ввода недоступными
            button1.Enabled = false;
            textBox1.Enabled = false;
        }
        //пользователь выбрал другую дату
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true; //делаем кнопку доступной
            textBox1.Clear(); //очищаем поле ввода
            textBox1.Focus(); //устанавливаем фокус на поле ввода            
        }
    }
}
