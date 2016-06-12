using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake
{
	public enum Direction
	{
		Up,
		Down,
		Left,
		Right,
		None
	}

	public class Player
	{
		Canvas gameCanvas;
		public List<Point> SnakeBody { get; set; }
		public Point Position;
		int foodAmount;
		public int Length { get; set; }
		public Brush Color { get; private set; }
		public int Size { get; private set; }
		public Direction CurrentDirection { get; set; }
		public Direction PreviousDirection { get; set; }

		public Player()
		{
			this.gameCanvas = new Canvas();
			Initialize();
		}

		public Player(Canvas gameCanvas, int snakeSize, Brush snakeColor, Point startingPosition, int foodAmount)
		{
			this.gameCanvas = gameCanvas;
			this.Size = snakeSize;
			this.Color = snakeColor;
			Initialize();
			Draw(startingPosition);
			this.Position = startingPosition;
			this.foodAmount = foodAmount;
		}

		private void Initialize()
		{
			this.SnakeBody = new List<Point>();
			this.CurrentDirection = Direction.None;
			this.PreviousDirection = Direction.None;
			//this.CurrentDirection = Direction.Right;
			this.Length = 100;
		}

		public void Draw(Point currentPosition)
		{
			Ellipse e = new Ellipse();
			e.Fill = Color;
			e.Width = Size;
			e.Height = Size;
			//e.Stroke = Brushes.Black;

			Canvas.SetTop(e, currentPosition.Y);
			Canvas.SetLeft(e, currentPosition.X);

			int count = gameCanvas.Children.Count;
			this.gameCanvas.Children.Add(e);
			this.SnakeBody.Add(currentPosition);

			if(count > Length)
			{
				gameCanvas.Children.RemoveAt(count - Length + foodAmount - 1);
				SnakeBody.RemoveAt(count - Length);
			}
		}

		/// <summary>
		/// Obsługa kierunków ruchu gracza. 
		/// Implementuje blokade poruszania się węża w kierunku swojego 'ciała'.
		/// </summary>

		public void Update(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.W:
					if (PreviousDirection != Direction.Down)
						CurrentDirection = Direction.Up;
					break;

				case Key.S:
					if (PreviousDirection != Direction.Up)
						CurrentDirection = Direction.Down;
					break;

				case Key.D:
					if (PreviousDirection != Direction.Left)
						CurrentDirection = Direction.Right;
					break;

				case Key.A:
					if (PreviousDirection != Direction.Right)
						CurrentDirection = Direction.Left;
					break;
			}
			PreviousDirection = CurrentDirection;
		}
	}
}
