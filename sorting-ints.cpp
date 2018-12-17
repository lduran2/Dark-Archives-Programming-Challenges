#include <stdio.h>

void arrprt(size_t len, void** arr, size_t element_size)
{
    /* pointer to array bytes */
    char* ptr;
    /* if array is empty, stop here */
    if (len <= 0)
    {
        return;
    }

    ptr = ((char*)arr);
    printf("%d", *arr);
    for (int k = 1; k < len; ++k)
    {
        /* move to next element */
        ptr += element_size;
        /* convert pointer before printing */
        printf(",%d", *(int*)ptr);
    }
}

int main(int argc, char** argv)
{
    const int LEN = 6;
    int test [] = {21, 2, 32713, 41, 5, 70};
    arrprt(LEN, (void**)test, sizeof(int));

    return 0;
}
