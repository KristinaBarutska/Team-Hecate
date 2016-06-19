namespace HecateMillionaire.Players
{
    using Common;
    using System;

    class Person
    {
        private string name;

        public string Name
        {
            get
            {
                return this.name;
            }
            protected set
            {
                if (value.Length < 4)
                {
                    throw new ArgumentException(GlobalErrorMessages.InvalidPlayerNameErrorMessage);
                }
                else
                {
                    this.name = value;
                }
            }
        }
    }
}
