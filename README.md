# Longest Monotonic Sub-Sequence

This is my project for Algorithms and Data Structures subject on [PJAIT](https://pja.edu.pl/en/).

The program must showing longest monotonic sub-sequence, its length and sum of its elements.

## Requirements:

-   Time Complexity of Algorithm: O(n)
-   Space Complexity of Algorithm: O(1)
-   The program must accept input data from the file, the location of which is given in the first parameter of the program invocation.
-   In the case of multiple sub-sequences of the same length, the one that comes first in the string should be considered.
-   It is forbidden to use ready-made structures, such as ArrayList or StringBuilder. The exceptions are those structures that are used for I/O operations, the String class and methods on it.

## Usage:

1. Build project
2. In project location run:
    ```bat
    start longest-monotonic-sub-sequence.exe "{locatization of .txt file with sequence}"
    ```

## Example:

`numbers.txt`

```text
3 6 12 4 7 19 20 20 9 11
```

`Terminal`

```bat
git clone https://github.com/ArtoszBart/longest-monotonic-sub-sequence.git
cd longest-monotonic-sub-sequence/
dotnet build
cd longest-monotonic-sub-sequence/obj/Debug/

start longest-monotonic-sub-sequence.exe "{locatization of .txt file with sequence}"
```

`Output`

```text
4 7 19 20 20
5 70
```
