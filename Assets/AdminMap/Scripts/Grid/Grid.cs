using UnityEngine;

namespace Assets.Scripts.Grid
{
    class Grid : MonoBehaviour
    {
        [SerializeField] private GridCell gridCellPrefab;
        public int size { get; set; }
        public GridCell[,] cells { set; get; }

        public Grid Initialize(int size)
        {
            this.size = size; 

            InstantiateGridCells();

            return this;
        }

        void InstantiateGridCells()
        {
            cells = new GridCell[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cells[i, j] = Instantiate(gridCellPrefab, transform);
                    cells[i, j].transform.position = new Vector3(i, 0, j);
                    cells[i, j].GridCoordinate = new Vector2Int(i, j);
                }
            }
        }

        public GridCell GetCell(int i, int j)
        {
            return cells[i, j];

        }

        public void SetCell(GridCell cell)
        {
            cells[
                (int)cell.GridCoordinate.x, 
                (int)cell.GridCoordinate.y] = cell;
        }

        public bool IsAvailable(int i, int j)
        {
            return cells[i, j].IsAvailable;
        }

        public void SetAvailable(int i, int j, bool isAvailable)
        {
            cells[i, j].IsAvailable = isAvailable;
        }

        public Transform GetTransform(int i, int j)
        {
            //Debug.Log("Grid GetTransform " + i + ", " + j);
            //Debug.Log(cells.GetLength(0) + ", " + cells.GetLength(1));
            return cells[i, j].transform;
        }
    }
}
