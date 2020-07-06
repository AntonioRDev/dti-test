using ProvaDTI.Databases;

namespace ProvaDTI
{
    class Program
    {
        static void Main(string[] args)
        {
            //var foodGroups = FakeFoodsDatabase.DietCombination(300);
            int userInput;
            var clinic = new Clinic();

            do
            {
                userInput = clinic.DisplayMenu();
            } while (userInput != 9);
        }
    }
}
