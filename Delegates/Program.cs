using System.Reflection;

/*
    C# compiler creates instances of a class derived from MulticastDelegate
    when you use the C# language keyword to declare delegate types.
*/

/*
    Делегаты в C# - это тип данных, который позволяет передавать методы как аргументы другим методам,
    а также сохранять их в переменных для последующего вызова. 
*/

namespace Delegates1
{
    internal class Program
    {
        public delegate void MyDelegate();
        public delegate bool MyFileringAlgorithm(int element);

        static void MethodForDelegate()
        {
            Console.WriteLine("Hello from delegate");
        }

        static void Main(string[] args)
        {
            // как мы создаем делегаты?
            
            MyDelegate myDel = new MyDelegate(MethodForDelegate);

            /////

            MyDelegate myDel2 = MethodForDelegate;

            ////
            
            MyDelegate myDel3 = delegate () { Console.WriteLine("Hello from delegate"); };

            ////

            MyDelegate myDel4 = () => Console.WriteLine("Hello from delegate");

            ///

            // внутри у нас MulticastDelegate
            MethodInfo method = myDel.Method;
            object? target = myDel.Target;

            // myDel.Invoke(); // the same as myDel();
            // myDel();

            // почему делегаты?

            var result = FilterElements(new[] { 1, 2, 3, 10, 20, 30, 101 });

            PrintItems(result);
            Console.ReadKey();

            //var filteringAlgorithm = new MyFileringAlgorithm(GraterThenTwenty);
            //var resultFromFilteringAlgorithm = FilterElementsWithAlgorithm(new[] { 1, 2, 3, 10, 20, 30, 101 }, GraterThenTwenty);
            //var resultFromFilteringAlgorithm = FilterElementsWithAlgorithm(new[] { 1, 2, 3, 10, 20, 30, 101 }, LessThenTen);
            //var resultFromFilteringAlgorithm = FilterElementsWithAlgorithm(new[] { 1, 2, 3, 10, 20, 30, 101 }, x => x < 10);

            //PrintItems(resultFromFilteringAlgorithm);
            //Console.ReadKey();

            static void PrintItems(IEnumerable<int> items)
            {
                foreach(var i in items)
                {
                    Console.WriteLine($"{i}");
                }
            }
        }

        static IEnumerable<int> FilterElementsWithAlgorithm(IEnumerable<int> elements, MyFileringAlgorithm fileringAlgorithm)
        {
            foreach (var element in elements)
            {
                if (fileringAlgorithm(element))
                {
                    yield return element;
                }
            }
        }

        static IEnumerable<int> FilterElements(IEnumerable<int> elements)
        {
            foreach (var element in elements)
            {
                if (element < 10)
                {
                    yield return element;
                }
            }

            //foreach (var element in elements)
            //{
            //    if (LessThenTen(myInt))
            //    { 
            //        result.Add(myInt);
            //    }
            //}

            //foreach (var element in elements)
            //{
            //    if (GraterThenTwenty(myInt))
            //    {
            //        result.Add(myInt);
            //    }
            //}
        }

        static bool LessThenTen(int item) => item < 10;

        static bool GraterThenTwenty(int item) => item > 20;
    }
}