using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SudokuGrid : MonoBehaviour
{
    public int colums = 0;
    public int rows = 0;
    public float Square_Offset = 0.0f;
    public GameObject grid_square;
    private Vector2 start_position;
    public float sqaure_scale = 1.0f;
    public float square_gap = 0.1f;
    public Color line_hightlight_color = Color.red;
    public Color line_Selected_color = Color.red;
    private List<GameObject> grid_sqaures_ = new List<GameObject>();

    public int width;
    public int height;

    public Button GetHintButton;
    public GameObject HintAmount;

    private bool ishintPush;
    //public GameObject SetInstuction;

    // Start is called before the first frame update
    void Start()
    {
        if (grid_square.GetComponent<GridSquare>() == null)
            Debug.LogError("This Game object need to have GridSquare script attached !");

        createGrid();
        SetGridNumber();

        start_position = this.gameObject.transform.position;
        GetHintButton.enabled = false;
        ishintPush = false;
    }

    private void createGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SpawnGridSquares()
    {
        int square_index = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < colums; column++)
            {
                grid_sqaures_.Add(Instantiate(grid_square) as GameObject);
                grid_sqaures_[grid_sqaures_.Count - 1].GetComponent<GridSquare>().SetSquareIndex(square_index);
                grid_sqaures_[grid_sqaures_.Count - 1].transform.SetParent(this.transform); //instantiate this game object as a child of the object holding the script.
                grid_sqaures_[grid_sqaures_.Count - 1].transform.localScale = new Vector3(sqaure_scale, sqaure_scale, sqaure_scale);

                square_index++;
            }
        }
    }

    private void SetSquaresPosition()
    {
        var sqaure_rect = grid_sqaures_[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
        bool row_moved = false;

        offset.x = sqaure_rect.rect.width * sqaure_rect.transform.localScale.x + Square_Offset;
        offset.y = sqaure_rect.rect.height * sqaure_rect.transform.localScale.y + Square_Offset;

        int column_number = 0;
        int row_number = 0;

        foreach (GameObject sqaure in grid_sqaures_)
        {
            if (column_number + 1 > colums)
            {
                row_number++;
                column_number = 0;
                square_gap_number.x = 0;
                row_moved = false;
            }

            var pos_x_offset = offset.x * column_number + (square_gap_number.x * square_gap);
            var pos_y_offset = offset.y * row_number + (square_gap_number.y * square_gap);

            if (column_number > 0 && column_number % 3 == 0)
            {
                square_gap_number.x++;
                pos_x_offset += square_gap;
            }
            if (row_number > 0 && row_number % 3 == 0 && row_moved == false)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += square_gap;
            }

            sqaure.GetComponent<RectTransform>().anchoredPosition = new Vector3(start_position.x +
                pos_x_offset, start_position.y - pos_y_offset,start_position.x);
            column_number++;
        }
    }

    private void SetGridNumber()
    {
        var data = Board.Instance.sudoku_game["GamePlay"][0];
        setGridSqaureData(data);
    }

    private void setGridSqaureData(Board.SudokuBoardData data)
    {
        for (int index = 0; index < grid_sqaures_.Count; index++)
        {
            grid_sqaures_[index].GetComponent<GridSquare>().SetNumber(data.unsolved_data[index]);
            grid_sqaures_[index].GetComponent<GridSquare>().SetCorrectNumber(data.solved_data[index]);
            grid_sqaures_[index].GetComponent<GridSquare>().SetHasDefaultValue(data.unsolved_data[index]
                != 0 && data.unsolved_data[index] == data.solved_data[index]);

        }
    }

    private void OnEnable()
    {
        GameEvents.onSquareSelected += OnSquareSeleted;
        GameEvents.OnCheckBoardCompleted += CheckBoardCompeleted;
        GameEvents.onUpdateSelectedSquareNumber += SetSqaure;
        GameEvents.onActiveHintButton += SetHintButton;
    }

    private void OnDisable()
    {
        GameEvents.onSquareSelected -= OnSquareSeleted;
        GameEvents.OnCheckBoardCompleted -= CheckBoardCompeleted;
        GameEvents.onUpdateSelectedSquareNumber -= SetSqaure;
        GameEvents.onActiveHintButton -= SetHintButton;
    }

    private void SetHintButton(bool step)
    {
        GetHintButton.enabled = step;
    }

    private void SetSquaresColor(int[] data, Color col)
    {
        foreach (var index in data)
        {
            var comp = grid_sqaures_[index].GetComponent<GridSquare>();

            if (comp.HasWrongValue() == false && comp.IsSelected() == false)
            {
                comp.SetSquareColour(col);
            }

        }
    }

    private void OnSquareSeleted(int square_index)
    {
        var horizontal_line = LineIndicator.instance.GetHorizontalLine(square_index);
        var vertical_line = LineIndicator.instance.GetVertailLine(square_index);
        var square = LineIndicator.instance.GetSquare(square_index);

        SetSquaresColor(LineIndicator.instance.GetAllSqauresIndexes(), Color.white);

        SetSquaresColor(horizontal_line, line_hightlight_color);
        SetSquaresColor(vertical_line, line_hightlight_color);
        SetSquaresColor(square, line_hightlight_color);

    }

    public void SetSqaure(string StringNumber)
    {
        int[] data = LineIndicator.instance.GetAllSqauresIndexes();
        foreach (var index in data)
        {
            var comp = grid_sqaures_[index].GetComponent<GridSquare>();
            var compnumber = comp.number_text.GetComponent<TMP_Text>().text;

            if (comp.HasWrongValue() == false && comp.IsSelected() == false)
            {
                if (compnumber.Contains(StringNumber))
                {
                    comp.number_text.GetComponent<TMP_Text>().fontSize = 100;
                    comp.SetSquareColour(line_Selected_color);
                    
                }
                else
                {
                    comp.number_text.GetComponent<TMP_Text>().fontSize = 80;
                    comp.SetSquareColour(Color.white);
                }

            }
        }
    }

    private void CheckBoardCompeleted()
    {
        foreach (var square in grid_sqaures_)
        {
            var comp = square.GetComponent<GridSquare>();

            if (comp.IsCorrectNumberSet() == false)
            {
                return;
            }
        }

        GameEvents.OnBoardCompletedMethod(ishintPush);
        
    }

    public void SolveSudoku()
    {
        foreach (var square in grid_sqaures_)
        {
            var comp = square.GetComponent<GridSquare>();
            comp.SetCorrectNumber();
        }

        CheckBoardCompeleted();
    }

    public void hint()
    {
        foreach (var square in grid_sqaures_)
        {
            var comp = square.GetComponent<GridSquare>();
            if (comp.IsSelected() && comp.HasWrongValue() == false)
            {
                comp.SetCorrectNumber();
                comp.number_text.GetComponent<TMP_Text>().color = new Color(0.5019608f, 0.145098f, 0.09803922f, 1f);

            }
        }

        HintAmount.SetActive(false);
        GetHintButton.interactable = false;
        GameEvents.OnAddKeyBoardMethod(false);
        CheckBoardCompeleted();

        ishintPush = true;
    }


}
