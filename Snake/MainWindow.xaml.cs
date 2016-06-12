using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snake
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Game myGame;
		public MainWindow()
		{
			InitializeComponent();

			this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
			myGame = new Game(gameCanvas);
		}

		private void OnButtonKeyDown(object sender, KeyEventArgs e)
		{
			myGame.Update(sender, e);
		}

		private bool CheckProperties()
		{
			if (!String.IsNullOrEmpty(colorComboBox.Text) && !String.IsNullOrEmpty(sizeComboBox.Text) && !String.IsNullOrEmpty(speedComboBox.Text) && !String.IsNullOrEmpty(foodComboBox.Text))
				return true;
			return false;
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			if (CheckProperties() && !myGame.HasStarted)
			{
				gameCanvas.Children.Clear();
				Brush snakeColor = Brushes.White;
				int gameSpeed = 10000;
				int foodAmount = 0;
				int snakeSize = 0;

				switch(colorComboBox.Text)
				{
					case "Red":
						snakeColor = Brushes.Red;
						break;
					case "Green":
						snakeColor = Brushes.Green;
						break;
					case "Blue":
						snakeColor = Brushes.Blue;
						break;
					case "White":
						snakeColor = Brushes.White;
						break;
					case "Black":
						snakeColor = Brushes.Black;
						break;
				}

				switch(speedComboBox.Text)
				{
					case "Slow":
						gameSpeed = 50000;
						break;
					case "Medium":
						gameSpeed = 10000;
						break;
					case "Fast":
						gameSpeed = 1000;
						break;
				}

				switch(foodComboBox.Text)
				{
					case "Little":
						foodAmount = 5;
						break;
					case "Medium":
						foodAmount = 10;
						break;
					case "Many":
						foodAmount = 20;
						break;
				}

				switch (sizeComboBox.Text)
				{
					case "Small":
						snakeSize = 4;
						break;
					case "Medium":
						snakeSize = 8;
						break;
					case "Large":
						snakeSize = 16;
						break;
				}

				myGame.StartGame(snakeColor, snakeSize, gameSpeed, foodAmount);
			}
			else
				MessageBox.Show("All properties must be selected!", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
		}
	}
}
