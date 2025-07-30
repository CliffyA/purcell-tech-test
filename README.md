## Purcell Tech Test

Hi hello, here's my submission for the tech test to find a missing number in an array.

```
In C# create a console app that finds the Missing Number using SOLID principles

Description: Given an array containing numbers taken from the range 0 to n, find the one number that is missing from the array.



Input:

An array of integers number where nums contains n distinct numbers from the range 0 to n.



Output:

Return the missing number.



Examples:

Input: [3, 0, 1]

Output: 2



Input: [9, 6, 4, 2, 3, 5, 7, 0, 1]

Output: 8
```

I've certainly overengineered things and had too much fun adding multiple solutions, and a micro "test suite".

I must note that the XOR solution I did not derive myself, tho I do understand it and can walk you through it if needs be.

### Output

```
$ ./Run.sh
Purcell Tech Test
Sort
  Time Taken: 44317 ms
Bool
  Time Taken: 1072 ms
Sum
  Time Taken: 208 ms
SumFormula
  Time Taken: 184 ms
Xor
  Time Taken: 180 ms
```