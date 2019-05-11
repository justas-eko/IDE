namespace IDE
{
    class IDEUtils
    {
        public string RemoveSpaces(string inputLine)
        {
            string s2 = inputLine;
            do
            {
                inputLine = s2;
                s2 = s2.Replace("  ", " ");
            } while (inputLine != s2);

            return inputLine;
        }

        public int ValidateInt(string input)
        {

            if (!int.TryParse(input, out int x))
            {
                throw new System.ArgumentException("Incorrect input!");
            }


            return x;
        }
    }
}
