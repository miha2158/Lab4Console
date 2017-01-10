using System;

namespace Lab4Console
{
    public class Tasks
    {
        public static int[] First(int[] array, int indexOfElementToDelete)
        {
            if ((indexOfElementToDelete >= array.Length) || (indexOfElementToDelete < 0))
                return array;

            int[] temp = new int[array.Length - 1];

            for (int j = 0, i = 0; j < temp.Length; j++, i++)
            {
                if (i == indexOfElementToDelete)
                    i++;

                temp[j] = array[i];
            }

            return temp;
        }

        public static int[] Second(int[] array, int indexOfElementToInsert, int element)
        {
            int[] temp = new int[array.Length + 1];

            if ((indexOfElementToInsert >= temp.Length) || (indexOfElementToInsert < 0))
                return array;

            for (int j = 0, i = 0;; j++, i++)
            {
                if (j == indexOfElementToInsert)
                    temp[j++] = element;

                if (j >= temp.Length)
                    break;

                temp[j] = array[i];
            }

            return temp;
        }

        public static int[] Third(int[] myArray)
        {
            int[] odd = new int[0];
            int[] even = new int[0];
            int[] array = new int[myArray.Length];

            for (int i = 0; i < array.Length; i++)
                array[i] = myArray[i];

            for (int i = 0, k = 0, j = 0; i < array.Length; i++)
                if (array[i]%2 == 0)
                {
                    var temp = new int[even.Length + 1];
                    for (int p = 0; p < even.Length; p++)
                        temp[p] = even[p];
                    even = temp;
                    even[k++] = array[i];
                }
                else
                {
                    int[] temp = new int[odd.Length + 1];
                    for (int p = 0; p < odd.Length; p++)
                        temp[p] = odd[p];
                    odd = temp;
                    odd[j++] = array[i];
                }

            for (int i = 0; i < even.Length; i++)
                array[i] = even[i];
            for (int i = 0; i < odd.Length; i++)
                array[even.Length + i] = odd[i];

            return array;
        }

        public static bool Fourth(int[] array, out double average, out int averageIndex)
        {
            average = 0;
            averageIndex = 0;

            foreach (int x in array)
                average += x;
            average /= array.Length;


            for (int i = 0; i < array.Length; i++)
                if (average == array[i])
                {
                    averageIndex = i;
                    return true;
                }

            return false;
        }

        public static int[] Fifth(int[] array)
        {
            int[] myArray = new int[array.Length];
            for (int i = 0; i < myArray.Length; i++)
                myArray[i] = array[i];

            for (int i = 1; i < myArray.Length; i++)
            {
                int temp = myArray[i];
                int j;
                for (j = i - 1; (j >= 0) && (myArray[j] > temp); j--)
                    myArray[j + 1] = myArray[j];
                myArray[j + 1] = temp;
            }

            return myArray;
        }

        public static bool BinarySearch(int[] array, int element, out int checks, out int index)
        {
            checks = 0;
            index = 0;
            if (array.Length <= 0)
                return false;

            int left = 0, right = array.Length - 1;
            do
            {
                checks++;
                index = (left + right)/2;

                if (array[index] < element)
                    left = index + 1;
                else right = index;
            } while (left != right);
            // || array[index] == element
            index = left;
            return array[left] == element;
        }
    }

    internal class Program
    {
        #region меню

        private static int SelectorY(string[] items)
        {
            Console.ResetColor();
            Console.CursorVisible = false;
            int top = Console.CursorTop, left = 4;
            int currentSelection = 0, previousSelection = 1;

            for (var i = 0; i < items.Length; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.WriteLine(items[i]);
            }

            bool selected = false;
            do
            {
                Console.SetCursorPosition(left, top + previousSelection);
                Console.WriteLine(items[previousSelection]);

                Console.SetCursorPosition(left, top + currentSelection);
                {
                    var temp = Console.BackgroundColor;
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = temp;
                }
                Console.WriteLine(items[currentSelection]);
                {
                    var temp = Console.BackgroundColor;
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = temp;
                }


                previousSelection = currentSelection;
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        currentSelection++;
                        break;
                    case ConsoleKey.UpArrow:
                        currentSelection--;
                        break;
                    case ConsoleKey.Enter:
                        selected = true;
                        break;
                }

                if (currentSelection < 0)
                    currentSelection = items.Length - 1;
                else if (currentSelection == items.Length)
                    currentSelection = 0;
            } while (!selected);

            Console.CursorTop = top + items.Length;
            ClearLines(items.Length);
            Console.SetCursorPosition(left, top);
            Console.WriteLine(items[currentSelection]);

            Console.CursorTop++;
            Console.CursorVisible = true;
            return currentSelection;
        }

        #endregion

        private static void ClearLines(int numberOfLines)
        {
            Console.CursorTop -= numberOfLines;
            for (int i = 0; i < numberOfLines; i++)
            {
                for (int j = 0; j < Console.WindowWidth - 1; j++)
                    Console.Write(" ");
                Console.CursorTop++;
                Console.CursorLeft = 0;
            }
            Console.CursorTop -= numberOfLines;
        }

        #region методы ввода - вывода

        const int C = 99999;

        private static int ReadInteger(string expectedInput)
        {
            int n = 0;
            bool ok = false;
            Console.WriteLine("Ожидаю ввод - {0}", expectedInput);
            do
            {
                try
                {
                    n = int.Parse(Console.ReadLine());
                    ok = true;
                }
                catch (OverflowException)
                {
                    ClearLines(2);
                    Console.WriteLine("Ожидалось число меньше {0}", C);
                }
                catch (FormatException)
                {
                    ClearLines(2);
                    Console.WriteLine("Ожидалось натуральное число - {0}", expectedInput);
                }
                if(n >= C)
                    Console.WriteLine("Ожидалось число меньше {0}", C);
            } while (!ok);
            Console.WriteLine();
            return n;
        }

        private static void WriteArray(int[] array)
        {
            foreach (int x in array)
                Console.Write(" {0,4}", x);
            Console.WriteLine();
            Console.WriteLine();
        }

        #endregion

        #region методы создания массивов

        private static int[] GenerateIntArray(int arrayLength)
        {
            Random randGen = new Random();
            int[] temp = new int[arrayLength];

            for (int i = 0; i < arrayLength; i++)
                temp[i] = randGen.Next(-100, 100);

            return temp;
        }

        private static int[] StringToIntArray(string input, out bool mistakes)
        {
            mistakes = false;
            if (input == string.Empty)
                return null;

            input = input.Trim();
            while (input.Contains("  "))
                input = input.Remove(input.IndexOf("  "), 1);

            if ((input == " ") || (input == string.Empty))
                return null;

            int n = 0;
            input += " ";
            int[] array = new int[n];
            while (input != string.Empty)
            {
                int temp;
                if (int.TryParse(input.Substring(0, input.IndexOf(" ")), out temp))
                {
                    int[] tempArray = new int[n + 1];
                    int j;
                    for (j = 0; j < n; j++)
                        tempArray[j] = array[j];
                    array = tempArray;
                    n++;
                    array[j] = temp;
                }
                else
                    mistakes = true;
                input = input.Remove(0, input.IndexOf(" ") + 1);
            }
            return array;
        }

        #endregion

        private static int[] GetArray()
        {
            Console.Clear();
            Console.WriteLine("Как получить массив?");
            Console.WriteLine();
            switch (SelectorY(new[] {"Сгенерировать", "Ввести"}))
            {
                case 0:
                {
                    int n;
                    do
                    {
                        n = ReadInteger("Положительное число - длина массива");

                    } while (n <= 0);
                    Console.Clear();
                    return GenerateIntArray(n);
                }
                case 1:
                {
                    int[] output = null;
                    bool boom;
                    string input = " ";
                    do
                    {
                        Console.WriteLine("Целые числа от {0} до {1}", int.MinValue, int.MaxValue);
                        //Console.WriteLine("");
                        Console.WriteLine("Введите элементы массива через ПРОБЕЛ");
                        if (input == string.Empty)
                        {
                            ClearLines(4);
                            Console.WriteLine("Массив не должна быть пустой. Введите его элементы через пробел");
                        }
                        input = Console.ReadLine();
                    } while (input == string.Empty);

                    //Console.Clear();
                    output = StringToIntArray(input, out boom);
                    if (boom)
                        Console.WriteLine("\nВы ввели не только числа\n");
                    return output;
                }
            }
            return null;
        }

        private static void Act(ref int[] array)
        {
            int selectedItem;

            do
            {
                Console.WriteLine();
                bool sorted = true;
                //int[] temp = Tasks.Fifth(array);

                {
                    for (int i = 0; i < array.Length - 1; i++)
                        if (array[i] > array[i + 1])
                            sorted = false;
                }

                #region Выбор действий

                if ((array == null) || (array.Length == 0))
                {
                    Console.WriteLine("Ваш массив пуст");
                    Console.WriteLine();
                    selectedItem = SelectorY(new[]
                    {
                        "Выход",
                        "Новый Массив",
                        "Добавить элемент с заданным номером"
                    });
                }
                else if (sorted)
                {
                    WriteArray(array);
                    selectedItem = SelectorY(new[]
                    {
                        "Выход",
                        "Новый Массив",
                        "Добавить элемент с заданным номером",
                        "Удалить элемент с заданным номером",
                        "Перестановка чётных элементов в начало, нечётных - в конец",
                        "Поиск элемента равного среднему арифметическому элементов массива",
                        "Сортировка простым включением",
                        "Бинарный поиск"
                    });
                }
                else
                {
                    WriteArray(array);
                    selectedItem = SelectorY(new[]
                    {
                        "Выход",
                        "Новый Массив",
                        "Добавить элемент с заданным номером",
                        "Удалить элемент с заданным номером",
                        "Перестановка чётных элементов в начало, нечётных - в конец",
                        "Поиск элемента равного среднему арифметическому элементов массива",
                        "Сортировка простым включением"
                    });
                }

                #endregion

                switch (selectedItem)
                {
                    case 1:
                        array = GetArray();
                        break;
                    case 2:
                    {
                        int n = ReadInteger("Куда вставить элемент") - 1;
                        int p;
                        if ((n >= 0) && (n <= array.Length))
                        {
                            p = ReadInteger("Какой элемент вставить");
                            array = Tasks.Second(array, n, p);
                        }
                        else if (array == null || array.Length == 0 && n == 0)
                        {
                            p = ReadInteger("Какой элемент вставить");
                            array = new int[1];
                            array[0] = p;
                        }
                        else
                            Console.WriteLine("Нельзя добавить элемент {0} в массив из {1} элементов", n + 1,
                                array.Length);
                    }
                        break;
                    case 3:
                    {
                        int n = ReadInteger("Номер удаляемого элемента");
                        if ((n > array.Length) || (n < 0))
                            Console.WriteLine("Нельзя удалить элемент {0} в массиве из {1} элементов", n, array.Length);
                        else array = Tasks.First(array, n - 1);
                    }
                        break;
                    case 4:
                        array = Tasks.Third(array);
                        break;
                    case 5:
                    {
                        double n;
                        int i;
                        if (!Tasks.Fourth(array, out n, out i))
                            Console.WriteLine("Такого элемента ({0}) нет", n);
                        else Console.WriteLine("Элемент {0} с индексом {1}", n, i + 1);
                    }
                        break;
                    case 6:
                        array = Tasks.Fifth(array);
                        break;
                    case 7:
                    {
                        int n = ReadInteger("Элемент для поиска");
                        int c;
                        int i;
                        if (Tasks.BinarySearch(array, n, out c, out i))
                            Console.WriteLine("Элемент с индексом {0}. Количество сравнений - {1}", i + 1, c);
                        else Console.WriteLine("Такого элемента нет");
                    }
                        break;
                }
            } while (selectedItem != 0);
        }

        private static void Main(string[] args)
        {
            int[] array = GetArray();

            Act(ref array);
        }
    }
}