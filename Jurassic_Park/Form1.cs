using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace Jurassic_Park
{
	public partial class Form1 : Form
	{
		//static public Semaphore sem = new Semaphore(3, 3); //1-й параметр - какому числу объектов доступен семафор, 2-й - max число объектов, использующих семафор
		//два потока для музея и сафари-парка
		ThreadStart TH;
		//Thread[] museum;
		Thread[] safari;

		//создадим семафор на разделяемый ресурс - машины
		Semaphore transport;
		//Semaphore guests; //хз ???

		//кол-во машин и людей
		int nCars;
		int nPeople;

		public Form1()
		{
			InitializeComponent();
		}

		//public void people()
		//{
		//	while (nPeople > 0) //while (true)
		//	{
				
		//		guests.WaitOne();
		//		richTextBox1.Invoke((ThreadStart)delegate () { richTextBox1.AppendText(Convert.ToString(Thread.CurrentThread.Name) + "садится в машину\n"); });
		//		transport.Release();
		//		richTextBox1.Invoke((ThreadStart)delegate () { richTextBox1.AppendText(Thread.CurrentThread.Name + "Катается\n"); });
		//		Thread.Sleep(1); //катается какое-то время
		//		richTextBox1.Invoke((ThreadStart)delegate () { richTextBox1.AppendText(Thread.CurrentThread.Name + "выходит из машины\n"); });
		//		guests.Release(); //машина освобождается
		//		nPeople--; //кол-во людей уменьшается на единицу
		//		Thread.Sleep(1); //поток засыпает на какое-то время
		//	}
		//}

		public void cars()
		{
			while (nCars > 0) //while (true)
			{
				//А ГДЕ ЛОГИКА?
				//Зачем тебе?
				//Ладно.
				Thread.Sleep(1000);
				transport.WaitOne();

				richTextBox2.Invoke((ThreadStart)delegate () { richTextBox2.AppendText(
					Convert.ToString(Thread.CurrentThread.Name) + " садится в машину\n"); });
				richTextBox2.Invoke((ThreadStart)delegate () { richTextBox2.AppendText(
					Thread.CurrentThread.Name + " катается\n"); });
				Random rand = new Random();
				Thread.Sleep(rand.Next(1000, 5000));
				nPeople--;
				richTextBox2.Invoke((ThreadStart)delegate () { richTextBox2.AppendText(
					Thread.CurrentThread.Name + " выходит из машины\n"); });
				transport.Release();
				Thread.Sleep(1000); 
			}

		}

		//запускаем наш поток с людьми и машинами
		public void button1_Click(object sender, EventArgs e)
		{
			nPeople = Int32.Parse(textBox1.Text);
			nCars = Int32.Parse(textBox2.Text);
			//museum = new Thread[nPeople];
			safari = new Thread[nCars];
			transport = new Semaphore(nPeople, nCars); //всего к семафору имеют доступ все люди, а максимальное ограничение определяется кол-вом машин

			//for (int i = 0; i < nPeople; i++)
			//{
			//	TH = new ThreadStart(people);
			//	museum[i] = new Thread(TH);
			//	museum[i].Name = $"Человек {i}";
			//	museum[i].Start();
			//}
			for (int i = 0; i < nCars; i++)
			{
				TH = new ThreadStart(cars);
				safari[i] = new Thread(TH);
				safari[i].Name =  $"Человек {i}";
				safari[i].Start();
			}
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			//for (int i = 0; i < nPeople; i++)
			//{
			//	museum[i].Abort();
			//}
			for (int i = 0; i < nCars; i++)
			{
				safari[i].Abort();
			}
		}

	}
}

