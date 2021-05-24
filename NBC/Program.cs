using System;
using System.Collections.Generic;

namespace NBC
{
    class Words
    {
        public string word { get; set; }
        public int s { get; set; }
        public int h { get; set; }
    }
    class Program
    {
        static double Pr(double V, double Lc, double yn)
        {
            return Math.Log((yn + 1.0) / (V + Lc));
        }
        static void Main(string[] args)
        {
            List<Words> words = new List<Words>();
            words.Add(new Words() { word = "предоставляю", s = 1, h = 0 });
            words.Add(new Words() { word = "услуги", s = 1, h = 0 });
            words.Add(new Words() { word = "бухгалтера", s = 1, h = 0 });
            words.Add(new Words() { word = "спешите", s = 1, h = 0 });
            words.Add(new Words() { word = "купить", s = 1, h = 1 });
            words.Add(new Words() { word = "чудотряпку", s = 1, h = 0 });
            words.Add(new Words() { word = "надо", s = 0, h = 1 });
            words.Add(new Words() { word = "молоко", s = 0, h = 1 });
            double sDc = 2.0, Dc = 1.0, D = 3.0;
            double sLc = 6.0, V = 8.0, Lc = 3.0;
            double y = 0.0, n = 0.0;
            bool spam_word_exist, ham_word_exist;
            double spam, ham;
            //Console.WriteLine(spam + " " + ham);
            char choice = 'д';
            while(choice == 'д')
            {
                spam_word_exist = false;
                ham_word_exist = false;
                spam = Math.Log(sDc / D);
                ham = Math.Log(Dc / D);
                Console.WriteLine("Слова в списке: ");
                foreach (Words w in words)
                    Console.WriteLine(w.word + " " + w.s + " " + w.h);
                string[] letter = Console.ReadLine().Split(' ');
                foreach (string a in letter)
                {
                    foreach (Words w in words)
                    {
                        if (a.Equals(w.word))
                        {
                            if (w.s != 0) { y += 1.0; }
                            if (w.h != 0) { n += 1.0; }
                        }
                    }
                    //Console.WriteLine(Pr(V, sLc, y) + " " + y + " " + n);
                    //Console.WriteLine(Pr(V, Lc, n) + " " + y + " " + n);
                    spam += Pr(V, sLc, y);
                    ham += Pr(V, Lc, n);
                    y = 0;
                    n = 0;
                }
                Console.WriteLine();
                Console.WriteLine("Рассчитаем значение выражения для класса SPAM" + spam);
                Console.WriteLine("Рассчитаем значение выражения для класса HAM" + ham);
                if (spam > ham)
                    foreach (string a in letter)
                    {
                        foreach (Words w in words)
                        {
                            if (a.Equals(w.word))
                            {
                                w.s += 1;
                                spam_word_exist = true;
                            }
                            else
                            {
                                V += 1.0;
                                Lc += 1.0;
                                D += 1.0;
                                Dc += 1.0;
                            }
                        }
                        if (spam_word_exist == false) words.Add(new Words() { word = a, s = 1, h = 0 });
                    }
                else
                    foreach (string a in letter)
                    {
                        foreach (Words w in words)
                        {
                            if (a.Equals(w.word))
                            {
                                w.h += 1;
                                ham_word_exist = true;
                            }
                            else
                            {
                                V += 1.0;
                                Lc += 1.0;
                                D += 1.0;
                                Dc += 1.0;
                            }
                        }
                        if (ham_word_exist == false) words.Add(new Words() { word = a, s = 0, h = 1 });
                    }
                Console.WriteLine("Чтобы продолжить нажмите - д, чтобы выйти - любой другой символ...");
                choice = Convert.ToChar(Console.ReadLine());
            }//while
        }
    }
}
