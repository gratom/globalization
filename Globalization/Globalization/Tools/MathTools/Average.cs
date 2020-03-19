using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globalization.Tools.MathTools
{
    public class Average<T>
    {
        public delegate T Averaging(IEnumerable<T> listOfElements);

        public T AverageValue { get { return averaging(elements); } }

        private List<T> elements;

        private Averaging averaging;

        private int counter = 0;
        private int maxCount;

        /// <summary>
        ///
        /// </summary>
        /// <param name="averaging"></param>
        /// <param name="countOfPool"></param>
        /// <param name="isPreInit"></param>
        public Average(Averaging averaging, int countOfPool = 10, bool isPreInit = false)
        {
            this.averaging = averaging;
            if (countOfPool < 1)
            {
                throw new Exception("Average can`t have " + countOfPool.ToString() + " element. Must be greater than 0!");
            }
            maxCount = countOfPool;
            elements = new List<T>(countOfPool);
            if (isPreInit)
            {
                for (int i = 0; i < countOfPool; i++)
                {
                    elements.Add(default);
                }
            }
        }

        public void SetNext(T value)
        {
        }

        public void Clear()
        {
        }
    }
}