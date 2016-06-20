﻿namespace HecateMillionaire.GameLogic
{
    using Contracts;
    using System.Text;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
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

        public Game() { }

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
            System.Console.OutputEncoding = Encoding.UTF8;

            System.Console.Title = "Hey";
            //System.Console.BackgroundColor = ConsoleColor.DarkRed;
            //System.Console.SetCursorPosition(60, 0);
            //System.Console.WriteLine("Enter your choice! =>>");
            //System.Console.ForegroundColor = ConsoleColor.Cyan;

            //initialize player
            //TODO - add player color
            System.Console.WriteLine("What's your name =>>");
            string playerName = Console.ReadLine();
            player = new Player(playerName);

            System.Console.Clear(); //clear console

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

                while (true) //use infinitely loop because of jokers
                {

                    System.Console.Clear(); //clear console

                    System.Console.WriteLine(questions[i]);
                    System.Console.WriteLine(questions[i].PrintAnswers(flag)); //print answers


                    //TODO ADD TIMER ? 
                    //TODO - да преместим проверката в метод на класа ?

                    OfferJoker(); //Print jokers

                    answer = Char.Parse(System.Console.ReadLine()); //take char answer

                    //chek for use joker
                    if (answer > '0' && answer <= '3')
                    {

                        switch (answer)
                        {
                            case '1':
                                if (player.SelectJoker(JokerType.FiftyFifty))
                                {
                                    flag = true; //for print only two answers
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
                                    // TODO
                                }
                                else
                                {
                                    Thread.Sleep(1000);
                                }
                                
                                break;
                            case '3':
                                if (player.SelectJoker(JokerType.CallFriend))
                                {
                                    // TODO
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
                    else
                    {
                        break;
                    }
                }
                //

                IsRight check = new IsRight(questions[i], answer);
                if (check.Tell())
                {
                    System.Console.WriteLine("Your answer is true");
                    //Add 100 scores if the answaer is right
                    player.Score += questions[i].QuestionScore;
                    System.Console.WriteLine("SCORE : {0} ", player.Score);
                    Thread.Sleep(1000); //white because of information

                }
                else
                {
                    System.Console.WriteLine("You are wrong");
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
            System.Console.WriteLine("jokers:");

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

        public void EndGame()
        {
            if (CheckForWinner())
            {
                Console.WriteLine("YOU'RE A HECATE MILIONAIRE !");
                Console.WriteLine("You have {0} lv", player.Score);

                //save record and name in file when game over 
                SaveInFile.SetFileRekord(player.Score, player.Name);
            }
            else
            {
                Console.WriteLine("GAME OVER !");
                Console.WriteLine("Do you want to try another game");
                //TODO PLAYER CHOICE YES/NO
                //restart game or Bye

            }
            //show best players
            ShowStatistics();

        }

        public void ShowStatistics()
        {
            //print players results
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