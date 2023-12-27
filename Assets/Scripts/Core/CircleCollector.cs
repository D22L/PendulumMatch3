using System;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollector
{
    private CircleCollectorView _view;
    private RewardConfig _rewardConfig;

    public event Action onFull;
    public int ResultScore { get; private set; }
    private const int _maxCount = 3;
    private int[,] _collectorMatrix = new int[_maxCount, _maxCount];
    public CircleCollector(CircleCollectorView circleCollectorView, RewardConfig rewardConfig)
    {
        _view = circleCollectorView;
        _rewardConfig = rewardConfig;

        _view.Init(AddCircle);
    }

    public void InitMatrix()
    {
        _collectorMatrix = new int[_maxCount, _maxCount];
        for (int r = 0; r < _collectorMatrix.GetLength(0); r++)
        {
            for (int c = 0; c < _collectorMatrix.GetLength(1); c++)
            {
                _collectorMatrix[r, c] = -1;
            }
        }

        _view.DestroyAllCircle();
    }

    private bool MatrixIsFull()
    {

        bool isFull = true;
        for (int r = 0; r < _collectorMatrix.GetLength(0); r++)
        {
            for (int c = 0; c < _collectorMatrix.GetLength(1); c++)
            {
                if (_collectorMatrix[r, c] == -1) return false;
            }
        }

        return isFull;
    }

    private void ClearPositionsInMatrix(List<Vector2> pos)
    {
        for (int i = 0; i < pos.Count; i++)
        {
            int r = (int)pos[i].x;
            int c = (int)pos[i].y;
            _collectorMatrix[r, c] = -1;
        }
    }

    private void CheckAndShiftingPositions()
    {
        for (int c = 0; c < _collectorMatrix.GetLength(1); c++)
        {
            for (int r = 0; r < _collectorMatrix.GetLength(0); r++)
            {
                if (r + 1 < _collectorMatrix.GetLength(0))
                {
                    if (_collectorMatrix[r, c] == -1 && _collectorMatrix[r + 1, c] != -1)
                    {
                        Vector2 oldPos = new Vector2(r + 1, c);
                        Vector2 newPos = new Vector2(r, c);
                        _collectorMatrix[r, c] = _collectorMatrix[r + 1, c];
                        _collectorMatrix[r + 1, c] = -1;
                        _view.SetNewPositionForCirlce(oldPos, newPos);
                    }

                }
            }
        }
    }

    private void AddCircle(int columNumber, int rowNumber, int colorIndex)
    {
        _collectorMatrix[rowNumber, columNumber] = colorIndex;      
        
        var haveMatch = CheckAndShifting();

        if (!haveMatch && MatrixIsFull())
        {
            onFull?.Invoke();
        }
    }

    private bool CheckAndShifting()
    {
        var matches = CheckMatches();
      
        if (matches.Count == _maxCount)
        {
            var colorIndex = _collectorMatrix[(int)matches[0].x, (int)matches[0].y];
             _view.DestroyCircle(matches);
            ClearPositionsInMatrix(matches);
            CheckAndShiftingPositions();
            var rewardData = _rewardConfig.RewardData[colorIndex];
            ResultScore += (int)rewardData.RewardSize;

            return CheckAndShifting();
        }

        return false;
    }

    private List<Vector2> CheckMatches()
    {
        List<Vector2> matches = new List<Vector2>();

        // check horizontal
        for (int r = 0; r < _collectorMatrix.GetLength(0); r++)
        {
            for (int c = 0; c < _collectorMatrix.GetLength(1); c++)
            {
                if (_collectorMatrix[r, c] < 0) break;
                if (c + 1 < _collectorMatrix.GetLength(1) && _collectorMatrix[r, c] == _collectorMatrix[r, c + 1])
                {
                    var coord = new Vector2(r, c);
                    if (!matches.Contains(coord)) matches.Add(coord);

                    matches.Add(new Vector2(r, c + 1));
                    if (matches.Count == _maxCount) return matches;
                }
            }
            matches.Clear();
        }

        // check vercical
        for (int c = 0; c < _collectorMatrix.GetLength(1); c++)
        {
            for (int r = 0; r < _collectorMatrix.GetLength(0); r++)
            {
                if (_collectorMatrix[r, c] < 0) break;
                if (r + 1 < _collectorMatrix.GetLength(0) && _collectorMatrix[r, c] == _collectorMatrix[r + 1, c])
                {
                    var coord = new Vector2(r, c);
                    if (!matches.Contains(coord)) matches.Add(coord);
                    matches.Add(new Vector2(r + 1, c));
                    if (matches.Count == _maxCount) return matches;
                }
            }
            matches.Clear();
        }

        // check diagonal bot to top
        List<Vector2> diagonalBotTop = new List<Vector2>();
        int columNumber = 0;
        for (int r = 0; r < _collectorMatrix.GetLength(0); r++)
        {

            if (_collectorMatrix[r, columNumber] < 0) break;

            diagonalBotTop.Add(new Vector2(r, columNumber));
            columNumber++;
        }

        if (HaveMatchesInList(diagonalBotTop))
        {
            return diagonalBotTop;
        }

        // check diagonal top to bot
        List<Vector2> diagonalTopBot = new List<Vector2>();
        columNumber = 0;
        for (int r = _collectorMatrix.GetLength(0) - 1; r >= 0; r--)
        {
            if (_collectorMatrix[r, columNumber] < 0) break;

            diagonalTopBot.Add(new Vector2(r, columNumber));
            columNumber++;
        }

        if (HaveMatchesInList(diagonalTopBot))
        {
            return diagonalTopBot;
        }

        return matches;
    }

    private bool HaveMatchesInList(List<Vector2> coords)
    {
        if (coords.Count > 0)
        {
            var first = _collectorMatrix[(int)coords[0].x, (int)coords[0].y];
            for (int i = 1; i < coords.Count; i++)
            {
                if (_collectorMatrix[(int)coords[i].x, (int)coords[i].y] != first) return false;
            }

        }
        return true;
    }
    public void ResetScore()
    {
        ResultScore = 0;
    }
}
