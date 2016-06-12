using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Snake
{
	class Game
	{
		DispatcherTimer timer;
		Canvas gameCanvas;
		Player snake;
		Food food;
		int score;
		public bool HasStarted { get; set; }
		int gameSpeed;
		int snakeSize;

		public Game(Canvas gameCanvas)
		{
			this.HasStarted = false;
			this.gameCanvas = gameCanvas;
			TextBlock textBlock = new TextBlock();
			textBlock.Text = "HOW TO START:";
			textBlock.Text += "\n1. SELECT PROPERTIES ON THE RIGHT";
			textBlock.Text += "\n2. PRESS 'START GAME'";
			textBlock.Text += "\n3. PRESS W, S, A OR D TO MOVE";
			textBlock.Text += "\n4. ENJOY ;)";
			textBlock.Foreground = new SolidColorBrush(Colors.White);
			textBlock.FontSize = 32;
			textBlock.FontFamily = new FontFamily("Impact");
			Canvas.SetLeft(textBlock, 10);
			Canvas.SetTop(textBlock, 10);
			gameCanvas.Children.Add(textBlock);
		}

		public void StartGame(Brush snakeColor, int snakeSize, int gameSpeed, int foodAmount)
		{
			this.gameSpeed = gameSpeed;
			timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(gameSpeed);
			timer.Tick += new EventHandler(timer_Tick);
			
			HasStarted = true;

			ImageBrush imgBrush = new ImageBrush();
			imgBrush.ImageSource = new BitmapImage(new Uri(@"bg.png", UriKind.Relative));
			gameCanvas.Background = imgBrush;

			this.snakeSize = snakeSize;
			this.score = 0;
			this.snake = new Player(gameCanvas, snakeSize, snakeColor, new Point(100, 100), foodAmount);
			this.food = new Food(gameCanvas);
			timer.Start();
			food.Initialize(foodAmount);
		}

		public void Update(object sender, KeyEventArgs e)
		{
			if (HasStarted)
				snake.Update(sender, e);
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (HasStarted)
			{
				switch (snake.CurrentDirection)
				{
					case Direction.Up:
						snake.Position.Y--;
						snake.Draw(snake.Position);
						break;

					case Direction.Down:
						snake.Position.Y++;
						snake.Draw(snake.Position);
						break;

					case Direction.Left:
						snake.Position.X--;
						snake.Draw(snake.Position);
						break;

					case Direction.Right:
						snake.Position.X++;
						snake.Draw(snake.Position);
						break;
				}

				if (snake.Position.X < 5 || snake.Position.X > 560 || snake.Position.Y < 5 || snake.Position.Y > 420)
					GameOver();

				for (int i = 0; i < food.FoodList.Count; i++)
				{
					if ((Math.Abs(food.FoodList[i].X - snake.Position.X) < snakeSize) && (Math.Abs(food.FoodList[i].Y - snake.Position.Y) < snakeSize))
					{
						snake.Length += 10;
						score += 10;
						food.FoodList.RemoveAt(i);
						gameCanvas.Children.RemoveAt(i);
						food.Draw(i);
						break;
					}
				}

				for(int i = 0; i < (snake.SnakeBody.Count - snake.Size * 2); i++)
				{
					Point point = new Point(snake.SnakeBody[i].X, snake.SnakeBody[i].Y);
					if((Math.Abs(point.X - snake.Position.X) < snake.Size) && (Math.Abs(point.Y - snake.Position.Y) < snake.Size))
					{
						GameOver();
						break;
					}
				}
			}
		}

		private void GameOver()
		{
			HasStarted = false;
			timer.Stop();
			gameCanvas.Children.Clear();
			gameCanvas.Background = Brushes.Black;
			TextBlock textBlock = new TextBlock();
			textBlock.Text = "    GAME OVER\nYOUR SCORE: " + score;
			textBlock.Foreground = new SolidColorBrush(Colors.White);
			textBlock.FontSize = 48;
			textBlock.FontStyle = FontStyles.Oblique;
			textBlock.FontFamily = new FontFamily("Impact");
			Canvas.SetLeft(textBlock, 130);
			Canvas.SetTop(textBlock, 130);
			gameCanvas.Children.Add(textBlock);

		}
	}
}
