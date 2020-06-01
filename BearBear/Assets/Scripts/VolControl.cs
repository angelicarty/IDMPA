using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolControl : MonoBehaviour
{
    public AudioMixer masterVol;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public Slider MasterVolSlider;
    public Toggle bgm, sfx, master;
    bool SFXisMute, BGMisMute, masterIsMute;


    public AudioSource wildAreaBGM, townBGM;

    public void inWildArea()
    {
        wildAreaBGM.Play();
        townBGM.Pause();
        //battleBGM.Pause();
    }

    public void inTown()
    {
        townBGM.Play();
        wildAreaBGM.Pause();
        //battleBGM.Pause();
    }


    private void Start()
    {
        inTown();
        if (!bgm.isOn)
        {
            BGMisMute = true;
            muteBGM();
        }
        if (!sfx.isOn)
        {
            SFXisMute = true;
            muteSFX();
        }
        if (!master.isOn)
        {
            masterIsMute = true;
            muteMaster();
        }
        float sliderValue = BGMSlider.value;
        setLevelBGM(sliderValue);
        sliderValue = SFXSlider.value;
        setLevelSFX(sliderValue);
        sliderValue = MasterVolSlider.value;
        SetLevelMaster(sliderValue);
    }

    public void SetLevelMaster(float sliderValue)
    {
        if(masterIsMute)
        {
            return;
        }
        masterVol.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    void muteMaster()
    {
        masterVol.SetFloat("MasterVol", -80);
    }

    public void masterSound(bool sound)
    {
        if(sound)
        {
            masterIsMute = false;
            unmuteBGM();
            unmuteSFX();
        }
        else //if master is muted, all is muted.
        {
            masterIsMute = true;
            muteBGM();
            muteSFX();
        }
    }

    public void setLevelBGM(float sliderValue)
    {
        if(BGMisMute || masterIsMute)
        {
            return;
        }
        masterVol.SetFloat("BGMVol", Mathf.Log10(sliderValue) * 20);
    }

    public void BGMSound(bool sound)
    {
        if(masterIsMute)
        {
            muteBGM();
        }
        if(sound)
        {
            BGMisMute = false;
            unmuteBGM();
        }
        else
        {
            BGMisMute = true;
            muteBGM();
        }
    }
    void muteBGM()
    {
        if (BGMisMute || masterIsMute)
        {
            masterVol.SetFloat("BGMVol", -80);
        }
    }

    void unmuteBGM()
    {
        if (!BGMisMute && !masterIsMute)
        {
            setLevelBGM(BGMSlider.value);
        }
    }

    public void setLevelSFX(float sliderValue)
    {
        if(SFXisMute || masterIsMute)
        {
            return;
        }
        masterVol.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SFXSound(bool sound)
    {
        if (masterIsMute)
        {
            muteSFX();
        }
        if (sound)
        {
            SFXisMute = false;
            unmuteSFX();
        }
        else
        {
            SFXisMute = true;
            muteSFX();
        }
    }

    void muteSFX()
    {
        if (SFXisMute || masterIsMute)
        {
            masterVol.SetFloat("SFXVol", -80);
        }
    }

    void unmuteSFX()
    {
        if (!SFXisMute && !masterIsMute)
        {
            setLevelSFX(SFXSlider.value);
        }
    }
}
