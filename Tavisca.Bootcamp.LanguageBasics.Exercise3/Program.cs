using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            List<int> mealList, tempList;
            int[] calorie = new int[fat.Length];
            int[] meal = new int[dietPlans.Length];
            int length;
            
            for (int index = 0; index < fat.Length; index++)  //calculating calorie array
            {
                calorie[index] = protein[index]*5 + carbs[index] * 5 + fat[index] * 9;
            }
            
            for (int index = 0; index < dietPlans.Length; index++)
            {
                 mealList = new List<int>();
                 tempList = new List<int>();

                foreach (char foodType in dietPlans[index])
                {
                    
                    if (mealList.Count() == 0)   //to check the food without any ties
                    {
                        switch (foodType)
                        {
                            case 'P':
                                mealList = Max(protein);
                                break;
                            case 'F':
                                mealList = Max(fat);
                                break;
                            case 'C':
                                mealList = Max(carbs);
                                break;
                            case 'T':
                                mealList = Max(calorie);
                                break;
                            case 'p':
                                mealList = Min(protein);
                                break;
                            case 'f':
                                mealList = Min(fat);
                                break;
                            case 'c':
                                mealList = Min(carbs);
                                break;
                            case 't':
                                mealList = Min(calorie);
                                break;                            
                            default :                                
                                break;
                        }
                    }

                    else                      //to check the food when tie between two meals occur
                    {
                        switch (foodType)
                        {
                            case 'P':
                                mealList = TieMax(protein,mealList);
                                break;
                            case 'F':
                                mealList = TieMax(fat, mealList);
                                break;
                            case 'C':
                                mealList = TieMax(carbs, mealList);
                                break;
                            case 'T':
                                mealList = TieMax(calorie, mealList);
                                break;
                            case 'p':
                                mealList = TieMin(protein, mealList);
                                break;
                            case 'f':
                                mealList = TieMin(fat, mealList);
                                break;
                            case 'c':
                                mealList = TieMin(carbs, mealList);
                                break;
                            case 't':
                                mealList = TieMin(calorie, mealList);
                                break;
                            default:
                                break;
                        }
                    }

                    
                    length = mealList.Count();
                                                            
                    if (length > 0)
                    {
                        meal[index] = mealList[0];
                    
                        tempList.Add(mealList[0]);
                    }
                       
                    else
                        meal[index] = tempList[0];


                }
            }
            return meal;
            
        }

        public static List<int> Min(int[] diet)
        {
            int minValue = 999;
            List<int> mealList = new List<int>();
            for(int index = 0; index < diet.Length; index++)
            {
                if (diet[index] < minValue)
                    minValue = diet[index];             
            }
            for(int index = 0; index < diet.Length; index++)
            {
                if (diet[index] == minValue)
                    mealList.Add(index);
            }
            return mealList;
        }

        public static List<int> Max(int[] diet)
        {
            int maxValue = -999;
            List<int> mealList = new List<int>();
            for (int index = 0; index < diet.Length; index++)
            {
                if (diet[index] > maxValue)
                    maxValue = diet[index];
            }
            for (int index = 0; index < diet.Length; index++)
            {
                if (diet[index] == maxValue)
                    mealList.Add(index);
            }
            return mealList;
        }

        public static List<int> TieMin(int[] diet, List<int> meal)
        {
            int minValue = 999;
            List<int> mealList = new List<int>();
            foreach (int index in meal)
            {
                if (diet[index] < minValue)
                    minValue = diet[index];
            }
            foreach (int index in meal)
            {
                if (diet[index] == minValue)
                    mealList.Add(index);
            }
            return mealList;
        }

        public static List<int> TieMax(int[] diet, List<int> meal)
        {
            int maxValue = -999;
            List<int> mealList = new List<int>();
            foreach (int index in meal)
            {
                if (diet[index] > maxValue)
                    maxValue = diet[index];
            }
            foreach (int index in meal)
            {
                if (diet[index] == maxValue)
                    mealList.Add(index);
            }
            return mealList;
        }
    }
}
