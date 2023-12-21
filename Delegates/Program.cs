using System.Reflection;

/*
    C# compiler creates instances of a class derived from MulticastDelegate
    when you use the C# language keyword to declare delegate types.
*/

/*
    Делегаты в C# - это тип, который представляет ссылки на методы с определенным списком параметров и
    типом возвращаемого значения
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

            var filteringAlgorithm = new MyFileringAlgorithm(GraterThenTwenty);
            var resultFromFilteringAlgorithm = FilterElementsWithAlgorithm(new[] { 1, 2, 3, 10, 20, 30, 101 }, GraterThenTwenty);
            //var resultFromFilteringAlgorithm = FilterElementsWithAlgorithm(new[] { 1, 2, 3, 10, 20, 30, 101 }, LessThenTen);
            //var resultFromFilteringAlgorithm = FilterElementsWithAlgorithm(new[] { 1, 2, 3, 10, 20, 30, 101 }, x => x < 10);

            PrintItems(resultFromFilteringAlgorithm);
            //Console.ReadKey();

            // цепочка делегатов

            // var chainedDelegate1 = () => Console.WriteLine("ChainedDelegate1 called");
            // var chainedDelegate2 = () => Console.WriteLine("ChainedDelegate2 called");
            // var chainedDelegate3 = () => Console.WriteLine("ChainedDelegate3 called");

            //var chainOfDelegates = chainedDelegate1 + chainedDelegate2 + chainedDelegate3;
            //chainOfDelegates();

            // or

            // chainedDelegate1 += chainedDelegate2;
            // chainedDelegate1 += chainedDelegate3;

            // chainedDelegate1();


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