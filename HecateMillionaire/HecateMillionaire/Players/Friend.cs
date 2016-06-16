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

        public string Respond()
        {
            //generate response
            //use joker call friend
            string[] responses = new string[] {"Sorry,I don't know the answer but you can try ", "I'm sure the correct answer is", 
                                                "I'm not sure but I think the answer is"};
            Random random = new Random();
            string randomResponse = responses[random.Next(2)];
            int randomIndex = random.Next(3);
            string response = randomResponse + randomIndex;

            return response;
        }
    }
}
