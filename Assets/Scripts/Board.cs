using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject boardSquaresContener;
    [SerializeField] private Sprite darkSquare;
    [SerializeField] private Sprite lightSquare;
    private SpriteRenderer[][] boardSquares;
    private SpriteRenderer lastRandomSquare;

    private void Awake()
    {
        lastRandomSquare = null;
        InitializeBoard();
    }

    // This will return the SpriteRenderer of a random square from boardSquares
    // This SpriteRenderer will always be differents than the one returned the
    // last time this function was called
    public SpriteRenderer GetRandomSquare()
    {
        int x = Random.Range(0, 8);
        int y = Random.Range(0, 8);

        while (boardSquares[y][x] == lastRandomSquare)
        {
            x = Random.Range(0, 8);
            y = Random.Range(0, 8);
        }
        lastRandomSquare = boardSquares[y][x];
        return boardSquares[y][x].GetComponent<SpriteRenderer>();
    }

    // Initialize boardSquares to be a SpriteRenderer[8][8]
    // and get the reference of each SpriteRenderer of the 64 squares of the board
    private void InitializeBoard()
    {
        boardSquares = new SpriteRenderer[8][];

        for (int i = 0; i < 8; i += 1)
            boardSquares[i] = new SpriteRenderer[8];
        FillBoard();
    }

    // Fill the SpriteRenderer[8][8] with the reference of
    // each SpriteRenderer of the 64 squares of the board
    // Set the sprite of each square with the right color
    private void FillBoard()
    {
        int x = 0;
        int y = 0;

        foreach (SpriteRenderer square in boardSquaresContener.GetComponentsInChildren<SpriteRenderer>())
        {
            if (x == 8)
            {
                x = 0;
                y += 1;
            }
            boardSquares[y][x] = square;
            if ((x + y) % 2 == 0)
                SetSquareSprite(square.gameObject, "dark");
            else
                SetSquareSprite(square.gameObject, "light");
            x += 1;
        }
    }

    // Set the sprite of the given GameObject depending on the given type
    private void SetSquareSprite(GameObject square, string type)
    {
        SpriteRenderer squareRenderer = square.GetComponent<SpriteRenderer>();

        if (type == "dark")
            squareRenderer.sprite = darkSquare;
        else
            squareRenderer.sprite = lightSquare;
    }
}