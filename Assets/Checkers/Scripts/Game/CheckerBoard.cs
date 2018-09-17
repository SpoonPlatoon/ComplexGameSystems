                                                                         
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckerBoard : MonoBehaviour
{
    #region Singleton
    // "Instance" keyword variable can be accessed everywhere
    public static CheckerBoard Instance { set; get; }
    #endregion
    #region Variables
    [Header("Game Logic")]
    public Piece[,] pieces = new Piece[8, 8]; // 2D Array - https://www.cs.cmu.edu/~mrmiller/15-110/Handouts/arrays2D.pdf
    public GameObject whitePiecePrefab, blackPiecePrefab; // Prefabs to spawn
    // Offset values of the board
    public Vector3 boardOffset = new Vector3(-4f, 0f, -4f);
    public Vector3 pieceOffset = new Vector3(.5f, .125f, .5f);
    public LayerMask hitLayers;
    public float rayDistance = 25f;

    public Transform chatMessageContainer;
    public GameObject messagePrefab;
    public GameObject highlightsContainer;
    public Text nameTag;
    public Transform canvas;
    public CanvasGroup alertCanvas;

    private float lastAlert;
    private float winTime;
    private bool alertActive;
    private bool gameIsOver;

    private bool isWhite; // Is the current character white?
    private bool isWhiteTurn; // Is it white's turn?
    private bool hasKilled; // Has the player killed a piece?
    private Piece selectedPiece; // Current selected piece
    private List<Piece> forcedPieces; // List storing the pieces that are forced moves
    private Vector2 mouseOver; // Mouse over value
    private Vector2 startDrag; // Position of start drag
    private Vector2 endDrag; // Position of end drag
    #endregion
    #region Unity Events
    void Start()
    {
        // Generate the board on startup
        GenerateBoard();
    }
    void Update()
    {
        UpdateMouseOver();

        // Is is white's turn or black's turn?
        if (isWhite ? isWhiteTurn : !isWhiteTurn)
        {
            // Convert coordinates to int (again to be sure)
            int x = (int)mouseOver.x;
            int y = (int)mouseOver.y;

            if (Input.GetMouseButtonDown(0))
            {
                SelectPiece(x, y);
            }

            // Is there a selectedPiece currently?
            if (selectedPiece != null)
            {
                // Update the drag position
                UpdatePieceDrag(selectedPiece);
                if (Input.GetMouseButtonUp(0))
                {
                    //MovePiece(selectedPiece, x, y);
                    TryMove((int)startDrag.x, (int)startDrag.y, x, y);
                    //selectedPiece = null;
                }
            }
        }
    }
    #endregion
    #region Modifiers
    void TryMove(int x1, int y1, int x2, int y2)
    {
        if (x1 < 0 || x1 > pieces.GetLength(0) || y1 < 0 || y1 > pieces.GetLength(1) || x2 < 0 || x2 > pieces.GetLength(0) || y2 < 0 || y2 > pieces.GetLength(1))
        {
            //x1 = start x
            //y1 = start y
            //x2 = end x
            //y2 = end y
            return;
        }
        if (selectedPiece != null)
        {
            MovePiece(selectedPiece, x2, y2);

            Piece temp = pieces[x1, y1];
            pieces[x1, y1] = pieces[x2, y2];
            pieces[x2, y2] = temp;

            selectedPiece = null;
        }
    }

    void SelectPiece(int x, int y)
    {
        //check if x and y is out of the bounds of the array
        if (x < 0 || x >= 8 ||
            y < 0 || y >= 8)
        {
            return;
        }

        //set piece to pieces[x,y]
        Piece p = pieces[x, y];

        if (p != null && !p.isWhite)
        {
            selectedPiece = p;
            startDrag = mouseOver;
        }
    }

    void MovePiece(Piece pieceToMove, int x, int y)
    {
        // Move the piece to world coordinates using x and y + offsets
        Vector3 coordinate = new Vector3(x, 0f, y);
        pieceToMove.transform.position = coordinate + boardOffset + pieceOffset;
    }
    #endregion
    #region Generators
    void GeneratePiece(bool isWhite, int x, int y)
    {
        GameObject prefab = isWhite ? whitePiecePrefab : blackPiecePrefab; // Which prefab is the piece?
        GameObject clone = Instantiate(prefab) as GameObject; // Instantiate the prefab 
        clone.transform.SetParent(transform); // Make checkerboard the parent of new piece
        Piece pieceScript = clone.GetComponent<Piece>(); // Get the "Piece" Component from clone ('Piece' needs to be attached to prefabs)
        pieces[x, y] = pieceScript; // Add piece component to array
        MovePiece(pieceScript, x, y); // Move the piece to correct world position
    }
    // Generate the board pieces
    void GenerateBoard()
    {
        // Generate white team
        for (int y = 0; y < 3; y++)
        {
            // If the remainder of /2 is zero, it is true
            // % = modulo - https://www.dotnetperls.com/modulo
            bool oddRow = (y % 2 == 0);
            // Loop through 8 and skip 2 every time
            for (int x = 0; // Initializer
                 x < 8;  // Condition
                 x += 2) // Incrementer / Iteration
                         // For Loops - https://www.tutorialspoint.com/csharp/csharp_for_loop.htm
            {
                // Generate piece here
                int desiredX = oddRow ? x : x + 1;
                int desiredY = y;
                GeneratePiece(true, desiredX, desiredY);
            }
        }

        // Generate black team
        for (int y = 7; y > 4; y--) // Go backwards from 7
        {
            bool oddRow = (y % 2 == 0); // Don't really need the extra '()' parenthesis
            for (int x = 0; x < 8; x += 2)
            {
                // Generate our piece
                int desiredX = oddRow ? x : x + 1;
                int desiredY = y;
                GeneratePiece(false, desiredX, desiredY);
            }
        }
    }
    #endregion
    #region Updaters
    void UpdateMouseOver()
    {
        // Does the main not camera exist?
        if (Camera.main == null)
        {
            Debug.Log("Unable to find Main Camera");
            // Exit the whole function
            return;
        }

        // Generate ray from mouse input to world
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Perform raycast
        if (Physics.Raycast(camRay, out hit, rayDistance, hitLayers))
        {
            // Convert world position to an array index (by converting to int aswell)
            mouseOver.x = (int)(hit.point.x - boardOffset.x);
            mouseOver.y = (int)(hit.point.z - boardOffset.z);
        }
        else
        {
            // '-1' means nothing was selected
            mouseOver.x = -1;
            mouseOver.y = -1;
        }
    }
    void UpdatePieceDrag(Piece pieceToDrag)
    {
        // Does the main camera not exist?
        if (Camera.main == null)
        {
            Debug.Log("Unable to find Main Camera");
            // Exit the functions
            return;
        }

        // Generate ray from mouse input to world
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Perform raycast
        if (Physics.Raycast(camRay, out hit, rayDistance, hitLayers))
        {
            // Start dragging the piece and move it just above the cursor
            pieceToDrag.transform.position = hit.point + Vector3.up;
        }
    }

    #endregion
    #region UI
    public void Alert(string text)
    {
        // Get text component from alertCanvas children
        alertCanvas.GetComponentInChildren<Text>().text = text;
        // Set alertCanvas alpha to 1
        alertCanvas.alpha = 1;
        // listAlert in time
        lastAlert = Time.time;
        // set alertActive to true
        alertActive = true;
    }
    public void UpdateAlert()
    {
        float timeDifference = Time.time - lastAlert;
        // Is alertActive
        if (alertActive)
        {
            // if time - lastAlert > 1.5 seconds
            if (Time.time - lastAlert > 1.5f)
            {
                // Set alertCanvas alpha to 1 - (time - lastAlert) - 1.5)
                alertCanvas.alpha = 1 - (timeDifference - 1.5f);
                // if time - lastalert > 2.5
                if (timeDifference > 2.5f)
                {
                    // Set alertActive to false
                    alertActive = false;
                }
            }
        }
    }
    public void ChatMessage(string msg)
    {
        // Instantiate clone of messagePrefab
        GameObject clone = Instantiate(messagePrefab);
        // Set clone parent to chatMessageContainer
        clone.transform.SetParent(chatMessageContainer);
        // Get text component in children from clone
        Text messageText = clone.GetComponentInChildren<Text>();
        // set text component's text to message
        messageText.text = msg;
    }
    public void SendChatMessage()
    {
        // Find Object of Type InputField
        InputField input = GameObject.Find("MessageInput").GetComponent<InputField>();
        // If inputField.text is empty
        if (input.text == "")
            return;
        // Call client.send("CMSG|" + text)
        // Set i.text to ""
        input.text = "";
    } 
    #endregion
}