using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;

public class UserNameDisplay : MonoBehaviour
{
    private Oidc oidc;
    private bool oidcReceived;
    public string userName;
    public TextMeshProUGUI textMeshPro;
    public bool isLoaded;


    [DllImport("__Internal")]
    public static extern void UserDisplayLoaded();

    // Start is called before the first frame update
    void Start()
    {
        oidc = null;
        userName = "";
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

        if (oidcReceived)
        {
            RenderUI();
        }
    }

    public void ReceiveOidc(string parameters)
    {
        oidc = JsonConvert.DeserializeObject<Oidc>(parameters);
        oidcReceived = true;
    }

    public void RenderUI()
    {
        float alpha = 0f;
        if (oidc.User != null)
        {
            alpha = 100f;
            SetUserName(oidc);
        }
        else
        {
            alpha = 0f;
            userName = "";
        }
        textMeshPro.text = userName;
        GetComponent<CanvasRenderer>().SetAlpha(alpha);
        
    }

    public void SetUserName(Oidc reactOidc)
    {
        userName = reactOidc.User.Profile.UniqueName;
    }

    
}
