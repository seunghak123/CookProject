using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Seunghak.Common
{
    public class SoundManager : UnitySingleton<SoundManager>
    {
        [SerializeField] private StudioEventEmitter bgmAudioSource;
        [SerializeField] private StudioEventEmitter voiceAudioSource;
        [SerializeField] private List<StudioEventEmitter> fxAudioSources;
        private string currentPlayBGM;
        public void PlayBGM(string soundName)
        {
            AudioClip resourceAudio = GameResourceManager.Instance.LoadObject(soundName,true) as AudioClip;
            //JStageData soundData = JsonDataManager.Instance.GetSingleData<JStageData>(soundName, E_JSON_TYPE.JStageData);
            UserOptionData optionData = UserDataManager.Instance.GetUserOptionData();
            if (optionData.IsMute)
            {
                return;
            }
            int volume = optionData.MasterVolume < optionData.SoundVolume ? optionData.MasterVolume : optionData.SoundVolume;
            float percentVolume = volume / 100.0f;
            if (resourceAudio == null)
            {
                return;
            }

            if (bgmAudioSource.name.Equals(resourceAudio.name))
            {
                return;
            }
            StartCoroutine(PlayBGM(resourceAudio, percentVolume));

        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.V))
            {
                //RuntimeManager.PlayOneShot(voiceAudioSource.EventReference);
                var instance = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps_2D");
                instance.start(); 
                //voiceAudioSource.Play();
                //RuntimeManager.IsMuted = false;
                //RuntimeManager.GetBus()
                //RuntimeManager.
                //if (voiceAudioSource != null)
                //{
                //    voiceAudioSource.Play();
                //}
            }
        }
        public void PlaySFX(string soundName)
        {
            var instance = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps_2D");
        }
      
        private IEnumerator PlayBGM(AudioClip playingClip, float soundVolume)
        {
            //while (bgmAudioSource.volume > 0)
            //{
            //    bgmAudioSource.volume -= Time.deltaTime;
            //    yield return WaitTimeManager.WaitForTimeSeconds(Time.deltaTime);
            //}
            //bgmAudioSource.volume = soundVolume;
            //bgmAudioSource.clip = playingClip;
            //bgmAudioSource.loop = true;
            //bgmAudioSource.Play();
            yield break;
        }
        public void PlaySound(string soundName, float soundVolume, bool isLoop/*,type도 넣어줄것*/)
        {
            UserOptionData optionData = UserDataManager.Instance.GetUserOptionData();
            if (optionData.IsMute)
            {
                return;
            }
            AudioClip resourceAudio = GameResourceManager.Instance.LoadObject(soundName,true) as AudioClip;

            int volume = optionData.MasterVolume < optionData.FBXVolume ? optionData.MasterVolume : optionData.FBXVolume;
            float percentVolume = volume / 100.0f;

            if (resourceAudio == null)
            {
                return;
            }

            StartCoroutine(PlayVoice(resourceAudio, percentVolume, isLoop));

        }
        public void PlayFxSound(string soundName, float soundVolume)
        {
            UserOptionData optionData = UserDataManager.Instance.GetUserOptionData();
            if (optionData.IsMute)
            {
                return;
            }
            AudioClip resourceAudio = GameResourceManager.Instance.LoadObject(soundName,true) as AudioClip;

            int volume = optionData.MasterVolume < optionData.FBXVolume ? optionData.MasterVolume : optionData.FBXVolume;
            float percentVolume = volume / 100.0f;

            if (resourceAudio == null)
            {
                return;
            }

            StartCoroutine(PlayFx(resourceAudio, percentVolume));
        }
        private IEnumerator PlayVoice(AudioClip playingClip, float soundVolume, bool isLoop)
        {
            //voiceAudioSource.volume = soundVolume;
            //voiceAudioSource.clip = playingClip;
            //voiceAudioSource.loop = isLoop;
            voiceAudioSource.Play();
            yield break;

        }
        private IEnumerator PlayFx(AudioClip playingClip, float soundVolume)
        {
            bool isPlaying = false;
            //여기서 현재 같은 Fx가 플레이중인가?

            //for(int i=0;i< fxAudioSources.Count;i++)
            //{
            //    if(fxAudioSources[i].clip.name.Equals(playingClip))
            //    {
            //        fxAudioSources[i].volume = soundVolume;
            //        fxAudioSources[i].clip = playingClip;
            //        fxAudioSources[i].loop = false;
            //        fxAudioSources[i].Play();
            //        yield break;
            //    }
            //}
            //for(int i=0;i< fxAudioSources.Count; i++)
            //{
            //    if (!fxAudioSources[i].isPlaying)
            //    {
            //        fxAudioSources[i].volume = soundVolume;
            //        fxAudioSources[i].clip = playingClip;
            //        fxAudioSources[i].loop = false;
            //        fxAudioSources[i].Play();
            //        isPlaying = true;
            //    }
            //}
            //if (!isPlaying)
            //{
            //    GameObject newObject = new GameObject();
            //    AudioSource newaudio = newObject.AddComponent<AudioSource>();
            //    fxAudioSources.Add(newaudio);
            //    newaudio.volume = soundVolume;
            //    newaudio.clip = playingClip;
            //    newaudio.loop = false;
            //    newaudio.Play();
            //}
            yield break;
        }
    }
}