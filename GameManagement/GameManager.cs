using SplashKitSDK;

namespace Minesweeper
{
    // SINGLETON design pattern: only one GameManager ever exists.
    // This class controls the overall game state, including the board, difficulty, timer, and win/loss.
    public class GameManager
    {
        private static GameManager _instance;
        private Board _board;

        //Current difficulty settings (grid size + mine count).
        private Difficulty _difficulty;

        //Current state of the game (Title, Playing, Won, Lost).
        private GameState _state;

        //Logical game clock used for tracking elapsed time.
        private Clock _clock;

        //SplashKit timer used to measure real-time intervals for ticking the clock.
        private SplashKitSDK.Timer _clockTimer;

        private bool _clockPaused;

        public Board Board 
        { 
            get 
            { 
                return _board; 
            } 
        }

        public Difficulty Difficulty 
        { 
            get 
            { 
                return _difficulty; 
            } 
        }

        // Singleton access point, if there isnt a instane or gamemanager yet make one.
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public Clock Clock
        {
            get
            {
                return _clock;
            }
        }

        //Pauses the game clock and stops the timer.
        public void PauseClock()
        {
            _clockPaused = true;
            _clockTimer.Stop();
        }

        //Private constructor ensures only one instance (Singleton pattern).
        private GameManager()
        {
            State = GameState.Title;
        }

        //Sets the selected difficulty before starting a game.
        public void SetDifficulty(Difficulty difficulty)
        {
            _difficulty = difficulty;
        }

        //Initializes and starts a new game session.
        public void StartGame()
        {
            //New board (WOOOOO).
            _board = new Board();

            //Reset clock.
            _clock = new Clock();

            //Create and start SplashKit timer for tick tracking.
            _clockTimer = new SplashKitSDK.Timer("ClockTimer");
            _clockTimer.Start();

            //Set game state to active gameplay.
            State = GameState.Playing;

            //Subscribe game logic observer to the board so it can react to events.
            _board.Subscribe(new GameObserver(this));
        }

        //Updates game logic each frame (mainly clock updates).
        public void Update()
        {
            if (State != GameState.Playing) 
            {
                return;
            }

            //Clock ticks every second.
            if (_clockTimer.Ticks > 1000)
            {
                _clock.Tick();
                _clockTimer.Reset();
                _clockTimer.Start();
            }
        }

        //Current game state (readonly outside of the class).
        public GameState State 
        { 
            get
            {
                return _state;
            } 
            private set
            {
                _state = value;
            }
        }

        //Reveals a cell on the board if the game is active.
        public void Reveal(int row, int col)
        {
            if (State != GameState.Playing) 
            {
                return;
            }
            _board.RevealCell(row, col);
        }

        //Toggles a flag on a cell if the game is active.
        public void Flag(int row, int col)
        {
            if (State != GameState.Playing) 
            {
                return;
            }
            _board.FlagCell(row, col);
        }

        //Called when the player wins the game.
        public void Win()
        {
            PauseClock();
            State = GameState.Won;
        }

        //Called when the player lose the game.
        public void Lose()
        {
            PauseClock();
            State = GameState.Lost;
        }

        //Resets the game back to the title screen.
        public void Reset()
        {
            _board = null;
            _difficulty = null;

            State = GameState.Title;
        }
    }
}
