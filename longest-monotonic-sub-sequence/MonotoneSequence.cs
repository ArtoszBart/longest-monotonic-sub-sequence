namespace longest_monotonic_sub_sequence
{
    class MonotoneSequence
    {
        private readonly int[] stack;
        private string order;
        private int currentLength;

        public MonotoneSequence(int maxLength, int firstElement)
        {
            stack = new int[maxLength];
            stack[0] = firstElement;
            currentLength = 1;
        }

        public bool TryPush(int element)
        {
            if (stack[currentLength - 1] > element && order == "asc")
            {
                return false;
            }

            if (stack[currentLength - 1] < element && order == "desc")
            {
                return false;
            }

            Push(element);

            if (string.IsNullOrEmpty(order) && currentLength >= 2)
            {
                DetectAndSetOrder();
            }

            return true;
        }

        public int GetLength()
        {
            return currentLength;
        }

        public int GetSum()
        {
            int sum = 0;
            for (int ii = 0; ii < currentLength; ii++)
            {
                sum += stack[ii];
            }
            return sum;
        }

        private void Push(int element)
        {
            stack[currentLength] = element;
            currentLength++;
        }

        private void DetectAndSetOrder()
        {
            if (stack[currentLength - 2] < stack[currentLength - 1])
            {
                order = "asc";
            }
            else if (stack[currentLength - 2] > stack[currentLength - 1])
            {
                order = "desc";
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int ii = 0; ii < currentLength; ii++)
            {
                result += stack[ii].ToString() + ' ';
            }

            return result;
        }
    }
}
