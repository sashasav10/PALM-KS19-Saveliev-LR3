using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp31
{
    struct MyTime
    {
        public int hour, minute, second;
        public MyTime(int h, int m, int s)
        {
            this.hour = h;
            this.minute = m;
            this.second = s;
        }
        public override string ToString()
        {
            DateTime td = new DateTime(0001, 1, 1, hour, minute, second);
            return td.ToString("HH:mm:ss");
        }
    };
    class Program
    {
        static int TimeSinceMidnight(MyTime t)
        {
            int secPerDay = 86400;//60 * 60 * 24
            int sec = (((t.second + t.minute * 60 + t.hour * 3600) % secPerDay) + secPerDay) % secPerDay;
            return sec;
        }
        static MyTime TimeSinceMidnight(int t)
        {
            int secPerDay = 86400;//60 * 60 * 24
            t = ((t % secPerDay) + secPerDay) % secPerDay;
            int h = t / 3600;
            int m = (t / 60) % 60;
            int s = t % 60;
            return new MyTime(h, m, s);
        }
        static MyTime AddOneMinute(ref MyTime t)
        {//Тут можно было бы воспользоваться предедущими функциями, но, чтобы менять конкретную структуру 
         // (t) и сохранять изменения, я их не использую. Аналогично и в последствии.

            int temp = (((t.second + (t.minute + 1) * 60 + t.hour * 3600) % 86400) + 86400) % 86400;
            t.hour = temp / 3600;
            t.minute = (temp / 60) % 60;
            t.second = temp % 60;
            return t;
        }
        static MyTime AddOneHour(ref MyTime t)
        {
            int temp = (((t.second + t.minute * 60 + (t.hour + 1) * 3600) % 86400) + 86400) % 86400;
            t.hour = temp / 3600;
            t.minute = (temp / 60) % 60;
            t.second = temp % 60;
            return t;
        }
        static MyTime AddOneSecond(ref MyTime t)
        {
            int temp = (((t.second + 1 + t.minute * 60 + t.hour * 3600) % 86400) + 86400) % 86400;
            t.hour = temp / 3600;
            t.minute = (temp / 60) % 60;
            t.second = temp % 60;
            return t;
        }
        static MyTime AddSeconds(MyTime t, int s)
        {
            int temp = (((t.second + s + t.minute * 60 + t.hour * 3600) % 86400) + 86400) % 86400;
            t.hour = temp / 3600;
            t.minute = (temp / 60) % 60;
            t.second = temp % 60;
            return t;
        }
        static int Difference(MyTime mt1, MyTime mt2)
        {
            return TimeSinceMidnight(mt1) - TimeSinceMidnight(mt2);
        }
        static string WhatLesson(MyTime mt)
        {
            int seconds = TimeSinceMidnight(mt);
            if (seconds > 0 && seconds < 28800) return "Пары еще не начались";
            else if (seconds >= 28800 && seconds < 33600) return "1-ая пара";
            else if (seconds >= 33600 && seconds < 34800) return "перемена между 1-ой и 2-ой парой";
            else if (seconds >= 34800 && seconds < 39600) return "2-ая пара";
            else if (seconds >= 39600 && seconds < 40800) return "перемена между 2-ой и 3-ей парой";
            else if (seconds >= 40800 && seconds < 45600) return "3-ая пара";
            else if (seconds >= 45600 && seconds < 46800) return "перемена между 3-ей и 4-ой парой";
            else if (seconds >= 46800 && seconds < 51600) return "4-ая пара";
            else if (seconds >= 51600 && seconds < 52800) return "перемена между 4-ой и 5-ой парой";
            else if (seconds >= 52800 && seconds < 57600) return "5-ая пара";
            else if (seconds >= 57600 && seconds < 58200) return "перемена между 5-ой и 6-ой парой";
            else if (seconds >= 58200 && seconds < 63000) return "6-ая пара";
            else if (seconds >= 63000 && seconds < 86400) return "пары уже кончились";
            else return "Вы ввели неверное значение";
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите время в формате 24 в виде <часы минуты секунды> через пробел:");
            string[] temp = Console.ReadLine().Split(' ');
            int h = Convert.ToInt32(temp[0]);
            int m = Convert.ToInt32(temp[1]);
            int s = Convert.ToInt32(temp[2]);
            MyTime t = new MyTime(h, m, s);
            Console.WriteLine(t.ToString());
            Console.WriteLine("Количество секунд с начала суток указанной даты");
            Console.WriteLine(TimeSinceMidnight(t));

            Console.WriteLine("Добавим к времени 1 минуту:  {0}", AddOneMinute(ref t));

            Console.WriteLine("Добавим к времени еще 1 час:  {0}", AddOneHour(ref t));

            Console.WriteLine("Добавим к времени еще 1 секунду:  {0}", AddOneSecond(ref t));

            Console.WriteLine("Введите количество секунд, которое хотите добавить:");
            int seconds = int.Parse(Console.ReadLine());
            Console.WriteLine(AddSeconds(t, seconds));

            Console.WriteLine("Введите количество секунд с начала суток, чтобы узнать время");
            int SecondFromTheStart = int.Parse(Console.ReadLine());
            Console.WriteLine(TimeSinceMidnight(SecondFromTheStart));

            Console.WriteLine("Для нахождения разницы двух моментов введите первое время в формате 24 в виде <часы минуты секунды> через пробел");
            string[] temp1 = Console.ReadLine().Split(' ');
            int h1 = Convert.ToInt32(temp1[0]);
            int m1 = Convert.ToInt32(temp1[1]);
            int s1 = Convert.ToInt32(temp1[2]);
            MyTime mt1 = new MyTime(h1, m1, s1);
            Console.WriteLine("Введите второе время в формате 24 в виде <часы минуты секунды> через пробел");
            string[] temp2 = Console.ReadLine().Split(' ');
            int h2 = Convert.ToInt32(temp2[0]);
            int m2 = Convert.ToInt32(temp2[1]);
            int s2 = Convert.ToInt32(temp2[2]);
            MyTime mt2 = new MyTime(h2, m2, s2);
            Console.WriteLine("Разница в секундах равняется: {0}", Difference(mt1, mt2));

            Console.WriteLine("Для того, чтобы узнать расписание звонков, введите нужное время в формате 24 в виде <часы минуты секунды> через пробел");
            string[] temp3 = Console.ReadLine().Split(' ');
            int h3 = Convert.ToInt32(temp3[0]);
            int m3 = Convert.ToInt32(temp3[1]);
            int s3 = Convert.ToInt32(temp3[2]);
            Console.WriteLine(WhatLesson(new MyTime(h3, m3, s3)));

            Console.ReadKey();
        }
    }
}