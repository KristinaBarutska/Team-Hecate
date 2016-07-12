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
    using HecateExceptions;
    using Questions.Contracts;
    using Levels;

    public class GameWithLevels : IGame, ISound
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
        private static readonly GameWithLevels GameInstance = null;
        private static IList<IQuestion> questions;
        private static Player player;
        private static int wrongAnswers;
        private static bool isSkippedQuestion;
        private static bool isUnlockJoker;
        private static int availableJokersCount;
        private static Level currentLevel;

        static GameWithLevels()
        {
            // create the instance only if the instance is null
            GameInstance = new GameWithLevels();
        }

        public static GameWithLevels GetInstance()
        {
            // return the already existing instance
            return GameInstance;
        }

        // singleton pattern
        // private constructor to restrict the game creation from outside
        private GameWithLevels()
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

            //initialize questions from class Level
            currentLevel = new Level1();
            questions = currentLevel.GetLevelQuestionsFromList();

            //questions = Game.InitializeQuestions(GameConstants.FileQuestions);
            //questions = Game.InitializeQuestionsWithLevels(GameConstants.FileQuestions);
            wrongAnswers = 0;
            availableJokersCount = 3;

            this.LoadMainMenu();
        }

        public void ResetGameAttributes()
        {
            //hide the jokers in the beginning of the game
            //initialize again the questions deleted by FiftyFity joker
            isUnlockJoker = false;
            ClearJokers();
            currentLevel = new Level1();
            questions = currentLevel.GetLevelQuestionsFromList();
            //questions = Game.InitializeQuestionsWithLevels(GameConstants.FileQuestions);
            availableJokersCount = 3;
        }

        public void PlayGame()
        {
            Console.Clear();
            ResetGameAttributes();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Black;

            // 17.6.2016, Kristina. Добавена е проверка за коректност на отговора
            for (int i = 0; i < questions.Count; i++)
            {
                char answer;
                bool flag = false;
                Question currentQuestion = (Question)questions[i];
                
                // use infinitely loop because of jokers
                while (true)
                {
                    Console.Clear(); // clear console

                    Console.WriteLine(currentQuestion);
                    Console.WriteLine(currentQuestion.PrintAnswers(flag)); // print answers

                    //check for level
                    if (currentLevel.GetType().Name.Equals("Level2"))
                    //if (currentLevel is Level2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (availableJokersCount > 0)
                        {
                            Console.WriteLine("BONUS!You unlock the jokers!");
                            Console.WriteLine("You have {0} availabale jokers!", availableJokersCount);
                        }
                        else
                        {
                            Console.WriteLine("You don't have jokers anymore!");
                        }

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        //QuestionLevel2 currentQuestionLevel2 = currentQuestion as QuestionLevel2;
                        isUnlockJoker = true;
                    }

                    if (currentLevel.GetType().Name.Equals("Level3"))
                    //if (currentLevel is Level3)
                    {
                        //QuestionLevel3 currentQuestionLevel3 = currentQuestion as QuestionLevel3;

                        if (!isSkippedQuestion)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("BONUS!You can skip one question!");
                            Console.ForegroundColor = ConsoleColor.Magenta;

                            Console.Write("Do you want to skip this question - y/n :");
                            var skipChoice = Char.Parse(Console.ReadLine());

                            if (skipChoice == 'y' || skipChoice == 'Y')
                            {
                                Console.Clear();
                                isSkippedQuestion = true;
                                Question nextQuestion = currentQuestion.SkipQuestion(currentQuestion, questions);
                                currentQuestion = nextQuestion;
                                
                                //print next question
                                Console.Clear(); //clear console
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Next question :");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine(currentQuestion);
                                Console.WriteLine(currentQuestion.PrintAnswers(flag)); //print answers
                            }
                        }
                    }

                    if (isUnlockJoker)
                    {
                        this.OfferJoker(); // Print jokers
                    }

                    //timer for answer
                    //answer = DisplayTime.CreateTimer(currentQuestion.TimerSeconds);
                    answer = DisplayTime.CreateTimer(currentLevel.TimerSeconds);

                    // answer = Char.Parse(Console.ReadLine()); take char answer

                    // chek for use joker
                    if (answer > '0' && answer <= '3')
                    {
                        // for print only two answers when use FiftyFifty joker or print another joker
                        flag = this.UseJoker(answer, currentQuestion.RightAnswerIndex, currentQuestion.Answers);
                        availableJokersCount--;
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

                QuestionChecker checker = new QuestionChecker(currentQuestion, answer);
                if (checker.Tell())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Your answer is true");
                    this.PlayCorrectSound();

                    // Add 100 scores if the answaer is right
                    //player.Score += currentQuestion.QuestionScore;
                    player.Score += currentLevel.QuestionScore;

                    Console.WriteLine("SCORE : {0} ", player.Score);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Thread.Sleep(500); // white because of information
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You are wrong");
                    this.PlayWrongSound();
                    wrongAnswers++;
                    
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Thread.Sleep(500); // white because of information

                    // game over if 5 wrong questions
                    if (wrongAnswers == GameConstants.MaxWrongAnswers)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(String.Format("You have {0} wrong answers !",GameConstants.MaxWrongAnswers));
                        Thread.Sleep(500);
                        EndGame();
                        //break;
                    }
                }
                if (isSkippedQuestion)
                {
                    currentQuestion = (Question)questions[i++];
                }
                // Край на промените на Кристина

                //if last question in level -> change the level
                if (i == questions.Count - 1)
                {
                    if (currentLevel.GetType().Name.Equals("Level1"))
                    {
                        currentLevel = new Level2();
                        questions.Clear();
                        questions = currentLevel.GetLevelQuestionsFromList();
                        i = -1;
                    }
                    else if (currentLevel.GetType().Name.Equals("Level2"))
                    {
                        //apply reduce coefficient for level2 if wrong answers are 3
                        if (wrongAnswers == GameConstants.MaxWrongAnswersForReduceScore)
                        {
                            ((Level2)currentLevel).ReduceScoresLevel2Coefficient = wrongAnswers;
                            player.Score = ((Level2)currentLevel).ApplyReduceScores(player);

                            Console.WriteLine(String.Format("You have {0} wrong answers !", GameConstants.MaxWrongAnswersForReduceScore));
                            Thread.Sleep(500);
                            Console.WriteLine(String.Format("You scores are reduced by {0}!", GameConstants.MaxWrongAnswersForReduceScore));
                            Console.WriteLine("SCORE : " + player.Score);
                        }
                        currentLevel = new Level3();
                        questions.Clear();
                        questions = currentLevel.GetLevelQuestionsFromList();
                        i = -1;
                    }
                }
            }
            if (currentLevel.GetType().Name.Equals("Level3"))
            //if (currentLevel is Level3)
            {
                //apply bonus coefficient for level3 if wrong answers are 0
                if (wrongAnswers == 0 && isSkippedQuestion)
                {
                    player.Score = ((Level3)currentLevel).ApplyBonusScores(player);

                    Console.WriteLine("You have no wrong answers!");
                    Thread.Sleep(500);
                    Console.WriteLine("Your scores are multiplyed by 2!");
                    Console.WriteLine("SCORE : " + player.Score);
                }
                this.EndGame();
            }
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

        private void ClearJokers()
        {
            var listJokers = player.Jokers;
            for (int i = 0; i < listJokers.Count; i++)
            {
                listJokers[i].IsUsed = false;
            }


        }

        public bool UseJoker(char answer, int rithAnswerIndex, string[] answersOfQuestion)
        {
            bool flag = false; // for print only two answers when use FiftyFifty joker

            try
            {
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
                        if (player.SelectJoker(JokerType.HelpFromPublic))
                        {
                            var fiftyFifty = player.Jokers[0]; // if used FiftyFifty joker

                            HelpFromPublicJoker help = new HelpFromPublicJoker(JokerType.HelpFromPublic);
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
            }
            catch (InvalidSecondChoiceJokerException isje)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(isje.Message);
                Console.ForegroundColor = ConsoleColor.Magenta;
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
            }
            else
            {
                string textLose = "Do you want to try another game?\n";

                Console.Clear();
                LoadImage(GameConstants.FileGameOver);
                this.PlayGameOverSound();

                var currentCol = (Console.WindowWidth / 2) - (textLose.Length / 2) - 5;
                Console.Write(new string(' ', currentCol));

                // Console.WriteLine("\n\tDo you want to try another game?\n");
                Console.WriteLine("You have win {0} lv but you're not THE CHAMPION", player.Score);
                currentCol = (Console.WindowWidth / 2) - (textLose.Length / 2);
                Console.Write(new string(' ', currentCol));
                Console.WriteLine(textLose);
            }
            player.GameOver();

            this.LoadMainMenu();
        }

        public void ShowStatistics()
        {
            // print players results
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;

            TheBestThreePlayers.ShowPlayerResultFileRekord(player.Name);
            Console.WriteLine();
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
                "SHOW BEST PLAYERS ?  =>>\n",
                "EXIT ?  =>>\n"
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
                    string playerName = string.Empty;
                    do
                    {
                        //if this is the first game of this player
                        //set player and start the game
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Player name must be at least 4 symbols\n");
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("What's your name?  =>>");
                        Console.BackgroundColor = ConsoleColor.Black;
                        playerName = Console.ReadLine();

                    } while (playerName.Length < 4);

                    player.Name = playerName;
                    PlayGame();
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

            if (player.Score == this.GetGameMaxScore())
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

        private int GetGameMaxScore()
        {
            int maxScore = (GameConstants.QuestionScoreLevel1 + GameConstants.QuestionScoreLevel2 + 
                GameConstants.QuestionScoreLevel3) * GameConstants.NumberOfQuestionPerLevel * GameConstants.BonusChampionCoefficient;
                
              
            return maxScore;
        }
    }
}
