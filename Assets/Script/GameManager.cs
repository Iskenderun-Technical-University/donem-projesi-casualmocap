using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("----CAR")]
    public GameObject[] Arabalar;    
    public int KacArabaOlsun;
    int KalanAracSayisiDegeri;
    int AktifAracIndex=0;
    

    [Header("CANVAS")]
    public Sprite AracGeldiGorseli;
    public TextMeshProUGUI KalanAracSayisi;
    public GameObject[] ArabaCanvasGorselleri;
    public TextMeshProUGUI[] Textler;
    public GameObject[] Panellerim;
    public GameObject[] TapToButonlar;

    [Header("----PLATFORM ")]
    public GameObject Platform_1;
    public GameObject Platform_2;
    public float[] DonusHizlari;
    bool DonusVarmi;


    [Header("----LEVEL")]
    public int ElmasSayisi;
    public ParticleSystem CarpmaEfekti;
    public AudioSource[] Sesler;
    public bool YukselecekPlatformVarmi;
    
    void Start()
    {
        
        DonusVarmi = true;
        VarsayilanDegerleriKontrolEt();

        KalanAracSayisiDegeri = KacArabaOlsun;        
       
         for (int i = 0; i < KacArabaOlsun; i++)
         {
            ArabaCanvasGorselleri[i].SetActive(true);
         }       
    }
    public void YeniArabaGetir()
    {
        
         KalanAracSayisiDegeri--;
        if (AktifAracIndex<KacArabaOlsun)
        {
            Arabalar[AktifAracIndex].SetActive(true);
        }else
        {
            Kazandin();
        }
       

        ArabaCanvasGorselleri[AktifAracIndex-1].GetComponent<Image>().sprite = AracGeldiGorseli;
         
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            Arabalar[AktifAracIndex].GetComponent<Car>().ilerle = true;
            AktifAracIndex++;
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            Panellerim[0].SetActive(false);
            Panellerim[3].SetActive(true);
        }
       

        if (DonusVarmi)
        {
            Platform_1.transform.Rotate(new Vector3(0, 0, -DonusHizlari[0]), Space.Self);
            if (Platform_2!=null)
            Platform_2.transform.Rotate(new Vector3(0, 0, DonusHizlari[1]), Space.Self);
        }
       
    }

    public void Kaybettin()
    {
        DonusVarmi = false;
        Textler[6].text = PlayerPrefs.GetInt("Elmas").ToString();
        Textler[7].text = SceneManager.GetActiveScene().name;
        Textler[8].text = (KacArabaOlsun - KalanAracSayisiDegeri).ToString();
        Textler[9].text = ElmasSayisi.ToString();
        Sesler[1].Play();
        Sesler[3].Play();
        Panellerim[1].SetActive(true);
        Panellerim[3].SetActive(false);
        Invoke("KaybettinButonuOrtayaCikart", 2f);
    }   
    void Kazandin()
    {
        PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") + ElmasSayisi);

        Textler[2].text = PlayerPrefs.GetInt("Elmas").ToString();
        Textler[3].text = SceneManager.GetActiveScene().name;
        Textler[4].text = (KacArabaOlsun - KalanAracSayisiDegeri).ToString();
        Textler[5].text = ElmasSayisi.ToString();
        Sesler[2].Play();
        Panellerim[2].SetActive(true);
        Panellerim[3].SetActive(false);
        Invoke("KazandinButonuOrtayaCikart", 2f);
    }
    void KaybettinButonuOrtayaCikart()
    {
        TapToButonlar[0].SetActive(true);
    }
    void KazandinButonuOrtayaCikart()
    {
        TapToButonlar[1].SetActive(true);
    }
    public void izleveDevamEt()
    {
        

    }
    public void izlevedahafazlakazan()
    {
        

    }
    public void Replay()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }
    public void SonrakiLevel()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    

    void VarsayilanDegerleriKontrolEt()
    {
       
        

        Textler[0].text = PlayerPrefs.GetInt("Elmas").ToString();
        Textler[1].text = SceneManager.GetActiveScene().name;
    }      
}
