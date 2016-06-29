namespace HecateMillionaire.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Media;
    using System.Text;
    using System.Threading;

    using Contracts;
    using HecateMillionaire.BaseTable;
    using Jokers;
    using Players;
    using Questions;

    public class Game : IGame, ISound
    {
        // start game - method to be called from Main
        // ask for player's name and color
        // initilize game - create player, load questions
        // play game - show the question, ask the player for his choice, set timer
        // check if it's correct, if it's not - game over, otherwise ask next question
        // game offers a joker - if player can't answer in time ?
        // end of game - show player's scores, show players statistics 
        // ask for new game
        // private static instance of the same class
        private static readonly Game GameInstance = null;
        private static List<Question> questions;
        private static Player player;
        private static int wrongAnswers;

        static Game()
        {
            // create the instance only if the instance is null
            GameInstance = new Game();
        }

        public static Game GetInstance()
        {
            // return the already existing instance
            return GameInstance;
        }

        // singleton pattern
        // private constructor to restrict the game creation from outside
        private Game()
        {
        }

        // methods from ISound
        public void PlayGameOverSound()
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(GameConstants.SoundGameOver))
            {
                // Use PlaySync to load and then play the sound.
                // The program will pause until the sound is complete.
                soundPlayer.PlaySync();
            }
        }

        // methods from IGame
        public void StartGame()
        {
            // load game logo and menu
            this.InitiliazeGame();
        }

        public void InitiliazeGame()
        {
            Console.Title = "~ Hecate Millionaire ~";

            // load game image and sound
            LoadImage(GameConstants.FileHecateStart);
            this.PlayStartSound();

            // initialize player and questions
            player = new Player();
            questions = Game.InitializeQuestions(GameConstants.FileQuestions);
            wrongAnswers = 0;

            this.LoadMainMenu();
        }

        public void PlayGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Black;

            // 17.6.2016, Kristina. Добавена е проверка за коректност на отговора
            for (int i = 0; i < questions.Count; i++)
            {
                char answer;
                bool flag = false;

                // use infinitely loop because of jokers
                while (true)
                {
                    Console.Clear(); // clear console

                    Console.WriteLine(questions[i]);
                    Console.WriteLine(questions[i].PrintAnswers(flag)); // print answers

                    this.OfferJoker(); // Print jokers
                    answer = DisplayTime.CreateTimer();

                    // answer = Char.Parse(Console.ReadLine()); take char answer

                    // chek for use joker
                    if (answer > '0' && answer <= '3')
                    {
                        // for print only two answers when use FiftyFifty joker or print another joker
                        flag = this.UseJoker(answer, questions[i].RightAnswerIndex, questions[i].Answers);
                    }
                    else
                    {
                        break;
                    }
                }

                if (answer == default(char))
                {
                    continue;
                }

                IsRight check = new IsRight(questions[i], answer);
                if (check.Tell())
                {
                    Console.WriteLine("Your answer is true");
                   this.PlayCorrectSound();

                    // Add 100 scores if the answaer is right
                    player.Score += questions[i].QuestionScore;

                    Console.WriteLine("SCORE : {0} ", player.Score);
                    Thread.Sleep(500); // white because of information
                }
                else
                {
                    Console.WriteLine("You are wrong");
                    this.PlayWrongSound();
                    wrongAnswers++;
                    Thread.Sleep(500); // white because of information

                    // game over if 3 wrong questions
                    if (wrongAnswers == GameConstants.MaxWrongAnswers)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have 3 wrong answers !");
                        Thread.Sleep(500);
                        break;
                    }
                }

                // Край на промените на Кристина
            }

            this.EndGame();
        }

        public bool CheckPlayerAnswer(char answer)
        {
            return false;
        }

        public void OfferJoker()
        {
            var listJoker = player.Jokers;

            Console.WriteLine();
            Console.WriteLine("Jokers:");

            for (int j = 0; j < listJoker.Count; j++)
            {
                if (listJoker[j].IsUsed != true)
                {
                    System.Console.WriteLine(j + 1 + " -> " + listJoker[j].Type);
                }
                else
                {
                    System.Console.BackgroundColor = ConsoleColor.Cyan;
                    System.Console.WriteLine(j + 1 + " -> " + listJoker[j].Type);
                    System.Console.BackgroundColor = ConsoleColor.Black;
                }
            }
        }

        public bool UseJoker(char answer, int rithAnswerIndex, string[] answersOfQuestion)
        {
            bool flag = false; // for print only two answers when use FiftyFifty joker

            switch (answer)
            {
                case '1':
                    if (player.SelectJoker(JokerType.FiftyFifty))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        Thread.Sleep(1000);
                    }

                    break;
                case '2':
                    if (player.SelectJoker(JokerType.HellFromPublic))
                    {
                        var fiftyFifty = player.Jokers[0]; // if used FiftyFifty joker

                        HelpFromPublicJoker help = new HelpFromPublicJoker(JokerType.HellFromPublic);
                        System.Console.WriteLine("\nPublic thing");
                        System.Console.WriteLine(help.Mind(rithAnswerIndex, fiftyFifty.IsUsed, answersOfQuestion));
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }

                    break;
                case '3':
                    if (player.SelectJoker(JokerType.CallFriend))
                    {
                        var fiftyFifty = player.Jokers[0]; // if used FiftyFifty joker

                        System.Console.WriteLine("\nWho friend you want to call!");
                        var friendName = System.Console.ReadLine();
                        CallFriendJoker frient = new CallFriendJoker(JokerType.CallFriend, friendName);
                        System.Console.WriteLine("{0} say: {1}", friendName, frient.Respond(fiftyFifty.IsUsed, answersOfQuestion));
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }

                    break;
                default:
                    break;
            }

            return flag;
        }

        public void EndGame()
        {
            if (this.CheckForWinner())
            {
                string textWin = "YOU'RE A HECATE MILIONAIRE ! - You have {0} lv\n";

                Console.Clear();
                LoadImage(GameConstants.FileChampion);
                this.PlayWinSound();

                var currentCol = (Console.WindowWidth / 2) - (textWin.Length / 2);
                Console.Write(new string(' ', currentCol));

                // Console.WriteLine("\n\tYOU'RE A HECATE MILIONAIRE ! - You have {0} lv\n", player.Score );
                Console.WriteLine(string.Format(textWin, player.Score));

                // save record and name in file when game over 
                player.GameOver();
            }
            else
            {
                string textLose = "Do you want to try another game?\n";

                Console.Clear();
                LoadImage(GameConstants.FileGameOver);
                this.PlayGameOverSound();

                var currentCol = (Console.WindowWidth / 2) - (textLose.Length / 2);
                Console.Write(new string(' ', currentCol));

                // Console.WriteLine("\n\tDo you want to try another game?\n");
                Console.WriteLine(textLose);
            }

            this.LoadMainMenu();
        }

        public void ShowStatistics()
        {
            // print players results
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;

            TheBestThreePlayers.Show(player.Name);
        }

        public void RestartGame()
        {
           this.StartGame();
        }

        private void LoadMainMenu()
        {
            string[] textInformation = new string[]
            {
                "START NEW GAME ?  =>> \n",
                "\tSHOW BEST PLAYERS ?  =>>\n",
                "\tEXIT ?  =>>\n"
            };

            string[] textForChoise = new string[]
            {
                "\tPress 'Enter' => for restart and play a new game\n",
                "\tPress 'Space' for close the game and see the result\n",
                "\tPress 'Esc' to close the game."
            };

            Console.ForegroundColor = ConsoleColor.White;

            ConsolePrintText.Print(textInformation);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();

            ConsolePrintText.Print(textForChoise);
            var choice = Console.ReadKey();

            if (choice.Key == ConsoleKey.Enter)
            {
                Console.Clear();

                // the player don't plays for first time
                if (!player.Name.Equals("Player"))
                {
                    player.Score = 0;
                    this.PlayGame();
                }
                else
                {
                    // if this is the first game of this player
                    // set player and start the game
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine("What's your name?  =>>");
                    string playerName = Console.ReadLine();
                    player.Name = playerName;
                    this.PlayGame();
                }
            }
            else if (choice.Key == ConsoleKey.Spacebar)
            {
                // show best players
                this.ShowStatistics();
                this.LoadMainMenu();
            }
            else if (choice.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            else
            {
                throw new ArgumentException("Invalid choice.Try again!");
            }
        }

        // helper methods
        private static List<Questions.Question> InitializeQuestions(string file)
        {
            // read text file
            StreamReader reader = new StreamReader(file);
            StringBuilder text = new StringBuilder();
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    text.Append(line);
                    text.Append(Environment.NewLine);
                    line = reader.ReadLine();
                }
            }

            // parse text to questions
            string[] questions = text.ToString().Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries);

            List<Question> questionsList = new List<Question>();
            for (int i = 0; i < questions.Length; i++)
            {
                string[] currentQuestion = questions[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                string questionText = currentQuestion[0];
                string answersStr = currentQuestion[1];
                int indexRightQuestion = int.Parse(currentQuestion[2]);

                string[] answers = answersStr.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                Question question = new Question(questionText, answers, indexRightQuestion - 1);

                questionsList.Add(question);
            }

            return questionsList;
        }

        private static void LoadImage(string filepath)
        {
            // Read from file
            string[] lines = File.ReadAllLines(filepath);

            Console.ForegroundColor = ConsoleColor.Red;

            ConsolePrintText.Print(lines);
        }

        private bool CheckForWinner()
        {
            bool isWinner = false;

            if (player.Score == GameConstants.MaxScore)
            {
                isWinner = true;
            }

            return isWinner;
        }

        public void PlayWinSound()
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(GameConstants.SoundWin))
            {
                // Use PlaySync to load and then play the sound.
                // The program will pause until the sound is complete.
                soundPlayer.PlaySync();
            }
        }

        public void PlayCorrectSound()
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(GameConstants.SoundCorrect))
            {
                // Use PlaySync to load and then play the sound.
                // The program will pause until the sound is complete.
                soundPlayer.PlaySync();
            }
        }

        public void PlayWrongSound()
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(GameConstants.SoundWrong))
            {
                // Use Play to bot wait too much time until the sound is complete and load next question
                soundPlayer.Play();
            }
        }

        public void PlayStartSound()
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(GameConstants.SoundStart))
            {
                // Use PlaySync to load and then play the sound.
                // The program will pause until the sound is complete.
                soundPlayer.PlaySync();
            }
        }
    }
}
