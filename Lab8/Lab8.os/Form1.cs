using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections; 

namespace Lab8.os
{

    public class MyBook 
    {
        public int bookNomer { get; set; }
        public String Avtor { get; set; }
        public String Nazva { get; set; }
        public String Vydavnyctvo { get; set; }
        public Int16 RikVyhodu { get; set; }

        public MyBook(int bookNomer, String Avtor, String Nazva, String Vydavnyctvo, Int16 RikVyhodu)
        {
            this.bookNomer = bookNomer;
            this.Avtor = Avtor;
            this.Nazva = Nazva;
            this.Vydavnyctvo = Vydavnyctvo;
            this.RikVyhodu = RikVyhodu;
        }

        public override string ToString()
        {
            return "Книга №" + bookNomer.ToString() + " Автор:" + Avtor + " Назва:" + Nazva + " Видавництво: "
            + Vydavnyctvo + " Рік:" + RikVyhodu.ToString();
        }
    }


    public class MyBooks1 : ArrayList, IEnumerable
    {
        public MyBook[] MyBooksArray { get; set; } 

        public MyBooks1(int kilkistKnyh)
        {
            MyBooksArray = new MyBook[kilkistKnyh];
        }

    }

    
    public class MyBooks2 : IEnumerable
    {
        MyBook[] myBooksArray;
        public MyBook[] MyBooksArray
        {
            get { return myBooksArray; }
            set { myBooksArray = value; }
        }

        public MyBooks2(int kilkistKnyh)
        {
            MyBooksArray = new MyBook[kilkistKnyh];
        }


        public IEnumerator GetEnumerator()
        {
            return myBooksArray.GetEnumerator();
        }
    }

    
    public class MyBooks3 : IEnumerable, IEnumerator
    {
        MyBooks3[] myBooksArray;
        int kilkistKnyh = 0;
        int CurrentNomer = 0;


        public int bookNomer { get; set; }
        public String Avtor { get; set; }
        public String Nazva { get; set; }
        public String Vydavnyctvo { get; set; }
        public Int16 RikVyhodu { get; set; }

        int position = -1; 


        public MyBooks3(int kilkistKnyh)
        {
            this.myBooksArray = new MyBooks3[kilkistKnyh];
            this.kilkistKnyh = kilkistKnyh;
        }

   
        public MyBooks3 this[int index]
        {
            get
            {
                if (index < kilkistKnyh && index >= 0)
                    return myBooksArray[index];
                else return null;
            }
            set
            {
                if (index < kilkistKnyh) myBooksArray[index] = value;
            }
        }

        public IEnumerator GetEnumerator()
        {
            Reset();
            for (int i = 0; i < CurrentNomer; i++)
            {
                
                yield return myBooksArray[i];
            }
        }


        public object Current
        {
            get
            {
                if (position >= 0 && position < CurrentNomer)
                    return myBooksArray[position];
                return null;
            }
        }

 
        public bool MoveNext()
        {
            position++;
            return (position < CurrentNomer);
        }

   
        public void Reset()
        {
            position = -1;
        }

        public override string ToString()
        {
            return "Книга №" + bookNomer.ToString() + " Автор:" + Avtor + " Назва:" + Nazva + " Видавництво: "
            + Vydavnyctvo + " Рік: " + RikVyhodu.ToString();
        }

    
        public void Add(int bookNomer, String Avtor, String Nazva, String Vydavnyctvo, Int16 RikVyhodu, ref int KodError)
        {
            if (CurrentNomer < kilkistKnyh)
            {
                this.myBooksArray[CurrentNomer] = new MyBooks3(1);
                this.myBooksArray[CurrentNomer].bookNomer = bookNomer;
                this.myBooksArray[CurrentNomer].Avtor = Avtor;
                this.myBooksArray[CurrentNomer].Nazva = Nazva;
                this.myBooksArray[CurrentNomer].Vydavnyctvo = Vydavnyctvo;
                this.myBooksArray[CurrentNomer].RikVyhodu = RikVyhodu;
                CurrentNomer++;
                KodError = 0;
            }
            else KodError = 1;
        }
    }


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
          
            if (label1 == null) return;

            label1.Text = "";
            string ss = "\n Вивід для класу MyBooks1 \n\n ";

      
            MyBooks1 mbs1 = new MyBooks1(100);
            mbs1.MyBooksArray[0] = new MyBook(1, "Е. Marija Remark", "Три товариші", "Ранок", 1981);
            mbs1.MyBooksArray[1] = new MyBook(2, "Нестайко", "В країні сонячних зайчиків", "Ранок", 1961);
            mbs1.MyBooksArray[2] = new MyBook(3, "Баскаков", "Радіотехнічні кола і сигнали ", "М.: Вища школа", 2000);

            foreach (MyBook b in mbs1.MyBooksArray)
            {
                if (b != null) ss = ss + b.ToString() + "\n";
            }

    
            ss = ss + "\n--- \n\n Вивід для класу MyBooks2 \n\n ";
            MyBooks2 mbs2 = new MyBooks2(100);
            mbs2.MyBooksArray[0] = new MyBook(1, "Е. Marija Remark", "Три товариші ", "Ранок", 1981);
            mbs2.MyBooksArray[1] = new MyBook(2, "Нестайко", "У країні сонячних зайчиків", "Ранок", 1961);
            mbs2.MyBooksArray[2] = new MyBook(3, "Баскаков", "Радіотехнічні кола і сигнали ", "М.: Вища школа", 2000);

            foreach (MyBook b in mbs2.MyBooksArray)
            {
                if (b != null) ss = ss + b.ToString() + "\n";
            }

      
            ss = ss + "\n--- \n\n Вивід для класу MyBooks3 \n\n ";
            MyBooks3 mbs3 = new MyBooks3(100);
            int KodError = 0;

            mbs3.Add(1, "Е. Marija Remark", "Три товариші ", "Ранок", 1981, ref KodError);
            if (KodError > 0) MessageBox.Show("Не вдалось додати книжку, оскільки масив переповнено");
            mbs3.Add(3, "Баскаков", "Радіотехнічні кола і сигнали ", "М.: Вища школа", 2000, ref KodError);
            if (KodError > 0) MessageBox.Show("Не вдалось додати книжку, оскільки масив переповнено");
            mbs3.Add(4, "Загребельний ", "Роксолана", "Світанок", 2000, ref KodError);
            if (KodError > 0) MessageBox.Show("Не вдалось додати книжку, оскільки масив переповнено");
            mbs3.Add(5, "В. Косик", "Україна і Німеччина у другій світовій війні ", "Наукове товариство ім. Шевченка у Львові", 1993, ref KodError);
            if (KodError > 0) MessageBox.Show("Не вдалось додати книжку, оскільки масив переповнено");


            foreach (MyBooks3 b in mbs3)
            {
                if (b != null) ss = ss + b.ToString() + " \n";
            }

      
            mbs3.Reset();

            ss = ss + "\n--- \n\n Вивід поточного елементу \n  \n";


            if (mbs3.MoveNext())
            {
                ss = ss + "Книга 0: " + mbs3.Current.ToString() + " \n";
            }

            ss = ss + " \n Ще раз MoveNext \n \n ";


            if (mbs3.MoveNext())
            {
                ss = ss + "Книга 1: " + mbs3.Current.ToString();
            }

            label1.Text = ss;
        }
    }
}