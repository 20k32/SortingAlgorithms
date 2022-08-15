namespace Sorting
{
    internal class Program
    {
        public static void Print(ref int[] arr, string name, char c = '-')
        {
            Console.WriteLine($"{name}:\n");
            string str = String.Empty;
            foreach (int i in arr)
            {
                str += i + " ";
            }
            Console.Write(str);
            Console.WriteLine("\n" + new string(c, str.Length - 1));
        }
        public static int[] Initialize(int arrSize, int from , int to)
        {
            int[] arr = new int[arrSize];
            Random random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(from, to+1);
            }
            return arr;
        }
        private static void Swap<T>(T[] item, int left, int right)
        {
            if (left == right) return;
            T temp = item[left];
            item[left] = item[right];
            item[right] = temp;
        }

        #region BubbleSort
        public static void BubbleSort(ref int[] arr)
        {
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i - 1].CompareTo(arr[i]) > 0)
                    {
                        Swap(arr, i - 1, i);
                        swapped = true;
                    }
                }
            }
            while (swapped != false);
        }
        #endregion

        #region InsertionSort
        private static void Insert(ref int[] arr, int to, int from)
        {
            int tmp = arr[to];
            arr[to] = arr[from];
            for (int current = from; current > to; current--)
            {
                arr[current] = arr[current - 1];
            }
            arr[to + 1] = tmp;
        }
        private static int FindIndex(int[] arr, int value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].CompareTo(value) > 0)
                    return i;
            }
            return -1;
        }
        public static void InsertionSort(ref int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i].CompareTo(arr[i - 1]) < 0)
                {
                    int index = FindIndex(arr, arr[i]);
                    Insert(ref arr, index, i);
                }
            }
        }
        #endregion

        #region SelectionSort
        private static int FindSmallestIndex(int[] arr, int tail)
        {
            int Current = arr[tail];
            int CurrentIndex = tail;
            for (int i = tail + 1; i < arr.Length; i++)
            {
                if (Current.CompareTo(arr[i]) > 0)
                {
                    CurrentIndex = i;
                    Current = arr[i];
                }
            }
            return CurrentIndex;
        }
        public static void SelectionSort(ref int[] arr)
        {
            int tail = 0;
            while (tail < arr.Length)
            {
                int index = FindSmallestIndex(arr, tail);
                Swap<int>(arr, tail, index);
                tail++;
            }
        }
        #endregion

        #region MergeSort
        private static void Merge(ref int[] arr, ref int[] left, ref int[] right)
        {
            int LeftIndex = 0, RightIndex = 0, TargetIndex = 0,
                TotalLength = left.Length + right.Length;
            while (TotalLength > 0)
            {
                if (LeftIndex >= left.Length)
                    arr[TargetIndex] = right[RightIndex++];
                else if (RightIndex >= right.Length)
                    arr[TargetIndex] = left[LeftIndex++];
                else if (left[LeftIndex].CompareTo(right[RightIndex]) < 0)
                    arr[TargetIndex] = left[LeftIndex++];
                else arr[TargetIndex] = right[RightIndex++];
                TargetIndex++;
                TotalLength--;
            }
        }
        public static void MergeSort(int[] arr)
        {
            if (arr.Length <= 1) return;
            int LeftPartSize = arr.Length / 2;
            int RightPartSize = arr.Length - LeftPartSize;
            int[] LeftArray = new int[LeftPartSize],
                  RightArray = new int[RightPartSize];
            Array.Copy(arr, 0, LeftArray, 0, LeftPartSize);
            Array.Copy(arr, LeftPartSize, RightArray, 0, RightPartSize);
            MergeSort(LeftArray);
            MergeSort(RightArray);
            Merge(ref arr, ref LeftArray, ref RightArray);
        }
        #endregion

        #region QuickSort
        public static void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }
        public static void QuickSort(int[] arr, int left, int right)
        {
            int i = left, j = right;
            int pivot = arr[(left + right) >> 1];
            while (i <= j)
            {
                while (arr[i] < pivot)
                    i++;

                while (arr[j] > pivot)
                    j--;

                if (i <= j)
                {
                    Swap<int>(arr, i, j);
                    i++;
                    j--;
                }
            }
            if (left < j)
                QuickSort(arr, left, j);
            if (i < right)
                QuickSort(arr, i, right);
        }
        #endregion

        static void Main(string[] args)
        {
            int[] tmp = Initialize(25, -100, 100), 
                  arr = new int[tmp.Length];
            Array.Copy(tmp, 0, arr, 0, arr.Length);
            Print(ref arr, "Initial Array");

            BubbleSort(ref arr);
            Print(ref arr, "Bubble");
            Array.Copy(tmp, 0, arr, 0, arr.Length);

            InsertionSort(ref arr);
            Print(ref arr, "Insertion");
            Array.Copy(tmp, 0, arr, 0, arr.Length);

            SelectionSort(ref arr);
            Print(ref arr, "Selection");
            Array.Copy(tmp, 0, arr, 0, arr.Length);

            MergeSort(arr);
            Print(ref arr, "Merge");
            Array.Copy(tmp, 0, arr, 0, arr.Length);

            QuickSort(arr);
            Print(ref arr, "Quick");
            Array.Copy(tmp, 0, arr, 0, arr.Length);
        }
    }
}