using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Extensions;
using GameLogging;

namespace Zoo
{
    public class ZooEnclosureAnimalSpawn : MonoBehaviourLogger, IAnimalSpawn
    {
        [SerializeField]
        Vector3 randomOffset;

        [SerializeField]
        Vector3 randomRotation;

        [SerializeField]
        private List<ZooAnimal> animalPrefabs = null;

        public ZooAnimal Spawn()
        {
            if (!IsValid())
            {
                LogWarning("Empty", nameof(animalPrefabs));
                return null;
            }

            int idx = Random.Range(0, animalPrefabs.Count);
            var chosen = animalPrefabs[idx];

            return Instantiate(chosen, GetPosition(), GetRotation(), transform);
        }

        public bool IsValid()
        {
            // покажу знание Linq но если честно я его не очень люблю
            return gameObject.activeSelf && animalPrefabs != null && animalPrefabs.Count > 0 && animalPrefabs.All(p => p != null);
        }

        protected Vector3 GetPosition()
        {
            return transform.position + GetRandomOffset();
        }

        private Vector3 GetRandomOffset()
        {
            return randomOffset.Randomize();
        }

        protected Quaternion GetRotation()
        {
            return Quaternion.Euler(randomRotation.Randomize());
        }
    }
}