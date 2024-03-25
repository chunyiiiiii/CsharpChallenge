using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    internal class Sort
    {
        static void Main(string[] args)
        {
            Sort sort = new Sort();

            // 创建测试用的整数数组
            int[] complexArray = GenerateRandomArray(500); 

            // 1.测试选择排序
            sort.TimeAndSort("Selection Sort", sort.SelectionSort, complexArray);

            // 重置测试用的整数数组
            complexArray = GenerateRandomArray(500); 

            // 2.测试冒泡排序
            sort.TimeAndSort("Bubble Sort", sort.BubbleSort, complexArray);

            // 重置测试用的整数数组
            complexArray = GenerateRandomArray(500);

            //3.测试插入排序
            sort.TimeAndSort("Insertion Sort", sort.InsertionSort, complexArray);

            // 重置测试用的整数数组
            complexArray = GenerateRandomArray(500);

            //4.测试堆排序
            sort.TimeAndSort("Heap Sort", sort.HeapSort, complexArray);

            // 重置测试用的整数数组
            complexArray = GenerateRandomArray(500);

            //5.测试希尔排序
            sort.TimeAndSort("Shell Sort", sort.ShellSort, complexArray);

            // 重置测试用的整数数组
            complexArray = GenerateRandomArray(500);

            //6.测试计数排序
            sort.TimeAndSort("Counting Sort", sort.CountingSort, complexArray);

            // 重置测试用的整数数组
            complexArray = GenerateRandomArray(500);

            //7.测试鸡尾酒排序
            sort.TimeAndSort("Cocktail Sort", sort.CocktailSort, complexArray);

            // 重置测试用的整数数组
            complexArray = GenerateRandomArray(500);

            //8.测试基数排序
            sort.TimeAndSort("Radix Sort", sort.RadixSort, complexArray);

        }

        // 生成随机数组
        static int[] GenerateRandomArray(int size)
        {
            int[] array = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(1, 1000); // 生成 1 到 1000 之间的随机数
            }
            return array;
        }

        // 通用方法：计时并调用排序算法
        void TimeAndSort(string sortName, Action<int[]> sortMethod, int[] array)
        {
            Stopwatch watch = Stopwatch.StartNew();
            sortMethod(array);
            watch.Stop();
            Console.Write(sortName + " Result: ");
            PrintArray(array);
            Console.WriteLine(sortName + " Time: " + watch.Elapsed.TotalMilliseconds.ToString("0.00 ms"));
            Console.ReadKey();
        }

        // 打印数组
        void PrintArray(int[] array)
        {
            Console.Write("[");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
                if (i < array.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("]");
        }

        /* 选择排序 */
        void SelectionSort(int[] nums)
        {
            int n = nums.Length;
            // 外循环：未排序区间为 [i, n-1]
            for (int i = 0; i < n - 1; i++)
            {
                // 内循环：找到未排序区间内的最小元素
                int k = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (nums[j] < nums[k])
                        k = j; // 记录最小元素的索引
                }
                // 将该最小元素与未排序区间的首个元素交换
                (nums[k], nums[i]) = (nums[i], nums[k]);
            }
        }

        /* 冒泡排序 */
        void BubbleSort(int[] nums)
        {
            // 外循环：未排序区间为 [0, i]
            for (int i = nums.Length - 1; i > 0; i--)
            {
                // 内循环：将未排序区间 [0, i] 中的最大元素交换至该区间的最右端
                for (int j = 0; j < i; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        // 交换 nums[j] 与 nums[j + 1]
                        (nums[j + 1], nums[j]) = (nums[j], nums[j + 1]);
                    }
                }
            }
        }

        /* 插入排序 */
        void InsertionSort(int[] nums)
        {
            // 外循环：已排序区间为 [0, i-1]
            for (int i = 1; i < nums.Length; i++)
            {
                int bas = nums[i], j = i - 1;
                // 内循环：将 base 插入到已排序区间 [0, i-1] 中的正确位置
                while (j >= 0 && nums[j] > bas)
                {
                    nums[j + 1] = nums[j]; // 将 nums[j] 向右移动一位
                    j--;
                }
                nums[j + 1] = bas;         // 将 base 赋值到正确位置
            }
        }

        /* 堆的长度为 n ，从节点 i 开始，从顶至底堆化 */
        void SiftDown(int[] nums, int n, int i)
        {
            while (true)
            {
                // 判断节点 i, l, r 中值最大的节点，记为 ma
                int l = 2 * i + 1;
                int r = 2 * i + 2;
                int ma = i;
                if (l < n && nums[l] > nums[ma])
                    ma = l;
                if (r < n && nums[r] > nums[ma])
                    ma = r;
                // 若节点 i 最大或索引 l, r 越界，则无须继续堆化，跳出
                if (ma == i)
                    break;
                // 交换两节点
                (nums[ma], nums[i]) = (nums[i], nums[ma]);
                // 循环向下堆化
                i = ma;
            }
        }

        /* 堆排序 */
        void HeapSort(int[] nums)
        {
            // 建堆操作：堆化除叶节点以外的其他所有节点
            for (int i = nums.Length / 2 - 1; i >= 0; i--)
            {
                SiftDown(nums, nums.Length, i);
            }
            // 从堆中提取最大元素，循环 n-1 轮
            for (int i = nums.Length - 1; i > 0; i--)
            {
                // 交换根节点与最右叶节点（交换首元素与尾元素）
                (nums[i], nums[0]) = (nums[0], nums[i]);
                // 以根节点为起点，从顶至底进行堆化
                SiftDown(nums, i, 0);
            }
        }

        /* 希尔排序 */
        void ShellSort(int[] nums)
        {
            int n = nums.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = nums[i];
                    int j;
                    for (j = i; j >= gap && nums[j - gap] > temp; j -= gap)
                    {
                        nums[j] = nums[j - gap];
                    }
                    nums[j] = temp;
                }
            }
        }

        /* 计数排序 */
        void CountingSort(int[] nums)
        {
            int n = nums.Length;
            int[] output = new int[n];
            int max = nums.Max() + 1;
            int[] count = new int[max];

            for (int i = 0; i < n; i++)
            {
                count[nums[i]]++;
            }

            for (int i = 1; i < max; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                output[count[nums[i]] - 1] = nums[i];
                count[nums[i]]--;
            }

            Array.Copy(output, nums, n);
        }

        /* 鸡尾酒排序 */
        void CocktailSort(int[] nums)
        {
            bool swapped = true;
            int start = 0;
            int end = nums.Length - 1;

            while (swapped)
            {
                swapped = false;

                for (int i = start; i < end; i++)
                {
                    if (nums[i] > nums[i + 1])
                    {
                        (nums[i], nums[i + 1]) = (nums[i + 1], nums[i]);
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;
                end--;

                for (int i = end - 1; i >= start; i--)
                {
                    if (nums[i] > nums[i + 1])
                    {
                        (nums[i], nums[i + 1]) = (nums[i + 1], nums[i]);
                        swapped = true;
                    }
                }

                start++;
            }
        }

        /* 基数排序 */
        void RadixSort(int[] nums)
        {
            int max = nums.Max();
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSortByDigit(nums, exp);
            }
        }

        void CountingSortByDigit(int[] nums, int exp)
        {
            int n = nums.Length;
            int[] output = new int[n];
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
            {
                count[(nums[i] / exp) % 10]++;
            }

            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(nums[i] / exp) % 10] - 1] = nums[i];
                count[(nums[i] / exp) % 10]--;
            }

            Array.Copy(output, nums, n);
        }

       
    }
}
