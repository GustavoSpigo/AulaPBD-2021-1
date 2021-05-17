using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChamandoPHP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( pegarInforamacaoPHP());
    }

    IEnumerator pegarInforamacaoPHP()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("pesquisa", "");

        UnityWebRequest uw = UnityWebRequest.Post("http://localhost/lala/index.php", webForm);
        yield return uw.SendWebRequest();

        if(uw.isHttpError || uw.isNetworkError)
        {
            Debug.Log("Deu ruim: " + uw.error);
        }
        else // Funfou
        {
            Debug.Log("Deu bom: " + uw.downloadHandler.text);
        }
    }
}

