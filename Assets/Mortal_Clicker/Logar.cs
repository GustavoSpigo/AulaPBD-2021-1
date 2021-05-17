using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Logar : MonoBehaviour
{
    public InputField infLogin;
    public InputField infSenha;

    public void FazerLogin()
    {
        StartCoroutine(verificarLogin(infLogin.text, infSenha.text));
    }

    private void LoginDeuCerto()
    {
        Debug.Log("aaeeee");
    }
    private void LoginDeuRuim()
    {
        Debug.Log("NÃO!");
    }
    IEnumerator verificarLogin(string Login, string Senha)
    {
        WWWForm wwwf = new WWWForm();
        wwwf.AddField("login", Login);
        wwwf.AddField("senha", Senha);

        UnityWebRequest w = UnityWebRequest.Post("http://localhost/mortal_clicker/verficaLogin.php", wwwf);

        yield return w.SendWebRequest();

        if (w.isHttpError || w.isNetworkError)
        {
            Debug.Log("Erro: " + w.error);
        }
        else
        {
            if (w.downloadHandler.text.Equals("Ok"))
            {
                LoginDeuCerto();
            }
            else
            {
                LoginDeuRuim();
            }
        }
    }
}
