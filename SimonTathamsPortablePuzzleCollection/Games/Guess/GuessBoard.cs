using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SimonTathamsPortablePuzzleCollection.Games.Guess
{
    /// <summary>
    /// This class is our main game.
    /// It contains informations about the Playground, the HiddenCode and the ValidationArea.
    /// </summary>
    [Serializable()]
    class GuessBoard
    {
        public List<List<string>> Playground = new List<List<string>>();
        public List<string> HiddenCode = new List<string>();
        public List<List<string>> ValidationArea = new List<List<string>>();

        public int CurrrentRow { get; set; }
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public int AmountOfColors { get; set; }

        public GuessBoard(int row, int col, int amountColor = 6)
        {
            RowCount = row;
            ColCount = col;
            AmountOfColors = amountColor;

            //Reset board
            CurrrentRow = 0;
            ResetPlayground();
            ResetValidationArea();

            //Set new code
            SetHiddenCode(amountColor);
        }

        public GuessBoard(GuessBoard _gameObject)
        {
            this.RowCount = _gameObject.RowCount;
            this.CurrrentRow = _gameObject.CurrrentRow;
            this.ColCount = _gameObject.ColCount;
            this.Playground = _gameObject.Playground;
            this.HiddenCode = _gameObject.HiddenCode;
            this.ValidationArea = _gameObject.ValidationArea;
            this.AmountOfColors = _gameObject.AmountOfColors;
        }
        public GuessBoard()
        {

        }

        private void ResetPlayground()
        {
            ColorConverter ColCon = new ColorConverter();
            Playground = new List<List<string>>();
            for (int row = 0; row < RowCount; row++)
            {
                Playground.Add(new List<string>());
                for (int col = 0; col < ColCount; col++)
                {
                    Playground[row].Add(ColCon.ConvertToString(Brushes.Gray));
                }
            }
        }
        private void ResetValidationArea()
        {
            ColorConverter ColCon = new ColorConverter();
            ValidationArea = new List<List<string>>();
            for (int row = 0; row < RowCount; row++)
            {
                ValidationArea.Add(new List<string>());
                for (int col = 0; col < ColCount; col++)
                {
                    ValidationArea[row].Add(ColCon.ConvertToString(Brushes.Gray));
                }
            }
        }

        /// <summary>
        /// Generates a random code of colors.
        /// </summary>
        /// <param name="amount">Sets the amount of colors from which to select.</param>
        private void SetHiddenCode(int amount)
        {
            Random randomColor = new Random();

            for (int i = 0; i < ColCount; i++)
            {
                int rnd = randomColor.Next(amount);
                HiddenCode.Add(GuessColorPalette.ColorPalette[rnd]);
            }
        }
    }
}
