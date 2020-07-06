namespace ProvaDTI
{
    class Program
    {
        static void Main(string[] args)
        {
            int userInput;
            var clinic = new Clinic();

            do
            {
                userInput = clinic.DisplayMenu();
            } while (userInput != 9);
        }
    }
}
