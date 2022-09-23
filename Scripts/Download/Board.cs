using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public struct SudokuBoardData
    {
        public int[] unsolved_data;
        public int[] solved_data;

        public SudokuBoardData(int[] unsolved, int[] solved) : this()
        {
            this.unsolved_data = unsolved;
            this.solved_data = solved;
        }
    };

    public Dictionary<string, List<SudokuBoardData>> sudoku_game = new Dictionary<string, List<SudokuBoardData>>();

    // Create the intital Sudoku Grid
    int[,] grid = new int[9, 9];
    int[,] puzzle = new int[9, 9];

    // default numbers removed
    [SerializeField] int difficulty = 60;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

    }

    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerSettings.difficulty;
        //Debug.Log("Difficulty is: " + difficulty);
        CreateGrid();
        CreatePuzzle();

        sudoku_game.Add("GamePlay",getData());

    }

    public List<Board.SudokuBoardData> getData()
    {
        List<Board.SudokuBoardData> data = new List<Board.SudokuBoardData>();

        int[] unsolved = unsolvedData().ToArray();
        int[] solved = solvedData().ToArray();

        data.Add(new Board.SudokuBoardData(unsolved, solved));

        return data;
    }

    public  List<int> solvedData()
    {
        List<int> solve = new List<int>();
        string output = "";
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                output += grid[i, j];
                solve.Add(grid[i, j]);
            }
            output += "\n";
        }

        string school = "";
        foreach (var unsol in solve)
        {
            school += unsol;
        }
       // Debug.Log("List: " + school);
        return solve;
    }

    public List<int> unsolvedData()
    {
        List<int> unsolve = new List<int>();
        string output = "";
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                output += puzzle[i, j];
                unsolve.Add(puzzle[i, j]);
            }
            output += "\n";
        }

        string school = "";
        foreach (var unsol in unsolve)
        {
            school += unsol;
        }
        //Debug.Log("List 2: " + school);
        return unsolve;
    }


    void ConsoleOutputGrid(int [,] g)
    {
        string output = "";
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                output += g[i, j];
            }
            output += "\n";
        }
       // Debug.Log(output +" Outpost");

    }

    bool ColumnContainsValue(int col, int value)
    {
        for (int i = 0; i < 9; i++)
        {
            if (grid[i, col] == value)
            {
                return true;
            }
        }

        return false;
    }

    bool RowContainsValue(int row, int value)
    {
        for (int i = 0; i < 9; i++)
        {
            if (grid[row, i] == value)
            {
                return true;
            }
        }

        return false;
    }

    bool SquareContainsValue(int row, int col, int value)
    {
        //blocks are 0-2, 3-5, 6-8
        //row / 3 is the first grid coord * 3 
        //ints 

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid[ row / 3 * 3 + i , col / 3 * 3 + j ] == value)
                {
                    return true;
                }
            }
        }

        return false;
    }

    bool CheckAll(int row, int col, int value)
    {
        if (ColumnContainsValue(col,value)) {
            //Debug.Log(row + " " + col);
            return false;
        }
        if (RowContainsValue(row, value))
        {
            //Debug.Log(row + " " + col);
            return false;
        }
        if (SquareContainsValue(row, col, value))
        {
            //Debug.Log(row + " " + col);
            return false;
        }

        return true;
    }

    bool IsValid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (grid[i,j] == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void CreateGrid()
    {
        List<int> rowList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> colList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        int value = rowList[SkillzCrossPlatform.Random.Range(0, rowList.Count)];
        SkillzCrossPlatform.Random.Value();
        grid[0, 0] = value;
        rowList.Remove(value);
        colList.Remove(value);

        for (int i = 1; i < 9; i++)
        {
            value = rowList[SkillzCrossPlatform.Random.Range(0, rowList.Count)];
            SkillzCrossPlatform.Random.Value();
            grid[i, 0] = value;
            rowList.Remove(value);
        }

        for (int i = 1; i < 9; i++)
        {
            value = colList[SkillzCrossPlatform.Random.Range(0, colList.Count)];
            SkillzCrossPlatform.Random.Value();
            if (i < 3)
            {
                while(SquareContainsValue(0, 0, value))
                {
                    value = colList[SkillzCrossPlatform.Random.Range(0, colList.Count)];
                    SkillzCrossPlatform.Random.Value();
                    // reroll
                }
            }
            grid[0, i] = value;
            colList.Remove(value);
        }

        for (int i = 6; i < 9; i++)
        {
            value = SkillzCrossPlatform.Random.Range(1, 10);
            while (SquareContainsValue(0, 8, value) || SquareContainsValue(8, 0, value) || SquareContainsValue(8, 8, value))
            {
                value = SkillzCrossPlatform.Random.Range(1, 10);
                SkillzCrossPlatform.Random.Value();
            }
            grid[i, i] = value;
        }

        ConsoleOutputGrid(grid);
        SolveSudoku();
    }

    bool SolveSudoku()
    {
        int row = 0;
        int col = 0;

        if (IsValid())
        {
            return true;
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (grid[i, j] == 0)
                {
                    row = i;
                    col = j;
                }
            }
        }

        for (int i = 1; i <=9; i++)
        {
            if (CheckAll(row, col, i)) {
                grid[row, col] = i;
                //ConsoleOutputGrid(grid);
                
                if (SolveSudoku())
                {
                    return true;
                }
                else
                {
                    grid[row, col] = 0;
                }
            }
        }
        return false;
    }

    void CreatePuzzle()
    {
        System.Array.Copy(grid, puzzle, grid.Length);

        // Remove cells
        for (int i = 0; i < difficulty; i++)
        {
            int row = SkillzCrossPlatform.Random.Range(0, 9);
            int col = SkillzCrossPlatform.Random.Range(0, 9);
            SkillzCrossPlatform.Random.Value();

            while (puzzle[row,col] == 0)
            {
                row = SkillzCrossPlatform.Random.Range(0, 9);
                col = SkillzCrossPlatform.Random.Range(0, 9);
                SkillzCrossPlatform.Random.Value();
            }
        
            puzzle[row, col] = 0;
        }

        // Make sure every generated puzzle has at least 8 different numbers on it. This ensures there is only one solution to each puzzle.
        List<int> onBoard = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        RandomizeList(onBoard);
        
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                for (int k = 0; k < onBoard.Count - 1; k++)
                {
                    if (onBoard[k] == puzzle[i,j])
                    {
                        onBoard.RemoveAt(k);
                    }
                }
            }
        }

        while (onBoard.Count - 1 > 1)
        {
            int row = SkillzCrossPlatform.Random.Range(0, 9);
            int col = SkillzCrossPlatform.Random.Range(0, 9);
            SkillzCrossPlatform.Random.Value();
            if (grid[row,col] == onBoard[0])
            {
                puzzle[row, col] = grid[row, col];
                onBoard.RemoveAt(0);
            }
        }

        ConsoleOutputGrid(puzzle);

    }

    void RandomizeList(List<int> l)
    {
        //var count = l.Count;
        //var last = count - 1;
        for (var i = 0; i < l.Count - 1; i++)
        {
            int rand = SkillzCrossPlatform.Random.Range(i, l.Count);
            SkillzCrossPlatform.Random.Value();
            int temp = l[i];
            l[i] = l[rand];
            l[rand] = temp;
        }
    }


    public void UpdatePuzzle(int row, int col, int value)
    {
        puzzle[row, col] = value;
    }

  
}
