﻿using CodeBase.Infrastructure.Service.SaveLoad;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        public BoxCollider2D Collider;
        
        [Inject]
        public void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _saveLoadService.SaveProgress();
            
            Debug.Log("Save Progress");
            
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if(!Collider)
                return;
            
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + new Vector3(Collider.offset.x, Collider.offset.y), Collider.size);
        }
    }
}