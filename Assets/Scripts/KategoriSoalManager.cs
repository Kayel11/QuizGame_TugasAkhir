using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class KategoriSoalManager : MonoBehaviour
{
    public AudioSource sfxButton;
    public float delay = 0.3f;

    public void PilihKategori(string namaFileSoal)
    {
        PlayerPrefs.SetString("KategoriSoal", namaFileSoal);
        PlayerPrefs.Save();

        StartCoroutine(PindahDenganSuara("QuizScene"));
    }

    public void KembaliKeForm()
    {
        StartCoroutine(PindahDenganSuara("FormInput"));
    }

    IEnumerator PindahDenganSuara(string namaScene)
    {
        if (sfxButton != null)
            sfxButton.Play();

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(namaScene);
    }
}