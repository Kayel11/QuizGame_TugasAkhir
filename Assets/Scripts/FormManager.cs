using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FormManager : MonoBehaviour
{
    public TMP_InputField inputNama;
    public TMP_InputField inputNIS;
    public AudioSource sfxButton;
    public float delay = 0.3f;

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

        StartCoroutine(PindahDenganSuara("KategoriSoal"));
    }

    public void KembaliToMainMenu()
    {
        StartCoroutine(PindahDenganSuara("MainMenu"));
    }

    IEnumerator PindahDenganSuara(string namaScene)
    {
        if (sfxButton != null) sfxButton.Play();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(namaScene);
    }
}