using System.Collections.Generic;
using System.Linq;
using Commons.Extensions;
using Commons.Utility;
using UnityEngine;


    public class AudioManagerComponent : MonoBehaviour
    {
        /// <summary>
        /// BGMのAudioSource
        /// </summary>
        [SerializeField] private AudioSource _bgmSource;

        /// <summary>
        /// SEのAudioSource
        /// </summary>
        [SerializeField] private AudioSource _seSource;

        /// <summary>
        /// BGMのAudioClip
        /// </summary>
        [SerializeField] private List<AudioClip> _bgmAudioClip;

        /// <summary>
        /// SEのAudioClip群
        /// </summary>
        [SerializeField] private List<AudioClip> _seAudioClipList = new List<AudioClip>();

        public void Start()
        {
            _bgmSource = InitializeAudioSource(_bgmSource, true);
            _seSource = InitializeAudioSource(_seSource, false);
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private AudioSource InitializeAudioSource(AudioSource audioSource, bool isLoop = false)
        {
            audioSource.loop = isLoop;
            audioSource.playOnAwake = false;

            return audioSource;
        }

        /// <summary>
        /// BGMを流す
        /// </summary>
        public void PlayBGM(BGM bgm)
        {
            var audioClip = _bgmAudioClip.FirstOrDefault(clip => clip.name == bgm.ToString());
            
            if (audioClip == null)
            {
                DebugUtility.Log(bgm + "は見つかりません");
                return;
            }
            
            _bgmSource.Play(audioClip);
        }

        /// <summary>
        /// SEを流す
        /// </summary>
        /// <param name="soundEffect">流したいSE</param>
        public void PlaySoundEffect(SoundEffect soundEffect)
        {
            var audioClip = _seAudioClipList.FirstOrDefault(clip => clip.name == soundEffect.ToString());

            if (audioClip == null)
            {
                DebugUtility.Log(soundEffect + "は見つかりません");
                return;
            }

            _seSource.PlayOneShot(audioClip);
        }
    }
