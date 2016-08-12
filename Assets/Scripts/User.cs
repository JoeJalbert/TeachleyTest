using UnityEngine;
using System.Collections;

public class User : MonoBehaviour {

    public string email;
    public string password;

    public void UpdateEmail(string s)
    {
        email = s;
    }

    public void UpdatePassword(string s)
    {
        password = s;
    }

}
