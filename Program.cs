//*****************************************************************************
//** 564. Find the Closest Palindrome  leetcode                              **
//**                                                                         **
//*****************************************************************************


char* nearestPalindromic(char* n) 
{
    int len = strlen(n);
    long original = atol(n);

    // Edge cases for small numbers
    if (original == 1)
        return strdup("0");
    if (original == 0)
        return strdup("1");


    // Build the first half of the palindrome
    char *halfStr = (char *)malloc((len + 1) / 2 + 1);
    strncpy(halfStr, n, (len + 1) / 2);
    halfStr[(len + 1) / 2] = '\0';
    
    long half = atol(halfStr);
    free(halfStr);

    // Generate three candidates by modifying the first half
    long candidate1, candidate2, candidate3;
    
    // 1st candidate: mirrored first half
    char *pal1 = (char *)malloc(len + 1);
    snprintf(pal1, len + 1, "%ld", half);
    int i;
    for (i = 0; i < len / 2; i++) 
    {
        pal1[len - 1 - i] = pal1[i];
    }
    candidate1 = atol(pal1);

    // 2nd candidate: incremented first half and mirrored
    char *pal2 = (char *)malloc(len + 1);
    snprintf(pal2, len + 1, "%ld", half + 1);
    for (i = 0; i < len / 2; i++) 
    {
        pal2[len - 1 - i] = pal2[i];
    }
    candidate2 = atol(pal2);

    // 3rd candidate: decremented first half and mirrored
    char *pal3 = (char *)malloc(len + 1);
    snprintf(pal3, len + 1, "%ld", half - 1);
    for (i = 0; i < len / 2; i++) 
    {
        pal3[len - 1 - i] = pal3[i];
    }
    candidate3 = atol(pal3);

    // Handle the case where candidate1 is equal to the original number
    if (candidate1 == original)
        candidate1 = LLONG_MAX;

    // Edge cases: smallest and largest possible palindromes of the same length
    long lowerBound = pow(10, len - 1) - 1;
    long upperBound = pow(10, len) + 1;

    // Compare the differences and choose the closest palindrome
    long closest = candidate1;
    long diffClosest = labs(original - closest);

    // Comparing candidate2
    if (labs(original - candidate2) < diffClosest || 
        (labs(original - candidate2) == diffClosest && candidate2 < closest))
    {
        closest = candidate2;
        diffClosest = labs(original - closest);
    }

    // Comparing candidate3
    if (labs(original - candidate3) < diffClosest || 
        (labs(original - candidate3) == diffClosest && candidate3 < closest))
    {
        closest = candidate3;
        diffClosest = labs(original - closest);
    }

    // Check the lower and upper bounds
    if (labs(original - lowerBound) < diffClosest || 
        (labs(original - lowerBound) == diffClosest && lowerBound < closest))
    {
        closest = lowerBound;
    }

    if (labs(original - upperBound) < labs(original - closest) || 
        (labs(original - upperBound) == labs(original - closest) && upperBound < closest))
    {
        closest = upperBound;
    }

    // Convert the result back to a string
    char* result = (char*)malloc(20);
    snprintf(result, 20, "%ld", closest);

    // Free the allocated memory for palindrome candidates
    free(pal1);
    free(pal2);
    free(pal3);

    return result;
}