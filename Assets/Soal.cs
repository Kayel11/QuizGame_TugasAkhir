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
    bool ambilSoal;
    char kunciJ;

    bool[] soalSelesai;


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

    // ========================================================
    // 
    // ========================================================
    public AudioSource audioSource; 
    public AudioClip suaraBenar;    
    public AudioClip suaraSalah;
    // ========================================================

    // Start is called before the first frame update
    void Start()
    {
        durasi = durasiPenilaian;

        soal = assetSoal.ToString().Split('#');

        soalSelesai = new bool[soal.Length];

        soalBag = new string[soal.Length, 10];
        maxSoal = soal.Length;
        OlahSoal();

        ambilSoal = true;
        TampilkanSoal();

        print(soalBag[1,2]); 

    }

    private void OlahSoal()
    {
        for(int i=0; i < soal.Length; i++)
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
        if(indexSoal < maxSoal)
        {
            if (ambilSoal)
            {
                for(int i=0; i < soal.Length; i++)
                {
                    int randomIndexSoal = Random.Range(0, soal.Length);
                    print("random: " + randomIndexSoal);
                    if (!soalSelesai[randomIndexSoal])
                    {
                        txtSoal.text = soalBag[randomIndexSoal, 0];
                        txtOpsiA.text = soalBag[randomIndexSoal, 1];
                        txtOpsiB.text = soalBag[randomIndexSoal, 2];
                        txtOpsiC.text = soalBag[randomIndexSoal, 3];
                        txtOpsiD.text = soalBag[randomIndexSoal, 4];
                        kunciJ = soalBag[randomIndexSoal, 5][0];

                        soalSelesai[randomIndexSoal] = true;

                        ambilSoal = false;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                
            }
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
            ambilSoal = true;
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


    // Update is called once per frame
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
                    txtHasil.text = "Jumlah Benar: " + jwbBenar + "\nJumlah Salah: " + jwbSalah + "\n\nScore: " +HitungNilai();

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