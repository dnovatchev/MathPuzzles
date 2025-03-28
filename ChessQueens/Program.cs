using System.Runtime.CompilerServices;

namespace ChessQueens
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var board4 = new int[][] {
                [0, 0, 0, 0], [0, 0, 0, 0], [0, 0, 0, 0], [0, 0, 0, 0]
            };
            var board5 = new int[][] {
                [0, 0, 0, 0, 0], [0, 0, 0, 0, 0], [0, 0, 0, 0, 0], [0, 0, 0, 0, 0], [0, 0, 0, 0, 0]
            };
            var board6 = new int[][] {
                [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0]
            };
            var board7 = new int[][] {
                [0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0], 
                [0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0]
            };
            var board8 = new int[][] {
                [0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0],
                [0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0, 0, 0]
            };
            var boards = new List<int[][]> { board4, board5, board6, board7, board8 };
            foreach(var board in boards)
            {
                var sol = new Solution();
                var result = sol.NumSolutions(board);
                var size = board.Length;
                Console.WriteLine($"On a {size}x{size} board there are {result} solutions.");
            }
        }
    }

    public class Solution
    {
        private static List<(int, int)> _PinnedLocations = new List<(int, int)> { };
        private static HashSet<string> _Solutions = new HashSet<string> { };

        public Solution()
        {
            _PinnedLocations = new List<(int, int)> { };
            _Solutions = new HashSet<string> { };
        }
        public long NumSolutions(int[][] board, int depth = 1)
        {
            var maxInd = board.Length - 1;
            var result = 0L;
            for(var i = 0; i <= maxInd; i++)
            {
                for(var j = 0; j <= maxInd; j++)
                {
                    if (board[i][j] != 0) continue;
                    _PinnedLocations.Add((i, j));
                    if (depth > maxInd)
                    {
                        var thisSolution = new List<(int, int)>(_PinnedLocations);
                        thisSolution.Sort();
                        var strSolution = String.Join(';', thisSolution.Select(loc => $"{loc.Item1}_{loc.Item2}"));
                        _Solutions.Add(strSolution);
                        result += 1;
                        _PinnedLocations.RemoveAt(depth - 1);
                        continue;
                    }
                    MarkQueensPositions(board, i, j, depth);
                    result += NumSolutions(board, depth + 1);
                    UnMarkQueensPositions(board, i, j, depth);
                    _PinnedLocations.RemoveAt(depth - 1);
                }
            }
            return _Solutions.Count;
        }
        private void MarkQueensPositions(int[][] board, int i, int j, int depth)
        {
            var maxInd = board.Length - 1;
            for (var k = 0; k <= maxInd; k++)
            {
                if (board[i][k] == 0) board[i][k] = depth;
                if (i - k >= 0 && j - k >= 0) if (board[i - k][j - k] == 0) board[i - k][j - k] = depth;
                if (i + k <= maxInd && j + k <= maxInd) if (board[i + k][j + k] == 0) board[i + k][j + k] = depth;
                if (i - k >= 0 && j + k <= maxInd) if (board[i - k][j + k] == 0) board[i - k][j + k] = depth;
                if (i + k <= maxInd && j - k >= 0) if (board[i + k][j - k] == 0) board[i + k][j - k] = depth;
            }
            for(var k = 0; k <= maxInd; k++)
            {
                if (board[k][j] == 0) board[k][j] = depth;
            }
        }
        private void UnMarkQueensPositions(int[][] board, int i, int j, int depth)
        {
            var maxInd = board.Length - 1;
            for (var k = 0; k <= maxInd; k++)
            {
                if (board[i][k] == depth) board[i][k] = 0;
                if (i - k >= 0 && j - k >= 0) if (board[i - k][j - k] == depth) board[i - k][j - k] = 0;
                if (i + k <= maxInd && j + k <= maxInd) if (board[i + k][j + k] == depth) board[i + k][j + k] = 0;
                if (i - k >= 0 && j + k <= maxInd) if (board[i - k][j + k] == depth) board[i - k][j + k] = 0;
                if (i + k <= maxInd && j - k >= 0) if (board[i + k][j - k] == depth) board[i + k][j - k] = 0;
            }
            for (var k = 0; k <= maxInd; k++)
            {
                if (board[k][j] == depth) board[k][j] = 0;
            }

        }
    }
}
