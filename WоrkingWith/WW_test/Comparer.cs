﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WW_test
{
    public class Comparer
    {
        public static Comparer<U> Get<U>(Func<U, U, bool> func)
        {
            return new Comparer<U>(func);
        }
    }

    public class Comparer<T> : Comparer, IEqualityComparer<T>
    {
        private Func<T, T, bool> comparisonFunction;

        public Comparer(Func<T, T, bool> func) { 
        comparisonFunction = func;
        }


        public bool Equals(T x, T y)
        {
            return comparisonFunction(x, y);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }
}
