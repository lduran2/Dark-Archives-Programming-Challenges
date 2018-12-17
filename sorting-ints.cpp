#include <stdio.h>

void arrprt(size_t len, int* arr)
{
    printf("%d", *arr);
    for (int k = 1; k < len; ++k)
    {
        printf(",%d", arr[k]);
    }
}

int main(int argc, char** argv)
{
    const int LEN = 6;
    int test [] = {21, 2, 32713, 41, 5, 70};
    arrprt(LEN, test);

    return 0;
}
