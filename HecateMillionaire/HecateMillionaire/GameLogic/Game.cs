namespace HecateMillionaire.GameLogic
{
    using Contracts;
    using System.Text;
    using System.IO;
    using System.Collections.Generic;
    using System.Threading;
    using System.Media;
    using System;
    using Players;
    using Questions;
    using WorkWithFile;
    using Jokers;
    using HecateMillionaire.BaseTable;

    public class Game : IGame, ISound
    {
        //start game - method to be called from Main
        //ask for player's name and color
        //initilize game - create player, load questions
        //play game - show the question, ask the player for his choice, set timer
        //check if it's correct, if it's not - game over, otherwise ask next question
        //game offers a joker - if player can't answer in time ?
        //end of game - show player's scores, show players statistics 
        //ask for new game

        private static List<Question> questions;
        private static Player player;
        private static int wrongAnswers;

        //singleton pattern
        //private constructor to restrict the game creation from outside
        private Game() { }

        //private static instance of the same class
        private static readonly Game gameInstance = null;

        static Game()
        {
            //create the instance only if the instance is null
            gameInstance = new Game();
        }
        public static Game GetInstance()
        {
            // return the already existing instance
            return gameInstance;
        }

        //methods from IGame
        public void StartGame()
        {
            //load game logo and menu
            InitiliazeGame();
        }

        public void InitiliazeGame()
        {
            //setup console
            Console.OutputEncoding = Encoding.UTF8;

            Console.Title = "~ Hecate Millionaire ~";

            //load game image and sound
            LoadImage(GameConstants.FILE_HECATE_START);
            playStartSound();

            //initialize player and questions
            player = new Player();
            questions = Game.InitializeQuestions(GameConstants.FILE_QUESTIONS);
            wrongAnswers = 0;

            LoadMainMenu();
        }

        public void PlayGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Black;

            //17.6.2016, Kristina. Добавена е проверка за коректност на отговора
            for (int i = 0; i < questions.Count; i++)
            {
                char answer;
                bool flag = false;

                //use infinitely loop because of jokers
                while (true)
                {

                    Console.Clear(); //clear console

                    Console.WriteLine(questions[i]);
                    Console.WriteLine(questions[i].PrintAnswers(flag)); //print answers

                    //TODO ADD TIMER ? 
                    //TODO - да преместим проверката в метод на класа ?


                    OfferJoker(); //Print jokers
                    answer = DisplayTime.CreateTimer();
                    //answer = Char.Parse(Console.ReadLine()); //take char answer

                    //chek for use joker
                    if (answer > '0' && answer <= '3')
                    {
                        //for print only two answers when use FiftyFifty joker or print another joker
                        flag = UseJoker(answer, questions[i].RightAnswerIndex, questions[i].Answers);
                    }
                    else
                    {
                        break;
                    }
                }
                //

                if (answer == default(char))
                {
                    continue;
                }

                IsRight check = new IsRight(questions[i], answer);
                if (check.Tell())
                {
                    Console.WriteLine("Your answer is true");
                    playCorrectSound();
                    //Add 100 scores if the answaer is right
                    player.Score += questions[i].QuestionScore;
                    Console.WriteLine("SCORE : {0} ", player.Score);
                    Thread.Sleep(1000); //white because of information

                }
                else
                {
                    Console.WriteLine("You are wrong");
                    playWrongSound();
                    wrongAnswers++;
                    Thread.Sleep(1000); //white because of information

                    //game over if 3 wrong questions
                    if (wrongAnswers == GameConstants.MAX_NUMBER_WRONG_ANSWERS)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have 3 wrong answers !");
                        Thread.Sleep(1000);
                        break;
                    }
                }
                //Край на промените на Кристина
            }
            EndGame();
        }

        public bool CheckPlayerAnswer(char answer) { return false; }

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

            bool flag = false; //for print only two answers when use FiftyFifty joker

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
                        var fiftyFifty = player.Jokers[0]; //if used FiftyFifty joker

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
                        var fiftyFifty = player.Jokers[0]; //if used FiftyFifty joker

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
            if (CheckForWinner())
            {
                Console.Clear();
                LoadImage(GameConstants.FILE_CHAMPION);
                playWinSound();
                Console.WriteLine("\n\tYOU'RE A HECATE MILIONAIRE ! - You have {0} lv\n", player.Score );

                //save record and name in file when game over 
                player.GameOver();
            }
            else
            {
                Console.Clear();
                LoadImage(GameConstants.FILE_GAME_OVER);
                playGameOverSound();

                Console.WriteLine("\tDo you want to try another game?\n");
            }
            LoadMainMenu();

        }

        public void ShowStatistics()
        {
            //print players results
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;

            TheBestThreePlayers.Show(player.Name);
        }

        public void RestartGame()
        {
            StartGame();
        }

        private void LoadMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\tSTART NEW GAME ?  =>> \n");
            Console.WriteLine("\tSHOW BEST PLAYERS ?  =>>\n");
            Console.WriteLine("\tEXIT ?  =>>\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();

            Console.WriteLine("\tPress 'Enter' => for restart and play a new game\n\tPress 'Space' for close the game and see the result\n\tPress 'Esc' to close the game.");

            var choice = Console.ReadKey();

            if (choice.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                
                //the player don't plays for first time
                if (!player.Name.Equals("Player"))
                {
                    player.Score = 0;
                    PlayGame();
                }
                else
                {
                    //if this is the first game of this player
                    //set player and start the game
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine("What's your name?  =>>");
                    string playerName = Console.ReadLine();
                    player.Name = playerName;
                    PlayGame();
                }             
            }
            else if (choice.Key == ConsoleKey.Spacebar)
            {
                //show best players
                ShowStatistics();
                LoadMainMenu();
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

        //helper methods

        private static List<Questions.Question> InitializeQuestions(string file)
        {
            //read text file
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

            //parse text to questions
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
            //Read from file
            string[] lines = File.ReadAllLines(filepath);

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Console.WriteLine("\t" + line);
            }
        }

        private bool CheckForWinner()
        {
            bool isWinner = false;

            if (player.Score == GameConstants.MAX_SCORE)
            {
                isWinner = true;
            }
            return isWinner;
        }


        //methods from ISound
        public void playGameOverSound()
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(GameConstants.SOUND_GAMEOVER))
            {
                // Use PlaySync to load and then play the sound.
                // The program will pause until the sound is complete.
                soundPlayer.PlaySync();
            }
        }

        public void playWinSound()
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(GameConstants.SOUND_WIN))
            {
                // Use PlaySync to load and then play the sound.
                // The program will pause until the sound is complete.
                soundPlayer.PlaySync();
            }
        }

        public void playCorrectSound()
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(GameConstants.SOUND_CORRECT))
            {
                // Use PlaySync to load and then play the sound.
                // The program will pause until the sound is complete.
                soundPlayer.PlaySync();
            }
        }

        public void playWrongSound()
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(GameConstants.SOUND_WRONG))
            {
                // Use Play to bot wait too much time until the sound is complete and load next question
                soundPlayer.Play();
            }
        }

        public void playStartSound()
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(GameConstants.SOUND_START))
            {
                // Use PlaySync to load and then play the sound.
                // The program will pause until the sound is complete.
                soundPlayer.PlaySync();
            }
        }
    }
}
