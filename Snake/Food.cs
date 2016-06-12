using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Snake
{
	class Food
	{
		Canvas gameCanvas;
		public List<Point> FoodList { get; private set; }
		private Random random;

		public Food(Canvas gameCanvas)
		{
			FoodList = new List<Point>();
			this.gameCanvas = gameCanvas;
			this.random = new Random();
		}

		public void Initialize(int amount)
		{
			for(int i = 0; i < amount; i++)
				Draw(i);
		}

		public void Draw(int foodIndex)
		{
			Point newFood = new Point(random.Next(10, 560), random.Next(10, 400));
			//ImageBrush imgBrush = new ImageBrush();
			//imgBrush.ImageSource = new BitmapImage(new Uri(@"apple.png", UriKind.Relative));

			Ellipse e = new Ellipse();
			//e.Fill = imgBrush;
			e.Fill = Brushes.Red;
			e.Width = e.Height = 8;

			Canvas.SetTop(e, newFood.Y);
			Canvas.SetLeft(e, newFood.X);
			gameCanvas.Children.Insert(foodIndex, e);
			FoodList.Insert(foodIndex, newFood);
		}
	}
}
