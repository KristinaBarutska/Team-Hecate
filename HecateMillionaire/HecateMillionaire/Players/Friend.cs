namespace HecateMillionaire.Players
{
    using System;

    class Friend
    {
        private string name;

        public Friend(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Friend name can't be empty");

                }
                else
                {
                    this.name = value;
                }
            }
        }

        public string Respond(bool flag, string[] answers)
        {
            //generate response
            //use joker call friend
            string[] responses = new string[] {"Sorry,I don't know the answer but you can try ", "I'm sure the correct answer is ",
                                                "I'm not sure but I think the answer is "};

            string response = "";

            //if used FiftyFifty joker
            if (flag)
            {
                var indexForPrintAnswer = 0;
                Random random = new Random();

                while (true)
                {
                    var index = random.Next(0, 3);
                    if (answers[index] != "")
                    {
                        indexForPrintAnswer = index;
                        break;
                    }
                }

                var charsAnswer = new[] { 'A', 'B', 'C', 'D' };
                string randomResponse = responses[random.Next(2)];
                char randomIndex = charsAnswer[indexForPrintAnswer];
                response = string.Format("{0}{1}", randomResponse, randomIndex);
            }
            else
            {

                var charsAnswer = new[] { 'A', 'B', 'C', 'D' };
                Random random = new Random();
                string randomResponse = responses[random.Next(2)];
                char randomIndex = charsAnswer[random.Next(3)];
                response = string.Format("{0}{1}", randomResponse, randomIndex);
            }

            return response;
        }
    }
}
