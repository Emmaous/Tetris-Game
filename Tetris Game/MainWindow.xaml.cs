using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Array containing the tile images
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
        };

        //Array containing the block images
        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
        };

        //2D-array to control images for every game grid
        private readonly Image[,] imageControls;

        private GameState gameState = new GameState();
        public MainWindow()
        {
            InitializeComponent();
            imageControls = GameCanvasSetup(gameState.Grid); // initializing image controls array
        }

        //Method to control and setup game images on the canvas
        private Image[,] GameCanvasSetup(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image() 
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };

                    //setting the canvas
                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 10); //setting the start of the cavas to be 2 rows from the top to allow space for spawning
                    Canvas.SetLeft(imageControl, c * cellSize);
                    TetrisCanvas.Children.Add(imageControl); //making the image controls a child of the canvas
                    imageControls[r, c] = imageControl;

                }
            }

            return imageControls;
        }

        //Method to draw Game Grid
        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Source = tileImages[id];
                }

            }
        }

        //Method to draw blocks
        private void DrawBlock(Block block)
        {
            foreach(Position p in block.TilePosition())
            {
                imageControls[p.Row, p.Column].Source = tileImages[block.BlockId];
            }
        }

        //method to preview the next block
        private void PreviewNextBlock(BlockQueue blockQueue)
        {
            Block next = blockQueue.NewBlock;
            NextBlock.Source = blockImages[next.BlockId];
        }

        //Method to draw the game according to current game state
        private void DrawGame(GameState gameState)
        {
            DrawGrid(gameState.Grid);
            DrawBlock(gameState.CurrentBlock);
            PreviewNextBlock(gameState.Queue);
        }

        //Method to handle game loop
        private async Task Gameloop ()
        {
            DrawGame(gameState);//start game

            while (!gameState.GameOver)
            {
                await Task.Delay(500); //500ms delay before block moves down
                gameState.MoveBlockDown();
                DrawGame(gameState);
            }

            GameOverMenu.Visibility = Visibility.Visible;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //checking current game state
            if (gameState.GameOver)
            {
                return;
            }

            //handling key presses for the Up, Down, Left, Right and R keys
            switch(e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right:
                    gameState.MoveBlockRight(); 
                    break;
                case Key.Down: 
                    gameState.MoveBlockDown(); 
                    break;
                case Key.Up:
                    gameState.RotateBlockCW(); 
                    break;
                case Key.R:
                    gameState.RotateBlockCCW(); 
                    break;
                default:
                    return;
            }

            DrawGame(gameState);
        }

        private async void TetrisCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await Gameloop();
        }

        //Method to handle restarting game when restart button is clicked
        private async void RestartGame_CLick(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility= Visibility.Hidden;
            await Gameloop();
        }
    }
}