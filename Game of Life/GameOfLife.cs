using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_Life
{
    internal class GameEngine
    {
        //Тест для работы обновления
        private bool[,] field;

        private readonly int rows;
        private readonly int cols;

        public uint CurrentGeneration { get; private set; }

        public GameEngine(int rows, int cols, int density)
        { 
            this.rows = rows;
            this.cols = cols;

            field = new bool[cols, rows];

            Random random = new Random();

            for (int x = 0; x < cols; x++)
            { 
                for(int y = 0; y < rows; y++) 
                {
                    field[x, y] = random.Next(density) == 0;
                }
            }
        }

        public void NextGeneration()
        { 
            var newField = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var neighboursCount = CountNeighbours(x, y);

                    var hasLife = field[x, y];

                    if (!hasLife && neighboursCount == 3)
                    {
                        newField[x, y] = true;
                    }
                    else if (hasLife && (neighboursCount < 2 || neighboursCount > 3))
                    {
                        newField[x, y] = false;
                    }
                    else
                    {
                        newField[x, y] = field[x, y];
                    }
                }
            }
            field = newField;

            CurrentGeneration++;
        }

        public bool[,] GetCurrentGeneration()
        {
            var result = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    result[x, y] = field[x, y];
                }
            }
            return result;
        }

        private int CountNeighbours(int x, int y)
        {
            int count = 0;

            for(int i = -1; i < 2; i++) 
            {
                for (int j = -1; j < 2; j++)
                { 
                    var col = (x + i + cols) % cols;
                    var row = (y + j + rows) % rows;

                    var isSelfCheking = col == x && row == y;
                    var hasLife = field[col, row];

                    if (hasLife && !isSelfCheking)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
