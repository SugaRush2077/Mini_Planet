using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    /*
    public static Vector3 Vector3MinusFloatValue(this Vector3 v, float f)
    {
        return new Vector3(v.x - f, v.y - f, v.z - f);
    }*/
    
    public static void initializeAndShuffle(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = i;
        }

        int n = nums.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            // swap
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
    }

}
