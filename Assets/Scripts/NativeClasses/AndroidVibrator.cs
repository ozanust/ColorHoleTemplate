using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AndroidVibrator {

    private static readonly AndroidJavaObject Vibrator = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("getSystemService", "vibrator");

    static AndroidVibrator()
    {
        // Trick Unity into giving the App vibration permission when it builds.
        // This check will always be false, but the compiler doesn't know that.
        if (Application.isEditor)
            Handheld.Vibrate();
    }

    public static void Vibrate(long milliseconds)
    {
        Vibrator.Call("vibrate", milliseconds);
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        Vibrator.Call("vibrate", pattern, repeat);
    }
}
