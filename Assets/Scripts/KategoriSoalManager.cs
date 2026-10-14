using UnityEngine;
using UnityEngine.SceneManagement;

public class KategoriSoalManager : MonoBehaviour
{
    public void PilihKategori(string namaFileSoal)
    {
        PlayerPrefs.SetString("KategoriSoal", namaFileSoal);
        PlayerPrefs.Save();

        // Pindah ke Scene Game/Quiz
        SceneManager.LoadScene("QuizScene");
    }

    public void KembaliKeForm()
    {
        SceneManager.LoadScene("FormInput");
    }
}