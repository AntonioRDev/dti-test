using ProvaDTI.Models;
using ProvaDTI.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ProvaDTI.Databases
{
    public static class FakeFoodsDatabase
    {
        private static List<Food> Foods { get; set; } = new List<Food>();
        static FakeFoodsDatabase()
        {
            // Popula a base
            Food food1 = new Food { Name = "Açúcar", FoodGroup = FoodGroup.Açúcares, CaloricAmount = 387, Portion = "100 g" };
            Food food2 = new Food { Name = "Mel", FoodGroup = FoodGroup.Açúcares, CaloricAmount = 304, Portion = "100 g" };
            Food food3 = new Food { Name = "Ovo", FoodGroup = FoodGroup.CarnesEovos, CaloricAmount = 74, Portion = "1 unidade" };
            Food food4 = new Food { Name = "Coxa de frango", FoodGroup = FoodGroup.CarnesEovos, CaloricAmount = 144, Portion = "100 g" };
            Food food5 = new Food { Name = "Aveia em flocos", FoodGroup = FoodGroup.Cereais, CaloricAmount = 354, Portion = "100 g" };
            Food food6 = new Food { Name = "Pão francês", FoodGroup = FoodGroup.Cereais, CaloricAmount = 137, Portion = "1 unidade" };
            Food food7 = new Food { Name = "Abacate", FoodGroup = FoodGroup.Frutas, CaloricAmount = 180, Portion = "100 g" };
            Food food8 = new Food { Name = "Banana ouro", FoodGroup = FoodGroup.Frutas, CaloricAmount = 112, Portion = "1 unidade" };
            Food food9 = new Food { Name = "Tomate", FoodGroup = FoodGroup.Hortaliças, CaloricAmount = 24, Portion= "1 tomate médio" };
            Food food10 = new Food { Name = "Brócolis", FoodGroup = FoodGroup.Hortaliças, CaloricAmount = 54, Portion= "1 xícara" };
            Food food11 = new Food { Name = "Feijão Carioca Cozido", FoodGroup = FoodGroup.Leguminosas, CaloricAmount = 76, Portion = "100 g" };
            Food food12 = new Food { Name = "Grãos de soja", FoodGroup = FoodGroup.Leguminosas, CaloricAmount = 446, Portion = "100 g" };
            Food food13 = new Food { Name = "Leite, 1% de gordura", FoodGroup = FoodGroup.Leite, CaloricAmount = 42, Portion = "100 g" };
            Food food14 = new Food { Name = "Leite em pó", FoodGroup = FoodGroup.Leite, CaloricAmount = 496, Portion = "100 g" };
            Food food15 = new Food { Name = "Óleo vegetal", FoodGroup = FoodGroup.Óleos, CaloricAmount = 884, Portion = "100 g" };
            Food food16 = new Food { Name = "Azeite de oliva", FoodGroup = FoodGroup.Óleos, CaloricAmount = 884, Portion = "100 g" };

            var foods = new Food[16] { food1, food2, food3, food4, food5, food6, food7,
                                        food8, food9, food10, food11, food12, food13, food14, food15, food16 };

            Foods.AddRange(foods);
        }

        public static List<List<Food>> DietCombination(double caloricGoal)
        {
            var dietFoods = new List<List<Food>>();
            var caloricCount = caloricGoal;

            for (int i = 0; i < Foods.Count; i++)
            {
                for(int j= i+1; j < Foods.Count; j++)
                {
                    dietFoods.Add(new List<Food>(new Food[] { Foods[i] }));

                    double caloricGroupCount = 0;
                    dietFoods.Last().ForEach(food => caloricGroupCount += food.CaloricAmount);

                    if(caloricGroupCount + Foods[j].CaloricAmount <= caloricGoal && Foods[i].FoodGroup != Foods[j].FoodGroup)
                    {
                        dietFoods.Last().Add(Foods[j]);
                    }
                }
            }

            foreach(var foodGroup in dietFoods)
            {
                foreach(var food in Foods)
                {
                    if (foodGroup.Count == 3)
                        break;

                    double caloricGroupCount = 0;
                    bool isDifferentGroup = true;

                    foodGroup.ForEach(f => {
                        caloricGroupCount += f.CaloricAmount;

                        if (f.FoodGroup == food.FoodGroup)
                            isDifferentGroup = false;
                    });

                    if (caloricGroupCount + food.CaloricAmount <= caloricGoal && isDifferentGroup)
                        foodGroup.Add(food);
                }
            }

            // Remove os grupos com menos de 3 alimentos
            var resultDietFoods = new List<List<Food>>(dietFoods);
            for (int n = 0; n < dietFoods.Count; n++)
            {
                if(dietFoods[n].Count < 3)
                {
                    resultDietFoods.Remove(dietFoods[n]);
                }
            }

            return resultDietFoods;
        }
    }
}
