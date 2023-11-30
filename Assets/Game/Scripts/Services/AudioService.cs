using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace Asteroids.Services
{
    public class AudioService : IAudioService
    {
        readonly Camera _camera;

        public AudioService(Camera camera)
        {
            _camera = camera;
        }
        public void Play(AudioClip clip)
        {
            _camera.GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }
}
