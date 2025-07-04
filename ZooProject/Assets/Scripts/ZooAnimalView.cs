using UnityEngine;
using GameLogging;
using System.Collections.Generic;

namespace Zoo
{
    // примитивный класс но для расширения сгодится
    [DisallowMultipleComponent]
    public class ZooAnimalView : MonoBehaviourLogger
    {
        [SerializeField]
        private List<Material> deadMaterials;

        private new MeshRenderer renderer;

        void Awake()
        {
            renderer = GetComponent<MeshRenderer>();
        }

        public void Die()
        {
            if (deadMaterials != null && deadMaterials.Count > 0)
                renderer.SetSharedMaterials(deadMaterials);
            else
                LogError("materials are null or empty");
        }
    }
}