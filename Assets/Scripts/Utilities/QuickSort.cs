
using static EdibleScoreboard;
using System.Collections.Generic;
using static EdibleController;

public class QuickSort
{
    // Testing stuff
    static private int PartitionINT(int[] intArray, int left, int right)
    {
        int pivot;
        int aux = (left + right) / 2;
        pivot = intArray[aux];

        while (true)
        {

            while (intArray[left] < pivot)
                left++;

            while (intArray[right] > pivot)
                right--;

            if (left <= right)
            {
                int temp = intArray[right];
                intArray[right] = intArray[left];
                intArray[left] = temp;
                left++;
                right--;
            }

            else
                return right;
        }
    }
    static public void QuickSortINT(int[] arr, int left, int right)
    {
        int pivot;

        if (left < right)
        {
            pivot = PartitionINT(arr, left, right);

            if (pivot > 1)
                QuickSortINT(arr, left, pivot - 1);

            if (pivot + 1 < right)
                QuickSortINT(arr, pivot + 1, right);
        }
    }

    // Edible scoreboard
    static private int PartitionEdibleScore(List<EdibleScore> edibleScores, int left, int right)
    {
        int pivot;
        int aux = (left + right) / 2;
        pivot = edibleScores[aux].score;

        while (true)
        {

            while (edibleScores[left].score < pivot)
                left++;

            while (edibleScores[right].score > pivot)
                right--;

            if (left <= right)
            {
                EdibleScore temp = edibleScores[right];
                edibleScores[right] = edibleScores[left];
                edibleScores[left] = temp;
                left++;
                right--;
            }

            else
                return right;
        }
    }
    static public void QuickSortEdibleScore(List<EdibleScore> edibleScores, int left, int right)
    {
        int pivot;

        if (left < right)
        {
            pivot = PartitionEdibleScore(edibleScores, left, right);

            if (pivot > 1)
                QuickSortEdibleScore(edibleScores, left, pivot - 1);

            if (pivot + 1 < right)
                QuickSortEdibleScore(edibleScores, pivot + 1, right);
        }
    }
}