namespace GrandeGifts.Helpers
{
    public class TextFormatter
    {
        public string RemoveDoubleSpaces(string input)
        {
            char[] inputToCharArray = input.ToCharArray();
            string output = "";

            int spaceCounter = 0;
            int i = 0;

            while (i < inputToCharArray.Length)
            {
                if (inputToCharArray[i] != ' ')
                {
                    spaceCounter = 0;
                    output += inputToCharArray[i];
                    i++;
                }
                else
                {
                    spaceCounter++;
                    if (spaceCounter <= 1)
                    {
                        output += inputToCharArray[i];
                    }
                    i++;
                }
            }
            return output;
        }

        public string CapitaliseFirstLetters(string input, bool setAllElseToLowerCase)
        {
            char[] inputToCharArray = input.ToCharArray();
            string output = "";

            bool lastCharWasASpace = true;
            int i = 0;

            while (i < inputToCharArray.Length)
            {
                if (inputToCharArray[i] == ' ')
                {
                    lastCharWasASpace = true;
                    output += inputToCharArray[i];
                    i++;
                }
                else
                {
                    if (lastCharWasASpace)
                    {
                        lastCharWasASpace = false;
                        output += inputToCharArray[i].ToString().ToUpper();
                        i++;
                    }
                    else
                    {
                        lastCharWasASpace = false;
                        if (setAllElseToLowerCase)
                        {
                            output += inputToCharArray[i].ToString().ToLower();
                        }
                        else
                        {
                            output += inputToCharArray[i];
                        }
                        i++;
                    }
                }
            }
            return output;
        }

        public string RemoveAllCharsExceptNumbers(string input)
        {
            char[] inputToCharArray = input.ToCharArray();
            string output = "";

            int i = 0;

            while (i < inputToCharArray.Length)
            {
                if (char.IsDigit(inputToCharArray[i]))
                {
                    output += inputToCharArray[i];
                }
                i++;
            }
            return output;
        }

        public string RemoveAllNumbersFromString(string input)
        {
            char[] inputToCharArray = input.ToCharArray();
            string output = "";

            int i = 0;

            while (i < inputToCharArray.Length)
            {
                if (!char.IsDigit(inputToCharArray[i]))
                {
                    output += inputToCharArray[i];
                }
                i++;
            }
            return output;
        }

        public string TruncateString(string input, int outputLength)
        {
            char[] inputToCharArray = input.ToCharArray();
            string output = "";

            int i = 0;

            while (i < outputLength)
            {
                output += inputToCharArray[i];
                i++;
            }
            return output;
        }

        public string TruncateString(string input, int outputLength, string stringToIncludeAtEnd)
        {
            char[] inputToCharArray = input.ToCharArray();
            string output = "";

            int i = 0;

            while (i < outputLength)
            {
                output += inputToCharArray[i];
                i++;
            }
            output += stringToIncludeAtEnd;
            return output;
        }
    }
}
