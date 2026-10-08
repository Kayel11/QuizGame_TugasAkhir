using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FormManager : MonoBehaviour
{
    public TMP_InputField inputNama;
    public TMP_InputField inputNIS;

    public void SimpanDataDanLanjut()
    {
        // Validasi jika Nama atau NIS masih kosong
        if (string.IsNullOrEmpty(inputNama.text) || string.IsNullOrEmpty(inputNIS.text))
        {
            Debug.Log("Nama dan NIS wajib diisi");
            return;
        }

        
        PlayerPrefs.SetString("NamaSiswa", inputNama.text);
        PlayerPrefs.SetString("NISSiswa", inputNIS.text);
        PlayerPrefs.Save();

        
        SceneManager.LoadScene("QuizScene"); 
    }

    public void KembaliToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}