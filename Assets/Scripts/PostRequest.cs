using UnityEngine;
using System.Collections;
using System.Net;
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class PostRequest : MonoBehaviour {

    public string url = "https://staging.teachley.com/api/m1/login/";
    public GameObject unauthText;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Sends a POST request with the email and password from the User object.
    public void PassData()
    {
        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

        HttpWebRequest http = null;
        
        try
        {
            http = (HttpWebRequest)WebRequest.Create(new Uri(url));
            http.ContentType = "application/json";
            http.Method = "POST";

            string json = JsonUtility.ToJson(GetComponent<User>());
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(json);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            WebResponse response = http.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string content = sr.ReadToEnd();
            Debug.Log("Login successfull: " + content);

            SceneManagement.LoadNextScene();
        }
        catch
        {
            unauthText.SetActive(true);
            http.Abort();
        }
    }

    public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                    }
                }
            }
        }
        return isOk;
    }
}
