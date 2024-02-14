#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace BlazorGodot.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiveTestCallFromGodot : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Game gameData)
        {
            // Process the game data: move characters one square to the right
            foreach (var square in gameData.Gameboard.Squares)
            {
                if (square.Occupant != null && square.Occupant.Type == "character")
                {
                    MoveCharacterToRight(gameData.Gameboard, square);
                }
            }

            // Serialize the updated game data to JSON
            var updatedJson = JsonConvert.SerializeObject(gameData);

            // Return the updated JSON
            return Ok(updatedJson);
        }

        private void MoveCharacterToRight(GameBoard gameBoard, Square currentSquare)
        {
            int newRow = currentSquare.Position.Row;
            int newColumn = currentSquare.Position.Column + 1;

            // Check if the new position is within the bounds of the game board
            if (newColumn < gameBoard.Dimensions.Width)
            {
                // Find the square to which the character should move
                var targetSquare = gameBoard.Squares
                    .Find(s => s.Position.Row == newRow && s.Position.Column == newColumn);

                if (targetSquare != null && targetSquare.Occupant.Type == "empty")
                {
                    // Move the character
                    targetSquare.Occupant = currentSquare.Occupant;
                    currentSquare.Occupant = new Occupant { Type = "empty" };
                }
            }
        }
    }

    public class Game
    {
        public List<Character> Characters { get; set; }
        public GameBoard Gameboard { get; set; }
    }

    public class Character
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
    }

    public class GameBoard
    {
        public Dimensions Dimensions { get; set; }
        public List<Square> Squares { get; set; }
    }

    public class Dimensions
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class Square
    {
        public Position Position { get; set; }
        public Occupant Occupant { get; set; }
    }

    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public class Occupant
    {
        public string Type { get; set; } // "character", "object", or "empty"
        public string Name { get; set; } // Name of the character or object, if applicable
    }
}
