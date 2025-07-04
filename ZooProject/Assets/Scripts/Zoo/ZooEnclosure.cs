using System;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using System.Linq;

namespace Zoo
{
    public class ZooEnclosure : MonoBehaviourEventSender
    {
        [SerializeField]
        private float timeToSpawn;

        [SerializeField, Range(1, 10)]
        private float secondsBetweenSpawns = 1;

        [SerializeField]
        private float slowFramerateFps = 10;

        [SerializeField]
        private bool slowMode = false;

        [SerializeField]
        List<IAnimalSpawn> spawnPoints = null;

        protected virtual void Awake()
        {
            GatherSpawnPoints();
        }

        void Update()
        {
            // если начало тормозить то прекращаем спавнить на всякий случай, вдруг 0 поставили в интервал :)
            slowMode = Time.deltaTime > MaxAdequateDeltaTime();
            if (slowMode)
                return;

            if (timeToSpawn <= Time.time)
            {
                timeToSpawn += secondsBetweenSpawns;
                SpawnRandomly();
            }
        }

        private float MaxAdequateDeltaTime()
        {
            return (1 / Math.Max(slowFramerateFps, 1));
        }

        private void SpawnRandomly()
        {
            // решил что хочу учитывать динамическое включение и выключение спавн пунктов
            var chosen = spawnPoints.Where(p => p.IsValid()).ChooseRandomly();
            if (chosen != null)
            {
                LogDebug("Spawned at ", chosen);
                chosen.Spawn();
            }
            else
            {
                LogError("Spawn not found:", chosen);
            }
        }

        /// <summary>
        /// я предпочитаю когда в редакторе можно посмотреть что происходит, но перетаскивать в массивы не люблю
        /// </summary>
        private void GatherSpawnPoints()
        {
            if (spawnPoints == null || spawnPoints.Count == 0)
            {
                var found = GetComponentsInChildren<IAnimalSpawn>(false);
                // Linq хоть я его и не люблю
                spawnPoints = found.ToList();
            }
            if (spawnPoints.Count == 0)
                LogError("No valid", nameof(IAnimalSpawn), "found");
            else
                LogInfo("Found ", spawnPoints.Count, nameof(IAnimalSpawn));
        }
    }
}