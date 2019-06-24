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
            int[] calorie = new int[fat.Length];
            int[] meal = new int[dietPlans.Length];
            
            for (int i = 0; i < fat.Length; i++)
            {
                calorie[i] = protein[i]*5 + carbs[i] * 5 + fat[i] * 9;
            }
            
            for (int i = 0; i < dietPlans.Length; i++)
            {
                List<int> al = new List<int>();
                List<int> l = new List<int>();

                foreach (char x in dietPlans[i])
                {
                    
                    if (al.Count() == 0)
                    {
                        switch (x)
                        {
                            case 'P':
                                al = Max(protein);
                                break;
                            case 'F':
                                al = Max(fat);
                                break;
                            case 'C':
                                al = Max(carbs);
                                break;
                            case 'T':
                                al = Max(calorie);
                                break;
                            case 'p':
                                al = Min(protein);
                                break;
                            case 'f':
                                al = Min(fat);
                                break;
                            case 'c':
                                al = Min(carbs);
                                break;
                            case 't':
                                al = Min(calorie);
                                break;                            
                            default :                                
                                break;
                        }
                    }

                    else
                    {
                        switch (x)
                        {
                            case 'P':
                                al = Tmax(protein,al);
                                break;
                            case 'F':
                                al = Tmax(fat,al);
                                break;
                            case 'C':
                                al = Tmax(carbs,al);
                                break;
                            case 'T':
                                al = Tmax(calorie,al);
                                break;
                            case 'p':
                                al = Tmin(protein,al);
                                break;
                            case 'f':
                                al = Tmin(fat,al);
                                break;
                            case 'c':
                                al = Tmin(carbs,al);
                                break;
                            case 't':
                                al = Tmin(calorie,al);
                                break;
                            default:
                                break;
                        }
                    }
                    
                    int a = al.Count();
                                                            
                    if (a > 0)
                    {
                        meal[i] = al[0];
                        l.Add(al[0]);
                    }
                       
                    else
                        meal[i] = l[0];
                }
            }
            
            return meal;
            throw new NotImplementedException();
        }

        
        //To calculate the minimum value of any one parameter i.e. proteins, fat etc.
        public static List<int> Min(int[] a)
        {
            int min = 999;
            List<int> l = new List<int>();
            for(int i = 0; i < a.Length; i++)
            {
                if (a[i] < min)
                    min = a[i];             
            }
            for(int i = 0; i < a.Length; i++)
            {
                if (a[i] == min)
                    l.Add(i);
            }
            return l;
        }

        
        //To calculate the maximum value of any one parameter i.e. proteins, fat etc.
        public static List<int> Max(int[] a)
        {
            int max = -999;
            List<int> l = new List<int>();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > max)
                    max = a[i];
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == max)
                    l.Add(i);
            }
            return l;
        }

        
        //To calculate the minimum value of any one parameter i.e. proteins, fat etc within the limited meals.
        public static List<int> Tmin(int[] a, List<int> b)
        {
            int min = 999;
            List<int> l = new List<int>();
            foreach (int i in b)
            {
                if (a[i] < min)
                    min = a[i];
            }
            foreach (int i in b)
            {
                if (a[i] == min)
                    l.Add(i);
            }
            return l;
        }

        
        //To calculate the maximum value of any one parameter i.e. proteins, fat etc within the limited meals.
        public static List<int> Tmax(int[] a, List<int> b)
        {
            int max = -999;
            List<int> l = new List<int>();
            foreach (int i in b)
            {
                if (a[i] > max)
                    max = a[i];
            }
            foreach (int i in b)
            {
                if (a[i] == max)
                    l.Add(i);
            }
            return l;
        }
    }
}
