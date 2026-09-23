using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class Soal : MonoBehaviour
{   
    public TextAsset assetSoal;

    private string[] soal;

    private string[,] soalBag;

    int indexSoal;
    int maxSoal;
    char kunciJ;

    // Komponen UI
    public TextMeshProUGUI txtSoal, txtOpsiA, txtOpsiB, txtOpsiC, txtOpsiD;

    bool isHasil;
    private float durasi;
    public float durasiPenilaian;

    int jwbBenar, jwbSalah;
    float nilai;

    public GameObject panel;
    public GameObject imgPenilaian, imgHasil;
    public TextMeshProUGUI txtHasil;

    // Audio
    public AudioSource audioSource; 
    public AudioClip suaraBenar;    
    public AudioClip suaraSalah;

    void Start()
    {
        durasi = durasiPenilaian;

        soal = assetSoal.ToString().Split('#');
        maxSoal = soal.Length;

        // 
        FisherYatesShuffle();

        soalBag = new string[soal.Length, 10];
        OlahSoal();

        // 
        indexSoal = 0;
        TampilkanSoal();
    }

    // FISHER-YATES SHUFFLE
    private void FisherYatesShuffle()
    {
        for (int i = soal.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            //
            string temp = soal[i];
            soal[i] = soal[randomIndex];
            soal[randomIndex] = temp;
        }
    }

    private void OlahSoal()
    {
        for(int i = 0; i < soal.Length; i++)
        {
            string[] tempSoal = soal[i].Split('+');
            for(int j = 0; j < tempSoal.Length; j++)
            {
                soalBag[i, j] = tempSoal[j];
                continue;
            }
            continue;
        }
    }

    private void TampilkanSoal()
    {
        // Panggil berurutan dari indexSoal karena array soal sudah acak di awal
        if (indexSoal < maxSoal)
        {
            txtSoal.text = soalBag[indexSoal, 0];
            txtOpsiA.text = soalBag[indexSoal, 1];
            txtOpsiB.text = soalBag[indexSoal, 2];
            txtOpsiC.text = soalBag[indexSoal, 3];
            txtOpsiD.text = soalBag[indexSoal, 4];
            kunciJ = soalBag[indexSoal, 5][0];
        }
    }

    public void Opsi(string opsiHuruf)
    {
        CheckJawaban(opsiHuruf[0]);

        if(indexSoal == maxSoal - 1)
        {
            isHasil = true;
        }
        else
        {
            indexSoal++;
        }

        panel.SetActive(true);
    }

    private float HitungNilai()
    {
        return nilai = (float)jwbBenar / maxSoal * 100;
    }

    public TextMeshProUGUI txtPenilaian;
    private void CheckJawaban(char huruf)
    {
        string penilaian;

        char jawabanBersih = huruf.ToString().Trim().ToUpper()[0];
        char kunciBersih = kunciJ.ToString().Trim().ToUpper()[0];

        if (jawabanBersih.Equals(kunciBersih))
        {
            penilaian = "Benar!";
            jwbBenar++;

            if (audioSource != null && suaraBenar != null)
            {
                audioSource.clip = suaraBenar;
                audioSource.Play();
            }
        }
        else
        {
            penilaian = "Salah!";
            jwbSalah++;

            if (audioSource != null && suaraSalah != null)
            {
                audioSource.clip = suaraSalah;
                audioSource.Play();
            }
        }

        txtPenilaian.text = penilaian;
    }

    void Update()
    {
        if (panel.activeSelf)
        {
            durasiPenilaian -= Time.deltaTime;

            if (isHasil)
            {
                imgPenilaian.SetActive(true);
                imgHasil.SetActive(false);

                if (durasiPenilaian <= 0)
                {
                    txtHasil.text = "Jumlah Benar: " + jwbBenar + "\nJumlah Salah: " + jwbSalah + "\n\nScore: " + HitungNilai();

                    imgPenilaian.SetActive(false);
                    imgHasil.SetActive(true);

                    durasiPenilaian = 0;
                }
            }
            else
            {
                imgPenilaian.SetActive(true);
                imgHasil.SetActive(false);

                if(durasiPenilaian <= 0)
                {
                    panel.SetActive(false);
                    durasiPenilaian = durasi;

                    TampilkanSoal();
                }
            }
        }
    }
}