using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;
using Assets;

public class UserNameDisplay : MonoBehaviour
{
    private UserData userData;
    public bool userDataReceived;
    public static string userName;
    public TextMeshProUGUI textMeshPro;
    public bool isLoaded;

    [DllImport("__Internal")]
    public static extern void UserDisplayLoaded();

    // Start is called before the first frame update
    void Start()
    {
        userData = null;
        userName = "";
        PlayerPrefs.SetString("playerName", userName);
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        
        GetComponent<CanvasRenderer>().SetAlpha(0f);

        isLoaded = true;

#if !UNITY_EDITOR && UNITY_WEBGL
                            UserDisplayLoaded();
#endif
       

    }


    // Update is called once per frame
    void Update()
    {

        if (userDataReceived)
        {
            RenderUI();
        }
    }

    public void ReceiveUserData(string parameters)
    {
        userData = JsonConvert.DeserializeObject<UserData>(parameters);
        SetUserName();
        userDataReceived = true;
    }

    public void RenderUI()
    {
        float alpha = 0f;
        if (userData != null)
        {
            alpha = 100f;
            
        }
        else
        {
            alpha = 0f;
            userName = "";
        }
        textMeshPro.text = userName;
        GetComponent<CanvasRenderer>().SetAlpha(alpha);
        
    }

    public void SetUserName()
    {
        userName = userData.DisplayName;
        PlayerPrefs.SetString("playerName", userName);
    }

    
}
