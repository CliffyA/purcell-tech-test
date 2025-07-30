using System;

public interface ISolution
{
	int FindMissingNumber(int[] input);
}

public class Sort : ISolution
{
	// sort array then iterate till the numbers are not sequential
	// be careful about first and lst missing!
	public int FindMissingNumber(int[] input)
	{
		Array.Sort(input);
		if (input[0] != 0)
			return 0;
		for (int i = 1; i < input.Length; i++)
			if (input[i] != input[i-1]+1)
				return input[i-1]+1;
		return input.Length;
	}
}

public class Bool : ISolution
{
	// create a bool array for every possible result, loop over to flag
	// then loop over to find the missing one
	public int FindMissingNumber(int[] input)
	{
		bool[] foundArray = new bool[input.Length+1];
		for (int i = 0; i < input.Length; i++)
			foundArray[input[i]] = true;
		for (int i = 0; i < foundArray.Length; i++)
			if (!foundArray[i])
				return i;
		return -1;
	}
}

public class Sum : ISolution
{
	// if we add together every number from 0..n we can expect a specific result
	// the difference to the actual sum is the number that's missing
	public int FindMissingNumber(int[] input)
	{
		long sum = 0;
		long expectedSum = input.Length;
		for (int i = 0; i < input.Length; i++)
		{
			sum += input[i];
			expectedSum += i;
		}
		return (int)(expectedSum - sum);
	}
}

public class SumFormula : ISolution
{
	// we can use a formula to work out the expected sum, instead of a loop

	// in a secuence of 0..n (n+1 total sequence length) we can continuously take the first and last number and it will add up to n
	// eg: 0+100, 1+99, 2+98 .. 99+1, 100+0
	// note that the above has counted every element twice (0+100, 100+0), so we need to halve the result
	
	// so there are n+1 elements in the sequence, the sum of first and last is 0+n = n, and we need to halve the result
	// n+1 * n / 2

	public int FindMissingNumber(int[] input)
	{
		long sum = 0;
		for (int i = 0; i < input.Length; i++)
			sum += input[i];
		long length = input.Length;
		long expectedSum = (length+1) * length / 2;
		return (int)(expectedSum - sum);
	}
}

public class Xor : ISolution
{
	// 100% google answer, i would not have derived this, nor would i use it in real work ;)

	// two equal numbers xor'd will cancel each other out a^a=0
	// also xor is associative, (a^b)^c == a^(b^c)

	// so if we xor 0..n and input[0]..input[n] and then xor the results
	// every element in both will cancel itself out, leaving the missing number

	// but we can do it all at once instead of two seperate loops

	public int FindMissingNumber(int[] input)
	{
		int missing = input.Length;
		for (int i = 0; i < input.Length; i++)
			missing ^= i ^ input[i];
		return missing;
	}
}


class Application
{
	static int Main(string[] args)
	{
		System.Console.Write("Purcell Tech Test\n");
		
		ISolution[] solutions = new ISolution[] {
			new Sort(),
			new Bool(),
			new Sum(),
			new SumFormula(),
			new Xor(),
		};

		// create a big shuffled array of ints for performance testing
		int[] bigInput = new int[100000000];
		Random random = new Random();
		for (int i = 0; i < bigInput.Length; i++)
			bigInput[i] = i;
		for (int i = bigInput.Length - 1; i > 0; i--)
		{
			int j = random.Next(i + 1);
			// Swap elements
			int temp = bigInput[i];
			bigInput[i] = bigInput[j];
			bigInput[j] = temp;
		}
		
		int removeIndex = random.Next(bigInput.Length);
		int bigInputExpected = bigInput[removeIndex];
		bigInput[removeIndex] = bigInput.Length;

		// validate examples
		foreach (ISolution solution in solutions)
		{
			Console.WriteLine(solution.GetType());

			{
				int[] input = new int[] {3,0,1};
				int expected = 2;
				int result = solution.FindMissingNumber(input);
				if (result != expected)
					throw new Exception("Incorrect result for " + solution.GetType().Name + " expected " + expected + " got " + result);
			}

			{
				int[] input = new int[] {9,6,4,2,3,5,7,0,1};
				int expected = 8;
				int result = solution.FindMissingNumber(input);
				if (result != expected)
					throw new Exception("Incorrect result for " + solution.GetType().Name + " expected " + expected + " got " + result);
			}

			{
				// first missing
				int[] input = new int[] {1,2,3,4,5};
				int expected = 0;
				int result = solution.FindMissingNumber(input);
				if (result != expected)
					throw new Exception("Incorrect result for " + solution.GetType().Name + " expected " + expected + " got " + result);
			}

			{
				// last missing
				int[] input = new int[] {0,1,2,3,4,5};
				int expected = 6;
				int result = solution.FindMissingNumber(input);
				if (result != expected)
					throw new Exception("Incorrect result for " + solution.GetType().Name + " expected " + expected + " got " + result);
			}

			{
				// big slow test case
				int[] inputCopy = (int[])bigInput.Clone();
				int startMs = Environment.TickCount;
				int result = solution.FindMissingNumber(inputCopy);
				if (result != bigInputExpected)
					throw new Exception("Incorrect result for " + solution.GetType().Name + " expected " + bigInputExpected + " got " + result);
				int endMs = Environment.TickCount;
				Console.WriteLine("  Time Taken: " + (endMs - startMs) + " ms");
			}
		}

		return 0;
	}
}
