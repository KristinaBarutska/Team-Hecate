namespace HecateMillionaire.GameLogic
{
    using Contracts;
    using System.Text;
    using System.IO;
    using System.Collections.Generic;
    using System.Threading;
    using System;
    using Players;
    using Questions;
    using WorkWithFile;
    using Jokers;

    public class Game : IGame
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
            //initiliaze questions and player
            InitiliazeGame();
            PlayGame();
        }

        public void InitiliazeGame()
        {
            //setup console
            Console.OutputEncoding = Encoding.UTF8;

            //load game image
            LoadImage(GameConstants.FILE_HECATE_START);


            //initialize player
            //TODO - add player color
            Console.Title = "~ Hecate Millionaire ~";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("What's your name?  =>>");
            string playerName = Console.ReadLine();
            player = new Player(playerName);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Black;
            //initialize questions
            questions = Game.InitializeQuestions(GameConstants.FILE_QUESTIONS);

        }

        public void PlayGame()
        {
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

                    answer = Char.Parse(Console.ReadLine()); //take char answer

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

                IsRight check = new IsRight(questions[i], answer);
                if (check.Tell())
                {
                    Console.WriteLine("Your answer is true");
                    //Add 100 scores if the answaer is right
                    player.Score += questions[i].QuestionScore;
                    Console.WriteLine("SCORE : {0} ", player.Score);
                    Thread.Sleep(1000); //white because of information

                }
                else
                {
                    Console.WriteLine("You are wrong");
                    Thread.Sleep(1000); //white because of information
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
                    System.Console.BackgroundColor = ConsoleColor.Red;
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

                        PublicHelp help = new PublicHelp();
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
                        Friend friend = new Friend("frienName");
                        System.Console.WriteLine(friend.Respond(fiftyFifty.IsUsed, answersOfQuestion));
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
                Console.WriteLine("\n\tYOU'RE A HECATE MILIONAIRE !");
                Console.WriteLine("\tYou have {0} lv", player.Score);

                //save record and name in file when game over 
                SaveInFile.SetFileRekord(player.Score, player.Name);
            }
            else
            {
                Console.Clear();
                LoadImage(GameConstants.FILE_GAME_OVER);
                //Console.WriteLine("GAME OVER !");
                Console.WriteLine("Do you want to try another game?");
                Console.WriteLine("Press 'Enter' => for restart and play a new game\n Press 'Space' for close the game and see the result\n Press 'Esc' to close the game.");

                var choice = Console.ReadKey();

                //TODO PLAYER CHOICE YES/NO
                //restart game or Bye
                if (choice.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    RestartGame();
                }
                else if (choice.Key == ConsoleKey.Spacebar)
                {
                    //show best players
                    ShowStatistics();
                    Console.WriteLine("\nBye!");
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


        }

        public void ShowStatistics()
        {
            //print players results
            TheBestThreePlayers.Show(player.Name);
        }

        public void RestartGame()
        {
            StartGame();
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

    }
}
