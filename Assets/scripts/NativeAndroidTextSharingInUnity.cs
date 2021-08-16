using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class NativeAndroidTextSharingInUnity : MonoBehaviour
{
    public Button shareButton;
    private bool isFocus = false;
    private bool isProcessing = false;
    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }
    public void ShareText()
    {
#if UNITY_ANDROID
		if (!isProcessing) {
			StartCoroutine (ShareTextInAnroid ());
		}
#else
        Debug.Log("No sharing set up for this platform.");
#endif
    }
#if UNITY_ANDROID
	public IEnumerator ShareTextInAnroid () {
        string[] shareEmail = {"kuro@playstudios.asia"};
		var shareSubject = "Support Email";
		var shareMessage = "Please describe your issue below: \n"+
                           "--------------------------------------\n";
		isProcessing = true;
		if (!Application.isEditor) {
			//Create intent for action send
			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
			//put text and subject extra
			intentObject.Call<AndroidJavaObject> ("setType", "message/rfc822");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string> ("EXTRA_EMAIL"), shareEmail);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string> ("EXTRA_SUBJECT"), shareSubject);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), shareMessage);
			//call createChooser method of activity class
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
			//AndroidJavaObject chooser =	intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share");
			currentActivity.Call ("startActivity", intentObject);
		}
		yield return new WaitUntil (() => isFocus);
		isProcessing = false;
	}
#endif
}

