using Asteroids.Services;
using UnityEngine;
using TMPro;
using Zenject;
namespace Asteroids.Addressable
{
    public class AddressablesManager : MonoBehaviour
    {
        [Inject] 
        private  AddressableService _addressableService;
         private void Start()
        {
            _addressableService.Initialize();
        }

      
    }
}