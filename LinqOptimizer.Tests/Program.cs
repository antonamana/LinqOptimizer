﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using LinqOptimizer.Core;


namespace LinqOptimizer.Tests
{
    class Program
    {

        sealed class KeyValuePairComparer<Value> : IComparer<KeyValuePair<int, Value>>
        {
            #region IComparer<Key> Members

            public int Compare(KeyValuePair<int, Value> x, KeyValuePair<int, Value> y)
            {
                if (x.Key < y.Key) return -1; else if (x.Key == y.Key) return 0; else return 1;
            }

            #endregion
        }

        public static Dictionary<T, List<T>> Grouping<T>(Dictionary<T, List<T>> dict, T key)
        {            
            List<T> list = null;   
            if(!dict.TryGetValue(key, out list))
            {
                list = new List<T>();
                dict.Add(key, list);
            }
            list.Add(key);
            
            return dict;
        }

        public static Dictionary<T, List<T>> Merge<T>(Dictionary<T, List<T>> left, Dictionary<T, List<T>> right)
        {
            foreach (var item in right)
            {
                List<T> list = null;
                if (!left.TryGetValue(item.Key, out list))
                {
                    list = new List<T>();
                    left.Add(item.Key, list);
                }
                list.AddRange(item.Value);
            }
            return left;
        }

        public static void Main(string[] args)
        {
            //Random random = new Random();
            //var nums = Enumerable.Range(1, 200000000).ToArray();

            //////var list = new KeyValuePair<int, int>[100000000];
            //////Measure(() =>
            //////{
            //////    for (int i = 0; i < nums.Length - 1; i++)
            //////    {
            //////        var item = nums[i];
            //////        list[i] = new KeyValuePair<int, int>(item, item);
            //////    }
            //////});

            //////Measure(() => Array.Sort(nums));
            //////var keys = nums.ToArray();
            //////var values = nums.ToArray();
            //////Measure(() => Array.Sort(keys, values));
            //////Measure(() => Array.Sort(list, new KeyValuePairComparer<int>()));
            //////Measure(() => nums.OrderBy(x => x).ToList());

            
            ////Measure(() => nums.Select(x => x).GroupBy(x => x).Select(g => g).ToList());
            //Measure(() => Console.WriteLine(nums.AsParallel().Aggregate(() => 0.0, (acc, v) => { return ((double) v + 1 + 1 + 1)  + acc; }, (left, right) => { return left + right; }, x => x)));
            //Measure(() => Console.WriteLine(nums.AsParallel().Select(x => (double)x).Select(x => x + 1).Select(x => x + 1).Select(x => x + 1).Sum()));
            ////Measure(() => Grouping(nums));

            //////Measure(() => nums.Sort((x, y) => { if (x < y) return -1; else if (x == y) return 0; else return 1; }));
            //////Measure(() => nums.Sort((x, y) => { if (x < y) return -1; else if (x == y) return 0; else return 1; }));
            //////Measure(() => nums.Sort(Comparer<int>.Default));


            
            //Measure(() => Console.WriteLine(ParallelismHelpers.ReduceCombine(nums, () => 0.0, (acc, v) => { return ((double) v + 1 + 1 + 1) + acc; }, (left, right) => { return left + right; }, x => x)));

            var tests = new ParallelQueryExprTests();
            //tests.OrderByTest();
        }

        static void Measure(Action action)
        {
            var watch = new Stopwatch();
            watch.Start();
            action();
            Console.WriteLine(watch.Elapsed);
        }
    }
}
