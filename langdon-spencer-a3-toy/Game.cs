// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        BodyPart[] snake = new BodyPart[5];
        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Interactive Toy");
            Draw.FillColor = Color.Red;
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            snake[0].Position = Input.GetMousePosition();
            for (int i = 1; i < snake.Length; i++) { snake[i].Constrain(snake[i - 1].Position); }
            for (int i = 0; i < snake.Length; i++) { Draw.Circle(snake[i].Position, 10); }
            Window.ClearBackground(255);
            Console.WriteLine(snake);
        }
    }

}
